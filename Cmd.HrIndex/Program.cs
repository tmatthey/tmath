/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2021 Thierry Matthey
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
using Math.Gps;
using Math.Tools.TrackReaders;
using Math.Tools.Base;
using Ganss.IO;

namespace Cmd.Activity
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Heart Read Index");
                Console.WriteLine("Usage: hrindex <double standing heart rate> [GPX/TCX [...]]");
                Environment.Exit(0);
            }



            var shr = double.Parse(args[0]);
            var filenames = new List<string>();
            for (var i = 1; i < args.Length; ++i)
                filenames.AddRange(Glob.Expand(args[i]).Select(f => f.FullName));
            Console.WriteLine("Activity\tDistance\tTime\tPace\tGAP\tMinetti\tHR Index");

            foreach (var filename in filenames)
            {
                var input = Deserializer.File(filename);
                if (input == null || input.TrackPoints.Count == 0 || !input.HeartRates().Any())
                    return;

                var track = input.GpsPoints().ToList();
                var time = input.ElapsedSeconds().ToList();
                var dist = Geodesy.Distance.HaversineAccumulated(track);
                var height = track.Select(pt => pt.Elevation).ToList();
                var hrm = input.HeartRates().ToList();
                var l0 = 0.0;
                var h0 = 0.0;
                var t0 = 0.0;
                var minettiFactor = 0.0;
                var index = 0.0;
                var ls = 0.0;
                var ts = 0.0;
                for (var i = 0; i < track.Count; i++)
                {
                    var l = dist[i];
                    var h = height[i];
                    var t = time[i];
                    if (i == 0)
                    {
                        l0 = l;
                        h0 = h;
                        t0 = t;
                        continue;
                    }
                    var gradient = 0.0;
                    var dl = l - l0;
                    var dt = t - t0;
                    if (i > 0 && dl > 0)
                    {
                        gradient = (h - h0) / dl;
                    }
                    var minetti = Function.MinettiFactor(gradient);
                    var v = dt > 0 ? dl / dt : 0.0;
                    if (v > 0.01)
                    {
                        minettiFactor += minetti * dl;
                        index += (hrm[i] - shr) / v * dt / minetti;
                        ls += dl;
                        ts += dt;
                        l0 = l;
                        h0 = h;
                        t0 = t;
                    }
                }
                var pace = time.Last() / (dist.Last() / 1000.0);
                Console.WriteLine($"{System.IO.Path.GetFileNameWithoutExtension(filename)}\t{System.Math.Round(dist.Last() / 1000.0, 2)}\t{ToMinSec(time.Last())}\t{ToMinSec(pace)}\t{ToMinSec(pace / (minettiFactor / ls))}\t{System.Math.Round(minettiFactor / ls, 4)}\t{System.Math.Round(index / ts, 2)}");

            }
        }
        private static string ToMinSec(double t)
        {
            var sec = t % 60.0;
            var min = (t - sec) / 60.0;
            return $"{((int)min)}:{(int)sec}";

        }

    }
}