/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2026 Thierry Matthey
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
        // https://www.gribble.org/cycling/power_v_speed.html
        public static (double gravity, double rolling, double drag, double equilibriumVelocity) CyclingForces(
            double velocity, double riderWeight, double bikeWeight,
            double gradient = 0.0, double headWind = 0.0,
            double rollingResistance = CyclistDefaults.RollingResistance,
            double airDensity = CyclistDefaults.AirDensity,
            double area = CyclistDefaults.FrontalArea,
            double dragCoefficient = CyclistDefaults.DragCoefficient)
        {
            var gravity = GravitationalAcceleration * (riderWeight + bikeWeight) *
                          System.Math.Sin(System.Math.Atan(gradient));

            var rolling = GravitationalAcceleration * (riderWeight + bikeWeight) *
                          System.Math.Cos(System.Math.Atan(gradient)) * rollingResistance;
            if (velocity < 0)
            {
                rolling *= -1.0;
            }

            var dragFac = 0.5 * area * dragCoefficient * airDensity;
            var drag = dragFac * (velocity + headWind) * (velocity + headWind);
            if (velocity + headWind < 0)
            {
                drag *= -1.0;
            }

            return (gravity, rolling, drag, System.Math.Sqrt(-(rolling + gravity) / dragFac) - headWind);
        }

        public static (double power, double legPower, double wheelPower, double driveTrainLoss, double brakingPower,
            double equilibriumVelocity) CyclingPowers(double velocity, double riderWeight, double bikeWeight,
                double gradient = 0.0, double headWind = 0.0,
                double rollingResistance = CyclistDefaults.RollingResistance,
                double airDensity = CyclistDefaults.AirDensity,
                double area = CyclistDefaults.FrontalArea,
                double dragCoefficient = CyclistDefaults.DragCoefficient,
                double driveTrainLossFactor = CyclistDefaults.DriveTrainLoss)
        {
            var (gravityForce, rollingForce, dragForce, equilibriumVelocity) = CyclingForces(velocity, riderWeight,
                bikeWeight, gradient, headWind, rollingResistance, airDensity, area, dragCoefficient);
            var totalForce = gravityForce + rollingForce + dragForce;

            var wheelPower = totalForce * velocity;

            var driveTrainFrac = 1.0;
            if (wheelPower >= 0.0)
            {
                driveTrainFrac -= driveTrainLossFactor;
            }

            var legPower = wheelPower / driveTrainFrac;

            double driveTrainLossPower, brakingPower, power;
            if (legPower >= 0.0)
            {
                driveTrainLossPower = legPower - wheelPower;
                brakingPower = 0.0;
                power = legPower;
            }
            else
            {
                brakingPower = legPower * -1.0;
                legPower = 0.0;
                wheelPower = 0.0;
                driveTrainLossPower = 0.0;
                power = -brakingPower;
            }

            return (power, legPower, wheelPower, driveTrainLossPower, brakingPower, equilibriumVelocity);
        }

        public static double CyclingVelocity(double power, double riderWeight, double bikeWeight,
            double gradient = 0.0, double headWind = 0.0,
            double rollingResistance = CyclistDefaults.RollingResistance,
            double airDensity = CyclistDefaults.AirDensity,
            double area = CyclistDefaults.FrontalArea,
            double dragCoefficient = CyclistDefaults.DragCoefficient,
            double driveTrainLoss = CyclistDefaults.DriveTrainLoss)
        {
            const double epsilon = 0.000001;
            var low = -50.0;
            var high = 50.0;
            var v = CyclingPowers(0, riderWeight, bikeWeight, gradient, headWind, rollingResistance, airDensity, area,
                dragCoefficient, driveTrainLoss).equilibriumVelocity;

            if (!double.IsNaN(v))
            {
                if (Comparison.IsZero(power))
                {
                    return v;
                }

                if (power < 0)
                {
                    high = v;
                    low = 0.0;
                }
                else
                {
                    low = v;
                }

                v = (high + low) / 2.0;
            }
            else
            {
                v = 0.0;
            }

            var p = CyclingPowers(v, riderWeight, bikeWeight, gradient, headWind, rollingResistance, airDensity, area,
                dragCoefficient, driveTrainLoss).power;
            var n = 0;
            do
            {
                if (System.Math.Abs(p - power) < epsilon)
                    break;

                if (p > power)
                    high = v;
                else
                    low = v;

                v = (high + low) / 2.0;
                p = CyclingPowers(v, riderWeight, bikeWeight, gradient, headWind, rollingResistance, airDensity, area,
                    dragCoefficient, driveTrainLoss).power;
            } while (n++ < 100);

            return v;
        }
    }
}
