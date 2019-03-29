/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2019 Thierry Matthey
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
using Math;
using Math.Clustering;
using Math.Gfx;
using Math.Gps;
using Math.Tools.Base;
using Math.Tools.TrackReaders;
using BitmapFileWriter = Math.Gfx.BitmapFileWriter;

namespace Cmd.Cluster
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var p = new CommandLineParser("cluster", args);

            p.SetupHelp(helpText =>
                {
                    Console.WriteLine(helpText);
                    Environment.Exit(0);
                }).SetupError((helpText, errorText) =>
                {
                    Console.WriteLine(errorText);
                    Console.WriteLine(helpText);
                    Environment.Exit(0);
                }).Setup("d", "directory with *.tcx and *.gpx files.", out var path, "./")
                .Setup("n", "Output name of cluster maps.", out var name, "cluster")
                .Setup("a", "Minimum number of activities.", out var n, 5)
                .Setup("l", "Minimum segment length.", out var minL, 20.0)
                .Setup("p", "MDL cost advantage.", out var cost, 5)
                .Setup("e", "Epsilon neighborhood", out var eps, 20.0);

            p.Parse();

            Console.WriteLine("Cluster");

            var activities = Deserializer.Directory(path);
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

                    BitmapFileWriter.PNG($"{name}.{k}.png", bitmap.Pixels);
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
