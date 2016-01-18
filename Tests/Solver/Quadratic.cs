using Shouldly;
using NUnit.Framework;

namespace Math.UnitTest.Tests
{
    [TestFixture]
    public class Quadratic
    {
        [Test]       
        public void OneRoot_ReturnsOneSolution()
        {
            double a = 1.0;
            double b = 2.0;
            double c = 1.0;

            var root = Solver.Quadratic(a, b, c);
            root.Count.ShouldBe(1);
        }
    }
}
