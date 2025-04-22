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
using Math.Interfaces;

namespace Math
{
    public static class Function
    {
        private const double MinettiMinX = -0.45;
        private const double MinettiMaxX = 0.45f;
        public static readonly int MaxFactorialInt = 20;
        public static readonly int MaxFibonacciInt = 93;
        private static readonly List<long> PrimesUpTo30 = new List<long> { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29 };
        public const double GravitationalAcceleration = 9.80665;
        public static readonly double MinettiZero = MinettiRaw(0.0);
        private static readonly double MinettiMinA = MinettiDiv(MinettiMinX);
        private static readonly double MinettiMinB = MinettiRaw(MinettiMinX);
        private static readonly double MinettiMaxA = MinettiDiv(MinettiMaxX);
        private static readonly double MinettiMaxB = MinettiRaw(MinettiMaxX);

        public static double Cbrt(double x)
        {
            return Root(x, 3);
        }

        public static double Qnrt(double x)
        {
            return Root(x, 5);
        }

        // Fast sin(x) [-PI/2, -PI/2] with absolute error < 0.0205
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

            if (sinAlpha > 1.0 - eps)
            {
                cosAlpha = 0.0;
                sinAlpha = 1.0;
            }
            else if (sinAlpha < -1 + eps)
            {
                cosAlpha = 0.0;
                sinAlpha = -1.0;
            }
        }

        public static double NormalizeAngle(double a)
        {
            if (Comparison.IsNumber(a))
            {
                while (a < 0) a += System.Math.PI * 2;
                while (a >= System.Math.PI * 2) a -= System.Math.PI * 2;

                if (Comparison.IsEqual(a, 0.0) || Comparison.IsEqual(a, System.Math.PI * 2))
                    return 0.0;
            }

            return a;
        }

        public static double NormalizeAnglePi(double a)
        {
            if (Comparison.IsNumber(a))
            {
                while (a <= -System.Math.PI) a += System.Math.PI * 2;
                while (a > System.Math.PI) a -= System.Math.PI * 2;

                if (Comparison.IsEqual(a, -System.Math.PI) || Comparison.IsEqual(a, System.Math.PI))
                    return System.Math.PI;
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

        public static ulong FactorialInt(int n)
        {
            if (n > MaxFactorialInt)
            {
                return 0ul;
            }

            ulong p = 1;
            for (var i = 1; i <= n; i++)
            {
                p *= (ulong)i;
            }

            return p;
        }

        public static double Factorial(int n)
        {
            double p = 1;
            for (var i = 1; i <= n; i++)
            {
                p *= i;
            }

            return p;
        }

        public static ulong FibonacciInt(int n)
        {
            if (n > MaxFibonacciInt)
            {
                return 0ul;
            }

            ulong x = 0;
            ulong y = 1;
            ulong z = 1;
            for (var i = 0; i < n; i++)
            {
                x = y;
                y = z;
                z = x + y;
            }

            return x;
        }

        public static double Fibonacci(int n)
        {
            var x = 0.0;
            var y = 1.0;
            var z = 1.0;
            for (var i = 0; i < n; i++)
            {
                x = y;
                y = z;
                z = x + y;
            }

            return x;
        }

        //
        // https://en.wikipedia.org/wiki/Fibonacci_number#Recognizing_Fibonacci_numbers
        //
        public static double FibonacciBinet(int n)
        {
            var sqrt5 = System.Math.Sqrt(5.0);
            var phi = (1 + sqrt5) / 2;
            return System.Math.Floor((System.Math.Pow(phi, n) - System.Math.Pow(-phi, -n)) / sqrt5);
        }

        public static int GCD(int a, int b)
        {
            // Greatest Common Denominator: Euclidean Algorithm
            return b == 0 ? a : GCD(b, a % b);
        }

        public static bool IsPrime(long n)
        {
            if (n < 2) return false;
            foreach (var p in PrimesUpTo30)
            {
                if (n == p) return true;
                if (n % p == 0) return false;
            }

            var nsq = (long)System.Math.Sqrt(n) + 1;
            for (long i = 30; i < nsq; i += 30)
            {
                if (n % (i + 1) == 0 ||
                    n % (i + 7) == 0 ||
                    n % (i + 11) == 0 ||
                    n % (i + 13) == 0 ||
                    n % (i + 17) == 0 ||
                    n % (i + 19) == 0 ||
                    n % (i + 23) == 0 ||
                    n % (i + 29) == 0)
                {
                    return false;
                }
            }

            return true;
        }

        private static double Root(double x, int n)
        {
            var y = x;
            if (Comparison.IsPositive(x))
            {
                y = System.Math.Pow(x, 1.0 / n);
            }
            else if (Comparison.IsNegative(x))
            {
                y = -System.Math.Pow(-x, 1.0 / n);
            }

            return y;
        }

        public static double Interpolate(double a, double x0, double x1)
        {
            return x0 * (1.0 - a) + x1 * a;
        }

        public static double Interpolate(double x, double x0, double x1, double y0, double y1)
        {
            var a = Comparison.IsEqual(x0, x1) ? 0.5 : (x - x0) / (x1 - x0);
            return Interpolate(a, y0, y1);
        }

        public static T Interpolate<T>(double a, T x0, T x1) where T : IInterpolate<T>
        {
            return x0.Interpolate(x1, a);
        }

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

        // https://www.gribble.org/cycling/power_v_speed.html
        public static (double gravity, double rolling, double drag, double equilibriumVelocity) CyclingForces(double velocity, double riderWeight, double bikeWeight,
            double gradient = 0.0, double headWind = 0.0, double rollingResistance = 0.005, double airDensity = 1.22601,
            double area = 0.509, double dragCoefficient = 0.63)
        {
            var gravity = GravitationalAcceleration * (riderWeight + bikeWeight) * System.Math.Sin(System.Math.Atan(gradient));

            var rolling = GravitationalAcceleration * (riderWeight + bikeWeight) * System.Math.Cos(System.Math.Atan(gradient)) * rollingResistance;
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

            return (gravity, rolling, drag,  System.Math.Sqrt(-(rolling + gravity) / dragFac) - headWind);
        }

        public static (double power, double legPower, double wheelPower, double driveTrainLoss, double brakingPower, double equilibriumVelocity) CyclingPowers(double velocity, double riderWeight, double bikeWeight,
            double gradient = 0.0, double headWind = 0.0, double rollingResistance = 0.005, double airDensity = 1.22601,
            double area = 0.509, double dragCoefficient = 0.63, double driveTrainLossFactor = 0.02)
        {
            var (gravityForce, rollingForce, dragForce, equilibriumVelocity) = CyclingForces(velocity, riderWeight, bikeWeight, gradient, headWind, rollingResistance, airDensity, area, dragCoefficient);
            var totalForce = gravityForce + rollingForce + dragForce;

            var wheelPower = totalForce * (velocity);

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
            double gradient = 0.0, double headWind = 0.0, double rollingResistance = 0.005, double airDensity = 1.22601,
            double area = 0.509, double dragCoefficient = 0.63, double driveTrainLoss = 0.02)
        {
            const double epsilon = 0.000001;
            var low = -50.0;
            var high = 50.0;
            var v = CyclingPowers(0, riderWeight, bikeWeight, gradient, headWind, rollingResistance, airDensity, area, dragCoefficient, driveTrainLoss).equilibriumVelocity;
            
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
            var p = CyclingPowers(v, riderWeight, bikeWeight, gradient, headWind, rollingResistance, airDensity, area, dragCoefficient, driveTrainLoss).power;
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
                p = CyclingPowers(v, riderWeight, bikeWeight, gradient, headWind, rollingResistance, airDensity, area, dragCoefficient, driveTrainLoss).power;
            } while (n++ < 100);

            return v;
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