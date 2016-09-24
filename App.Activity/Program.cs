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
using Math.Gps;
using Tools.TrackReaders;

namespace App.Activity
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var path = ".\\";
            var p = new FluentCommandLineParser();

            p.Setup<string>('d')
                .Callback(v => path = v)
                .SetDefault(path)
                .WithDescription("Directory with *.tcx and *.gpx files.");


            p.SetupHelp("?", "help")
                .Callback(text =>
                {
                    Console.WriteLine(text);
                    Environment.Exit(0);
                });
            p.Parse(args);

            Console.WriteLine("Activity");

            var activities = (from activity in Reader.ParseDirectory(path)
                where
                activity.GpsPoints().Count() == activity.Times().Count()
                select activity).OrderBy(a => a.Date.Ticks).ToList();
            var list = (from activity in activities select activity.GpsPoints().ToList()).ToList();
            Console.WriteLine("Tracks: {0}", list.Count);
            Console.WriteLine("Points: {0}", list.Sum(t => t.Count));
            for (var i = 0; i < list.Count; i++)
            {
                Console.WriteLine("Track\t{0}\t{1}", i, list[i].Count);
                var t0 = activities[i].Seconds().First();
                var seconds = activities[i].Seconds().Select(t1 => t1 - t0).ToList();
                var track = new GpsTrack(list[i]).CreateFlatTrack();
                var vel = new List<double>();
                var dist = new List<double>();
                var time = new List<double>();
                for (var j = 0; j < track.Displacement.Count; j++)
                {
                    var d = track.Displacement[j];
                    var t = seconds[j] - seconds[System.Math.Max(j - 1, 0)];
                    if (j + 2 < track.Displacement.Count &&
                        !Comparison.IsZero(track.Displacement[j]) &&
                        Comparison.IsZero(track.Displacement[j + 1]) &&
                        !Comparison.IsZero(track.Displacement[j + 2]))
                    {
                        d = track.Displacement[j]/2.0;
                    }
                    else if (0 < j && j + 1 < track.Displacement.Count &&
                             !Comparison.IsZero(track.Displacement[j - 1]) &&
                             Comparison.IsZero(track.Displacement[j]) &&
                             !Comparison.IsZero(track.Displacement[j + 1]))
                    {
                        d = track.Displacement[j - 1]/2.0;
                    }
                    var v = t > 0.0 ? d/t : 0;
                    time.Add(t);
                    vel.Add(v);
                    dist.Add(d);
                }

                var m = 10;
                for (var j = 0; j < track.Displacement.Count; j++)
                {
                    var v = 0.0;
                    var t = 0.0;
                    var n = Comparison.IsLessEqual(vel[j], 1.0/3.6) || Comparison.IsLess(2*m, time[j]) ? 0 : m;
                    var j0 = System.Math.Max(j - n, 0);
                    while (j0 < j && seconds[j] - seconds[j0] - time[j] > n)
                        j0++;


                    var j1 = System.Math.Min(j + n, track.Displacement.Count - 1);
                    while (j < j1 && seconds[j1] - seconds[j] > n)
                        j1--;

                    for (var k = j0; k <= j1; k++)

                    {
                        v += vel[k]*time[k];
                        t += time[k];
                    }
                    v = t > 0.0 ? v/t : 0;
                    for (var k = 0; k < System.Math.Max(time[j], 1); k++)
                        Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", track.Displacement[j], dist[j], time[j], vel[j], v);
                }
            }
        }
    }
}