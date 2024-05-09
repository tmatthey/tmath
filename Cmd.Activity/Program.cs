/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2024 Thierry Matthey
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

namespace Cmd.Activity
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            var p = new CommandLineParser("activity", args);

            p.SetupHelp(helpText =>
            {
                Console.WriteLine(helpText);
                Environment.Exit(0);
            }).SetupError((helpText, errorText) =>
            {
                Console.WriteLine(errorText);
                Console.WriteLine(helpText);
                Environment.Exit(0);
            }).Setup("d", "directory with *.tcx and *.gpx files.", out var path, "./");

            p.Parse();

            Console.WriteLine("Activity");

            var activities = (from activity in Deserializer.Directory(path)
                where
                    activity.GpsPoints().Count() == activity.Times().Count()
                select activity).OrderBy(a => a.Date.Ticks).ToList();
            var list = (from activity in activities select activity.GpsPoints().ToList()).ToList();
            Console.WriteLine("Tracks: {0}", list.Count);
            Console.WriteLine("Points: {0}", list.Sum(t => t.Count));
            for (var i = 0; i < list.Count; i++)
            {
                var activity = activities[i];
                var seconds = activity.Seconds().Select(t1 => t1 - activity.Seconds().First()).ToList();
                var gpsTrack = activity.GpsPoints().ToList();
                var vel = new List<double>();
                var dist = Geodesy.Distance.Haversine(gpsTrack);
                for (var j = 0; j + 1 < seconds.Count; j++)
                {
                    vel.Add(dist[j] / (seconds[j + 1] - seconds[j]));
                }

                gpsTrack = GpsFiltering.InterpolateDuplicates(activity.GpsPoints().ToList(), seconds).ToList();
                Console.WriteLine("Track {0} {1} {2} {3} {4}", activity.Name, i, gpsTrack.Count,
                    GpsFiltering.FindDuplicates(activity.GpsPoints().ToList()).Count(),
                    GpsFiltering.FindDuplicates(gpsTrack).Count());
                var vel2 = new List<double>();
                var dist2 = Geodesy.Distance.Haversine(gpsTrack);
                Tools.DistanceVelocityAcceleration(gpsTrack, out _, out var vel3, out var acc3);
                vel3 = Statistics.Arithmetic.CenteredMovingAverage(vel3, 10.0);
                for (var j = 0; j + 1 < seconds.Count; j++)
                {
                    vel2.Add(dist2[j] / (seconds[j + 1] - seconds[j]));
                    var a = j > 0
                        ? (vel2[j] - vel2[j - 1]) / (seconds[j + 1] - seconds[j - 1]) * 2.0
                        : vel2[j] / seconds[j + 1];
                    Console.WriteLine("\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}", seconds[j + 1], dist2[j], vel3[j],
                        acc3[j], vel2[j], a, dist[j],
                        vel[j]);
                }

                Console.WriteLine(Statistics.Arithmetic.Variance(vel, seconds));
                Console.WriteLine(Statistics.Arithmetic.Variance(vel2, seconds));
                Console.WriteLine(Statistics.Arithmetic.Variance(vel3, seconds));
            }
        }
    }
}