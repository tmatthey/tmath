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
            var a3 = a * a * a * a * a;
            a3.ShouldBe(x, 1e-13);
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
    }
}