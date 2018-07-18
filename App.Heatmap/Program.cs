/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2018 Thierry Matthey
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
using System.Linq;
using Math.Clustering;
using Math.Gfx;
using Math.Tools.Base;
using Math.Tools.TrackReaders;
using BitmapFileWriter = Math.Gfx.BitmapFileWriter;

namespace App.Heatmap
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var p = new CommandLineParser("heatmap", args);

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
                .Setup("n", "Output name of heatmap maps.", out var name, "heatmap").Setup("c",
                    "Coloring scheme: log (default), median and normalized.", out var coloring, ColorMap.log);

            p.Parse();


            Console.WriteLine("Heatmap");

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
                var heatMap = new HeatMap();
                foreach (var i in cluster)
                {
                    heatMap.Add(list[i]);
                }

                Console.WriteLine("Map {0}: {1}", k, cluster.Count);
                double[,] bitmap;
                Timer.Start();
                switch (coloring)
                {
                    case ColorMap.log:
                        bitmap = heatMap.Log(5.0, 8000);
                        break;
                    case ColorMap.median:
                        bitmap = heatMap.Median(5.0, 8000);
                        break;
                    case ColorMap.normalized:
                        bitmap = heatMap.Normalized(5.0, 8000);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                Timer.Stop();

                BitmapFileWriter.PNG(string.Format("{0}.{1}.png", name, k), bitmap, HeatColorMapping.Default);
                k++;
            }
        }

        private enum ColorMap
        {
            median = 1,
            log = 2,
            normalized = 3
        }
    }
}