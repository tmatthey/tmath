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

namespace Math
{
    public class Regression
    {
        // First order regression without weight : a*x + b
        static public void Linear(IList<double> x, IList<double> y, out double a, out double b)
        {
            IList<double> w = Enumerable.Repeat(1.0, x.Count).ToList();
            Linear(x, y, w, out a, out b);
        }

        // First order regression weighted : a*x + b
        // http://academic.macewan.ca/burok/Stat252/notes/regression1.pdf
        static public void Linear(IList<double> x, IList<double> y, IList<double> w, out double a, out double b)
        {
            a = double.NaN;
            b = double.NaN;
            if (x.Count > 0 && x.Count == y.Count && y.Count == w.Count)
            {
                var sx = 0.0;
                var sy = 0.0;
                var ss = 0.0;
                for (var i = 0; i < x.Count; ++i)
                {
                    ss += w[i];
                    sx += x[i] * w[i];
                    sy += y[i] * w[i];
                }
                var st2 = 0.0;
                var aa = 0.0;
                var sxdss = sx / ss;
                for (var i = 0; i < x.Count; ++i)
                {
                    var t = x[i] - sxdss;
                    st2 += t * t * w[i];
                    aa += t * y[i] * w[i];
                }
                if (!Comparison.IsEqual(st2, 0.0) && !Comparison.IsEqual(ss, 0.0))
                {
                    a = aa / st2;
                    b = (sy - sx * a) / ss;
                }
            }
        }
    }
}
