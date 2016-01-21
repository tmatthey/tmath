using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;

namespace Math.Tests
{
    [TestFixture]
    public class ComparisonTests
    {
        private const double Epsilon = double.Epsilon;

        [TestCase(0, true)]
        [TestCase(Comparison.Epsilon, true)]
        [TestCase(-Comparison.Epsilon, true)]
        [TestCase(2.0 * Comparison.Epsilon, true)]
        [TestCase(-2.0 * Comparison.Epsilon, true)]
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
        [TestCase(2.0 * Comparison.Epsilon, false)]
        [TestCase(-2.0 * Comparison.Epsilon, false)]
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
        [TestCase(2.0 * Comparison.Epsilon, true)]
        [TestCase(-2.0 * Comparison.Epsilon, false)]
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
        [TestCase(2.0 * Comparison.Epsilon, false)]
        [TestCase(-2.0 * Comparison.Epsilon, true)]
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
        [TestCase(2.0 * Epsilon, false)]
        [TestCase(-2.0 * Epsilon, false)]
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
        [TestCase(2.0 * Epsilon, true)]
        [TestCase(-2.0 * Epsilon, false)]
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
        [TestCase(2.0 * Epsilon, false)]
        [TestCase(-2.0 * Epsilon, true)]
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

        [TestCase(12.0, 12.0, true)]
        [TestCase(12.0, 12.05, false)]
        [TestCase(0.0, 1e-13, false)]
        [TestCase(0.0, 1e-14, true)]
        [TestCase(12.0 + double.Epsilon * 2.0, 12.0 + double.Epsilon * 2.01, true)]
        public void IsEqual(double x, double y, bool expected)
        {
            var result = Comparison.IsEqual(x, y);
            result.ShouldBe(expected);
        }

        [TestCase(12.0, 12.0, 0.1, true)]
        [TestCase(12.0, 12.05, 0.1, true)]
        [TestCase(12.0, 12.05, double.Epsilon, false)]
        [TestCase(double.Epsilon * 2.0, double.Epsilon * 2.01, double.Epsilon, true)]
        public void IsEqualWithUserDefinedEpsilon(double x, double y, double eps, bool expected)
        {
            var result = Comparison.IsEqual(x, y, eps);
            result.ShouldBe(expected);
        }

        [Test]
        public void UniqueSorted()
        {
            var vec = new List<double>() { 1.02, 1.0, 1.0 + 1e-14, 3.0 };
            var res = Comparison.UniqueSorted(vec);
            res.Count.ShouldBe(3);
            res[0].ShouldBe(1.0);
            res[1].ShouldBe(1.02);
            res[2].ShouldBe(3.0);
        }

        [Test]
        public void UniqueSortedWithUserDefinedEpsilon()
        {
            var vec = new List<double>() { 1.02, 1.01, 0.99, 3.0 };
            var res = Comparison.UniqueSorted(vec, 0.1);
            res.Count.ShouldBe(2);
            res[0].ShouldBe(0.99);
            res[1].ShouldBe(3.0);
        }
    }
}