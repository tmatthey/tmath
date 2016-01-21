using Math.Algebra;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;

namespace Math.Test
{
    [TestFixture]
    public class AlgebraTests
    {
        [Test]
        public void Polynomial_WithEmptyCoefficients_ReturnsZeroFunction()
        {
            var p = new Polynomial(new List<double>() { });
            p.dp().Count.ShouldBe(0);
            p.p().Count.ShouldBe(1);
            p.p()[0].ShouldBe(0.0);
            p.P().Count.ShouldBe(2);
            p.P()[0].ShouldBe(0.0);
            p.P()[1].ShouldBe(0.0);
        }

        [TestCase(1.1)]
        [TestCase(-1.1)]
        [TestCase(0)]
        public void Polynomial_WithConstant_ReturnsConstantFunction(double c)
        {
            var p = new Polynomial(new List<double>() { c });
            p.dp().Count.ShouldBe(0);
            p.p().Count.ShouldBe(1);
            p.p()[0].ShouldBe(c);
            p.P().Count.ShouldBe(2);
            p.P()[0].ShouldBe(0.0);
            p.P()[1].ShouldBe(c);
        }

        [TestCase(1.0, 2.0)]
        [TestCase(-1.0, 2.0)]
        [TestCase(1.0, -2.0)]
        [TestCase(-1.0, -2.0)]
        [TestCase(-1.0, -1.0)]
        [TestCase(0.0, 0.0)]
        [TestCase(1.0, 1.0)]
        public void Polynomial_pWithRoot_ReturnsZero(double x0, double x1)
        {
            double a2, a1, a0;
            CreateEq(x0, x1, out a2, out a1, out a0);
            var p = new Polynomial(new List<double>() { a0, a1, a2 });

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
        public void Polynomial_IntegrateDerivative_ReturnsPolynomial(double x0, double x1)
        {
            double a2, a1, a0;
            CreateEq(x0, x1, out a2, out a1, out a0);
            var p = new Polynomial(new List<double>() { a0, a1, a2 });
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
        public void Polynomial_DerivativeIntegrate_ReturnsPolynomial(double x0, double x1)
        {
            double a2, a1, a0;
            CreateEq(x0, x1, out a2, out a1, out a0);
            var p = new Polynomial(new List<double>() { a0, a1, a2 });
            var res = new Polynomial(p.P()).dp();
            res.Count.ShouldBe(p.p().Count);
            for (var i = 0; i < res.Count; ++i)
                res[i].ShouldBe(p.p()[i]);
        }

        private void CreateEq(double x0, double x1, out double a, out double b, out double c)
        {
            a = 1.0;
            b = -(x0 + x1);
            c = x0 * x1;
        }
    }
}