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
using System.Linq;
using Math.Interfaces;

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
                return x.Sum() / x.Count;
            }

            public static double Variance(List<double> x)
            {
                if (x.Count == 0)
                    return double.NaN;
                if (x.Count == 1)
                    return 0.0;
                var u = Mean(x);
                return x.Sum(xi => (xi - u) * (xi - u)) / x.Count;
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
                return x.Select((t, i) => w[i] * (t - u) * (t - u)).Sum() / w.Sum();
            }

            public static List<double> CenteredMovingAverage(List<double> x, double size)
            {
                var res = new List<double>(x);
                if (x.Count < 2 || Comparison.IsLessEqual(size, 1.0))
                    return res;
                var border = size / 2.0 - 0.5;
                var n = (int) System.Math.Floor(border);
                var rest = border - n;
                for (var i = 0; i < x.Count; i++)
                {
                    var l = 0.0;
                    var sum = 0.0;
                    if (Comparison.IsLess(0.0, rest))
                    {
                        if (0 <= i - n - 1)
                        {
                            sum += x[i - n - 1] * rest;
                            l += rest;
                        }

                        if (i + n + 1 < x.Count)
                        {
                            sum += x[i + n + 1] * rest;
                            l += rest;
                        }
                    }

                    for (var j = System.Math.Max(i - n, 0); j < System.Math.Min(i + n + 1, x.Count); j++)
                    {
                        sum += x[j];
                        l++;
                    }

                    res[i] = sum / l;
                }

                return res;
            }

            public static List<double> CenteredMovingAverage(List<double> x, double size, List<double> w)
            {
                var res = new List<double>(x);
                if (x.Count < 2)
                    return res;
                for (var i = 0; i < x.Count; i++)
                {
                    if (Comparison.IsLessEqual(size, w[i]))
                        continue;
                    var border = (size - w[i]) / 2.0;
                    var l = w[i];
                    var sum = w[i] * x[i];
                    var a = 0.0;
                    var j = i + 1;
                    while (j < x.Count && Comparison.IsLess(a, border))
                    {
                        var h = System.Math.Min(a + w[j], border) - a;
                        sum += h * x[j];
                        l += h;
                        a += w[j];
                        j++;
                    }

                    a = 0.0;
                    j = i - 1;
                    while (0 <= j && Comparison.IsLess(a, border))
                    {
                        var h = System.Math.Min(a + w[j], border) - a;
                        sum += h * x[j];
                        l += h;
                        a += w[j];
                        j--;
                    }

                    res[i] = sum / l;
                }

                return res;
            }

            //
            // https://april.eecs.umich.edu/pdfs/olson2011orientation.pdf
            //
            public static double MeanAngle(IList<double> angles)
            {
                if (angles.Count == 0)
                    return double.NaN;
                if (angles.Count == 1)
                    return Function.NormalizeAngle(angles[0]);

                var normalized = new List<double>();
                var squareSum = 0.0;
                var sum = 0.0;
                foreach (var angle in angles)
                {
                    var a = Function.NormalizeAngle(angle);
                    normalized.Add(a);
                    sum += a;
                    squareSum += a * a;
                }

                normalized = normalized.OrderBy(num => num).ToList();
                var s = sum;
                var variance = squareSum - sum * sum / normalized.Count;

                for (var i = 0; i < normalized.Count - 1; i++)
                {
                    sum += 2.0 * System.Math.PI;
                    squareSum += 4.0 * System.Math.PI * (normalized[i] + System.Math.PI);
                    var x = squareSum - sum * sum / normalized.Count;
                    if (Comparison.IsLess(x, variance))
                    {
                        variance = x;
                        s = sum;
                    }
                }

                return Function.NormalizeAngle(s / normalized.Count);
            }

            public static double MeanAngle(IList<Vector2D> list)
            {
                return MeanAngle(list, Vector2D.E1);
            }

            public static double MeanAngle<T>(IList<T> list, T axis) where T : IVector<T>
            {
                var angles = (from v in list where Comparison.IsLess(0.0, v.Norm2()) select axis.Angle(v)).ToList();
                return MeanAngle(angles);
            }
        }
    }
}