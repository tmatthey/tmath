/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2019 Thierry Matthey
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
    // Ex. : GPS error
    public class FilterDuplicatesEnd : FilterDuplicates
    {
        public override int Takes()
        {
            return 1;
        }

        protected override List<GpsPointExt> Interpolate(IList<GpsPoint> track, IList<double> time, IList<int> startIdx,
            List<int> endIdx)
        {
            var i0 = startIdx[0];
            var i1 = endIdx[0];
            var res = new List<GpsPointExt> {new GpsPointExt(track[i0 - 1], i0 - 1)};

            for (var j = i0; j < i1; j++)
                res.Add(new GpsPointExt(track[i0 - 1].Interpolate(track[i1],
                    (time[j] - time[i0 - 1]) / (time[i1] - time[i0 - 1])), j));

            res.Add(new GpsPointExt(track[i1], i1));
            res.Add(new GpsPointExt(track[i1 + 1], i1 + 1));
            return res;
        }
    }
}