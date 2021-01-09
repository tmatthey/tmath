/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2021 Thierry Matthey
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
    public class SolverTests
    {
        private const double Epsilon = 1e-13;

        [TestCase(1.2, 2.3)]
        [TestCase(-1.2, 2.3)]
        [TestCase(1.2, -2.3)]
        [TestCase(-1.2, -2.3)]
        public void LinearEq_ValidInput_ReturnsOneSolution(double a, double b)
        {
            var x = Solver.LinearEq(a, b);
            var y = a * x + b;
            y.ShouldBe(0.0);
        }

        [TestCase(1.0)]
        [TestCase(-1.0)]
        [TestCase(13.17)]
        [TestCase(-13.17)]
        public void QuadraticEq_OneRoot_ReturnsOneSolution(double x)
        {
            CreateEq(x, x, out var a, out var b, out var c);

            var root = Solver.QuadraticEq(a, b, c);
            root.Count.ShouldBe(1);
            root[0].ShouldBe(x);
        }

        [TestCase(1.0, 13.1, 1.0)]
        [TestCase(-1.0, 13.1, 1.1)]
        [TestCase(13.17, -13.1, 1.2)]
        [TestCase(-13.17, -13.1, -1.1)]
        public void QuadraticEq_TwoRoots_ReturnsTwoSolutions(double x0, double x1, double f)
        {
            CreateEq(x0, x1, out var a, out var b, out var c);
            a *= f;
            b *= f;
            c *= f;
            var root = Solver.QuadraticEq(a, b, c);
            root.Count.ShouldBe(2);
            root[0].ShouldBe(System.Math.Min(x0, x1), Epsilon);
            root[1].ShouldBe(System.Math.Max(x0, x1), Epsilon);
        }

        [TestCase(1.2, 2.3)]
        [TestCase(-1.2, 2.3)]
        [TestCase(1.2, -2.3)]
        [TestCase(-1.2, -2.3)]
        public void QuadraticEq_Linear_ReturnsOneSolution(double b, double c)
        {
            var a = 0.0;
            var root = Solver.QuadraticEq(a, b, c);
            root.Count.ShouldBe(1);
            var y = b * root[0] + c;
            y.ShouldBe(0.0);
        }

        [TestCase(1.0, 2.0, 3.0, 1.1)]
        [TestCase(-1.0, 2.0, 3.0, 1.2)]
        [TestCase(1.0, -2.0, 3.0, 1.3)]
        [TestCase(-1.0, -2.0, 3.0, 1.4)]
        [TestCase(1.0, 2.0, -3.0, 1.5)]
        [TestCase(-1.0, 2.0, -3.0, 1.6)]
        [TestCase(1.0, -2.0, -3.0, 1.7)]
        [TestCase(-1.0, -2.0, -3.0, 1.8)]
        [TestCase(0.0, 2.0, 3.0, 1.1)]
        [TestCase(0.0, -2.0, 3.0, 1.3)]
        [TestCase(0.0, 2.0, -3.0, 1.1)]
        [TestCase(0.0, -2.0, -3.0, 1.3)]
        [TestCase(1.0, 0.0, 3.0, 1.5)]
        [TestCase(-1.0, 0.0, 3.0, 1.7)]
        [TestCase(1.0, 0.0, -3.0, 1.5)]
        [TestCase(-1.0, 0.0, -3.0, 1.7)]
        [TestCase(1.0, 2.0, 0.0, 1.1)]
        [TestCase(-1.0, 2.0, 0.0, 1.2)]
        [TestCase(1.0, -2.0, 0.0, 1.3)]
        [TestCase(-1.0, -2.0, 0.0, 1.4)]
        public void CubicEq_ThreeDifferentRoots_ReturnsSolutions(double x0, double x1, double x2, double f)
        {
            var s = new List<double> { x0, x1, x2 };
            s.Sort();
            CreateEq(x0, x1, x2, out var a, out var b, out var c, out var d);
            a *= f;
            b *= f;
            c *= f;
            d *= f;

            var root = Solver.CubicEq(a, b, c, d);
            root.Count.ShouldBe(3);
            root[0].ShouldBe(s[0], Epsilon);
            root[1].ShouldBe(s[1], Epsilon);
            root[2].ShouldBe(s[2], Epsilon);
        }

        [TestCase(-1.0, -2.0, 1.4)]
        [TestCase(1.0, -2.0, 1.4)]
        [TestCase(-1.0, 2.0, 1.4)]
        [TestCase(1.0, 2.0, 1.4)]
        [TestCase(0.0, 2.0, 1.4)]
        [TestCase(0.0, -2.0, 1.4)]
        [TestCase(-3.0, 0.0, 1.4)]
        [TestCase(3.0, 0.0, 1.4)]
        public void CubicEq_TwoDifferentRoots_ReturnsSolutions(double x0, double x1, double f)
        {
            var s = new List<double> { x0, x1 };
            s.Sort();
            CreateEq(x0, x1, x1, out var a, out var b, out var c, out var d);
            a *= f;
            b *= f;
            c *= f;
            d *= f;

            var root = Solver.CubicEq(a, b, c, d);
            root.Count.ShouldBe(2);
            root[0].ShouldBe(s[0], Epsilon);
            root[1].ShouldBe(s[1], Epsilon);
        }

        [TestCase(-3.0, 1.4)]
        [TestCase(0.0, 1.4)]
        [TestCase(3.0, 1.4)]
        public void CubicEq_OneDifferentRoots_ReturnsSolutions(double x0, double f)
        {
            var s = new List<double> { x0 };
            s.Sort();
            CreateEq(x0, x0, x0, out var a, out var b, out var c, out var d);
            a *= f;
            b *= f;
            c *= f;
            d *= f;

            var root = Solver.CubicEq(a, b, c, d);
            root.Count.ShouldBe(1);
            root[0].ShouldBe(s[0], Epsilon);
        }

        [TestCase(0, 0, 0, 0, double.NaN, double.NaN, double.NaN)]
        [TestCase(1, 0, 0, 0, 0, double.NaN, double.NaN)]
        [TestCase(1, 0, 0, 1, -1, double.NaN, double.NaN)]
        public void CubicSolver_Generic(double a, double b, double c, double d, double x0, double x1, double x2)
        {
            var expected = new List<double>();
            if (!double.IsNaN(x0))
                expected.Add(x0);
            if (!double.IsNaN(x1))
                expected.Add(x1);
            if (!double.IsNaN(x2))
                expected.Add(x2);
            expected.Sort();

            var x = Solver.CubicEq(a, b, c, d);

            x.Count.ShouldBe(expected.Count);
            for (var i = 0; i < x.Count; i++)
                x[i].ShouldBe(expected[i]);
        }

        [TestCase(-1.0, -2.0, 0.0, 1.0, 1.4)]
        [TestCase(1.0, 2.0, 0.0, -1.0, 1.4)]
        [TestCase(1.0, 2.0, 2.0, 1.0, 1.4)]
        [TestCase(1.0, 2.0, 2.0, 2.0, 1.4)]
        [TestCase(2.0, 2.0, 2.0, 2.0, 1.4)]
        public void QuarticEq_GivenRoots_ReturnsSolutions(double x0, double x1, double x2, double x3, double f)
        {
            var s = new List<double> { x0, x1, x2, x3 };
            CreateEq(x0, x1, x2, x3, out var a, out var b, out var c, out var d, out var e);
            a *= f;
            b *= f;
            c *= f;
            d *= f;
            e *= f;

            s = s.Distinct().ToList();
            s.Sort();
            var root = Solver.QuarticEq(a, b, c, d, e);
            root.Count.ShouldBe(s.Count);
            if (s.Count > 0) root[0].ShouldBe(s[0], Epsilon);
            if (s.Count > 1) root[1].ShouldBe(s[1], Epsilon);
            if (s.Count > 2) root[2].ShouldBe(s[2], Epsilon);
            if (s.Count > 3) root[3].ShouldBe(s[3], Epsilon);
        }

        [TestCase(-17, 23, 1e-15)]
        [TestCase(-17, 23, 1e-6)]
        [TestCase(-17, 23, 1e-5)]
        [TestCase(-17, 23, 1e-4)]
        public void Bisection_ReturnsRootsWithEpsilon(double x0, double x1, double eps)
        {
            CreateEq(x0, x1, out var a, out var b, out var c);
            var p = new Polynomial(new List<double> { c, b, a });
            var x = Solver.Bisection(100, eps, x0 - 5.0, x0 + 2.0, p.p);
            var y = p.p(x);
            y.ShouldBe(0.0, eps);
        }

        [TestCase(-17, 23, 1e-15)]
        [TestCase(-17, 23, 1e-6)]
        [TestCase(-17, 23, 1e-5)]
        [TestCase(-17, 23, 1e-4)]
        public void Secant_ReturnsRootsWithEpsilon(double x0, double x1, double eps)
        {
            CreateEq(x0, x1, out var a, out var b, out var c);
            var p = new Polynomial(new List<double> { c, b, a });
            var x = Solver.Secant(100, eps, x0 - 5.0, x0 + 2.0, p.p);
            var y = p.p(x);
            y.ShouldBe(0.0, eps);
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

        private void CreateEq(double x0, double x1, double x2, double x3, out double a, out double b, out double c,
            out double d, out double e)
        {
            a = 1.0;
            b = -(x0 + x1 + x2 + x3);
            c = x0 * x1 + x0 * x2 + x0 * x3 +
                x1 * x2 + x1 * x3 +
                x2 * x3;
            d = -x1 * x2 * x3 -
                x0 * x2 * x3 -
                x0 * x1 * x3 -
                x0 * x1 * x2;
            e = x0 * x1 * x2 * x3;
        }

        [Test]
        public void Bisection_FunctionIsNaN_ReturnsNaN()
        {
            var p = new Polynomial(new List<double> { 1, -3, double.NaN });
            var x = Solver.Bisection(100, 1e-15, 1.5, 2, p.p);
            x.ShouldBe(double.NaN);
        }

        [Test]
        public void Bisection_NoRoot_ReturnsNaN()
        {
            var p = new Polynomial(new List<double> { 1, 0, 1 });
            var x = Solver.Bisection(10, 1e-15, 3, -1.1, p.p);
            x.ShouldBe(double.NaN);
        }

        [Test]
        public void Bisection_RootAtMiddle_ReturnsRoot()
        {
            var p = new Polynomial(new List<double> { 2, -3, 1 });
            var x = Solver.Bisection(5, 1e-15, 1.5, 2.5, p.p);
            x.ShouldBe(2);
        }

        [Test]
        public void Bisection_RootAtX0_ReturnsRoot()
        {
            var p = new Polynomial(new List<double> { 2, -3, 1 });
            var x = Solver.Bisection(100, 1e-15, 1, 1.5, p.p);
            x.ShouldBe(1);
        }

        [Test]
        public void Bisection_RootAtX1_ReturnsRoot()
        {
            var p = new Polynomial(new List<double> { 2, -3, 1 });
            var x = Solver.Bisection(100, 1e-15, 1.5, 2, p.p);
            x.ShouldBe(2);
        }

        [Test]
        public void Bisection_RootCloseAtMiddle_ReturnsRoot()
        {
            var p = new Polynomial(new List<double> { 2, -3, 1 });
            var x = Solver.Bisection(15, 1e-5, 1.4, 2.5, p.p);
            x.ShouldBe(2, 1e-5);
        }

        [Test]
        public void LinearEq_InvalidInput_ReturnsNaN()
        {
            var x = Solver.LinearEq(0.0, 1.0);
            x.ShouldBe(double.NaN);
        }

        [Test]
        public void PolynomialEq_EmptyCoefficients_ReturnsEmpty()
        {
            var root = Solver.PolynomialEq(new List<double>());
            root.Count.ShouldBe(0);
        }

        [Test]
        public void PolynomialEq_OneNonZeroCoefficients_ReturnsZero()
        {
            var root = Solver.PolynomialEq(new List<double> { 0.0, 0.0, 0.0, 17.0, 0.0 });
            root.Count.ShouldBe(1);
            root[0].ShouldBe(0.0);
        }

        [Test]
        public void PolynomialEq_CubicOneRootAndZeroRoot_ReturnsTwoRoots()
        {
            var root = Solver.PolynomialEq(new List<double> { 0.0, 0.0, -87.0, 41.0, -7.0, 1.0 });
            root.Count.ShouldBe(2);
            root[0].ShouldBe(0.0);
            root[1].ShouldBe(3.0, 1e-13);
        }

        [Test]
        public void PolynomialEq_QudraticAndMultipleZeroRoot_ReturnsExpected()
        {
            var root = Solver.PolynomialEq(new List<double> { 0.0, 0.0, 0.0, 2.0, -3.0, 1.0 });
            root.Count.ShouldBe(3);
            root[0].ShouldBe(0.0);
            root[1].ShouldBe(1.0);
            root[2].ShouldBe(2.0);
        }

        [Test]
        public void PolynomialEq_Quintic_ReturnsThreeRoots()
        {
            var p = new Polynomial(new List<double> { 120.0, -44, 14.0, -7.0, -4, 1.0 });
            var root = Solver.PolynomialEq(p.p().ToList());
            root.Count.ShouldBe(3);
            root[0].ShouldBe(-3.0);
            root[1].ShouldBe(2.0);
            root[2].ShouldBe(5.0);
        }

        [Test]
        public void PolynomialEq_QuinticNonInts_ReturnsFiveRoots()
        {
            var p = new Polynomial(new List<double> { 54.0, -36.0, -37.5, 10.0, 8.5, 1.0 });
            var root = Solver.PolynomialEq(p.p().ToList());
            root.Count.ShouldBe(5);
            root[0].ShouldBe(-6.0);
            root[1].ShouldBe(-3.0);
            root[2].ShouldBe(-2.0);
            root[3].ShouldBe(1.0);
            root[4].ShouldBe(1.5);
        }

        [Test]
        public void PolynomialEq_Septic_ReturnsRoots()
        {
            var p = new Polynomial(new List<double> { 0.0, 0.0, 1.0, 3.0, -87.0, 41.0, -7.0, 1.0 });
            var root = Solver.PolynomialEq(p.p().ToList());
            root.Count.ShouldBe(4);
            p.p(root[0]).ShouldBe(0.0, 1e-13);
            p.p(root[1]).ShouldBe(0.0, 1e-13);
            p.p(root[2]).ShouldBe(0.0, 1e-13);
            p.p(root[3]).ShouldBe(0.0, 1e-13);
        }

        [Test]
        public void PolynomialEq_Sextic_ReturnsRoots()
        {
            var p = new Polynomial(new List<double> { 2.0, -3.0, 3.0, -3.0, 3.0, -3.0, 1 });
            var root = Solver.PolynomialEq(p.p().ToList());
            root.Count.ShouldBe(2);
            p.p(root[0]).ShouldBe(0.0, 1e-15);
            p.p(root[1]).ShouldBe(0.0, 1e-15);
        }

        [Test]
        public void PolynomialEq_ZeroCoefficients_ReturnsEmpty()
        {
            var root = Solver.PolynomialEq(new List<double> { 0.0, 0.0, 0.0 });
            root.Count.ShouldBe(0);
        }

        [Test]
        public void QuadraticEq_NoRoot_ReturnsEmptyList()
        {
            var root = Solver.QuadraticEq(1, 0, 1);
            root.Count.ShouldBe(0);
        }

        [Test]
        public void QuarticEq_WithFourRoots_ReturnsSolitions()
        {
            var root = Solver.QuarticEq(1.0, 0.4, -6.49, 7.244, -2.112);
            root.Count.ShouldBe(4);
            root[0].ShouldBe(-3.2, Epsilon);
            root[1].ShouldBe(0.5, Epsilon);
            root[2].ShouldBe(1.1, Epsilon);
            root[3].ShouldBe(1.2, Epsilon);
        }

        [Test]
        public void QuarticEq_ZeroA_ReturnsCubicSolution()
        {
            var a = 0.0;
            CreateEq(1.0, 2.0, 3.0, out var b, out var c, out var d, out var e);
            var root = Solver.QuarticEq(a, b, c, d, e);
            root.Count.ShouldBe(3);
            root[0].ShouldBe(1.0, Epsilon);
            root[1].ShouldBe(2.0, Epsilon);
            root[2].ShouldBe(3.0, Epsilon);
        }

        [TestCase(0, 0, 0, 0)]
        [TestCase(1, 0, 0, 0)]
        [TestCase(1, 1, 0, 0)]
        [TestCase(1, 1, 1, 0)]
        [TestCase(1, 1, 1, 1)]
        [TestCase(1, 1, 1, 2)]
        [TestCase(1, 1, 2, 2)]
        [TestCase(1, 2, 2, 2)]
        [TestCase(1, 2, 2, 3)]
        [TestCase(1, 2, 3, 4)]
        public void QuarticEq_Roots(double x0, double x1, double x2, double x3)
        {

            var (a, b, c, d, e) = CreateQuarticEq(x0, x1, x2, x3);

            var x = Solver.QuarticEq(a, b, c, d, e);

            var expected = new List<double> { x0, x1, x2, x3 }.Distinct().ToList();
            expected.Sort();

            x.Count.ShouldBe(expected.Count);
            var p = new Polynomial(new List<double> { e, d, c, b, a });
            for (var i = 0; i < x.Count; i++)
            {
                p.p(x[i]).ShouldBe(0, 1e-13);
                x[i].ShouldBe(expected[i], 1e-13);
            }
        }

        [Test]
        public void CubicEq_OneSingleRoot_ReturnsSolutions()
        {
            var a = 1.0;
            var b = -9;
            var c = -9;
            var d = -10;
            var root = Solver.CubicEq(a, b, c, d);
            root.Count.ShouldBe(1);
            root[0].ShouldBe(10.0, Epsilon);
        }

        [Test]
        public void CubicEq_Quadratic_ReturnsOneSolutions()
        {
            var a = 0.0;
            var b = 1;
            var c = -2;
            var d = 1;
            var root = Solver.CubicEq(a, b, c, d);
            root.Count.ShouldBe(1);
            root[0].ShouldBe(1, Epsilon);
        }
        [TestCase(0, 0, 0)]
        [TestCase(1, 0, 0)]
        [TestCase(1, 1, 0)]
        [TestCase(1, 1, 1)]
        [TestCase(1, 1, 2)]
        [TestCase(1, 2, 2)]
        [TestCase(1, 2, 3)]
        public void CubicEq_Roots(double x0, double x1, double x2)
        {

            var (a, b, c, d) = CreateCubicEq(x0, x1, x2);

            var x = Solver.CubicEq(a, b, c, d);

            var expected = new List<double> { x0, x1, x2}.Distinct().ToList();
            expected.Sort();

            x.Count.ShouldBe(expected.Count);
            var p = new Polynomial(new List<double> { d, c, b, a });
            for (var i = 0; i < x.Count; i++)
            {
                p.p(x[i]).ShouldBe(0, 1e-13);
                x[i].ShouldBe(expected[i], 1e-13);
            }
        }

        [Test]
        public void Secant_FunctionIsNaN_ReturnsNaN()
        {
            var p = new Polynomial(new List<double> { 1, -3, double.NaN });
            var x = Solver.Secant(100, 1e-15, 1.5, 2, p.p);
            x.ShouldBe(double.NaN);
        }

        [Test]
        public void Secant_NoRoot_ReturnsNaN()
        {
            var p = new Polynomial(new List<double> { 1, 0, 1 });
            var x = Solver.Secant(50, 1e-15, 3, -1.1, p.p);
            x.ShouldBe(double.NaN);
        }

        [Test]
        public void Secant_RootAtMiddle_ReturnsRoot()
        {
            var p = new Polynomial(new List<double> { 2, -3, 1 });
            var x = Solver.Secant(50, 1e-15, 1.5, 2.5, p.p);
            x.ShouldBe(2);
        }

        [Test]
        public void Secant_RootAtX0_ReturnsRoot()
        {
            var p = new Polynomial(new List<double> { 2, -3, 1 });
            var x = Solver.Secant(100, 1e-15, 1, 1.5, p.p);
            x.ShouldBe(1);
        }

        [Test]
        public void Secant_RootAtX1_ReturnsRoot()
        {
            var p = new Polynomial(new List<double> { 2, -3, 1 });
            var x = Solver.Secant(100, 1e-15, 1.5, 2, p.p);
            x.ShouldBe(2);
        }

        [Test]
        public void Secant_RootCloseAtMiddle_ReturnsRoot()
        {
            var p = new Polynomial(new List<double> { 2, -3, 1 });
            var x = Solver.Secant(10, 1e-15, 1.4, 2.5, p.p);
            x.ShouldBe(2, 1e-5);
        }

        private static (double a, double b, double c, double d, double e) CreateQuarticEq(double x0, double x1, double x2, double x3)
        {
            var a = 1.0;
            var b = -(x0 + x1 + x2 + x3);
            var c = x0 * x1 + x0 * x2 + x0 * x3 + x1 * x2 + x1 * x3 + x2 * x3;
            var d = -x1 * x2 * x3 - x0 * x2 * x3 - x0 * x1 * x3 - x0 * x1 * x2;
            var e = x0 * x1 * x2 * x3;
            return (a, b, c, d, e);
        }

        private static (double a, double b, double c, double d) CreateCubicEq(double x0, double x1, double x2)
        {
            var a = 1.0;
            var b = -(x0 + x1 + x2);
            var c = x0 * x1 + x0 * x2 + x1 * x2;
            var d = -x0 * x1 * x2;
            return (a, b, c, d);
        }
    }
}