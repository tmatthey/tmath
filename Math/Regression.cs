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

using System.Collections.Generic;
using System.Linq;

namespace Math
{
    public class Regression
    {
        // First order regression without weight : a*x + b
        public static void Linear(IList<double> x, IList<double> y, out double a, out double b)
        {
            IList<double> w = Enumerable.Repeat(1.0, x.Count).ToList();
            Linear(x, y, w, out a, out b);
        }

        // First order regression weighted : a*x + b
        // http://academic.macewan.ca/burok/Stat252/notes/regression1.pdf
        public static void Linear(IList<double> x, IList<double> y, IList<double> w, out double a, out double b)
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

        // Second order regression without weight : a*x^2 + b*x + c
        public static void Quadratic(IList<double> x, IList<double> y, out double a, out double b, out double c)
        {
            IList<double> w = Enumerable.Repeat(1.0, x.Count).ToList();
            Quadratic(x, y, w, out a, out b, out c);
        }

        // Second order regression weighted : a*x^2 + b*x + c
        public static void Quadratic(IList<double> x, IList<double> y, IList<double> w, out double a, out double b,
            out double c)
        {
            a = double.NaN;
            b = double.NaN;
            c = double.NaN;
            if (x.Count > 0 && x.Count == y.Count && y.Count == w.Count)
            {
                var w1 = 0.0;
                var wx = 0.0;
                var wx2 = 0.0;
                var wx3 = 0.0;
                var wx4 = 0.0;
                var wy = 0.0;
                var wyx = 0.0;
                var wyx2 = 0.0;

                for (var i = 0; i < x.Count; ++i)
                {
                    w1 += w[i];
                    var tmpx = w[i] * x[i];
                    wx += tmpx;
                    tmpx *= x[i];
                    wx2 += tmpx;
                    tmpx *= x[i];
                    wx3 += tmpx;
                    tmpx *= x[i];
                    wx4 += tmpx;
                    var tmpy = w[i] * y[i];
                    wy += tmpy;
                    tmpy *= x[i];
                    wyx += tmpy;
                    tmpy *= x[i];
                    wyx2 += tmpy;
                }

                var den = wx2 * wx2 * wx2 - 2.0 * wx3 * wx2 * wx + wx4 * wx * wx + wx3 * wx3 * w1 - wx4 * wx2 * w1;
                if (Comparison.IsEqual(den, 0.0))
                    return;
                a = (wx * wx * wyx2 - wx2 * w1 * wyx2 - wx2 * wx * wyx + wx3 * w1 * wyx + wx2 * wx2 * wy -
                     wx3 * wx * wy) / den;
                b = (-wx2 * wx * wyx2 + wx3 * w1 * wyx2 + wx2 * wx2 * wyx - wx4 * w1 * wyx - wx3 * wx2 * wy +
                     wx4 * wx * wy) / den;
                c = (wx2 * wx2 * wyx2 - wx3 * wx * wyx2 - wx3 * wx2 * wyx + wx4 * wx * wyx + wx3 * wx3 * wy -
                     wx4 * wx2 * wy) / den;
            }
        }
    }
}