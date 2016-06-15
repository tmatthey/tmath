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

using System;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests
{
    [TestFixture]
    public class Segment2DTests
    {
        [TestCase(-1)]
        [TestCase(4)]
        public void ArrayOp_WithOutOfBoundIndex_Throws(int i)
        {
            var s = new Segment2D();
            Should.Throw<IndexOutOfRangeException>(() =>
            {
                var a = s[i];
            });
        }

        [Test]
        public void Array_ReturnsExpected()
        {
            const double x0 = 0.1;
            const double y0 = 0.2;
            const double x1 = 0.1;
            const double y1 = 0.2;
            var s = new Segment2D(new Vector2D(x0, y0), new Vector2D(x1, y1));
            var c = s.Array;
            c.Length.ShouldBe(s.Dimensions*2);
            c[0].ShouldBe(x0);
            c[1].ShouldBe(y0);
            c[2].ShouldBe(x1);
            c[3].ShouldBe(y1);
        }

        [Test]
        public void ArrayOp_ReturnsExpected()
        {
            const double x0 = 0.1;
            const double y0 = 0.2;
            const double x1 = 0.1;
            const double y1 = 0.2;
            var s = new Segment2D(new Vector2D(x0, y0), new Vector2D(x1, y1));
            s[0].ShouldBe(x0);
            s[1].ShouldBe(y0);
            s[2].ShouldBe(x1);
            s[3].ShouldBe(y1);
        }

        [Test]
        public void ConstructorEmpty_ZeroVectors()
        {
            var s = new Segment2D();
            s.A.ShouldBe(Vector2D.Zero);
            s.B.ShouldBe(Vector2D.Zero);
        }

        [Test]
        public void ConstructorSegment_DefinedSegment()
        {
            var v0 = new Vector2D(1, 2);
            var v1 = new Vector2D(3, 4);

            var s = new Segment2D(new Segment2D(v0, v1));
            s.A.ShouldBe(v0);
            s.B.ShouldBe(v1);
        }

        [Test]
        public void ConstructorVectors_DefinedSegment()
        {
            var v0 = new Vector2D(1, 2);
            var v1 = new Vector2D(3, 4);

            var s = new Segment2D(v0, v1);
            s.A.ShouldBe(v0);
            s.B.ShouldBe(v1);
        }

        [Test]
        public void Dimension_Is2()
        {
            var s = new Segment2D();
            s.Dimensions.ShouldBe(2);
        }

        [Test]
        public void Euclidean_Cross()
        {
            var v0 = new Vector2D(-1.1, 0);
            var v1 = new Vector2D(1.2, 0);
            var v2 = new Vector2D(0, -3.3);
            var v3 = new Vector2D(0, 1.3);

            var s0 = new Segment2D(v0, v1);
            var s1 = new Segment2D(v2, v3);
            s0.EuclideanNorm(s1).ShouldBe(0.0);
        }

        [Test]
        public void Euclidean_Example()
        {
            var v0 = new Vector2D(1, 2);
            var v1 = new Vector2D(3, 4);
            var v2 = new Vector2D(7, 3);
            var v3 = new Vector2D(19, 17);
            var per = v1.EuclideanNorm(v2);

            var s0 = new Segment2D(v0, v1);
            var s1 = new Segment2D(v2, v3);
            s0.EuclideanNorm(s1).ShouldBe(per);
        }

        [Test]
        public void Euclidean_Orthogonal()
        {
            var v0 = new Vector2D(-1, 0);
            var v1 = new Vector2D(1, 0);
            var v2 = new Vector2D(0, 1);
            var v3 = new Vector2D(0, 2);

            var s0 = new Segment2D(v0, v1);
            var s1 = new Segment2D(v2, v3);
            s0.EuclideanNorm(s1).ShouldBe(1.0);
        }

        [Test]
        public void Euclidean_OrthogonalOpposite()
        {
            var v0 = new Vector2D(0, 1);
            var v1 = new Vector2D(0, 2);
            var v2 = new Vector2D(-1, 0);
            var v3 = new Vector2D(1, 0);

            var s0 = new Segment2D(v0, v1);
            var s1 = new Segment2D(v2, v3);
            s0.EuclideanNorm(s1).ShouldBe(1.0);
        }

        [Test]
        public void Euclidean_Parallel()
        {
            var v0 = new Vector2D(-1, 0);
            var v1 = new Vector2D(1, 0);
            var v2 = new Vector2D(1, 1);
            var v3 = new Vector2D(2, 1);

            var s0 = new Segment2D(v0, v1);
            var s1 = new Segment2D(v2, v3);
            s0.EuclideanNorm(s1).ShouldBe(1.0);
        }

        [Test]
        public void Euclidean_Point()
        {
            var v0 = new Vector2D(-1, 0);
            var v1 = new Vector2D(1, 0);
            var v2 = new Vector2D(2, 0);

            var s0 = new Segment2D(v0, v1);
            var s1 = new Segment2D(v2, v2);
            s0.EuclideanNorm(s1).ShouldBe(1.0);
        }

        [Test]
        public void Euclidean_Touch()
        {
            var v0 = new Vector2D(-1, 0);
            var v1 = new Vector2D(1, 0);
            var v2 = new Vector2D(1, 0);
            var v3 = new Vector2D(2, 2);

            var s0 = new Segment2D(v0, v1);
            var s1 = new Segment2D(v2, v3);
            s0.EuclideanNorm(s1).ShouldBe(0.0);
        }

        [Test]
        public void Length()
        {
            var v0 = new Vector2D(1, 2);
            var v1 = new Vector2D(3, 4);

            var s = new Segment2D(v0, v1);
            s.Length().ShouldBe(v0.EuclideanNorm(v1));
        }

        [Test]
        public void ModifiedNorm_ReturnsTrajectoryHausdorffDistance()
        {
            var v0 = new Vector2D(-1.1, 0);
            var v1 = new Vector2D(1.2, 0);
            var v2 = new Vector2D(0, -3.3);
            var v3 = new Vector2D(0, 1.3);

            var s0 = new Segment2D(v0, v1);
            var s1 = new Segment2D(v2, v3);
            s0.ModifiedNorm(s1).ShouldBe(Geometry.TrajectoryHausdorffDistance(s0, s1));
        }

        [Test]
        public void Vector()
        {
            var v0 = new Vector2D(1, 2);
            var v1 = new Vector2D(3, 4);

            var s = new Segment2D(v0, v1);
            s.Vector().ShouldBe(v1 - v0);
        }
    }
}