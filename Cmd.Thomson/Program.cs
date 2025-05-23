﻿/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2025 Thierry Matthey
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
using System.Diagnostics;
using System.Linq;
using Math;
using Math.Tools.Base;

namespace Cmd.Thomson
{
    //
    // https://en.wikipedia.org/wiki/Thomson_problem
    //
    public static class Program
    {
        private static void Main(string[] args)
        {
            var p = new CommandLineParser("thomson", args);

            p.SetupHelp(helpText =>
            {
                Console.WriteLine(helpText);
                Environment.Exit(0);
            }).SetupError((helpText, errorText) =>
            {
                Console.WriteLine(errorText);
                Console.WriteLine(helpText);
                Environment.Exit(0);
            }).Setup("n", "N", out var n, 12);

            p.Parse();


            Console.WriteLine("Thomas : {0}", n);
            Console.WriteLine("Itr\tU\tdiff\tmin\tmax");

            var minRInitial = Conversion.DegToRad(20.0) / System.Math.PI;
            var rnd = new Random();
            var x = new List<Vector3D>();
            for (var i = 0; i < n; i++)
                x.Add(new Vector3D(rnd.Next(), rnd.Next(), rnd.Next()).Normalized());

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var last = 0;
            const int delta = 1;
            long k = 0;
            var xp = new Vector3D[n];
            while (last < 60 * 60 / delta)
            {
                k++;
                var minR = minRInitial / System.Math.Log(k + 1);
                for (var i = 0; i < n; i++)
                {
                    var F = Vector3D.E1;
                    for (var j = 0; j < n; j++)
                    {
                        if (i != j)
                        {
                            var ang = x[i].Angle(x[j]) / System.Math.PI;
                            var r = System.Math.Max(ang, minR);
                            var q = System.Math.Min((1.0 / r - 1.0) * minR, 0.8);
                            var b = q * q * System.Math.PI;
                            F = F.Rotate(x[i] ^ x[j], b);
                        }
                    }

                    var axis = F ^ Vector3D.E1;
                    var angle = F.Angle(Vector3D.E1);
                    xp[i] = x[i].Rotate(axis, angle);
                }

                for (var i = 0; i < n; i++)
                    x[i] = xp[i].Normalized();

                if (Comparison.IsLess(last + delta, stopwatch.Elapsed.TotalSeconds))
                {
                    last += delta;
                    var U = 0.0;
                    var angles = new List<double>();
                    for (var i = 0; i < n; i++)
                    {
                        var e = double.PositiveInfinity;
                        for (var j = 0; j < n; j++)
                        {
                            if (i != j)
                            {
                                e = System.Math.Min(e, x[i].Angle(x[j]));
                                U += 0.5 / x[i].EuclideanNorm(x[j]);
                            }
                        }

                        angles.Add(e);
                    }

                    angles = angles.OrderBy(num => num).ToList();
                    var e0 = angles.First();
                    var e1 = angles.Last();
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", k, U, Conversion.RadToDeg(e1 - e0),
                        Conversion.RadToDeg(e0), Conversion.RadToDeg(e1));
                }
            }
        }
    }
}