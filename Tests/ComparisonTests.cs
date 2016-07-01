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
using NUnit.Framework;
using Shouldly;

namespace Math.Tests
{
    [TestFixture]
    public class ComparisonTests
    {
        private const double Epsilon = double.Epsilon;

        [TestCase(0, true)]
        [TestCase(Comparison.Epsilon, true)]
        [TestCase(-Comparison.Epsilon, true)]
        [TestCase(2.0*Comparison.Epsilon, true)]
        [TestCase(-2.0*Comparison.Epsilon, true)]
        [TestCase(13.34, true)]
        [TestCase(-13.34, true)]
        [TestCase(double.NaN, false)]
        [TestCase(double.PositiveInfinity, false)]
        [TestCase(double.NegativeInfinity, false)]
        public void IsNumber(double x, bool expected)
        {
            var result = Comparison.IsNumber(x);
            result.ShouldBe(expected);
        }

        [TestCase(0, true)]
        [TestCase(Comparison.Epsilon, true)]
        [TestCase(-Comparison.Epsilon, true)]
        [TestCase(2.0*Comparison.Epsilon, false)]
        [TestCase(-2.0*Comparison.Epsilon, false)]
        [TestCase(13.34, false)]
        [TestCase(-13.34, false)]
        [TestCase(double.NaN, false)]
        [TestCase(double.PositiveInfinity, false)]
        [TestCase(double.NegativeInfinity, false)]
        public void IsZero(double x, bool expected)
        {
            var result = Comparison.IsZero(x);
            result.ShouldBe(expected);
        }

        [TestCase(0, false)]
        [TestCase(Comparison.Epsilon, false)]
        [TestCase(-Comparison.Epsilon, false)]
        [TestCase(2.0*Comparison.Epsilon, true)]
        [TestCase(-2.0*Comparison.Epsilon, false)]
        [TestCase(13.34, true)]
        [TestCase(-13.34, false)]
        [TestCase(double.NaN, false)]
        [TestCase(double.PositiveInfinity, false)]
        [TestCase(double.NegativeInfinity, false)]
        public void IsPositive(double x, bool expected)
        {
            var result = Comparison.IsPositive(x);
            result.ShouldBe(expected);
        }

        [TestCase(0, false)]
        [TestCase(Comparison.Epsilon, false)]
        [TestCase(-Comparison.Epsilon, false)]
        [TestCase(2.0*Comparison.Epsilon, false)]
        [TestCase(-2.0*Comparison.Epsilon, true)]
        [TestCase(13.34, false)]
        [TestCase(-13.34, true)]
        [TestCase(double.NaN, false)]
        [TestCase(double.PositiveInfinity, false)]
        [TestCase(double.NegativeInfinity, false)]
        public void IsNegative(double x, bool expected)
        {
            var result = Comparison.IsNegative(x);
            result.ShouldBe(expected);
        }

        [TestCase(0, true)]
        [TestCase(Epsilon, true)]
        [TestCase(-Epsilon, true)]
        [TestCase(2.0*Epsilon, false)]
        [TestCase(-2.0*Epsilon, false)]
        [TestCase(13.34, false)]
        [TestCase(-13.34, false)]
        [TestCase(double.NaN, false)]
        [TestCase(double.PositiveInfinity, false)]
        [TestCase(double.NegativeInfinity, false)]
        public void IsZeroWithUserDefinedEpsilon(double x, bool expected)
        {
            var result = Comparison.IsZero(x, Epsilon);
            result.ShouldBe(expected);
        }

        [TestCase(0, false)]
        [TestCase(Epsilon, false)]
        [TestCase(-Epsilon, false)]
        [TestCase(2.0*Epsilon, true)]
        [TestCase(-2.0*Epsilon, false)]
        [TestCase(13.34, true)]
        [TestCase(-13.34, false)]
        [TestCase(double.NaN, false)]
        [TestCase(double.PositiveInfinity, false)]
        [TestCase(double.NegativeInfinity, false)]
        public void IsPositiveWithUserDefinedEpsilon(double x, bool expected)
        {
            var result = Comparison.IsPositive(x, Epsilon);
            result.ShouldBe(expected);
        }

        [TestCase(0, false)]
        [TestCase(Epsilon, false)]
        [TestCase(-Epsilon, false)]
        [TestCase(2.0*Epsilon, false)]
        [TestCase(-2.0*Epsilon, true)]
        [TestCase(13.34, false)]
        [TestCase(-13.34, true)]
        [TestCase(double.NaN, false)]
        [TestCase(double.PositiveInfinity, false)]
        [TestCase(double.NegativeInfinity, false)]
        public void IsNegativeWithUserDefinedEpsilon(double x, bool expected)
        {
            var result = Comparison.IsNegative(x, Epsilon);
            result.ShouldBe(expected);
        }

        [TestCase(11.0, 11.0, true)]
        [TestCase(13.0, 13.05, false)]
        [TestCase(0.0, 1e-13, false)]
        [TestCase(12.0 + double.Epsilon*2.0, 12.0 + double.Epsilon*2.01, true)]
        [TestCase(0.0, 1e-14, true)]
        public void IsEqual(double x, double y, bool expected)
        {
            var result = Comparison.IsEqual(x, y);
            result.ShouldBe(expected);
        }

        [TestCase(12.0, 12.0, 0.1, true)]
        [TestCase(12.0, 12.05, 0.1, true)]
        [TestCase(12.0, 12.05, double.Epsilon, false)]
        [TestCase(double.Epsilon*2.0, double.Epsilon*2.01, double.Epsilon, true)]
        public void IsEqualWithUserDefinedEpsilon(double x, double y, double eps, bool expected)
        {
            var result = Comparison.IsEqual(x, y, eps);
            result.ShouldBe(expected);
        }

        [TestCase(11.0, 11.0, true)]
        [TestCase(13.05, 13.0, false)]
        [TestCase(1e-13, 0.0, false)]
        [TestCase(12.0 + double.Epsilon*2.0, 12.0 + double.Epsilon*2.01, true)]
        [TestCase(0.0, 1e-14, true)]
        [TestCase(5.0, 6.0, true)]
        public void IsLessEqual(double x, double y, bool expected)
        {
            var result = Comparison.IsLessEqual(x, y);
            result.ShouldBe(expected);
        }

        [TestCase(11.0, 11.0, false)]
        [TestCase(13.05, 13.0, false)]
        [TestCase(1e-13, 0.0, false)]
        [TestCase(-1e-11, 0.0, true)]
        [TestCase(0.0, 1e-11, true)]
        [TestCase(5.0, 6.0, true)]
        public void IsLess(double x, double y, bool expected)
        {
            var result = Comparison.IsLess(x, y);
            result.ShouldBe(expected);
        }

        [TestCase(12.0, 12.0, 0.1, true)]
        [TestCase(12.0, 12.05, 0.1, true)]
        [TestCase(12.05, 12.0, double.Epsilon, false)]
        [TestCase(double.Epsilon*2.0, double.Epsilon*2.01, double.Epsilon, true)]
        [TestCase(5.0, 6.0, 0.1, true)]
        public void IsLessEqualWithUserDefinedEpsilon(double x, double y, double eps, bool expected)
        {
            var result = Comparison.IsLessEqual(x, y, eps);
            result.ShouldBe(expected);
        }

        [Test]
        public void UniqueAverageSorted()
        {
            var vec = new List<double> {1.02, 1.0, 1.0 + 1e-14, 3.0};
            var res = Comparison.UniqueAverageSorted(vec);
            res.Count.ShouldBe(3);
            res[0].ShouldBe(1.0 + 0.5e-15, 1e-10);
            res[1].ShouldBe(1.02);
            res[2].ShouldBe(3.0);
        }

        [Test]
        public void UniqueAverageSortedWithUserDefinedEpsilon()
        {
            var vec = new List<double> {-1.2, 1.02, 1.01, 0.99};
            var res = Comparison.UniqueAverageSorted(vec, 0.1);
            res.Count.ShouldBe(2);
            res[1].ShouldBe((1.02 + 1.01 + 0.99)/3.0, 1e-10);
            res[0].ShouldBe(-1.2);
        }
    }
}