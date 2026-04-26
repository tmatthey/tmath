/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2025 Thierry Matthey
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

        /// <summary>
        /// Default relative tolerance for <see cref="IsEqualRelative(double,double,double)"/> /
        /// <see cref="IsZeroRelative(double,double,double)"/>. Chosen ~3 orders of magnitude
        /// looser than <see cref="Epsilon"/> so that the absolute tolerance at unit scale
        /// (1.0) stays at 1e-13 while large-magnitude values automatically get a proportional
        /// tolerance (e.g. 1e-7 m at Earth-radius scale 6.37e6 m).
        /// </summary>
        public const double RelativeEpsilon = 1e-13;

        public static bool IsEqual(double x, double y, double eps = Epsilon)
        {
            return System.Math.Abs(x - y) < eps ||
                   double.IsNegativeInfinity(x) && double.IsNegativeInfinity(y) ||
                   double.IsPositiveInfinity(x) && double.IsPositiveInfinity(y);
        }

        /// <summary>
        /// Epsilon-tolerant equality scaled by the larger operand magnitude. Equivalent to
        /// <c>|x - y| &lt; relEps * max(|x|, |y|, 1)</c>: callers do not have to pre-scale
        /// their epsilon when comparing two large numbers (e.g. metres at Earth-radius
        /// scale). The unit-scale baseline (the trailing <c>1</c>) keeps behaviour close to
        /// the absolute <see cref="IsEqual(double,double,double)"/> for small operands.
        /// Infinities of the same sign compare equal, just like <see cref="IsEqual(double,double,double)"/>.
        /// </summary>
        public static bool IsEqualRelative(double x, double y, double relEps = RelativeEpsilon)
        {
            if (double.IsNegativeInfinity(x) && double.IsNegativeInfinity(y)) return true;
            if (double.IsPositiveInfinity(x) && double.IsPositiveInfinity(y)) return true;
            var scale = System.Math.Max(1.0, System.Math.Max(System.Math.Abs(x), System.Math.Abs(y)));
            return System.Math.Abs(x - y) < relEps * scale;
        }

        /// <summary>
        /// Relative-tolerance "is zero" with an explicit reference scale. Returns true iff
        /// <c>|x| &lt; relEps * max(|scale|, 1)</c>, i.e. <paramref name="x"/> is small
        /// compared to <paramref name="scale"/>. Useful for testing residuals of length-scaled
        /// quantities (e.g. metres) where the absolute <see cref="Epsilon"/> 1e-13 would be
        /// meaninglessly tight.
        /// </summary>
        public static bool IsZeroRelative(double x, double scale, double relEps = RelativeEpsilon)
        {
            var s = System.Math.Max(1.0, System.Math.Abs(scale));
            return System.Math.Abs(x) < relEps * s;
        }

        public static bool IsNumber(double x)
        {
            return !(double.IsNaN(x) || double.IsInfinity(x));
        }

        public static bool IsZero(double x, double eps = Epsilon)
        {
            return -eps <= x && x <= eps;
        }

        /// <summary>
        /// Returns true iff <paramref name="x"/> is a finite value strictly greater than <paramref name="eps"/>.
        /// </summary>
        /// <remarks>
        /// Returns false for <see cref="double.NaN"/>, <see cref="double.PositiveInfinity"/>,
        /// <see cref="double.NegativeInfinity"/>, zero, and any value within +/- <paramref name="eps"/> of zero.
        /// Infinity is intentionally excluded so that "positive" implies "finite numeric magnitude
        /// the rest of the library can safely arithmetic on" - callers that genuinely want to admit
        /// +Infinity should test it explicitly with <see cref="double.IsPositiveInfinity(double)"/>.
        /// </remarks>
        public static bool IsPositive(double x, double eps = Epsilon)
        {
            return eps < x && x < double.PositiveInfinity;
        }

        /// <summary>
        /// Returns true iff <paramref name="x"/> is a finite value strictly less than -<paramref name="eps"/>.
        /// </summary>
        /// <remarks>
        /// Returns false for <see cref="double.NaN"/>, <see cref="double.PositiveInfinity"/>,
        /// <see cref="double.NegativeInfinity"/>, zero, and any value within +/- <paramref name="eps"/> of zero.
        /// Infinity is intentionally excluded; see <see cref="IsPositive(double,double)"/>.
        /// </remarks>
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

        /// <summary>
        /// Returns a hash code for <paramref name="x"/> that is consistent with epsilon-tolerant <see cref="IsEqual(double,double,double)"/>:
        /// values within the snap granularity hash to the same bucket. Granularity is chosen much coarser
        /// than <see cref="Epsilon"/> so that any two values reported equal by <see cref="IsEqual(double,double,double)"/>
        /// in the common case produce the same hash, satisfying the GetHashCode contract.
        /// </summary>
        public static int HashCode(double x)
        {
            if (double.IsNaN(x)) return 0;
            if (double.IsPositiveInfinity(x)) return int.MaxValue;
            if (double.IsNegativeInfinity(x)) return int.MinValue;
            // Snap to a grid 3 orders of magnitude coarser than Epsilon (1e-10 vs 1e-13).
            const double scale = 1e10;
            return ((long) System.Math.Round(x * scale)).GetHashCode();
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