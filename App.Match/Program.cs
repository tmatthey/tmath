/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016 Thierry Matthey
 *
 * Permission is hereby granted, free of charge, to any person
 * obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without
 * restriction, including without limitation the rights to use, copy,
 * modify, merge, publish, distribute, sublicense, and/or sell copies
 * of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS
 * BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN
 * ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
 * CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 * ***** END LICENSE BLOCK *****
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Fclp;
using Math;
using Math.Clustering;
using Math.Gps;
using Tools.Base;
using Tools.TrackReaders;

namespace App.Match
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var path = ".\\";
            var name = "match";
            var n = 5;
            var eps = 20.0;
            var minL = 20.0;
            var cost = 5;
            var p = new FluentCommandLineParser();

            p.Setup<string>('d')
                .Callback(v => path = v)
                .SetDefault(path)
                .WithDescription("Directory with *.tcx and *.gpx files.");

            p.Setup<string>('n')
                .Callback(v => name = v)
                .SetDefault(name)
                .WithDescription("Ouput name of matches.");

            p.Setup<int>('a')
                .Callback(v => n = v)
                .SetDefault(n)
                .WithDescription("Minimum number of activites.");

            p.Setup<double>('e')
                .Callback(v => eps = v)
                .SetDefault(eps)
                .WithDescription("Epsilon neighborhood");

            p.Setup<double>('l')
                .Callback(v => minL = v)
                .SetDefault(minL)
                .WithDescription("Minimum segment length");

            p.Setup<int>('p')
                .Callback(v => cost = v)
                .SetDefault(cost)
                .WithDescription("MDL cost advantage");

            p.SetupHelp("?", "help")
                .Callback(text =>
                {
                    Console.WriteLine(text);
                    Environment.Exit(0);
                });
            p.Parse(args);

            Console.WriteLine("Match");


            var activities = (from activity in Reader.ParseDirectory(path)
                where
                    activity.GpsPoints().Count() == activity.HeartRates().Count() &&
                    activity.GpsPoints().Count() == activity.Times().Count() &&
                    activity.HeartRates().Sum() > activity.HeartRates().Count()*20
                select activity).ToList();
            var list = (from activity in activities select activity.GpsPoints().ToList()).ToList();

            Console.WriteLine("Tracks: {0}", list.Count);
            Console.WriteLine("Points: {0}", list.Sum(t => t.Count));

            Timer.Start();
            var clusters = PolylineNeighbours.Cluster(list);
            Timer.Stop();

            var k = 0;
            Console.WriteLine("Maps: {0}", clusters.Count);


            foreach (var cluster in clusters)
            {
                var center = new Vector3D();
                var m = 0;
                foreach (var i in cluster)
                {
                    foreach (var pt in list[i])
                    {
                        center += pt;
                        m++;
                    }
                }
                center /= (double) m;
                var tracks = new List<List<Vector2D>>();
                var flatTracks = new List<FlatTrack>();
                var size = new BoundingRect();
                foreach (var i in cluster)
                {
                    var track = new GpsTrack(list[i]);
                    var flatTrack = track.CreateFlatTrack(center);
                    size.Expand(flatTrack.Size);
                    tracks.Add(flatTrack.Track);
                    flatTracks.Add(flatTrack);
                }
                Console.WriteLine("Cluster {0}: {1}", k, cluster.Count);
                Timer.Start();
                var db = TraClus.Cluster(tracks, n, eps, true, minL, cost);
                Timer.Stop();
                if (db.Any())
                {
                    Console.WriteLine("Segments : {0}", db.Count);
                    var sn = 0;
                    Console.WriteLine("Segment\tTrack\tDistance\tData\tLength\tTime\tHR\tSpeed\tPace\tIndex");
                    foreach (var segment in db)
                    {

                        foreach (var i in segment.SegmentIndices.Indices())
                        {
                            var d = 0.0;
                            for (var l=0;l+1<segment.Segment.Count;l++)
                            {
                                d += segment.Segment[l].EuclideanNorm(segment.Segment[l + 1]);
                            }
                            var j = cluster[i];
                            Console.Write("{0}\t{1}\t{2}\t{3}\t", sn,i,d,activities[j].Times().First());
                            var hr = activities[j].HeartRates().ToList();
                            var t0 = activities[j].Seconds().First();
                            var seconds = activities[j].Seconds().Select(t1 => t1 - t0).ToList();
                            var analyzer = new NeighbourDistanceCalculator(flatTracks[i], eps);
                            var current = analyzer.Analyze(segment.Segment, eps);
                            var neighbours = current.Neighbours;
                            if (neighbours.Count > 1)
                            {
                                var first = neighbours.First().First();
                                var last = neighbours.Last().First();
                                if (first.Reference < last.Reference)
                                {
                                    var f0 = first.Fraction;
                                    var i0 = first.Reference;
                                    var f1 = last.Fraction;
                                    var i1 = last.Reference;
                                    if (Comparison.IsZero(f1))
                                    {
                                        f1 = 1.0;
                                        i1--;
                                    }
                                    var t = ((1.0 - f1)*seconds[i1] + f1*seconds[i1 + 1]) -
                                            ((1.0 - f0)*seconds[i0] + f0*seconds[i0 + 1]);
                                    var h = (1.0 - f0)*hr[i0 + 1]*(seconds[i0 + 1] - seconds[i0]) +
                                                    f1*hr[i1+1]*(seconds[i1 + 1] - seconds[i1]);
                                    for (var l = i0 + 1; l < i1; l++)
                                    {
                                        h += hr[l + 1]*(seconds[l + 1] - seconds[l]);
                                    }
                                    h /= t;
                                    var len = last.RefDistance - first.RefDistance;
                                    var v = len/t;
                                    var index = (h - 70.0)/v;
                                    Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", len, t, h, v*3.6,60.0/(v*3.6), index);    
                                }
                                else
                                {
                                    Console.WriteLine("Wrong direction");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Few points");
                            }
                        }
                        sn++;
                    }
                }
                k++;
            }
        }
    }
}