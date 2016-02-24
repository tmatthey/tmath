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
    public class RegressionTests
    {
        [Test]
        public void Linear_WithDifferentListCounts_ReturnsNaN()
        {
            var x = new List<double> { 2.4 };
            var y = new List<double> { 225, 184, 220, 240, 180, 184, 186, 215 };
            double a, b;
            Regression.Linear(x, y, out a, out b);
            a.ShouldBe(double.NaN);
            b.ShouldBe(double.NaN);
        }

        [Test]
        public void Linear_WithZeroListCounts_ReturnsNaN()
        {
            var x = new List<double>();
            var y = new List<double>();
            double a, b;
            Regression.Linear(x, y, out a, out b);
            a.ShouldBe(double.NaN);
            b.ShouldBe(double.NaN);
        }

        [Test]
        public void Linear_WithOnePoint_ReturnsNaN()
        {
            var x = new List<double> { 1 };
            var y = new List<double> { 1 };
            double a, b;
            Regression.Linear(x, y, out a, out b);
            a.ShouldBe(double.NaN);
            b.ShouldBe(double.NaN);
        }

        [Test]
        public void Linear_WithTwoPoints_ReturnsNaN()
        {
            var x = new List<double> { 1, 2 };
            var y = new List<double> { 1, 1 };
            double a, b;
            Regression.Linear(x, y, out a, out b);
            a.ShouldBe(0.0);
            b.ShouldBe(1.0);
        }

        [Test]
        public void Linear_WithoutWeightExample2_ReturnsExpected()
        {
            // http://academic.macewan.ca/burok/Stat252/notes/regression1.pdf
            var x = new List<double>() { 2.4, 1.6, 2.0, 2.6, 1.4, 1.6, 2.0, 2.2 };
            var y = new List<double>() { 225, 184, 220, 240, 180, 184, 186, 215 };
            double a, b;
            Regression.Linear(x, y, out a, out b);
            a.ShouldBe(62.65 / 1.235, 1e-10);
            b.ShouldBe(1634.0 / 8.0 - (62.65 / 1.235) * 15.8 / 8.0, 1e-10);
        }
    }
}
