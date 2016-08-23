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
    public static class Function
    {
        public static readonly int MaxFactorialInt = 20;
        public static readonly int MaxFibonacciInt = 93;
        private static readonly List<long> PrimesUpTo30 = new List<long> {2, 3, 5, 7, 11, 13, 17, 19, 23, 29};

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
            return x/System.Math.PI*(3.0 - x*x*4.0/System.Math.PI/System.Math.PI);
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
                while (a < 0) a += System.Math.PI*2;
                while (a >= System.Math.PI*2) a -= System.Math.PI*2;

                if (Comparison.IsEqual(a, 0.0) || Comparison.IsEqual(a, System.Math.PI*2))
                    return 0.0;
            }
            return a;
        }

        public static double NormalizeAnglePi(double a)
        {
            if (Comparison.IsNumber(a))
            {
                while (a <= -System.Math.PI) a += System.Math.PI*2;
                while (a > System.Math.PI) a -= System.Math.PI*2;

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

        //
        // https://april.eecs.umich.edu/pdfs/olson2011orientation.pdf
        //
        public static double AverageAngle(IList<double> angles)
        {
            if (angles.Count == 0)
                return double.NaN;
            if (angles.Count == 1)
                return NormalizeAngle(angles[0]);

            var normalized = new List<double>();
            var sqrSum = 0.0;
            var sum = 0.0;
            foreach (var angle in angles)
            {
                var a = NormalizeAngle(angle);
                normalized.Add(a);
                sum += a;
                sqrSum += a * a;
            }
            normalized = normalized.OrderBy(num => num).ToList();
            var average = sum / normalized.Count;
            var variance = sqrSum - sum * sum / normalized.Count;

            foreach (var a in normalized)
            {
                sum += 2.0 * System.Math.PI;
                sqrSum += 4.0 * System.Math.PI * (a + System.Math.PI);
                var x = sqrSum - sum * sum / normalized.Count;
                if (Comparison.IsLess(x, variance))
                {
                    variance = x;
                    average = sum / normalized.Count;
                }
            }


            return NormalizeAngle(average);
        }

        public static double AverageAngle(IList<Vector2D> list)
        {
            return AverageAngle(list, Vector2D.E1);
        }

        public static double AverageAngle(IList<Vector2D> list, Vector2D axis)
        {
            var angles = (from v in list where Comparison.IsLess(0.0, v.Norm2()) select axis.Angle(v)).ToList();
            return AverageAngle(angles);
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
                p *= (ulong) i;
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
            var phi = (1 + sqrt5)/2;
            return System.Math.Floor((System.Math.Pow(phi, n) - System.Math.Pow(-phi, -n))/sqrt5);
        }

        public static int GCD(int a, int b)
        {
            // Greatest Common Denominator: Euclidean Algorithm
            return b == 0 ? a : GCD(b, a%b);
        }

        public static bool IsPrime(long n)
        {
            if (n < 2) return false;
            foreach (var p in PrimesUpTo30)
            {
                if (n == p) return true;
                if (n%p == 0) return false;
            }
            var nsq = (long) System.Math.Sqrt(n) + 1;
            for (long i = 30; i < nsq; i += 30)
            {
                if (n%(i + 1) == 0 ||
                    n%(i + 7) == 0 ||
                    n%(i + 11) == 0 ||
                    n%(i + 13) == 0 ||
                    n%(i + 17) == 0 ||
                    n%(i + 19) == 0 ||
                    n%(i + 23) == 0 ||
                    n%(i + 29) == 0)
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
                y = System.Math.Pow(x, 1.0/n);
            }
            else if (Comparison.IsNegative(x))
            {
                y = -System.Math.Pow(-x, 1.0/n);
            }
            return y;
        }
    }
}