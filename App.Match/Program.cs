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

            var activities = Reader.ParseDirectory(path);
            var list = (from activity in activities where 
                            activity.GpsPoints().Count() == activity.HeartRates().Count() && 
                            activity.GpsPoints().Count() == activity.Times().Count() && 
                            activity.HeartRates().Sum() > activity.HeartRates().Count() * 20 select activity.GpsPoints().ToList()).ToList();


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
                var size = new BoundingRect();
                foreach (var i in cluster)
                {
                    var track = new GpsTrack(list[i]);
                    var transformed = track.CreateTransformedTrack(center);
                    size.Expand(transformed.Size);
                    tracks.Add(transformed.Track);
                }
                Console.WriteLine("Cluster {0}: {1}", k, cluster.Count);
                Timer.Start();
                var db = TraClus.Cluster(tracks, n, eps, true, minL, cost);
                Timer.Stop();
                if (db.Any())
                {
                    Console.WriteLine("Segments : {0}", db.Count);
                }
                k++;
            }
        }
    }
}