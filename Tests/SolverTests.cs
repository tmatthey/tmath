using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Linq;

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
        public void Linear_ValidInput_ReturnsOneSolution(double a, double b)
        {
            var x = Math.Solver.Linear(a, b);
            var y = a * x + b;
            y.ShouldBe(0.0);
        }

        [Test]
        public void Linear_InvalidInput_ReturnsNaN()
        {
            var x = Math.Solver.Linear(0.0, 1.0);
            x.ShouldBe(double.NaN);
        }

        [TestCase(1.0)]
        [TestCase(-1.0)]
        [TestCase(13.17)]
        [TestCase(-13.17)]
        public void Quadratic_OneRoot_ReturnsOneSolution(double x)
        {
            double a, b, c;
            CreateEq(x, x, out a, out b, out c);

            var root = Math.Solver.Quadratic(a, b, c);
            root.Count.ShouldBe(1);
            root[0].ShouldBe(x);
        }

        [TestCase(1.0, 13.1, 1.0)]
        [TestCase(-1.0, 13.1, 1.1)]
        [TestCase(13.17, -13.1, 1.2)]
        [TestCase(-13.17, -13.1, -1.1)]
        public void Quadratic_TwoRoots_ReturnsTwoSolutions(double x0, double x1, double f)
        {
            double a, b, c;
            CreateEq(x0, x1, out a, out b, out c);
            a *= f;
            b *= f;
            c *= f;
            var root = Math.Solver.Quadratic(a, b, c);
            root.Count.ShouldBe(2);
            root[0].ShouldBe(System.Math.Min(x0, x1), Epsilon);
            root[1].ShouldBe(System.Math.Max(x0, x1), Epsilon);
        }

        [TestCase(1.2, 2.3)]
        [TestCase(-1.2, 2.3)]
        [TestCase(1.2, -2.3)]
        [TestCase(-1.2, -2.3)]
        public void Quadratic_Linear_ReturnsOneSolution(double b, double c)
        {
            var a = 0.0;
            var root = Math.Solver.Quadratic(a, b, c);
            root.Count.ShouldBe(1);
            var y = b * root[0] + c;
            y.ShouldBe(0.0);
        }

        [Test]
        public void Quadratic_NoRoot_ReturnsEmptyList()
        {
            var root = Math.Solver.Quadratic(1, 0, 1);
            root.Count.ShouldBe(0);
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
        public void Qubic_ThreeDifferentRoots_ReturnsSolutions(double x0, double x1, double x2, double f)
        {
            var s = new List<double>() { x0, x1, x2 };
            s.Sort();
            double a, b, c, d;
            CreateEq(x0, x1, x2, out a, out b, out c, out d);
            a *= f;
            b *= f;
            c *= f;
            d *= f;

            var root = Math.Solver.Cubic(a, b, c, d);
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
        public void Qubic_TwoDifferentRoots_ReturnsSolutions(double x0, double x1, double f)
        {
            var s = new List<double>() { x0, x1 };
            s.Sort();
            double a, b, c, d;
            CreateEq(x0, x1, x1, out a, out b, out c, out d);
            a *= f;
            b *= f;
            c *= f;
            d *= f;

            var root = Math.Solver.Cubic(a, b, c, d);
            root.Count.ShouldBe(2);
            root[0].ShouldBe(s[0], Epsilon);
            root[1].ShouldBe(s[1], Epsilon);
        }

        [TestCase(-3.0, 1.4)]
        [TestCase(0.0, 1.4)]
        [TestCase(3.0, 1.4)]
        public void Qubic_OneDifferentRoots_ReturnsSolutions(double x0, double f)
        {
            var s = new List<double>() { x0 };
            s.Sort();
            double a, b, c, d;
            CreateEq(x0, x0, x0, out a, out b, out c, out d);
            a *= f;
            b *= f;
            c *= f;
            d *= f;

            var root = Math.Solver.Cubic(a, b, c, d);
            root.Count.ShouldBe(1);
            root[0].ShouldBe(s[0], Epsilon);
        }

        [Test]
        public void Qubic_OneSingleRoot_ReturnsSolutions()
        {
            var a = 1.0;
            var b = -9;
            var c = -9;
            var d = -10;
            var root = Math.Solver.Cubic(a, b, c, d);
            root.Count.ShouldBe(1);
            root[0].ShouldBe(10.0, Epsilon);
        }

        [Test]
        public void Qubic_Qudratic_ReturnsOneSolutions()
        {
            var a = 0.0;
            var b = 1;
            var c = -2;
            var d = 1;
            var root = Math.Solver.Cubic(a, b, c, d);
            root.Count.ShouldBe(1);
            root[0].ShouldBe(1, Epsilon);
        }

        [TestCase(-1.0, -2.0, 0.0, 1.0, 1.4)]
        [TestCase(1.0, 2.0, 0.0, -1.0, 1.4)]
        [TestCase(1.0, 2.0, 2.0, 1.0, 1.4)]
        [TestCase(1.0, 2.0, 2.0, 2.0, 1.4)]
        [TestCase(2.0, 2.0, 2.0, 2.0, 1.4)]
        public void Quartic_GivenRoots_ReturnsSolutions(double x0, double x1, double x2, double x3, double f)
        {
            var s = new List<double>() { x0, x1, x2, x3 };
            double a, b, c, d, e;
            CreateEq(x0, x1, x2, x3, out a, out b, out c, out d, out e);
            a *= f;
            b *= f;
            c *= f;
            d *= f;
            e *= f;

            s = s.Distinct().ToList<double>();
            s.Sort();
            var root = Math.Solver.Quartic(a, b, c, d, e);
            root.Count.ShouldBe(s.Count);
            if (s.Count > 0) root[0].ShouldBe(s[0], Epsilon);
            if (s.Count > 1) root[1].ShouldBe(s[1], Epsilon);
            if (s.Count > 2) root[2].ShouldBe(s[2], Epsilon);
            if (s.Count > 3) root[3].ShouldBe(s[3], Epsilon);
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

        private void CreateEq(double x0, double x1, double x2, double x3, out double a, out double b, out double c, out double d, out double e)
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
    }
}