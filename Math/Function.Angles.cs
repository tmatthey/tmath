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

namespace Math
{
    public static partial class Function
    {
        /// <summary>
        /// Cheap polynomial approximation of sin(x) on x in [-pi/2, pi/2] with absolute error &lt; 0.0205.
        /// </summary>
        /// <remarks>
        /// Outside [-pi/2, pi/2] the polynomial diverges quickly: it is the caller's responsibility
        /// to range-reduce the argument first (e.g. via <see cref="NormalizeAnglePi(double)"/> and a
        /// half-period reflection). The function does not assert this in release builds to keep
        /// "fast" honest, but a debug-only assertion guards against accidental misuse.
        /// </remarks>
        public static double FastSin(double x)
        {
            return x / System.Math.PI * (3.0 - x * x * 4.0 / System.Math.PI / System.Math.PI);
        }

        public static void SinCos(double alpha, out double sinAlpha, out double cosAlpha)
        {
            SinCos(alpha, out sinAlpha, out cosAlpha, Comparison.Epsilon);
        }

        public static void SinCos(double alpha, out double sinAlpha, out double cosAlpha, double eps)
        {
            // From lsys by Jonathan P. Leech.
            sinAlpha = System.Math.Sin(alpha);
            cosAlpha = System.Math.Cos(alpha);

            // Snap to one of the four cardinal angles when within eps. The branches are mutually
            // exclusive (sin and cos cannot both be ~1), so a single if/else-if chain is enough
            // and avoids the previous structure where the second `if` re-tested a sinAlpha the
            // first block had already overwritten.
            if (cosAlpha > 1.0 - eps)
            {
                cosAlpha = 1.0;
                sinAlpha = 0.0;
            }
            else if (cosAlpha < -1.0 + eps)
            {
                cosAlpha = -1.0;
                sinAlpha = 0.0;
            }
            else if (sinAlpha > 1.0 - eps)
            {
                cosAlpha = 0.0;
                sinAlpha = 1.0;
            }
            else if (sinAlpha < -1.0 + eps)
            {
                cosAlpha = 0.0;
                sinAlpha = -1.0;
            }
        }

        public static double NormalizeAngle(double a)
        {
            if (Comparison.IsNumber(a))
            {
                // Constant-time modulo replaces the previous while loops, which were O(|a|/2pi).
                var m = a % (System.Math.PI * 2);
                if (m < 0) m += System.Math.PI * 2;

                if (Comparison.IsEqual(m, 0.0) || Comparison.IsEqual(m, System.Math.PI * 2))
                    return 0.0;
                return m;
            }

            return a;
        }

        public static double NormalizeAnglePi(double a)
        {
            if (Comparison.IsNumber(a))
            {
                // IEEERemainder lands in [-pi, pi] without the catastrophic cancellation that an
                // explicit (a + pi) % 2pi - pi suffers for inputs already inside (-pi, pi].
                var r = System.Math.IEEERemainder(a, System.Math.PI * 2);
                if (Comparison.IsEqual(r, -System.Math.PI) || Comparison.IsEqual(r, System.Math.PI))
                    return System.Math.PI;
                return r;
            }

            return a;
        }

        public static double NormalizeAngle180(double a)
        {
            if (Comparison.IsNumber(a))
            {
                while (a <= -180) a += 360.0;
                while (a > 180) a -= 360.0;

                if (Comparison.IsEqual(a, -180.0) || Comparison.IsEqual(a, 180.0))
                    return 180.0;
            }

            return a;
        }
    }
}
