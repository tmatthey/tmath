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

using System.Collections.Generic;
using Math.Interfaces;

namespace Math
{
    public static partial class Function
    {
        // Largest argument that still fits in a ulong: 20! = 2432902008176640000 and
        // F(93) = 12200160415121876738. Promoted from `static readonly` to `const` so they
        // can be used in switch/attribute contexts and folded by the JIT.
        public const int MaxFactorialInt = 20;
        public const int MaxFibonacciInt = 93;

        private static readonly List<long> PrimesUpTo30 =
            new List<long> { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29 };

        public static double Cbrt(double x)
        {
            return Root(x, 3);
        }

        public static double Qnrt(double x)
        {
            return Root(x, 5);
        }

        public static ulong FactorialInt(int n)
        {
            if (n > MaxFactorialInt)
            {
                throw new System.OverflowException(
                    $"FactorialInt({n}) overflows ulong; maximum supported argument is {MaxFactorialInt}.");
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
                throw new System.OverflowException(
                    $"FibonacciInt({n}) overflows ulong; maximum supported argument is {MaxFibonacciInt}.");
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

        // https://en.wikipedia.org/wiki/Fibonacci_number#Recognizing_Fibonacci_numbers
        public static double FibonacciBinet(int n)
        {
            var sqrt5 = System.Math.Sqrt(5.0);
            var phi = (1 + sqrt5) / 2;
            return System.Math.Floor((System.Math.Pow(phi, n) - System.Math.Pow(-phi, -n)) / sqrt5);
        }

        public static int GCD(int a, int b)
        {
            // Greatest Common Divisor: Euclidean Algorithm. The mathematical GCD is non-negative;
            // for negative inputs the recursive form returned a sign that depended on the order
            // of arguments. Take absolute values up front.
            a = System.Math.Abs(a);
            b = System.Math.Abs(b);
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

            var nsq = (long) System.Math.Sqrt(n) + 1;
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
    }
}
