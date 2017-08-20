/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2017 Thierry Matthey
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
using Math.Tools.TrackReaders;


namespace App.Minetti
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var f = "C:\\Users\\tmatthey\\Desktop\\jfm.gpx";
            var p = new FluentCommandLineParser();


            p.Setup<string>('f')
                .Callback(v => f = v)
                .SetDefault(f)
                .WithDescription("File name");

 
            p.SetupHelp("?", "help")
                .Callback(text =>
                {
                    Console.WriteLine(text);
                    Environment.Exit(0);
                });
            p.Parse(args);


            var input = Deserializer.File(f);
            if (input == null || input.TrackPoints.Count == 0)
                return;

            var track = input.GpsPoints().ToList();
            var time = input.ElapsedSeconds().ToList();
            var dist = Geodesy.Distance.HaversineAccumulated(track);
            var height = track.Select(pt => pt.Elevation).ToList();
            var l0 = 0.0;
            var h0 = 0.0;
            var sum = 0.0;
            for (var i = 0; i < track.Count; i++)
            {
                var l = dist[i];
                var h = height[i];
                var t = time[i];
                var gradient = 0.0;
                var dl = l - l0;
                if (i > 0 && dl > 0)
                {
                    gradient = (h - h0) / dl;
                }
                var minetti = Function.MinettiFactor(gradient);
                if (i == 0 || (dl > 20 && l > 500))
                {
                    sum += minetti * dl;
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", t, l, h, gradient, sum > 0 ? minetti : 1.0,
                        sum > 0 ? sum / l : 1.0);
                    l0 = l;
                    h0 = h;
                }
            }
        }
    }
}