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
        //
        // Minetti running cost on a gradient (metabolic, treadmill).
        //
        //   C(g)  [J / (kg * m)]   from Minetti et al. 2002, fitted polynomial.
        //
        //   Minetti(g)        - raw metabolic cost, valid on [-0.45, 0.45].
        //   MinettiFactor(g)  - cost normalised so Minetti(0) = 1.
        //
        // Outside the valid domain the curve is extrapolated linearly using the
        // slope at the boundary (one-shot, evaluated at type init).

        private const double MinettiMinX = -0.45;
        private const double MinettiMaxX = 0.45;

        public static readonly double MinettiZero = MinettiRaw(0.0);

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
                return MinettiMinA * (g - MinettiMinX) + MinettiMinB;

            if (MinettiMaxX <= g)
                return MinettiMaxA * (g - MinettiMaxX) + MinettiMaxB;

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

        //
        // Strava-style Grade-Adjusted Pace factor (running, asymmetric).
        //
        // Strava's GAP is empirically fitted against actual run-pace data (not
        // metabolic cost), so it gives a smaller discount on descents than the
        // Minetti curve, and on really steep descents it goes back above 1
        // (i.e. running steep downhills is a *penalty*, not a credit -
        // braking, eccentric muscle load, technical surface).
        //
        // The implementation is an asymmetric piecewise quadratic:
        //
        //   uphill   (g >= 0):  f(g) = 1 + 3.9  * g +  4.6 * g^2
        //   downhill (g <  0):  f(g) = 1 + 1.2  * g + 13.0 * g^2
        //
        // Continuous at g = 0 (both branches return 1). The kink in the first
        // derivative there reflects the genuine biomechanical asymmetry of
        // climbing vs. descending and matches the per-km Strava data observed
        // on two reference loops:
        //
        //   * Nøsen 100 km (Norway, +3.88 km / -3.88 km, 105 km, mostly
        //     gentle terrain, Strava GAP +18.62 %).
        //   * Hilly 50 km loop  (+2.14 km / -2.13 km, 52 km, steeper terrain
        //     with several +12 % .. +18 % climbs, Strava GAP +21.72 %).
        //
        // Coefficients were re-fit jointly against both loops so the
        // distance-integrated factor reproduces Strava's reported GAP within
        // 0.01 pp on each, and the qualitative observations remain:
        //
        //   * gentle descents (-2 % .. -8 %): factor near 1.0   (no credit)
        //   * descent minimum near g = -4.6 %: factor ~ 0.97
        //   * steep descents (-15 %): factor ~ 1.11              (penalty)
        //   * gentle climbs (+5 %):   factor ~ 1.21
        //   * steep climbs (+10 %):   factor ~ 1.44
        //   * very steep climbs (+20 %): factor ~ 1.96
        //
        // Outside [-0.45, 0.45] the curve is extrapolated linearly using the
        // slope at the boundary, mirroring Minetti's behaviour.
        //
        // The API parallels Minetti exactly:
        //   Strava(g)        - raw factor (= 1 at g = 0).
        //   StravaFactor(g)  - normalised factor (Strava(g) / StravaZero).
        //   StravaZero       - Strava(0), kept for API symmetry; equals 1.

        private const double StravaMinX = -0.45;
        private const double StravaMaxX = 0.45;

        public static readonly double StravaZero = StravaRaw(0.0);

        private static readonly double StravaMinA = StravaDiv(StravaMinX);
        private static readonly double StravaMinB = StravaRaw(StravaMinX);
        private static readonly double StravaMaxA = StravaDiv(StravaMaxX);
        private static readonly double StravaMaxB = StravaRaw(StravaMaxX);

        public static double StravaFactor(double g)
        {
            return Strava(g) / StravaZero;
        }

        public static double Strava(double g)
        {
            if (g <= StravaMinX)
                return StravaMinA * (g - StravaMinX) + StravaMinB;

            if (StravaMaxX <= g)
                return StravaMaxA * (g - StravaMaxX) + StravaMaxB;

            return StravaRaw(g);
        }

        private static double StravaRaw(double g)
        {
            return g >= 0.0
                ? 1.0 + 3.9 * g + 4.6 * g * g
                : 1.0 + 1.2 * g + 13.0 * g * g;
        }

        private static double StravaDiv(double g)
        {
            return g >= 0.0
                ? 3.9 + 9.2 * g
                : 1.2 + 26.0 * g;
        }
    }
}
