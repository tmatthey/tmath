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
using NUnit.Framework;
using Shouldly;

namespace Math.Tests
{
    [TestFixture]
    public class StatisticsTests
    {
        [TestCase(-17.19)]
        [TestCase(double.NegativeInfinity)]
        [TestCase(double.NaN)]
        public void ArithmeticMean_OneNumber_ReturnsElement(double x)
        {
            var list = new List<double> {x};
            Statistics.Arithmetic.Mean(list).ShouldBe(x);
        }

        [TestCase(-17.19, 0.0)]
        [TestCase(double.NegativeInfinity, 1.23)]
        [TestCase(double.NaN, 2.34)]
        public void ArithmeticMeanWeighted_OneNumber_ReturnsElement(double x, double w)
        {
            var list = new List<double> {x};
            var weight = new List<double> {w};
            Statistics.Arithmetic.Mean(list, weight).ShouldBe(x);
        }

        [TestCase(-17.19)]
        [TestCase(double.NegativeInfinity)]
        [TestCase(double.NaN)]
        public void ArithmeticVariance_OneNumber_ReturnsElement(double x)
        {
            var list = new List<double> {x};
            Statistics.Arithmetic.Variance(list).ShouldBe(0.0);
        }

        [TestCase(-17.19, 0.0)]
        [TestCase(double.NegativeInfinity, 1.23)]
        [TestCase(double.NaN, 2.34)]
        public void ArithmeticVarianceWeighted_OneNumber_ReturnsElement(double x, double w)
        {
            var list = new List<double> {x};
            var weight = new List<double> {w};
            Statistics.Arithmetic.Variance(list, weight).ShouldBe(0.0);
        }

        [Test]
        public void ArithmeticMean_EmptyList_ReturnsNaN()
        {
            var list = new List<double>();
            Statistics.Arithmetic.Mean(list).ShouldBe(double.NaN);
        }

        [Test]
        public void ArithmeticMean_ThreeNumbers_ReturnsMean()
        {
            var list = new List<double> {3.1, 17, -19};
            Statistics.Arithmetic.Mean(list).ShouldBe((3.1 + 17 - 19)/3.0);
        }

        [Test]
        public void ArithmeticMeanWeighted_EmptyList_ReturnsNaN()
        {
            var list = new List<double>();
            var weight = new List<double>();
            Statistics.Arithmetic.Mean(list, weight).ShouldBe(double.NaN);
        }

        [Test]
        public void ArithmeticMeanWeighted_ThreeNumbers_ReturnsMean()
        {
            var list = new List<double> {3.1, 17, -19};
            var weight = new List<double> {2.3, 3.4, 5.6};
            Statistics.Arithmetic.Mean(list, weight).ShouldBe((3.1*2.3 + 17*3.4 - 19*5.6)/(2.3 + 3.4 + 5.6));
        }

        //
        [Test]
        public void ArithmeticVariance_EmptyList_ReturnsNaN()
        {
            var list = new List<double>();
            Statistics.Arithmetic.Variance(list).ShouldBe(double.NaN);
        }

        [Test]
        public void ArithmeticVariance_ThreeNumbers_ReturnsVariance()
        {
            var list = new List<double> {2, 3, 7};
            Statistics.Arithmetic.Variance(list).ShouldBe((2*2 + 1 + 3*3)/3.0);
        }

        [Test]
        public void ArithmeticVarianceWeighted_EmptyList_ReturnsNaN()
        {
            var list = new List<double>();
            var weight = new List<double>();
            Statistics.Arithmetic.Variance(list, weight).ShouldBe(double.NaN);
        }

        [Test]
        public void ArithmeticVarianceWeighted_ThreeNumbers_ReturnsVariance()
        {
            var list = new List<double> { 30, 20, 15 };
            var weight = new List<double> { 400, 500, 600 };
            var u = Statistics.Arithmetic.Mean(list, weight);
            Statistics.Arithmetic.Variance(list, weight)
                .ShouldBe((400 * (30 - u) * (30 - u) + 500 * (20 - u) * (20 - u) + 600 * (15 - u) * (15 - u)) / weight.Sum());
        }

        [Test]
        public void ArithmeticVarianceWeighted_ThreeNumbers_ReturnsVarianceExpandedNoWeights()
        {
            var list = new List<double> { 30, 20, 15 };
            var weight = new List<double> { 1, 2, 3 };
            var listNoWeights = new List<double> { 30, 20, 20, 15, 15, 15 };
            Statistics.Arithmetic.Variance(list, weight)
                .ShouldBe(Statistics.Arithmetic.Variance(listNoWeights), 1e-10);
        }

        [Test]
        public void ArithmeticVarianceWeighted_ThreeNumbersTrivialWeigths_ReturnsVarianceNoWeights()
        {
            var list = new List<double> { 30, 20, 15 };
            var weight = new List<double> { 1, 1, 1 };
            Statistics.Arithmetic.Variance(list, weight)
                .ShouldBe(Statistics.Arithmetic.Variance(list), 1e-10);
        }
    }
}