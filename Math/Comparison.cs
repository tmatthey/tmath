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

using System.Collections.Generic;
using System.Linq;

namespace Math
{
    public static class Comparison
    {
        public const double Epsilon = 1e-13; //double.Epsilon;

        public static bool IsEqual(double x, double y, double eps = Epsilon)
        {
            return System.Math.Abs(x - y) < eps ||
                   double.IsNegativeInfinity(x) && double.IsNegativeInfinity(y) ||
                   double.IsPositiveInfinity(x) && double.IsPositiveInfinity(y);
        }

        public static bool IsNumber(double x)
        {
            return !(double.IsNaN(x) || double.IsInfinity(x));
        }

        public static bool IsZero(double x, double eps = Epsilon)
        {
            return -eps <= x && x <= eps;
        }

        public static bool IsPositive(double x, double eps = Epsilon)
        {
            return eps < x && x < double.PositiveInfinity;
        }

        public static bool IsNegative(double x, double eps = Epsilon)
        {
            return double.NegativeInfinity < x && x < -eps;
        }

        public static bool IsLessEqual(double x, double y, double eps = Epsilon)
        {
            return IsEqual(x, y, eps) || x <= y;
        }

        public static bool IsLess(double x, double y, double eps = Epsilon)
        {
            return x <= y && !IsEqual(x, y, eps);
        }

        public static IList<double> UniqueAverageSorted(IList<double> v, double eps = Epsilon)
        {
            var vTmp = new List<double>(v);
            vTmp.Sort();
            var res = new List<double>();
            var tmp = new List<double>();
            for (var i = 0; i < vTmp.Count;)
            {
                if (i + 1 < vTmp.Count && IsEqual(vTmp[i], vTmp[i + 1], eps * 2.0))
                {
                    if (tmp.Count == 0)
                    {
                        tmp.Add(vTmp[i]);
                    }

                    tmp.Add(vTmp[i + 1]);
                    vTmp.RemoveAt(i + 1);
                }
                else if (tmp.Count > 0)
                {
                    res.Add(tmp.Sum() / tmp.Count);
                    tmp.Clear();
                    i++;
                }
                else
                {
                    res.Add(vTmp[i]);
                    i++;
                }
            }

            return res;
        }
    }
}