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
using System.Linq;
using Math;
using Math.Gps;
using Math.Tools.Base;
using Math.Tools.TrackReaders;

namespace Cmd.Normalize
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            var p = new CommandLineParser("normalize", args);

            p.SetupHelp(helpText =>
                {
                    Console.WriteLine(helpText);
                    Environment.Exit(0);
                }).SetupError((helpText, errorText) =>
                {
                    Console.WriteLine(errorText);
                    Console.WriteLine(helpText);
                    Environment.Exit(0);
                }).Setup("n", "Number of points.", out var n, 12).Setup("t", "End time [s]", out var endTime, 0.0)
                .Setup("l", "End length [m]", out var endLength, 0.0).Setup("f", "File name", out string f).Setup("c",
                    "Equidistant by time or length", out var equiType, NormalzieType.Length);

            p.Parse();


            Console.WriteLine("normalize : {0}", n);
            Console.WriteLine("Time\tDistance");

            var input = Deserializer.File(f);
            if (input == null || input.TrackPoints.Count == 0)
                return;

            var track = input.GpsPoints().ToList();
            var elapsed = input.ElapsedSeconds().ToList();
            var totalTime = elapsed.Last();
            var tf = endTime > 0.0 ? endTime / totalTime : 1.0;
            var dist = Geodesy.Distance.HaversineAccumulated(track);
            var totalDist = dist.Last();
            var lf = endLength > 0.0 ? endLength / totalDist : 1.0;

            Console.WriteLine("{0}\t{1}", 0.0, 0.0);

            if (n > 1 && track.Count > 1)
            {
                var dt = totalTime / n;
                var dl = totalDist / n;
                var st = 0.0;
                var sl = 0.0;
                var j = 0;
                for (var i = 1; i < n; i++)
                {
                    if (equiType == NormalzieType.Length)
                    {
                        sl += dl;
                        while (j + 1 < dist.Count && Comparison.IsLess(dist[j + 1], sl))
                            j++;
                        st = Function.Interpolate(sl, dist[j], dist[j + 1], elapsed[j], elapsed[j + 1]);
                    }
                    else
                    {
                        st += dt;
                        while (j + 1 < elapsed.Count && Comparison.IsLess(elapsed[j + 1], st))
                            j++;
                        sl = Function.Interpolate(st, elapsed[j], elapsed[j + 1], dist[j], dist[j + 1]);
                    }

                    Console.WriteLine("{0}\t{1}", st * tf, sl * lf);
                }
            }

            Console.WriteLine("{0}\t{1}", totalTime * tf, totalDist * lf);
        }


        private enum NormalzieType
        {
            Time = 1,
            Length = 2
        }
    }
}