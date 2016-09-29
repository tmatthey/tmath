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
    public static class Statistics
    {
        public static class Arithmetic
        {
            public static double Mean(List<double> x)
            {
                if (x.Count == 0)
                    return double.NaN;
                if (x.Count == 1)
                    return x[0];
                return x.Sum()/x.Count;
            }

            public static double Variance(List<double> x)
            {
                if (x.Count == 0)
                    return double.NaN;
                if (x.Count == 1)
                    return 0.0;
                var u = Mean(x);
                return x.Sum(xi => (xi - u)*(xi - u))/x.Count;
            }

            public static double Mean(List<double> x, List<double> w)
            {
                if (x.Count == 0)
                    return double.NaN;
                if (x.Count == 1)
                    return x[0];
                return x.Select((t, i) => t * w[i]).Sum() / w.Sum();
            }

            public static double Variance(List<double> x, List<double> w)
            {
                if (x.Count == 0)
                    return double.NaN;
                if (x.Count == 1)
                    return 0.0;
                var u = Mean(x, w);
                return x.Select((t, i) => w[i]*(t - u)*(t - u)).Sum() / w.Sum();
            }
        }
    }
}