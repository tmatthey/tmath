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

using NUnit.Framework;
using Shouldly;

namespace Math.Tests
{
    [TestFixture]
    public class Circle3DTests
    {
        [TestCase(0.0)]
        [TestCase(1.0)]
        [TestCase(13.17)]
        [TestCase(-13.17)]
        public void Constructor_WithRadius_ZeroCenterAndSetRadius(double r)
        {
            var c = new Circle3D(r);
            c.Radius.ShouldBe(r);
            c.Center.ShouldBe(Vector3D.Zero);
            c.Normal.ShouldBe(Vector3D.E3);
        }

        [Test]
        public void Constructor_WithCenterRadius_CreatesExpected()
        {
            var r = 13.17;
            var v = new Vector3D(12, -14.3, 15);
            var n = new Vector3D(12.3, 14.3, 15);
            var c = new Circle3D(v, n, r);
            c.Radius.ShouldBe(r);
            c.Center.ShouldBe(v);
            c.Normal.ShouldBe(n);
        }

        [Test]
        public void Constructor_ZeroCenterAndRadius()
        {
            var c = new Circle3D();
            c.Radius.ShouldBe(0.0);
            c.Center.ShouldBe(Vector3D.Zero);
        }

        [Test]
        public void Create_WithOnePoint_returnsCorrectCircle()
        {
            var u = new Vector3D(0.1, 0.2, -45.8);
            var c = Circle3D.Create(u);
            c.Center.ShouldBe(u);
            c.Normal.ShouldBe(u);
            c.Radius.ShouldBe(0.0);
        }

        [Test]
        public void Create_WithThreePoints_returnsCorrectCircle()
        {
            var u = new Vector3D(0, 0,2);
            var v = new Vector3D(1, 1,2);
            var w = new Vector3D(2, 0,2);
            var c = Circle3D.Create(u, v, w);
            c.Center.ShouldBe(new Vector3D(1, 0, 2));
            c.Normal.ShouldBe(-Vector3D.E3);
            c.Radius.ShouldBe(1.0);
        }

        [Test]
        public void Create_WithThreePointsBigCircle_returnsCorrectCircle()
        {
            var u = new Vector3D(0, 0, 2);
            var v = new Vector3D(1, 1e-5, 2);
            var w = new Vector3D(2, 0, 2);
            var c = Circle3D.Create(u, v, w);
            c.Center.X.ShouldBe(1, 1e-7);
            c.Radius.ShouldBe(50000, 1e-5);
            c.Normal.ShouldBe(-Vector3D.E3);
        }

        [Test]
        public void Create_WithThreePointsCollinear_returnsCorrectCircleTwoPoints()
        {
            var u = new Vector3D(1, 2,2);
            var v = new Vector3D(2, 1, 2);
            var w = new Vector3D(1.5, 1.5, 2);
            var c = Circle3D.Create(u, v, w);
            c.Center.ShouldBe((u + v)*0.5);
            c.Radius.ShouldBe(System.Math.Sqrt(0.5));
        }

        [Test]
        public void Create_WithTwoPoints_returnsCorrectCircle()
        {
            var u = new Vector3D(1, 2, 2);
            var v = new Vector3D(2, 1, 2);
            var c = Circle3D.Create(u, v);
            c.Center.ShouldBe((u + v)*0.5);
            c.Radius.ShouldBe(System.Math.Sqrt(0.5));
        }

        [Test]
        public void Create_WithTwoSamePoints_returnsCorrectCenterZeroRadius()
        {
            var u = new Vector3D(1, 2, 2);
            var c = Circle3D.Create(u, u);
            c.Center.ShouldBe(u);
            c.Radius.ShouldBe(0.0);
        }

        [Test]
        public void Create_WithTwoSamePointsOneDiffrent_returnsCorrectCircleTwoPoints()
        {
            var u = new Vector3D(1, 2, 2);
            var v = new Vector3D(2, 1, 2);
            var c = Circle3D.Create(u, v, v);
            c.Center.ShouldBe((u + v)*0.5);
            c.Radius.ShouldBe(System.Math.Sqrt(0.5));
        }

        [Test]
        public void Equals_WithItself_ReturnsTrue()
        {
            var p = new Circle3D(new Vector3D(1, 2, 2), 0.2);
            p.Equals(p).ShouldBe(true);
        }

        [Test]
        public void Equals_WithNull_ReturnsFalse()
        {
            var p = new Circle3D(new Vector3D(1, 2, 2), new Vector3D(2, 1, 0), 0.2);
            Circle3D q = null;
            p.Equals(q).ShouldBe(false);
        }

        [Test]
        public void OpEqual_WithDiffrentRef_ReturnsTrue()
        {
            var p = new Circle3D(new Vector3D(1, 2, 2), new Vector3D(2, 1, 0), 0.2);
            var q = new Circle3D(new Vector3D(1, 2, 2), new Vector3D(2, 1, 0), 0.2);
            (p == q).ShouldBe(true);
        }

        [Test]
        public void Equals_WithDiffrentRef_ReturnsTrue()
        {
            var p = new Circle3D(new Vector3D(1, 2, 2), new Vector3D(2, 1, 0), 0.2);
            var q = new Circle3D(new Vector3D(1, 2, 2), new Vector3D(2, 1, 0), 0.2);
            p.Equals(q).ShouldBe(true);
        }

        [Test]
        public void OpEqual_WithDiffrentRefColinearNormal_ReturnsTrue()
        {
            var p = new Circle3D(new Vector3D(1, 2, 2), new Vector3D(2, 1, 0), 0.2);
            var q = new Circle3D(new Vector3D(1, 2, 2), new Vector3D(-4, -2, 0), 0.2);
            (p == q).ShouldBe(true);
        }

        [Test]
        public void OpNotEqual_NotEqualCenter_ReturnsTrue()
        {
            var p = new Circle3D(new Vector3D(1, 2, 2), new Vector3D(2, 1, 0), 0.2);
            var q = new Circle3D(new Vector3D(1, 2.01, 2), new Vector3D(2, 1, 0), 0.2);
            (p != q).ShouldBe(true);
        }

        [Test]
        public void OpNotEqual_NotEqualRadius_ReturnsTrue()
        {
            var p = new Circle3D(new Vector3D(1, 2, 2), new Vector3D(2, 1, 0), 0.2);
            var q = new Circle3D(new Vector3D(1, 2, 2), new Vector3D(2, 1, 0), 0.201);
            (p != q).ShouldBe(true);
        }
    }
}
