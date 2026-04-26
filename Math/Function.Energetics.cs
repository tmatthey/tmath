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
        private const double MinettiMinX = -0.45;
        private const double MinettiMaxX = 0.45;

        public static readonly double MinettiZero = MinettiRaw(0.0);

        // Minetti piecewise-linear extrapolation outside [MinettiMinX, MinettiMaxX] uses
        // the slope of the polynomial at the boundary, evaluated once.
        private static readonly double MinettiMinA = MinettiDiv(MinettiMinX);
        private static readonly double MinettiMinB = MinettiRaw(MinettiMinX);
        private static readonly double MinettiMaxA = MinettiDiv(MinettiMaxX);
        private static readonly double MinettiMaxB = MinettiRaw(MinettiMaxX);

        public static double MinettiFactor(double g)
        {
            return Minetti(g) / MinettiZero;
        }

        public static double Minetti(double g)
        {
            if (g <= MinettiMinX)
            {
                return MinettiMinA * (g - MinettiMinX) + MinettiMinB;
            }

            if (MinettiMaxX <= g)
            {
                return MinettiMaxA * (g - MinettiMaxX) + MinettiMaxB;
            }

            return MinettiRaw(g);
        }

        private static double MinettiRaw(double g)
        {
            return 106.7731478 * g * g * g * g * g - 47.23550515 * g * g * g * g - 33.40634794 * g * g * g +
                   49.35038999 * g * g + 19.12318478 * g + 3.389064903;
            // return 155.4*g*g*g*g*g - 30.4*g*g*g*g - 43.3*g*g*g + 46.3*g*g + 19.5*g + 3.6;
        }

        private static double MinettiDiv(double g)
        {
            return 106.7731478 * g * g * g * g * 5.0 - 47.23550515 * g * g * g * 4.0 - 33.40634794 * g * g * 3.0 +
                   49.35038999 * g * 2.0 + 19.12318478;
        }
    }
}
