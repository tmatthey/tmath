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

using System;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests
{
    [TestFixture]
    public class Segment3DTests
    {
        [TestCase(-1)]
        [TestCase(6)]
        public void ArrayOp_WithOutOfBoundIndex_Throws(int i)
        {
            var s = new Segment3D();
            Should.Throw<ArgumentException>(() => s[i]);
        }

        [TestCase(-1, -1, 0, 0, 0, 0, 1, 1, true)]
        [TestCase(-1, 0, 0, 0, 0, 0, 1, 1, true)]
        [TestCase(0, 0, -1, -1, 0, 1, 1, 0, false)]
        [TestCase(0, 0, 1, -1, 0, 1, 1, 0, false)]
        [TestCase(0, 0, 1, 1, 0, 1, 1, 0, true)]
        [TestCase(0, 0, 1, 1, 0.1, 0.0, 0.1, 0.2, true)]
        [TestCase(0, 0, 1, 1, 0.1, 0.1, 0.1, 0.1, true)]
        [TestCase(0, 0, 1, 1, 0.1, 0.1, 0.1, 0.2, true)]
        [TestCase(0, 0, 1, 1, 0.1, 0.2, 0.1, 0.0, true)]
        [TestCase(0, 0, 1, 1, 0.1, 0.2, 0.1, 0.1, true)]
        [TestCase(0, 0, 1, 1, 0.1, 0.2, 0.1, 0.2, false)]
        [TestCase(0, 1, 1, 0, 0, 0, -1, -1, false)]
        [TestCase(0, 1, 1, 0, 0, 0, 1, -1, false)]
        [TestCase(0, 1, 1, 0, 0.1, 0.1, -1, -1, false)]
        [TestCase(0.1, 0.0, 0.1, 0.2, 0, 0, 1, 1, true)]
        [TestCase(0.1, 0.1, -1, -1, 0, 1, 1, 0, false)]
        [TestCase(0.1, 0.1, 0.1, 0.1, 0, 0, 1, 1, true)]
        [TestCase(0.1, 0.1, 0.1, 0.2, 0, 0, 1, 1, true)]
        [TestCase(0.1, 0.2, 0.1, 0.0, 0, 0, 1, 1, true)]
        [TestCase(0.1, 0.2, 0.1, 0.1, 0, 0, 1, 1, true)]
        [TestCase(0.1, 0.2, 0.1, 0.2, 0, 0, 1, 1, false)]
        [TestCase(2, 2, -1, -1, 0, 0, 1, 1, true)]
        [TestCase(2, 3, -1, 0, 0, 0, 1, 1, false)]
        public void IsIntersecting_SamePlane(double x0, double y0, double x1, double y1, double u0, double v0,
            double u1,
            double v1, bool intersects)
        {
            var c = 13.7;
            var s0 = new Segment3D(new Vector3D(x0, y0, c), new Vector3D(x1, y1, c));
            var s1 = new Segment3D(new Vector3D(u0, v0, c), new Vector3D(u1, v1, c));
            s0.IsIntersecting(s1).ShouldBe(intersects);
        }

        [TestCase(-1, -1, 0, 0, 0, 0, 1, 1)]
        [TestCase(-1, 0, 0, 0, 0, 0, 1, 1)]
        [TestCase(0, 0, -1, -1, 0, 1, 1, 0)]
        [TestCase(0, 0, 1, -1, 0, 1, 1, 0)]
        [TestCase(0, 0, 1, 1, 0, 1, 1, 0)]
        [TestCase(0, 0, 1, 1, 0.1, 0.0, 0.1, 0.2)]
        [TestCase(0, 0, 1, 1, 0.1, 0.1, 0.1, 0.1)]
        [TestCase(0, 0, 1, 1, 0.1, 0.1, 0.1, 0.2)]
        [TestCase(0, 0, 1, 1, 0.1, 0.2, 0.1, 0.0)]
        [TestCase(0, 0, 1, 1, 0.1, 0.2, 0.1, 0.1)]
        [TestCase(0, 0, 1, 1, 0.1, 0.2, 0.1, 0.2)]
        [TestCase(0, 1, 1, 0, 0, 0, -1, -1)]
        [TestCase(0, 1, 1, 0, 0, 0, 1, -1)]
        [TestCase(0, 1, 1, 0, 0.1, 0.1, -1, -1)]
        [TestCase(0.1, 0.0, 0.1, 0.2, 0, 0, 1, 1)]
        [TestCase(0.1, 0.1, -1, -1, 0, 1, 1, 0)]
        [TestCase(0.1, 0.1, 0.1, 0.1, 0, 0, 1, 1)]
        [TestCase(0.1, 0.1, 0.1, 0.2, 0, 0, 1, 1)]
        [TestCase(0.1, 0.2, 0.1, 0.0, 0, 0, 1, 1)]
        [TestCase(0.1, 0.2, 0.1, 0.1, 0, 0, 1, 1)]
        [TestCase(0.1, 0.2, 0.1, 0.2, 0, 0, 1, 1)]
        [TestCase(2, 2, -1, -1, 0, 0, 1, 1)]
        [TestCase(2, 3, -1, 0, 0, 0, 1, 1)]
        public void IsIntersecting_NotSamePlane_AlwaysFalse(double x0, double y0, double x1, double y1, double u0,
            double v0, double u1,
            double v1)
        {
            var c = 13.7;
            var d = c + 0.1;
            var s0 = new Segment3D(new Vector3D(x0, y0, c), new Vector3D(x1, y1, c));
            var s1 = new Segment3D(new Vector3D(u0, v0, d), new Vector3D(u1, v1, d));
            s0.IsIntersecting(s1).ShouldBe(false);
        }

        [TestCase(-1, -1, 0, 0, 0, 0, 1, 1, true)]
        [TestCase(-1, 0, 0, 0, 0, 0, 1, 1, true)]
        [TestCase(0, 0, -1, -1, 0, 1, 1, 0, false)]
        [TestCase(0, 0, 1, -1, 0, 1, 1, 0, false)]
        [TestCase(0, 0, 1, 1, 0, 1, 1, 0, true)]
        [TestCase(0, 0, 1, 1, 0.1, 0.0, 0.1, 0.2, true)]
        [TestCase(0, 0, 1, 1, 0.1, 0.1, 0.1, 0.1, true)]
        [TestCase(0, 0, 1, 1, 0.1, 0.1, 0.1, 0.2, true)]
        [TestCase(0, 0, 1, 1, 0.1, 0.2, 0.1, 0.0, true)]
        [TestCase(0, 0, 1, 1, 0.1, 0.2, 0.1, 0.1, true)]
        [TestCase(0, 0, 1, 1, 0.1, 0.2, 0.1, 0.2, false)]
        [TestCase(0, 1, 1, 0, 0, 0, -1, -1, false)]
        [TestCase(0, 1, 1, 0, 0, 0, 1, -1, false)]
        [TestCase(0, 1, 1, 0, 0.1, 0.1, -1, -1, false)]
        [TestCase(0.1, 0.0, 0.1, 0.2, 0, 0, 1, 1, true)]
        [TestCase(0.1, 0.1, -1, -1, 0, 1, 1, 0, false)]
        [TestCase(0.1, 0.1, 0.1, 0.1, 0, 0, 1, 1, true)]
        [TestCase(0.1, 0.1, 0.1, 0.2, 0, 0, 1, 1, true)]
        [TestCase(0.1, 0.2, 0.1, 0.0, 0, 0, 1, 1, true)]
        [TestCase(0.1, 0.2, 0.1, 0.1, 0, 0, 1, 1, true)]
        [TestCase(0.1, 0.2, 0.1, 0.2, 0, 0, 1, 1, false)]
        [TestCase(2, 2, -1, -1, 0, 0, 1, 1, true)]
        [TestCase(2, 3, -1, 0, 0, 0, 1, 1, false)]
        public void IsIntersecting_NotSamePlaneAndBigEpsilon(double x0, double y0, double x1, double y1, double u0,
            double v0, double u1,
            double v1, bool intersects)
        {
            var eps = 0.001;
            var c = 13.7;
            var d = c + eps;
            var s0 = new Segment3D(new Vector3D(x0, y0, c), new Vector3D(x1, y1, c));
            var s1 = new Segment3D(new Vector3D(u0, v0, d), new Vector3D(u1, v1, d));
            s0.IsIntersecting(s1, eps * 2.0).ShouldBe(intersects);
        }

        [Test]
        public void Array_ReturnsExpected()
        {
            const double x0 = 0.1;
            const double y0 = 0.2;
            const double z0 = 0.3;
            const double x1 = 0.1;
            const double y1 = 0.2;
            const double z1 = 0.3;
            var s = new Segment3D(new Vector3D(x0, y0, z0), new Vector3D(x1, y1, z1));
            var c = s.Array;
            c.Length.ShouldBe(s.Dimensions * 2);
            c[0].ShouldBe(x0);
            c[1].ShouldBe(y0);
            c[2].ShouldBe(z0);
            c[3].ShouldBe(x1);
            c[4].ShouldBe(y1);
            c[5].ShouldBe(z1);
        }

        [Test]
        public void ArrayOp_ReturnsExpected()
        {
            const double x0 = 0.1;
            const double y0 = 0.2;
            const double z0 = 0.3;
            const double x1 = 0.1;
            const double y1 = 0.2;
            const double z1 = 0.3;
            var s = new Segment3D(new Vector3D(x0, y0, z0), new Vector3D(x1, y1, z1));
            s[0].ShouldBe(x0);
            s[1].ShouldBe(y0);
            s[2].ShouldBe(z0);
            s[3].ShouldBe(x1);
            s[4].ShouldBe(y1);
            s[5].ShouldBe(z1);
        }

        [Test]
        public void Clone()
        {
            var u = new Vector3D(1.2, 2.3, 3.4);
            var v = new Vector3D(1.1, 2.2, 3.3);
            var a = new Segment3D(u, v);
            var b = a.Clone();
            ReferenceEquals(a, b).ShouldBe(false);
            ReferenceEquals(a.A, b.A).ShouldBe(false);
            ReferenceEquals(a.B, b.B).ShouldBe(false);
            a.Equals(b).ShouldBe(true);
            a.A.Equals(b.A).ShouldBe(true);
            a.B.Equals(b.B).ShouldBe(true);
            a.IsEqual(b).ShouldBe(true);
            a.A.IsEqual(b.A).ShouldBe(true);
            a.B.IsEqual(b.B).ShouldBe(true);
        }

        [Test]
        public void ConstructorEmpty_ZeroVectors()
        {
            var s = new Segment3D();
            s.A.ShouldBe(Vector3D.Zero);
            s.B.ShouldBe(Vector3D.Zero);
        }

        [Test]
        public void ConstructorSegment_DefinedSegment()
        {
            var v0 = new Vector3D(1, 2, 4.5);
            var v1 = new Vector3D(3, 4, 6.7);

            var s = new Segment3D(new Segment3D(v0, v1));
            s.A.ShouldBe(v0);
            s.B.ShouldBe(v1);
        }

        [Test]
        public void ConstructorVectors_DefinedSegment()
        {
            var v0 = new Vector3D(1, 2, 3.4);
            var v1 = new Vector3D(3, 4, 5.6);

            var s = new Segment3D(v0, v1);
            s.A.ShouldBe(v0);
            s.B.ShouldBe(v1);
        }

        [Test]
        public void Dimension_Is3()
        {
            var s = new Segment3D();
            s.Dimensions.ShouldBe(3);
        }

        [Test]
        public void Equals_SameRefSegment3D_ReturnsTrue()
        {
            var v = new Segment3D(new Vector3D(1, 2, 3), new Vector3D(4, 5, 6));
            var u = v;
            v.Equals(u).ShouldBe(true);
        }

        [Test]
        public void Equals_SameSegment3D_ReturnsTrue()
        {
            var v = new Segment3D(new Vector3D(1, 2, 3), new Vector3D(4, 5, 6));
            var u = new Segment3D(v);
            v.Equals(u).ShouldBe(true);
        }

        [Test]
        public void Equals_WithNullptr_ReturnsFalse()
        {
            var v = new Segment3D(new Vector3D(1, 2, 3), new Vector3D(4, 5, 6));
            v.Equals(null).ShouldBe(false);
        }

        [Test]
        public void Euclidean_Cross()
        {
            var v0 = new Vector3D(-1, -1, -1);
            var v1 = new Vector3D(1.5, 1.5, 1.5);
            var v2 = new Vector3D(2, -2, 0);
            var v3 = new Vector3D(-3, 3, 0);

            var s0 = new Segment3D(v0, v1);
            var s1 = new Segment3D(v2, v3);
            s0.EuclideanNorm(s1).ShouldBe(0.0);
        }

        [Test]
        public void Euclidean_CrossPlanar()
        {
            var v0 = new Vector3D(-1.5, -1.5, 0);
            var v1 = new Vector3D(1, 1, 0);
            var v2 = new Vector3D(-1, 1, 1);
            var v3 = new Vector3D(2, -2, 1);

            var s0 = new Segment3D(v0, v1);
            var s1 = new Segment3D(v2, v3);
            s0.EuclideanNorm(s1).ShouldBe(1.0, 1e-13);
        }

        [Test]
        public void Euclidean_Diagonal()
        {
            var v0 = new Vector3D(0, 0, 0);
            var v1 = new Vector3D(1, 1, 1);
            var v2 = new Vector3D(2, 2, 2);
            var v3 = new Vector3D(3, 3, 3);

            var s0 = new Segment3D(v0, v1);
            var s1 = new Segment3D(v2, v3);
            s0.EuclideanNorm(s1).ShouldBe(v1.EuclideanNorm(v2));
        }

        [Test]
        public void Euclidean_Example()
        {
            var v0 = new Vector3D(1, 2, 7);
            var v1 = new Vector3D(3, 4, -9);
            var v2 = new Vector3D(7, 3, 13);
            var v3 = new Vector3D(19, 17, 17);
            var per = v0.EuclideanNorm(v2);

            var s0 = new Segment3D(v0, v1);
            var s1 = new Segment3D(v2, v3);
            s0.EuclideanNorm(s1).ShouldBe(per);
        }

        [Test]
        public void Euclidean_Orthogonal()
        {
            var v0 = new Vector3D(-1, 0, 0);
            var v1 = new Vector3D(1, 0, 0);
            var v2 = new Vector3D(0, 1, 0);
            var v3 = new Vector3D(0, 2, 0);

            var s0 = new Segment3D(v0, v1);
            var s1 = new Segment3D(v2, v3);
            s0.EuclideanNorm(s1).ShouldBe(1.0);
        }

        [Test]
        public void Euclidean_OrthogonalOpposite()
        {
            var v0 = new Vector3D(0, 1, 0);
            var v1 = new Vector3D(0, 2, 0);
            var v2 = new Vector3D(-1, 0, 0);
            var v3 = new Vector3D(1, 0, 0);

            var s0 = new Segment3D(v0, v1);
            var s1 = new Segment3D(v2, v3);
            s0.EuclideanNorm(s1).ShouldBe(1.0);
        }

        [Test]
        public void Euclidean_Point()
        {
            var v0 = new Vector3D(-1, 0, 17);
            var v1 = new Vector3D(1, 0, 0);
            var v2 = new Vector3D(2, 0, 0);

            var s0 = new Segment3D(v0, v1);
            var s1 = new Segment3D(v2, v2);
            s0.EuclideanNorm(s1).ShouldBe(1.0);
        }

        [Test]
        public void Euclidean_Touch()
        {
            var v0 = new Vector3D(-1, 0, -17);
            var v1 = new Vector3D(1, 0, 0);
            var v2 = new Vector3D(1, 0, 0);
            var v3 = new Vector3D(2, 2, 2);

            var s0 = new Segment3D(v0, v1);
            var s1 = new Segment3D(v2, v3);
            s0.EuclideanNorm(s1).ShouldBe(0.0);
        }

        [Test]
        public void GetHashCode_DifferentObjects_ReturnsDifferentHashCode()
        {
            var s0 = new Segment3D(Vector3D.E1, Vector3D.E2);
            var s1 = new Segment3D(Vector3D.E3, Vector3D.One);
            s0.GetHashCode().ShouldNotBe(s1.GetHashCode());
        }

        [Test]
        public void Length()
        {
            var v0 = new Vector3D(1, 2, 13);
            var v1 = new Vector3D(3, 4, 11);

            var s = new Segment3D(v0, v1);
            s.Length().ShouldBe(v0.EuclideanNorm(v1));
        }

        [Test]
        public void ModifiedNorm_ReturnsTrajectoryHausdorffDistance()
        {
            var v0 = new Vector3D(-1, -1, -1);
            var v1 = new Vector3D(1.5, 1.5, 1.5);
            var v2 = new Vector3D(2, -2, 0);
            var v3 = new Vector3D(-3, 3, 0);

            var s0 = new Segment3D(v0, v1);
            var s1 = new Segment3D(v2, v3);
            s0.ModifiedNorm(s1).ShouldBe(Geometry.TrajectoryHausdorffDistance(s0, s1));
        }

        [Test]
        public void Vector()
        {
            var v0 = new Vector3D(1, 2, 13);
            var v1 = new Vector3D(3, 4, 11);

            var s = new Segment3D(v0, v1);
            s.Vector().ShouldBe(v1 - v0);
        }

        [TestCase(1, 2, 3, 6, 9, 4, 0.5)]
        [TestCase(1, 24, 3, 6, 9, -84, 0.95)]
        public void Tangent(double x0, double y0, double z0, double x1, double y1, double z1, double p)
        {
            var a = new Vector3D(x0, y0, z0);
            var b = new Vector3D(x1, y1, z1);
            var s = new Segment3D(a, b);
            var t = s.Tangent(p);
            var expected = (b - a).Normalized();
            (t - expected).Norm().ShouldBeLessThan(1e-9);
        }

        [TestCase(1, 24, 3, 6, 9, -84, 0.95)]
        [TestCase(1, 2, 3, 6, 9, 4, 0.5)]
        public void Evaluate(double x0, double y0, double z0, double x1, double y1, double z1, double p)
        {
            var a = new Vector3D(x0, y0, z0);
            var b = new Vector3D(x1, y1, z1);
            var s = new Segment3D(a, b);
            var v = s.Evaluate(p);
            var expected = a * (1.0 - p) + b * p;
            (v - expected).Norm().ShouldBeLessThan(1e-9);
        }

        [TestCase(1, 2, 3, 6, 9, 4)]
        [TestCase(1, 24, 3, 6, 9, -84)]
        public void Evaluate_Start(double x0, double y0, double z0, double x1, double y1, double z1)
        {
            var a = new Vector3D(x0, y0, z0);
            var b = new Vector3D(x1, y1, z1);
            var s = new Segment3D(a, b);
            var v = s.Evaluate(0.0);
            (v - a).Norm().ShouldBeLessThan(1e-9);
        }

        [TestCase(1, 2, 3, 6, 9, 4)]
        [TestCase(1, 24, 3, 6, 9, -84)]
        public void Evaluate_End(double x0, double y0, double z0, double x1, double y1, double z1)
        {
            var a = new Vector3D(x0, y0, z0);
            var b = new Vector3D(x1, y1, z1);
            var s = new Segment3D(a, b);
            var v = s.Evaluate(1.0);
            (v - b).Norm().ShouldBeLessThan(1e-9);
        }

        [TestCase(1, 2, 3, 6, 9, 4, 0.5)]
        [TestCase(1, 24, 3, 6, 9, -84, 0.95)]
        public void dEvaluate(double x0, double y0, double z0, double x1, double y1, double z1, double p)
        {
            var a = new Vector3D(x0, y0, z0);
            var b = new Vector3D(x1, y1, z1);
            var s = new Segment3D(a, b);
            var v = s.dEvaluate(p);
            var expected = b - a;
            v.ShouldBe(expected);
        }

        [TestCase(1, 2, 3, 6, 9, 4, 0.5)]
        [TestCase(1, 24, 3, 6, 9, -84, 0.95)]
        public void d2Evaluate(double x0, double y0, double z0, double x1, double y1, double z1, double p)
        {
            var a = new Vector3D(x0, y0, z0);
            var b = new Vector3D(x1, y1, z1);
            var s = new Segment3D(a, b);
            var v = s.d2Evaluate(p);
            v.ShouldBe(Vector3D.Zero);
        }

        [TestCase(1, 2, 3, 6, 9, 4)]
        [TestCase(1, 24, 3, 6, 9, -84)]
        public void Length_TestCase(double x0, double y0, double z0, double x1, double y1, double z1)
        {
            var a = new Vector3D(x0, y0, z0);
            var b = new Vector3D(x1, y1, z1);
            var s = new Segment3D(a, b);
            var l = s.Length();
            l.ShouldBe((b - a).Norm());
        }

        [TestCase(1, 2, 3, 6, 9, 4, 0.5)]
        [TestCase(1, 24, 3, 6, 9, -84, 0.95)]
        public void Kappa(double x0, double y0, double z0, double x1, double y1, double z1, double p)
        {
            var a = new Vector3D(x0, y0, z0);
            var b = new Vector3D(x1, y1, z1);
            var s = new Segment3D(a, b);
            s.Kappa(p).ShouldBe(0);
        }

        [TestCase(0.5, 0.3)]
        [TestCase(0.5, 0.5)]
        [TestCase(0.3, 0.5)]
        [TestCase(-0.1, 0.5)]
        [TestCase(0, 0.5)]
        [TestCase(1, 0.5)]
        [TestCase(1.1, 0.5)]
        public void Split(double split, double t)
        {
            var bezier = new Segment3D()
            {
                A = new Vector3D(160, 120, 15),
                B = new Vector3D(200, 35, -31)
            };
            var p = bezier.Evaluate(t);

            var (b0, b1) = bezier.Split(split);
            if (Comparison.IsLessEqual(split, 0) || Comparison.IsLessEqual(1.0, split))
            {
                b0.IsEqual(bezier).ShouldBeTrue();
                b1.ShouldBeNull();
            }
            else if (Comparison.IsEqual(split, t))
            {
                (b0.Evaluate(1) - p).Norm().ShouldBeLessThan(1e-9);
                (b1.Evaluate(0) - p).Norm().ShouldBeLessThan(1e-9);
            }
            else if (t < split)
            {
                (b0.Evaluate(t / split) - p).Norm().ShouldBeLessThan(1e-9);
            }
            else
            {
                (b1.Evaluate((t - split) / (1.0 - split)) - p).Norm().ShouldBeLessThan(1e-9);
            }
        }
    }
}