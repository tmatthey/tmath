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

using System.Collections.Generic;
using System.Linq;

namespace Math.Gps
{
    public static class Filter
    {
        public static IList<GpsPoint> InterpolateDublicates(IList<GpsPoint> track)
        {
            var time = new List<double>();
            for (var i = 0; i < track.Count; i++)
                time.Add(i);
            return InterpolateDublicates(track, time);
        }

        public static IList<GpsPoint> InterpolateDublicates(IList<GpsPoint> track, IList<double> time)
        {
            var res = track.Select(gpsPoint => new GpsPoint(gpsPoint)).ToList();
            if (track.Count < 4)
                return res;
            List<int> startIdx;
            List<int> endIdx;
            var zeros = FindDuplicates(track, out startIdx, out endIdx);

            if (zeros == 0)
                return res;

            for (var i = 0; i < startIdx.Count; i++)
            {
                var i0 = startIdx[i];
                var i1 = endIdx[i];
                if (!(0 < i0 && i1 + 1 < track.Count))
                    continue;

                var err0 = double.PositiveInfinity;
                if (1 < i0 && i0 + 1 == i1)
                {
                    var vel0 = new List<double>
                    {
                        track[i0 - 2].HaversineDistance(track[i0 - 1])/(time[i0] - time[i0 - 2]),
                        track[i0 - 1].HaversineDistance(track[i1])/(time[i1] - time[i0]),
                        track[i1].HaversineDistance(track[i1 + 1])/(time[i1 + 1] - time[i1])
                    };
                    err0 = Statistics.Arithmetic.Variance(vel0,
                        new List<double> {time[i0] - time[i0 - 2], time[i1] - time[i0], time[i1 + 1] - time[i1]});
                }

                var vel1 = new List<double>
                {
                    track[i0 - 1].HaversineDistance(track[i0])/(time[i0] - time[i0 - 1]),
                    track[i0].HaversineDistance(track[i1 + 1])/(time[i1 + 1] - time[i0])
                };
                var err1 = Statistics.Arithmetic.Variance(vel1,
                    new List<double> {time[i0] - time[i0 - 1], time[i1 + 1] - time[i0]});
                var vel2 = new List<double>
                {
                    track[i0 - 1].HaversineDistance(track[i1])/(time[i1] - time[i0 - 1]),
                    track[i1].HaversineDistance(track[i1 + 1])/(time[i1 + 1] - time[i1])
                };
                var err2 = Statistics.Arithmetic.Variance(vel2,
                    new List<double> {time[i1] - time[i0 - 1], time[i1 + 1] - time[i1]});
                if (Comparison.IsLess(err0, System.Math.Min(err1, err2)))
                {
                    res[i0 - 1] = track[i0 - 2].Interpolate(track[i0 - 1], 0.5);
                    res[i0] = track[i0 - 1];
                    //for (var j = i0+1 ; j < i1 ; j++)
                    //    res[j] = track[i0 - 1].Interpolate(track[i1 ],
                    //        (time[j] - time[i0 ]) / (time[i1] - time[i0]));
                }
                else
                {
                    var k = err1 < err2 ? 1 : 0;
                    for (var j = i0 + k; j < i1 + k; j++)
                        res[j] = track[i0 + k - 1].Interpolate(track[i1 + k],
                            (time[j] - time[i0 + k - 1])/(time[i1 + k] - time[i0 + k - 1]));
                }
            }

            return res;
        }

        public static int CountDuplicates(IList<GpsPoint> track)
        {
            var n = 0;
            var isZero = false;
            for (var i = 0; i + 1 < track.Count; i++)
            {
                if (track[i].Equals(track[i + 1]))
                {
                    if (!isZero)
                    {
                        n++;
                        isZero = true;
                    }
                }
                else
                {
                    isZero = false;
                }
            }
            return n;
        }

        public static IEnumerable<int> FindDuplicates(IList<GpsPoint> track)
        {
            var res = new List<int>();
            var lastAdd = -1;
            for (var i = 0; i + 1 < track.Count; i++)
            {
                if (track[i].Equals(track[i + 1]))
                {
                    if (lastAdd != i)
                        res.Add(i);
                    res.Add(i + 1);
                    lastAdd = i + 1;
                }
            }
            return res;
        }


        private static int FindDuplicates(IList<GpsPoint> track, out List<int> startIdx,
            out List<int> endIdx)
        {
            startIdx = new List<int>();
            endIdx = new List<int>();
            var isZero = false;
            for (var i = 0; i + 1 < track.Count; i++)
            {
                if (track[i].Equals(track[i + 1]))
                {
                    if (!isZero)
                    {
                        startIdx.Add(i);
                        endIdx.Add(i + 1);
                        isZero = true;
                    }
                    else
                    {
                        endIdx[endIdx.Count - 1] = i + 1;
                    }
                }
                else
                {
                    isZero = false;
                }
            }
            return startIdx.Count;
        }
    }
}