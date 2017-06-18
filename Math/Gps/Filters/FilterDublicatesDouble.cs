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

namespace Math.Gps.Filters
{
    public class FilterDublicatesDouble : FilterDublicates
    {
        //
        //  d  = a / (2.0 + x);
        //  l0 = d * (1.0 + 0.0 * x);
        //  l1 = d * (1.0 + 1.0 * x);
        //  l2 = d * (1.0 + 2.0 * x);
        //  l3 = d * (1.0 + 3.0 * x);
        //  a + B = l0 + l1 + l2 + l3
        //

        public override int Takes()
        {
            return 2;
        }

        protected override List<GpsPointExt> Interpolate(IList<GpsPoint> track, IList<double> time, IList<int> startIdx,
            List<int> endIdx)
        {
            var i0 = startIdx[0];
            var i1 = endIdx[0];
            var j0 = startIdx[1];
            var j1 = endIdx[1];

            var res = new List<GpsPointExt>();

            if (i1 + 1 == j0 && i0 + 1 == i1 && j0 + 1 == j1)
            {
                var u = time[i1] - time[i0 - 1];
                var a = track[i0 - 1].HaversineDistance(track[i1])/u;
                var v = time[j1] - time[j0 - 1];
                var b = track[j0 - 1].HaversineDistance(track[j1])/v;
                var x = 2.0*(a - b)/(b - a*5.0);
                var x0 = 1.0/(2.0 + x);
                var x1 = a/(2.0 + x)*(1.0 + 2.0*x)/b;
                if (Comparison.IsLess(0.1, x0) && Comparison.IsLess(x0, 0.9) && Comparison.IsLess(0.1, x1) &&
                    Comparison.IsLess(x1, 0.9))
                {
                    res.Add(new GpsPointExt(track[i0 - 1], i0 - 1));
                    res.Add(new GpsPointExt(track[i0 - 1].Interpolate(track[i1], x0), i0));
                    res.Add(new GpsPointExt(track[i1], i1));
                    res.Add(new GpsPointExt(track[j0 - 1].Interpolate(track[j1], x1), j0));
                    res.Add(new GpsPointExt(track[j1], j1));
                    res.Add(new GpsPointExt(track[j1 + 1], j1 + 1));
                }
            }
            return res;
        }
    }
}