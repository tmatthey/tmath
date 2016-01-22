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
    public class Solver
    {
        public static double LinearEq(double a, double b)
        {
            if (Comparison.IsZero(a))
            {
                return double.NaN;
            }
            return -b / a;
        }

        public static IList<double> QuadraticEq(double a, double b, double c)
        {
            var res = new List<double>();

            // Linar case
            if (Comparison.IsZero(a))
            {
                var x = LinearEq(b, c);
                if (!double.IsNaN(x))
                {
                    res.Add(x);
                }
                return res;
            }

            // Normal form: x^2 + px + q = 0
            var p = b / (2.0 * a);
            var q = c / a;

            var D = p * p - q;
            if (Comparison.IsZero(D))
            {
                res.Add(-p);
            }
            else if (Comparison.IsPositive(D))
            {
                var s = System.Math.Sqrt(D);
                res.Add(s - p);
                res.Add(-s - p);
                res = Comparison.UniqueAverageSorted(res).ToList();
            }
            return res;
        }

        public static IList<double> CubicEq(double a, double b, double c, double d)
        {
            if (Comparison.IsZero(a))
            {
                return QuadraticEq(b, c, d);
            }
            var res = new List<double>();

            // Depressed cubic form: x^3 + px + q = 0
            var p = 1.0 / 3.0 * (-1.0 / 3.0 * b / a * b / a + c / a);
            var q = 1.0 / 2 * (2.0 / 27.0 * b / a * b / a * b / a - 1.0 / 3.0 * b / a * c / a + d / a);
            var D = q * q + p * p * p;
            if (Comparison.IsZero(D))
            {
                if (Comparison.IsZero(q))
                {
                    // One triple solution
                    res.Add(0.0);
                }
                else
                {
                    // One single and double solution
                    double u = Function.Cbrt(-q);
                    res.Add(2 * u);
                    res.Add(-u);
                }
            }
            else if (Comparison.IsNegative(D))
            {
                // Three single solutions
                var phi = 1.0 / 3.0 * System.Math.Acos(-q / System.Math.Sqrt(-p * p * p));
                var t = 2.0 * System.Math.Sqrt(-p);
                res.Add(t * System.Math.Cos(phi));
                res.Add(-t * System.Math.Cos(phi + System.Math.PI / 3.0));
                res.Add(-t * System.Math.Cos(phi - System.Math.PI / 3.0));
            }
            else
            {
                // One single solution
                var s = System.Math.Sqrt(D);
                var u = Function.Cbrt(s - q);
                var v = -Function.Cbrt(s + q);
                res.Add(u + v);
            }

            // Resubstitute
            for (var i = 0; i < res.Count; ++i)
            {
                res[i] -= b / (3.0 * a);
            }
            res = Comparison.UniqueAverageSorted(res).ToList();
            return res;
        }

        public static IList<double> QuarticEq(double a, double b, double c, double d, double e)
        {
            if (Comparison.IsZero(a))
            {
                return CubicEq(b, c, d, e);
            }

            //	Reduced form: x^4 + px^2 + qx + r = 0
            var p = -3.0 / 8 * b / a * b / a + c / a;
            var q = 1.0 / 8 * b / a * b / a * b / a - 1.0 / 2 * b / a * c / a + d / a;
            var r = -3.0 / 256 * b / a * b / a * b / a * b / a + 1.0 / 16 * b / a * b / a * c / a - 1.0 / 4 * b / a * d / a + e / a;

            var res = new List<double>();
            if (Comparison.IsZero(r))
            {
                //  y(y^3 + py + q) = 0
                res.AddRange(CubicEq(1.0, 0.0, p, q));
                res.Add(0.0);
            }
            else
            {
                var z = CubicEq(1.0, -1.0 / 2.0 * p, -r, 1.0 / 2.0 * r * p - 1.0 / 8 * q * q)[0];
                var u = z * z - r;
                var v = 2 * z - p;
                if (Comparison.IsZero(u)) u = 0.0;
                if (Comparison.IsZero(v)) v = 0.0;
                if (u >= 0.0 && v >= 0.0)
                {
                    u = System.Math.Sqrt(u);
                    v = System.Math.Sqrt(v);
                    res.AddRange(QuadraticEq(1.0, q < 0 ? -v : v, z - u));
                    res.AddRange(QuadraticEq(1.0, q < 0 ? v : -v, z + u));
                }
            }

            // Resubstitute
            for (var i = 0; i < res.Count; ++i)
            {
                res[i] -= b / (4.0 * a);
            }
            res = Comparison.UniqueAverageSorted(res).ToList();
            return res;
        }
    }
}