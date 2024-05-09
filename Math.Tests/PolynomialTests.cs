/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2024 Thierry Matthey
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
    public class PolynomialTests
    {
        [TestCase(1.1)]
        [TestCase(-1.1)]
        [TestCase(0)]
        public void WithConstant_ReturnsConstantFunction(double c)
        {
            var p = new Polynomial(new List<double> {c});
            p.dp().Count.ShouldBe(0);
            p.p().Count.ShouldBe(1);
            p.p()[0].ShouldBe(c);
            p.P().Count.ShouldBe(2);
            p.P()[0].ShouldBe(0.0);
            p.P()[1].ShouldBe(c);
        }

        [TestCase(2.3)]
        [TestCase(-23.3)]
        [TestCase(0.0)]
        public void QudraticDerivative_ReturnsExpected(double x)
        {
            CreateEq(2.0, 3.0, out var a2, out var a1, out var a0);
            var p = new Polynomial(new List<double> {a0, a1, a2});
            p.dp(x).ShouldBe(x * 2.0 * a2 + a1);
        }

        [TestCase(2.3)]
        [TestCase(-23.3)]
        [TestCase(0.0)]
        public void QudraticIntegral_ReturnsExpected(double x)
        {
            CreateEq(2.0, 3.0, out var a2, out var a1, out var a0);
            var p = new Polynomial(new List<double> {a0, a1, a2});
            p.P(x).ShouldBe(x * x * x / 3.0 * a2 + x * x / 2.0 * a1 + x * a0, 1e-10);
        }

        [TestCase(1.0, 2.0)]
        [TestCase(-1.0, 2.0)]
        [TestCase(1.0, -2.0)]
        [TestCase(-1.0, -2.0)]
        [TestCase(-1.0, -1.0)]
        [TestCase(0.0, 0.0)]
        [TestCase(1.0, 1.0)]
        public void pWithRoot_ReturnsZero(double x0, double x1)
        {
            CreateEq(x0, x1, out var a2, out var a1, out var a0);
            var p = new Polynomial(new List<double> {a0, a1, a2});

            p.p(x0).ShouldBe(0.0);
            p.p(x1).ShouldBe(0.0);
        }

        [TestCase(1.0, 2.0)]
        [TestCase(-1.0, 2.0)]
        [TestCase(1.0, -2.0)]
        [TestCase(-1.0, -2.0)]
        [TestCase(-1.0, -1.0)]
        [TestCase(0.0, 0.0)]
        [TestCase(1.0, 1.0)]
        public void FindRoot_ReturnsRoot(double x0, double x1)
        {
            CreateEq(x0, x1, out var a2, out var a1, out var a0);
            var p = new Polynomial(new List<double> {a0, a1, a2});

            var u00 = p.FindRoot(x0 - 1e-5).Real;
            var u01 = p.FindRoot(x0 + 1e-5).Real;
            var u10 = p.FindRoot(x1 - 1e-5).Real;
            var u11 = p.FindRoot(x1 + 1e-5).Real;

            u00.ShouldBe(x0, 1e-8);
            u01.ShouldBe(x0, 1e-8);
            u10.ShouldBe(x1, 1e-8);
            u11.ShouldBe(x1, 1e-8);
        }

        [TestCase(1.0, 2.0)]
        [TestCase(-1.0, 2.0)]
        [TestCase(1.0, -2.0)]
        [TestCase(-1.0, -2.0)]
        [TestCase(-1.0, -1.0)]
        [TestCase(0.0, 0.0)]
        [TestCase(1.0, 1.0)]
        public void IntegrateDerivative_ReturnsPolynomial(double x0, double x1)
        {
            CreateEq(x0, x1, out var a2, out var a1, out var a0);
            var p = new Polynomial(new List<double> {a0, a1, a2});
            var res = new Polynomial(p.dp()).P();
            res.Count.ShouldBe(p.p().Count);
            // Constant not preserved since first applying derivative
            for (var i = 1; i < res.Count; ++i)
                res[i].ShouldBe(p.p()[i]);
        }

        [TestCase(1.0, 2.0)]
        [TestCase(-1.0, 2.0)]
        [TestCase(1.0, -2.0)]
        [TestCase(-1.0, -2.0)]
        [TestCase(-1.0, -1.0)]
        [TestCase(0.0, 0.0)]
        [TestCase(1.0, 1.0)]
        public void DerivativeIntegrate_ReturnsPolynomial(double x0, double x1)
        {
            CreateEq(x0, x1, out var a2, out var a1, out var a0);
            var p = new Polynomial(new List<double> {a0, a1, a2});
            var res = new Polynomial(p.P()).dp();
            res.Count.ShouldBe(p.p().Count);
            for (var i = 0; i < res.Count; ++i)
                res[i].ShouldBe(p.p()[i]);
        }

        [TestCase(1.0, 2.0)]
        [TestCase(2.0, 1.0)]
        [TestCase(1.0, -2.0)]
        [TestCase(-1.0, 2.0)]
        public void DivideByRootWithCubicEq_ReturnsExpectedReducedPolynomial(double x0, double x1)
        {
            CreateEq(x0, x1, x1, out var a3, out var a2, out var a1, out var a0);
            var p = new Polynomial(new List<double> {a0, a1, a2, a3});

            CreateEq(x0, x1, out var b2, out var b1, out var b0);
            var p0 = new Polynomial(new List<double> {b0, b1, b2});

            CreateEq(x1, x1, out b2, out b1, out b0);
            var p1 = new Polynomial(new List<double> {b0, b1, b2});

            var r0 = p.DivideByRoot(x1);
            r0.p().Count.ShouldBe(p0.p().Count);
            for (var i = 0; i < r0.p().Count; i++)
            {
                r0.p()[i].ShouldBe(p0.p()[i]);
            }

            var r1 = p.DivideByRoot(x0);
            r1.p().Count.ShouldBe(p1.p().Count);
            for (var i = 0; i < r1.p().Count; i++)
            {
                r1.p()[i].ShouldBe(p1.p()[i]);
            }
        }

        [TestCase(1.5, 2.0)]
        [TestCase(-1.5, 2.0)]
        [TestCase(1.5, -2.0)]
        [TestCase(-1.5, -2.0)]
        public void DivideByRootLinear_ReturnsZeroRemaining(double a, double b)
        {
            var p = new Polynomial(new List<double> {a, b});
            var r = p.DivideByRoot(-p.p()[0] / p.p()[1]);
            r.p().Count.ShouldBe(1);
            r.p()[0].ShouldBe(p.p()[1]);
        }

        private void CreateEq(double x0, double x1, out double a, out double b, out double c)
        {
            a = 1.0;
            b = -(x0 + x1);
            c = x0 * x1;
        }

        private void CreateEq(double x0, double x1, double x2, out double a, out double b, out double c, out double d)
        {
            a = 1.0;
            b = -(x0 + x1 + x2);
            c = x0 * x1 + x0 * x2 + x1 * x2;
            d = -x0 * x1 * x2;
        }

        [Test]
        public void AllZeroCoeff_ReturnZeroFunction()
        {
            var p = new Polynomial(new List<double> {0.0, 0.0, 0.0});
            p.p().Count.ShouldBe(1);
            p.p()[0].ShouldBe(0.0);
        }

        [Test]
        public void Clone()
        {
            var p = new Polynomial(new List<double> {1.112, 1.07, 1.9});
            var q = p.Clone();
            ReferenceEquals(p, q).ShouldBe(false);
            p.p().ShouldBe(q.p());
            p.P().ShouldBe(q.P());
            p.dp().ShouldBe(q.dp());
        }

        [Test]
        public void WithEmptyCoefficients_ReturnsZeroFunction()
        {
            var p = new Polynomial(new List<double>());
            p.dp().Count.ShouldBe(0);
            p.p().Count.ShouldBe(1);
            p.p()[0].ShouldBe(0.0);
            p.P().Count.ShouldBe(2);
            p.P()[0].ShouldBe(0.0);
            p.P()[1].ShouldBe(0.0);
        }

        [Test]
        public void WithHeadingZeroCoeff_AreRemoved()
        {
            var p = new Polynomial(new List<double> {1.112, 0.0, 0.0});
            p.p().Count.ShouldBe(1);
            p.p()[0].ShouldBe(1.112);
        }
    }
}