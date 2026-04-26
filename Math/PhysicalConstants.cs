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
    /// <summary>
    /// Centralized physical constants used by the geodesy / dynamics models.
    /// </summary>
    public static class PhysicalConstants
    {
        /// <summary>Standard gravity, m/s^2 (CGPM definition).</summary>
        public const double GravitationalAcceleration = 9.80665;

        /// <summary>Air density at 15 C, sea level, dry-air ISA model, kg/m^3.</summary>
        public const double AirDensitySeaLevel = 1.22601;
    }

    /// <summary>
    /// Sensible defaults for the cycling power/velocity model. The values are sourced from
    /// gribble.org's analysis (https://www.gribble.org/cycling/power_v_speed.html) and match
    /// the historical literal arguments of <see cref="Function.CyclingForces"/> /
    /// <see cref="Function.CyclingPowers"/> / <see cref="Function.CyclingVelocity"/>. They
    /// are exposed here so callers can refer to them by name instead of repeating literals
    /// at every call site.
    /// </summary>
    public static class CyclistDefaults
    {
        /// <summary>Coefficient of rolling resistance (Crr) for a road bike on tarmac.</summary>
        public const double RollingResistance = 0.005;

        /// <summary>Frontal area, m^2 (drops/hoods average).</summary>
        public const double FrontalArea = 0.509;

        /// <summary>Aerodynamic drag coefficient, dimensionless.</summary>
        public const double DragCoefficient = 0.63;

        /// <summary>Mechanical loss across the drive train (chain + bearings), dimensionless.</summary>
        public const double DriveTrainLoss = 0.02;

        /// <summary>Air density default - alias for <see cref="PhysicalConstants.AirDensitySeaLevel"/>.</summary>
        public const double AirDensity = PhysicalConstants.AirDensitySeaLevel;
    }
}
