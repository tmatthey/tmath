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
using System.Linq;
using Fclp;
using Math.Clustering;
using Math.Gfx;
using Tools.Base;
using Tools.TrackReaders;

namespace App.Heatmap
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var path = ".\\";
            var name = "heatmap";
            var coloring = ColorMap.log;
            var p = new FluentCommandLineParser();

            p.Setup<string>('d')
                .Callback(v => path = v)
                .SetDefault(path)
                .WithDescription("Directory with *.tcx and *.gpx files.");

            p.Setup<string>('n')
                .Callback(v => name = v)
                .SetDefault(name)
                .WithDescription("Ouput name of heatmaps.");

            p.Setup<ColorMap>('c')
                .Callback(v => coloring = v)
                .SetDefault(coloring)
                .WithDescription("Coloring scheme: log (default), median and normalized.");

            p.SetupHelp("?", "help")
                .Callback(text =>
                {
                    Console.WriteLine(text);
                    Environment.Exit(0);
                });

            p.Parse(args);

            Console.WriteLine("Heatmap");

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

        [Flags]
        private enum ColorMap
        {
            median = 1,
            log = 2,
            normalized = 3
        }
    }
}