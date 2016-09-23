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
        public static IList<GpsPoint> RemoveHoles(IList<GpsPoint> track)
        {
            if (track.Count < 4)
                return track;
            var d = new List<double>();
            var squareSum = 0.0;
            var sum = 0.0;
            var startIdx = new List<int>();
            var endIdx = new List<int>();
            var isZero = false;
            for (var i = 0; i + 1 < track.Count; i++)
            {
                var l = Geodesy.Distance.Haversine(track[i], track[i + 1]);
                d.Add(l);
                sum += l;
                squareSum += l*l;
                if (Comparison.IsZero(l))
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

            var res = track.Select(gpsPoint => new GpsPoint(gpsPoint)).ToList();

            var n = d.Count;
            for (var i = 0; i < startIdx.Count; i++)
            {
                var i0 = startIdx[i];
                var i1 = endIdx[i];
                var m = i1 - i0;
                var variance = squareSum - sum*sum/(n - m);
                var varianceFront = double.PositiveInfinity;
                var varianceBack = double.PositiveInfinity;
                if (i0 > 0)
                {
                    var a = d[i0 - 1];
                    varianceFront = squareSum + a*a*(1.0/(m + 1) - 1.0) - sum*sum/n;
                }
                if (i1 < d.Count)
                {
                    var a = d[i1];
                    varianceBack = squareSum + a*a*(1.0/(m + 1) - 1.0) - sum*sum/n;
                }
                if (Comparison.IsLessEqual(varianceFront, varianceBack) &&
                    Comparison.IsLessEqual(varianceFront, variance))
                {
                    var a = d[i0 - 1];
                    squareSum += a*a*(1.0/(m + 1) - 1.0);
                    for (var j = i0 - 1; j < i1; j++)
                        d[j] = a/(m + 1);
                    for (var j = 0; j < m; j++)
                        res[j + i0] = res[i0 - 1].Interpolate(res[i1], (j + 1.0)/(m + 1.0));
                }
                else if (Comparison.IsLessEqual(varianceBack, varianceFront) &&
                         Comparison.IsLessEqual(varianceBack, variance))
                {
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
    }
}