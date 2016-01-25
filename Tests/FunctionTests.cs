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

using NUnit.Framework;
using Shouldly;

namespace Math.Tests
{
    [TestFixture]
    public class FunctionTests
    {
        [TestCase(27.1)]
        [TestCase(0.13)]
        [TestCase(0.0)]
        [TestCase(-27.1)]
        [TestCase(-0.13)]
        public void Cbrt_WithNumber_ReturnsExpectedResult(double x)
        {
            var a = Function.Cbrt(x);
            var a3 = a * a * a;
            a3.ShouldBe(x, 1e-13);
        }

        [TestCase(double.NaN)]
        [TestCase(double.NegativeInfinity)]
        [TestCase(double.PositiveInfinity)]
        public void Cbrt_WithNotNumber_ReturnsInput(double x)
        {
            var a = Function.Cbrt(x);
            var a3 = a * a * a;
            a3.ShouldBe(x);
        }

        [TestCase(27.1)]
        [TestCase(0.13)]
        [TestCase(0.0)]
        [TestCase(-27.1)]
        [TestCase(-0.13)]
        public void Qnrt_WithNumber_ReturnsExpectedResult(double x)
        {
            var a = Function.Qnrt(x);
            var a5 = a * a * a * a * a;
            a5.ShouldBe(x, 1e-13);
        }

        [TestCase(double.NaN)]
        [TestCase(double.NegativeInfinity)]
        [TestCase(double.PositiveInfinity)]
        public void Qnrt_WithNotNumber_ReturnsInput(double x)
        {
            var a = Function.Qnrt(x);
            var a3 = a * a * a * a * a;
            a3.ShouldBe(x);
        }

        [TestCase(-System.Math.PI / 2.0)]
        [TestCase(-0.1)]
        [TestCase(0.0)]
        [TestCase(0.1)]
        [TestCase(System.Math.PI / 2.0)]
        public void FastSin_WithInRange_ReturnsExpected(double a)
        {
            var y = Function.FastSin(a);
            y.ShouldBe(System.Math.Sin(a), 0.0205);
        }

        [TestCase(7, 7 - System.Math.PI * 2)]
        [TestCase(-1, -1 + System.Math.PI * 2)]
        [TestCase(3.1, 3.1)]
        [TestCase(0.0, 0.0)]
        [TestCase(System.Math.PI * 2, 0.0)]
        public void NormalizeAngle(double a, double expected)
        {
            var y = Function.NormalizeAngle(a);
            y.ShouldBe(expected);
        }

        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 2)]
        [TestCase(4, 3)]
        [TestCase(5, 5)]
        [TestCase(6, 8)]
        [TestCase(7, 13)]
        [TestCase(8, 21)]
        [TestCase(9, 34)]
        [TestCase(10, 55)]
        [TestCase(11, 89)]
        [TestCase(12, 144)]
        public void FibonacciInt(int n, int f)
        {
            Function.FibonacciInt(n).ShouldBe((ulong)f);
        }

        [Test]
        public void FibonacciInt_Max()
        {
            var fdmaxm1 = Function.Fibonacci(Function.MaxFibonacciInt - 1);
            var fmax = Function.FibonacciInt(Function.MaxFibonacciInt);
            var fdmax = Function.Fibonacci(Function.MaxFibonacciInt);
            var fmaxp1 = Function.FibonacciInt(Function.MaxFibonacciInt + 1);
            var fdmaxp1 = Function.Fibonacci(Function.MaxFibonacciInt + 1);
            fmax.ShouldBeGreaterThan(fmaxp1);
            ((double)fmax).ShouldBe(fdmax);
            fmaxp1.ShouldBe(0ul);
            fdmaxp1.ShouldBe(fdmaxm1 + fdmax);
        }

        [TestCase(0, 1ul)]
        [TestCase(1, 1ul)]
        [TestCase(2, 2ul)]
        [TestCase(3, 6ul)]
        [TestCase(4, 24ul)]
        [TestCase(5, 120ul)]
        [TestCase(6, 720ul)]
        [TestCase(7, 5040ul)]
        [TestCase(8, 40320ul)]
        [TestCase(9, 362880ul)]
        [TestCase(10, 3628800ul)]
        [TestCase(11, 39916800ul)]
        [TestCase(12, 479001600ul)]
        [TestCase(20, 2432902008176640000ul)]
        [TestCase(21, 0ul)] // Overflow
        public void FactorialInt(int n, ulong f)
        {
            Function.FactorialInt(n).ShouldBe(f);
        }

        [Test]
        public void FactorialInt_Max()
        {
            var fmax = Function.FactorialInt(Function.MaxFactorialInt);
            var fdmax = Function.Factorial(Function.MaxFactorialInt);
            var fmaxp1 = Function.FactorialInt(Function.MaxFactorialInt + 1);
            var fdmaxp1 = Function.Factorial(Function.MaxFactorialInt + 1);
            fmax.ShouldBeGreaterThan(fmaxp1);
            ((double)fmax).ShouldBe(fdmax);
            fmaxp1.ShouldBe(0ul);
            fdmaxp1.ShouldBe(fdmax * (Function.MaxFactorialInt + 1));
        }

        [TestCase(0, 1)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 6)]
        [TestCase(4, 24)]
        [TestCase(5, 120)]
        [TestCase(6, 720)]
        [TestCase(7, 5040)]
        [TestCase(8, 40320)]
        [TestCase(9, 362880)]
        [TestCase(10, 3628800)]
        [TestCase(11, 39916800)]
        [TestCase(12, 479001600)]
        [TestCase(20, 2432902008176640000)]
        [TestCase(21, 2432902008176640000.0 * 21.0)]
        [TestCase(25, 2432902008176640000.0 * (21.0 * 22.0 * 23.0 * 24.0 * 25.0))]
        public void Factorial(int n, double f)
        {
            Function.Factorial(n).ShouldBe(f);
        }

        [TestCase(36, 8, 4)]
        [TestCase(36, 1, 1)]
        [TestCase(51, 3, 3)]
        [TestCase(81, 54, 27)]
        public void GCD(int a, int b, int gcd)
        {
            Function.GCD(a, b).ShouldBe(gcd);
        }
    }
}