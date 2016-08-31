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
            var epsTrack = 25.0;
            var minL = 20.0;
            var cost = 5;
            var p = new FluentCommandLineParser();
            var hrStanding = 40+26;

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
                .WithDescription("Epsilon cluster neighborhood");

            p.Setup<double>('f')
                .Callback(v => epsTrack = v)
                .SetDefault(epsTrack)
                .WithDescription("Epsilon track");

            p.Setup<double>('l')
                .Callback(v => minL = v)
                .SetDefault(minL)
                .WithDescription("Minimum segment length");

            p.Setup<int>('p')
                .Callback(v => cost = v)
                .SetDefault(cost)
                .WithDescription("MDL cost advantage");

            p.Setup<int>('r')
                .Callback(v => hrStanding = v)
                .SetDefault(hrStanding)
                .WithDescription("Standing heart rate");

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
                    Console.WriteLine("Segment No\tTrack No\tSeg Distance [m]\tDate\tDirection\tCommon\tTrack Seg Distance [m]\tTime [s]\tHR\tSpeed [Km/h]\tPace [min/km]\tHR Index");
                    foreach (var segment in db)
                    {

                        foreach (var i in segment.SegmentIndices.Indices())
                        {
                            var flatTrack = flatTracks[i];
                            var analyzer = new NeighbourDistanceCalculator(flatTrack, eps);
                            var current = analyzer.Analyze(segment.Segment, epsTrack);
                            var neighbours = current.Neighbours;

                            if (neighbours.Count < 2) 
                                continue;

                            var d = 0.0;
                            for (var l = 0; l + 1 < segment.Segment.Count; l++)
                                d += segment.Segment[l].EuclideanNorm(segment.Segment[l + 1]);

                            double a, b;
                            Regression.Linear(Enumerable.Range(0, neighbours.Count).Select(dummy => (double)dummy).ToList(), 
                                neighbours.Select(neighbour => neighbour[0].Reference).Select(dummy => (double) dummy).ToList(), out a, out b);

                            var j = cluster[i];
                            var hr = activities[j].HeartRates().ToList();
                            var t0 = activities[j].Seconds().First();
                            var seconds = activities[j].Seconds().Select(t1 => t1 - t0).ToList();
                            var refIndex = (from neighbour in neighbours from pt in neighbour select pt.Reference).Distinct().OrderBy(num => num).ToList();
                            var t = 0.0;
                            var h = 0.0;
                            var len = 0.0;
                            var com = 0;
                            for (var l = 0; l + 1 < refIndex.Count; l++)
                            {
                                var i0 = refIndex[l];
                                var i1 = refIndex[l + 1];
                                if (i0 + 1 == i1)
                                {
                                    var dt = seconds[i1] - seconds[i0];
                                    t += dt;
                                    h += dt*hr[i1];
                                    len += flatTrack.Displacement[i1];
                                    com++;
                                }
                            }
                            Console.Write("{0}\t{1}\t{2}\t{3}\t", sn, i, d, activities[j].Times().First());
                            if (Comparison.IsLess(0, t) && Comparison.IsLess(0.5, System.Math.Abs(a)))
                            {
                                var dir = (a < 0 ? -1.0 : 1.0);
                                h /= t;
                                var v = len/t;
                                var index = (h - hrStanding)/v;
                                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}", dir, com / ((double)refIndex.Count-1.0), len, t, h, v * 3.6, 60.0 / (v * 3.6), index);    
                            }
                            else
                            {
                                Console.WriteLine("No direction");
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