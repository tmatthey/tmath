using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests
{
    [TestFixture]
    public class ComparisonlTests
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
    }
}