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
using System.Linq;
using Fclp;
using Math;
using Math.Gps;
using Math.Tools.TrackReaders;

namespace App.Compare
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var refFile = "";
            var curFile = "";
            var eps = 10.0;
            var p = new FluentCommandLineParser();

            p.Setup<string>('r')
                .Callback(v => refFile = v)
                .SetDefault(refFile)
                .WithDescription("GPX/TCX reference file");

            p.Setup<string>('c')
                .Callback(v => curFile = v)
                .SetDefault(curFile)
                .WithDescription("GPX/TCX to compared file");

            p.Setup<double>('e')
                .Callback(v => eps = v)
                .SetDefault(eps)
                .WithDescription("Neighborhood/epsilon distance");


            p.SetupHelp("?", "help")
                .Callback(text =>
                {
                    Console.WriteLine(text);
                    Environment.Exit(0);
                });
            p.Parse(args);


            var refInput = Deserializer.File(refFile);
            var curInput = Deserializer.File(curFile);
            if (refInput == null || refInput.TrackPoints.Count == 0 || curInput == null ||
                curInput.TrackPoints.Count == 0)
                return;

            Console.WriteLine("compare {0} vs {1}", refInput.Name, curInput.Name);
            Console.WriteLine("Time\tDist\tElev\tTime\tDist\tElev\tEps\tdt\tndt");
            var refTrack = refInput.GpsPoints().ToList();
            var refTime = refInput.ElapsedSeconds().ToList();
            var refDist = Geodesy.Distance.HaversineAccumulated(refTrack);
            var curTrack = curInput.GpsPoints().ToList();
            var curTime = curInput.ElapsedSeconds().ToList();
            var curDist = Geodesy.Distance.HaversineAccumulated(curTrack);
            var analyzer = new NeighbourGpsDistanceCalculator(refTrack, System.Math.Max(eps, 50.0));
            var current = analyzer.Analyze(curTrack, eps);
            var f = (curTime.Last() - refTime.Last())/curDist.Last();
            foreach (var neighbour in current.Neighbours)
            {
                var first = neighbour.First();
                var i = first.Current;
                var j = first.Reference;
                var ct = curTime[i];
                var cd = curDist[i];
                var ca = curTrack[i].Elevation;
                var a = first.Fraction;
                var rt = Function.Interpolate(a, refTime[j], refTime[j + 1]);
                var rd = Function.Interpolate(a, refDist[j], refDist[j + 1]);
                var ra = Function.Interpolate(a, refTrack[j].Elevation, refTrack[j + 1].Elevation);
                var dt = ct - rt;
                var ndt = dt - cd*f;
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}", rt, rd, ra, ct, cd, ca,
                    first.MinDistance, dt, ndt);
            }
        }
    }
}