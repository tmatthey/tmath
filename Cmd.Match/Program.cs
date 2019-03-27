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
using System.Globalization;
using System.Linq;
using Math;
using Math.Clustering;
using Math.Gps;
using Math.Tools.TrackReaders;
using Math.Tools.Base;

namespace Cmd.Match
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var p = new CommandLineParser("match", args);

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
                .Setup("a", "Minimum number of activities.", out var n, 5)
                .Setup("l", "Minimum segment length.", out var minL, 20.0)
                .Setup("p", "MDL cost advantage.", out var cost, 5)
                .Setup("e", "Epsilon neighborhood", out var eps, 20.0)
                .Setup("r", "Standing heart rate", out var hrStanding, 40 + 26)
                .Setup("f", "Epsilon track", out var epsTrack, 25.0);

            p.Parse();


            Console.WriteLine("Match");


            var activities = (from activity in Deserializer.Directory(path)
                where
                    activity.GpsPoints().Count() == activity.HeartRates().Count() &&
                    activity.GpsPoints().Count() == activity.Times().Count() &&
                    activity.HeartRates().Sum() > activity.HeartRates().Count() * 20
                select activity).OrderBy(a => a.Date.Ticks).ToList();
            var list =
                activities.Select(
                    activity =>
                        GpsFiltering.InterpolateDublicates(activity.GpsPoints().ToList(),
                            activity.Seconds().ToList()).ToList()).ToList();

            Console.WriteLine("Tracks: {0}", list.Count);
            Console.WriteLine("Points: {0}", list.Sum(t => t.Count));

            var clusters = GpsSegmentClustering.FindGlobalCommonSegments(list, n, eps, minL, cost, epsTrack);

            Console.WriteLine("Maps: {0}", clusters.Count);

            var k = 0;
            foreach (var cluster in clusters)
            {
                var count =
                    (from segmentResult in cluster from track in segmentResult.TrackSegments select track.Id).Distinct()
                    .Count();

                Console.WriteLine("Cluster {0}: {1}", k, count);
                Console.WriteLine("Segments : {0}", cluster.Count);
                Console.WriteLine(
                    "Segment No\tTrack No\tSeg Distance [m]\tDate\tName\tDirection\tCommon\tFirst\tLast\tCoverage\tFirst\tLast\tTrack Seg Distance [m]\tHR dist [m]\tTime [s]\tHR\tSpeed [Km/h]\tPace [min/km]\tHR Index\tHR Index II");

                var sn = 0;
                foreach (var segment in cluster)
                {
                    var fac = 0.0;
                    foreach (var track in segment.TrackSegments)
                    {
                        var j = track.Id;
                        Console.Write("{0}\t{1}\t{2}\t{3}\t{4}\t", sn, j, segment.Length, activities[j].Times().First(),
                            activities[j].Name);
                        if (Comparison.IsLess(0, track.Length) &&
                            Comparison.IsLess(0.5, System.Math.Abs(track.Direction)))
                        {
                            var hr = activities[j].HeartRates().ToList();
                            var seconds = activities[j].Seconds().ToList();
                            var t = 0.0;
                            var index1 = 0.0;
                            var h = 0.0;
                            var d = 0.0;
                            Tools.DistanceVelocityAcceleration(list[j], seconds, out var dist, out var vel, out _);
                            vel = Statistics.Arithmetic.CenteredMovingAverage(vel, 10.0);
                            for (var l = 0; l < track.Indices.Count; l++)
                            {
                                var i = track.Indices[l];
                                if (0 < i && i + 1 < list[j].Count)
                                {
                                    var dt = (seconds[i + 1] - seconds[i - 1]) / 2.0;
                                    t += dt;
                                    index1 += (hr[i] - hrStanding) / vel[i] * dt;
                                    d += (dist[i + 1] - dist[i - 1]) / 2.0;
                                    h += hr[i] * dt;
                                }
                            }

                            h /= t;
                            var v = d / t;
                            index1 /= t;
                            var perfectMatch = Comparison.IsEqual(track.Common, 1.0) &&
                                               Comparison.IsEqual(track.Coverage, 1.0);
                            if (Comparison.IsZero(fac) && perfectMatch)
                            {
                                fac = index1 / ((h - hrStanding) * t);
                            }

                            var index2 = 0.0;
                            if (perfectMatch)
                            {
                                index2 = (h - hrStanding) * t * fac;
                            }

                            var index2Str = Comparison.IsZero(index2)
                                ? ""
                                : index2.ToString(CultureInfo.InvariantCulture);
                            Console.WriteLine(
                                "{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}",
                                track.Direction < 0 ? -1.0 : 1.0,
                                track.Common, track.SegmentFirst, track.SegmentLast, track.Coverage, track.First,
                                track.Last, track.Length, d, t, h, v * 3.6, 60.0 / (v * 3.6), index1, index2Str);
                        }
                        else
                        {
                            Console.WriteLine("No direction");
                        }
                    }

                    sn++;
                }

                k++;
            }
        }
    }
}
