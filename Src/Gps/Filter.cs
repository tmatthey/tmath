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
        public static IList<GpsPoint> SmoothZeroDisplacements(IList<GpsPoint> track)
        {
            int holes, start, end;
            return SmoothZeroDisplacements(track, out holes, out start, out end);
        }

        public static IList<GpsPoint> SmoothZeroDisplacements(IList<GpsPoint> track, out int holes, out int start,
            out int end)
        {
            holes = 0;
            start = 0;
            end = 0;
            var res = track.Select(gpsPoint => new GpsPoint(gpsPoint)).ToList();
            if (track.Count < 4)
                return res;

            List<int> startIdx;
            List<int> endIdx;
            var d = Geodesy.Distance.Haversine(track);
            holes = FindHoles(d, out startIdx, out endIdx);

            if (startIdx.Count == 0)
                return res;

            var squareSum = 0.0;
            var sum = 0.0;
            foreach (var l in d)
            {
                sum += l;
                squareSum += l*l;
            }


            var n = d.Count;
            for (var i = 0; i < startIdx.Count; i++)
            {
                var i0 = startIdx[i];
                var i1 = endIdx[i];
                var m = i1 - i0;
                var variance = squareSum - sum*sum/(n - m);
                var varianceStart = double.PositiveInfinity;
                var varianceEnd = double.PositiveInfinity;
                if (i0 > 0)
                {
                    var a = d[i0 - 1];
                    varianceStart = squareSum + a*a*(1.0/(m + 1) - 1.0) - sum*sum/n;
                }
                if (i1 < d.Count)
                {
                    var a = d[i1];
                    varianceEnd = squareSum + a*a*(1.0/(m + 1) - 1.0) - sum*sum/n;
                }
                if (Comparison.IsLessEqual(varianceStart, varianceEnd) &&
                    Comparison.IsLessEqual(varianceStart, variance))
                {
                    start++;
                    var a = d[i0 - 1];
                    squareSum += a*a*(1.0/(m + 1) - 1.0);
                    for (var j = i0 - 1; j < i1; j++)
                        d[j] = a/(m + 1);
                    for (var j = 0; j < m; j++)
                        res[j + i0] = res[i0 - 1].Interpolate(res[i1], (j + 1.0)/(m + 1.0));
                }
                else if (Comparison.IsLessEqual(varianceEnd, varianceStart) &&
                         Comparison.IsLessEqual(varianceEnd, variance))
                {
                    end++;
                    var a = d[i1];
                    squareSum += a*a*(1.0/(m + 1) - 1.0);
                    for (var j = i0; j < i1 + 1; j++)
                        d[j] = a/(m + 1);
                    for (var j = 0; j < m; j++)
                        res[j + i0 + 1] = res[i0].Interpolate(res[i1 + 1], (j + 1.0)/(m + 1.0));
                }
            }
            return res;
        }

        public static IList<GpsPoint> SmoothZeroDisplacements(IList<GpsPoint> track, IList<double> time)
        {
            int holes, start, end;
            return SmoothZeroDisplacements(track, time, out holes, out start, out end);
        }

        public static IList<GpsPoint> SmoothZeroDisplacements(IList<GpsPoint> track, IList<double> time, out int holes,
            out int start, out int end)
        {
            holes = 0;
            start = 0;
            end = 0;
            var res = track.Select(gpsPoint => new GpsPoint(gpsPoint)).ToList();
            if (track.Count < 4)
                return res;
            List<int> startIdx;
            List<int> endIdx;
            var d = Geodesy.Distance.Haversine(track);
            holes = FindHoles(d, out startIdx, out endIdx);

            if (startIdx.Count == 0)
                return res;

            var w = new List<double>();
            for (var i = 0; i + 1 < time.Count; i++)
                w.Add(time[i + 1] - time[i]);

            for (var i = 0; i < startIdx.Count; i++)
            {
                var i0 = startIdx[i];
                var i1 = endIdx[i];
                var sumX = 0.0;
                var sumW = 0.0;
                var sumWrest = 0.0;
                for (var j = 0; j < d.Count; j++)
                {
                    if (!(i0 <= j && j < i1))
                    {
                        sumX += w[j]*d[j];
                        sumW += w[j];
                    }
                    else
                    {
                        sumWrest += w[j];
                    }
                }
                var u = sumX/sumW;
                var variance = 0.0;
                for (var j = 0; j < d.Count; j++)
                {
                    if (!(i0 <= j && j < i1))
                    {
                        variance += w[i]*(d[j] - u)*(d[j] - u);
                    }
                }
                //variance /= sumW;

                var varianceStart = double.PositiveInfinity;
                var varianceEnd = double.PositiveInfinity;
                var dStart = new List<double>();
                var dEnd = new List<double>();
                if (i0 > 0)
                {
                    var a = d[i0 - 1];
                    var b = w[i0 - 1];
                    var newSumX = sumX - a*b;
                    var dx = a/(b + sumWrest);
                    dStart = d.ToList();
                    for (var j = i0 - 1; j < i1; j++)
                    {
                        dStart[j] = dx*w[j];
                        newSumX += w[j]*dStart[j];
                    }
                    var newU = newSumX/(sumW + sumWrest);
                    varianceStart = 0.0;
                    for (var j = 0; j < d.Count; j++)
                    {
                        varianceStart += w[j]*(dStart[j] - newU)*(dStart[j] - newU);
                    }
                    //varianceStart /= sumW + sumWrest;
                }
                if (i1 < d.Count)
                {
                    var a = d[i1];
                    var b = w[i1];
                    var newSumX = sumX - a*b;
                    var dx = a/(b + sumWrest);
                    dEnd = d.ToList();
                    for (var j = i0; j <= i1; j++)
                    {
                        dEnd[j] = dx*w[j];
                        newSumX += w[j]*dEnd[j];
                    }
                    var newU = newSumX/(sumW + sumWrest);
                    varianceEnd = 0.0;
                    for (var j = 0; j < d.Count; j++)
                    {
                        varianceEnd += w[j]*(dEnd[j] - newU)*(dEnd[j] - newU);
                    }
                    //varianceEnd /= sumW + sumWrest;
                }
                if (Comparison.IsLessEqual(varianceStart, varianceEnd) &&
                    Comparison.IsLessEqual(varianceStart, variance))
                {
                    start++;
                    var b = w[i0 - 1];
                    var dx = 1.0/(b + sumWrest);

                    var c = 0.0;
                    for (var j = i0; j < i1; j++)
                    {
                        c += w[j - 1];
                        res[j] = res[i0 - 1].Interpolate(res[i1], c*dx);
                    }
                    d = dStart;
                }
                else if (Comparison.IsLessEqual(varianceEnd, varianceStart) &&
                         Comparison.IsLessEqual(varianceEnd, variance))
                {
                    end++;
                    var b = w[i1];
                    var dx = 1.0/(b + sumWrest);

                    var c = 0.0;
                    for (var j = i0 + 1; j < i1 + 1; j++)
                    {
                        c += w[j - 1];
                        res[j] = res[i0].Interpolate(res[i1 + 1], c*dx);
                    }
                    d = dEnd;
                }
            }
            return res;
        }

        private static int FindHoles(IList<double> d, out List<int> startIdx,
            out List<int> endIdx)
        {
            startIdx = new List<int>();
            endIdx = new List<int>();
            var isZero = false;
            for (var i = 0; i < d.Count; i++)
            {
                if (Comparison.IsZero(d[i]))
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
                else
                    isZero = false;
            }
            return startIdx.Count;
        }
    }
}