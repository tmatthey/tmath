using NUnit.Framework;
using Shouldly;

namespace Math.Tests.Function
{
    [TestFixture]
    public class AlgebraicTests
    {
        [TestCase(27.1)]
        [TestCase(0.13)]
        [TestCase(0.0)]
        [TestCase(-27.1)]
        [TestCase(-0.13)]
        public void Cbrt_WithNumber_ReturnsExpectedResult(double x)
        {
            var a = Math.Function.Cbrt(x);
            var a3 = a * a * a;
            a3.ShouldBe(x, 1e-13);
        }

        [TestCase(double.NaN)]
        [TestCase(double.NegativeInfinity)]
        [TestCase(double.PositiveInfinity)]
        public void Cbrt_WithNotNumber_ReturnsInput(double x)
        {
            var a = Math.Function.Cbrt(x);
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
            var a = Math.Function.Qnrt(x);
            var a3 = a * a * a * a * a;
            a3.ShouldBe(x, 1e-13);
        }

        [TestCase(double.NaN)]
        [TestCase(double.NegativeInfinity)]
        [TestCase(double.PositiveInfinity)]
        public void Qnrt_WithNotNumber_ReturnsInput(double x)
        {
            var a = Math.Function.Qnrt(x);
            var a3 = a * a * a * a * a;
            a3.ShouldBe(x);
        }
    }
}