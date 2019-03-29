/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2019 Thierry Matthey
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

using NUnit.Framework;
using Shouldly;

namespace Math.Tests
{
    [TestFixture]
    public class Circle2DTests
    {
        [TestCase(0.0)]
        [TestCase(1.0)]
        [TestCase(13.17)]
        [TestCase(-13.17)]
        public void Constructor_WithRadius_ZeroCenterAndSetRadius(double r)
        {
            var c = new Circle2D(r);
            c.Radius.ShouldBe(r);
            c.Center.ShouldBe(Vector2D.Zero);
        }

        [TestCase(2, false)]
        [TestCase(1.10001, false)]
        [TestCase(1.1, true)]
        [TestCase(1, true)]
        [TestCase(0, true)]
        [TestCase(-1, true)]
        [TestCase(-1.1, true)]
        [TestCase(-1.10001, false)]
        [TestCase(-2, false)]
        public void IsInside_returnsExpected(double y, bool inside)
        {
            var v = new Vector2D(-13.4, 0.0);
            var c = new Circle2D(v, 1.1);
            var u = new Vector2D(v.X, y);
            c.IsInside(u).ShouldBe(inside);
        }

        [TestCase(false, false)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(true, true)]
        public void OpEqual_NullPointer_ReturnsExpected(bool a, bool b)
        {
            var x = a ? new Circle2D() : null;
            var y = b ? new Circle2D() : null;
            (x == y).ShouldBe(a == b);
        }

        [TestCase(false, false)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(true, true)]
        public void OpNotEqual_NullPointer_ReturnsExpected(bool a, bool b)
        {
            var x = a ? new Circle2D() : null;
            var y = b ? new Circle2D() : null;
            (x != y).ShouldBe(a != b);
        }

        [TestCase(3, 3, 0)]
        [TestCase(1, 1, 0)]
        [TestCase(0.5, 0.1, 1.4)]
        public void Euclidean(double r0, double r1, double expected)
        {
            var c0 = new Circle2D(Vector2D.Zero, r0);
            var c1 = new Circle2D(Vector2D.E1 * 2, r1);
            c0.EuclideanNorm(c1).ShouldBe(expected);
        }

        [TestCase(3, 3, 0)]
        [TestCase(1, 1, 0)]
        [TestCase(0.5, 0.1, 1.4)]
        public void ModifiedNorm(double r0, double r1, double expected)
        {
            var c0 = new Circle2D(Vector2D.Zero, r0);
            var c1 = new Circle2D(Vector2D.E1 * 2, r1);
            c0.ModifiedNorm(c1).ShouldBe(expected);
        }

        [Test]
        public void Center_IsCopied()
        {
            var center = new Vector2D(1, 2);
            var p = new Circle2D(center, 0.2);
            p.Center.X = 0;
            center.X.ShouldBe(1);
        }

        [Test]
        public void Clone()
        {
            var u = new Vector2D(1.2, 2.3);
            var a = new Circle2D(u, 19.17);
            var b = (Circle2D) a.Clone();
            ReferenceEquals(a, b).ShouldBe(false);
            ReferenceEquals(a.Center, b.Center).ShouldBe(false);
            a.Equals(b).ShouldBe(true);
            a.Center.Equals(b.Center).ShouldBe(true);
            a.IsEqual(b).ShouldBe(true);
            a.Center.IsEqual(b.Center).ShouldBe(true);
        }

        [Test]
        public void Constructor_WithCenterRadius_CreatesExpected()
        {
            var r = 13.17;
            var v = new Vector2D(12, -14.3);
            var c = new Circle2D(v, r);
            c.Radius.ShouldBe(r);
            c.Center.ShouldBe(v);
        }

        [Test]
        public void Constructor_ZeroCenterAndRadius()
        {
            var c = new Circle2D();
            c.Radius.ShouldBe(0.0);
            c.Center.ShouldBe(Vector2D.Zero);
        }

        [Test]
        public void Create_WithOnePoint_returnsCorrectCircle()
        {
            var u = new Vector2D(1, 3.3);
            var c = Circle2D.Create(u);
            c.Center.ShouldBe(u);
            c.Radius.ShouldBe(0.0);
        }

        [Test]
        public void Create_WithThreePoints_returnsCorrectCircle()
        {
            var u = new Vector2D(0, 0);
            var v = new Vector2D(1, 1);
            var w = new Vector2D(2, 0);
            var c = Circle2D.Create(u, v, w);
            c.Center.ShouldBe(new Vector2D(1, 0));
            c.Radius.ShouldBe(1.0);
        }

        [Test]
        public void Create_WithThreePointsBigCircle_returnsCorrectCircle()
        {
            var u = new Vector2D(0, 0);
            var v = new Vector2D(1, 1e-10);
            var w = new Vector2D(2, 0);
            var c = Circle2D.Create(u, v, w);
            c.Center.X.ShouldBe(1, 1e-13);
            c.Radius.ShouldBe(5000000000);
        }

        [Test]
        public void Create_WithThreePointsCollinear_returnsCorrectCircleTwoPoints()
        {
            var u = new Vector2D(1, 2);
            var v = new Vector2D(2, 1);
            var w = new Vector2D(1.5, 1.5);
            var c = Circle2D.Create(u, v, w);
            c.Center.ShouldBe((u + v) * 0.5);
            c.Radius.ShouldBe(System.Math.Sqrt(0.5));
        }

        [Test]
        public void Create_WithTwoPoints_returnsCorrectCircle()
        {
            var u = new Vector2D(1, 2);
            var v = new Vector2D(2, 1);
            var c = Circle2D.Create(u, v);
            c.Center.ShouldBe((u + v) * 0.5);
            c.Radius.ShouldBe(System.Math.Sqrt(0.5));
        }

        [Test]
        public void Create_WithTwoSamePoints_returnsCorrectCenterZeroRadius()
        {
            var u = new Vector2D(1, 2);
            var c = Circle2D.Create(u, u);
            c.Center.ShouldBe(u);
            c.Radius.ShouldBe(0.0);
        }

        [Test]
        public void Create_WithTwoSamePointsOneDiffrent_returnsCorrectCircleTwoPoints()
        {
            var u = new Vector2D(1, 2);
            var v = new Vector2D(2, 1);
            var c = Circle2D.Create(u, v, v);
            c.Center.ShouldBe((u + v) * 0.5);
            c.Radius.ShouldBe(System.Math.Sqrt(0.5));
        }

        [Test]
        public void Dimension_Is2()
        {
            var v = new Circle2D();
            v.Dimensions.ShouldBe(2);
        }

        [Test]
        public void Equals_WithDiffrentRef_ReturnsTrue()
        {
            var p = new Circle2D(new Vector2D(1, 2), 0.2);
            var q = new Circle2D(new Vector2D(1, 2), 0.2);
            p.Equals(q).ShouldBe(true);
        }

        [Test]
        public void Equals_WithItself_ReturnsTrue()
        {
            var p = new Circle2D(new Vector2D(1, 2), 0.2);
            p.Equals(p).ShouldBe(true);
        }

        [Test]
        public void Equals_WithNull_ReturnsFalse()
        {
            var p = new Circle2D(new Vector2D(1, 2), 0.2);
            Circle2D q = null;
            p.Equals(q).ShouldBe(false);
        }

        [Test]
        public void GetHashCode_DifferentObjects_ReturnsDifferentHashCode()
        {
            var p = new Circle2D(new Vector2D(1, 2), 0.2);
            var q = new Circle2D(new Vector2D(1, 2), 0.201);
            p.GetHashCode().ShouldNotBe(q.GetHashCode());
        }

        [Test]
        public void OpEqual_WithDiffrentRef_ReturnsTrue()
        {
            var p = new Circle2D(new Vector2D(1, 2), 0.2);
            var q = new Circle2D(new Vector2D(1, 2), 0.2);
            (p == q).ShouldBe(true);
        }

        [Test]
        public void OpNotEqual_NotEqualCenter_ReturnsTrue()
        {
            var p = new Circle2D(new Vector2D(1, 2), 0.2);
            var q = new Circle2D(new Vector2D(1, 2.01), 0.2);
            (p != q).ShouldBe(true);
        }

        [Test]
        public void OpNotEqual_NotEqualRadius_ReturnsTrue()
        {
            var p = new Circle2D(new Vector2D(1, 2), 0.2);
            var q = new Circle2D(new Vector2D(1, 2), 0.201);
            (p != q).ShouldBe(true);
        }
    }
}