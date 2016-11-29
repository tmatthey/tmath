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
using Math.Gfx;
using Math.Gps;
using Math.Tools.TrackReaders;
using Tools.Base;

namespace App.Cluster
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var path = ".\\";
            var name = "cluster";
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
                .WithDescription("Ouput name of cluster maps.");

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

            Console.WriteLine("Cluster");

            var activities = Reader.ParseDirectory(path);
            var list = activities.Select(a => a.GpsPoints().ToList()).ToList();

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
                    var flatTrack = track.CreateFlatTrack(center);
                    size.Expand(flatTrack.Size);
                    tracks.Add(flatTrack.Track);
                }
                Console.WriteLine("Cluster {0}: {1}", k, cluster.Count);
                Timer.Start();
                var db = TraClus.Cluster(tracks, n, eps, true, minL, cost);
                Timer.Stop();
                if (db.Any())
                {
                    Console.WriteLine("Segments : {0}", db.Count);
                    var bitmap = new Bitmap(size.Min, size.Max, 5, 8000);
                    foreach (var s in tracks)
                    {
                        for (var i = 0; i + 1 < s.Count; i++)
                            Draw.Bresenham(s[i], s[i + 1], bitmap.SetMagnitude, 0.025);
                    }
                    foreach (var s in db)
                    {
                        for (var i = 0; i + 1 < s.Segment.Count; i++)
                            Draw.XiaolinWu(s.Segment[i], s.Segment[i + 1], bitmap.Set);
                    }
                    BitmapFileWriter.PNG(string.Format("{0}.{1}.png", name, k), bitmap.Pixels);
                }
                else
                {
                    Console.WriteLine("Empty");
                }
                k++;
            }
        }
    }
}