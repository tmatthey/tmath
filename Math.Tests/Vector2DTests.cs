/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2018 Thierry Matthey
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
    public class Vector2DTests
    {
        [TestCase(1.123)]
        [TestCase(-1.123)]
        [TestCase(0.0)]
        public void ConstructorWithConstant(double c)
        {
            var v = new Vector2D(c);
            v.X.ShouldBe(c);
            v.Y.ShouldBe(c);
        }

        [TestCase(false, false)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(true, true)]
        public void OpEqual_NullPointer_ReturnsExpected(bool a, bool b)
        {
            var x = a ? new Vector2D() : null;
            var y = b ? new Vector2D() : null;
            (x == y).ShouldBe(a == b);
        }

        [TestCase(false, false)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(true, true)]
        public void OpNotEqual_NullPointer_ReturnsExpected(bool a, bool b)
        {
            var x = a ? new Vector2D() : null;
            var y = b ? new Vector2D() : null;
            (x != y).ShouldBe(a != b);
        }

        [TestCase(-1)]
        [TestCase(2)]
        public void ArrayOp_WithOutOfBoundIndex_Throws(int i)
        {
            var v = new Vector2D();
            double x;
            Should.Throw<ArgumentException>(() => x = v[i]);
        }

        [TestCase(0, 13, 0)]
        [TestCase(179, 14, 179)]
        [TestCase(180, 15, 180)]
        [TestCase(181, 16, -179)]
        [TestCase(-179, 17, -179)]
        [TestCase(-180, 18, 180)]
        [TestCase(-181, 19, 179)]
        public void Angle(double a, double b, double expected)
        {
            a = Conversion.DegToRad(a);
            b = Conversion.DegToRad(b);
            var v0 = Vector2D.E1.Rotate(b);
            var v1 = Vector2D.E1.Rotate(a + b);
            var angle = Conversion.RadToDeg(v0.Angle(v1));
            angle.ShouldBe(expected, 1e-13);
        }

        [Test]
        public void Add()
        {
            var a = new Vector2D(1, 2);
            var b = new Vector2D(17.3, 19.2);

            var r = a.Add(b);

            r.X.ShouldBe(a.X + b.X);
            r.Y.ShouldBe(a.Y + b.Y);
        }

        [Test]
        public void Angle_Example()
        {
            var c = 180.0 / System.Math.PI;
            var v = new Vector2D(-0.8, -13.5);
            var w = new Vector2D(30.9, 45.6);
            var a0 = v.Angle(w) * c;
            var a1 = System.Math.Acos(v.Dot(w) / (v.Norm() * w.Norm())) * c;
            a0.ShouldBe(a1);
        }

        [Test]
        public void Angle_RotateDiagonalBy45_ReturnsId()
        {
            var expected = 45.0 / 180 * System.Math.PI;
            var v = new Vector2D(System.Math.Sqrt(0.5));
            var w = v.Rotate(expected);
            var angle = v.Angle(w);
            angle.ShouldBe(expected);
        }

        [Test]
        public void Angle_WithNeg45_returnsNeg45()
        {
            var v = new Vector2D(0, 1);
            var w = new Vector2D(1, 1);
            var a = v.Angle(w);
            a.ShouldBe(-System.Math.PI / 180.0 * 45.0, 1e-13);
        }

        [Test]
        public void Angle_WithPos45_ReturnsPos45()
        {
            var v = new Vector2D(1, 1);
            var w = new Vector2D(0, 1);
            var a = v.Angle(w);
            a.ShouldBe(System.Math.PI / 180.0 * 45.0);
        }

        [Test]
        public void AngleAbs_WithNeg45_returnsPos45()
        {
            var v = new Vector2D(0, 1);
            var w = new Vector2D(1, 1);
            var a = v.AngleAbs(w);
            a.ShouldBe(System.Math.PI / 180.0 * 45.0, 1e-13);
        }

        [Test]
        public void Array_ReturnsExpected()
        {
            const double x = 0.1;
            const double y = 0.2;
            var v = new Vector2D(x, y);
            var c = v.Array;
            c.Length.ShouldBe(v.Dimensions);
            c[0].ShouldBe(x);
            c[1].ShouldBe(y);
        }

        [Test]
        public void ArrayOp_ReturnsExpected()
        {
            const double x = 0.1;
            const double y = 0.2;
            var v = new Vector2D(x, y);
            v[0].ShouldBe(x);
            v[1].ShouldBe(y);
        }

        [Test]
        public void Clone()
        {
            var v = new Vector2D(1.1, 2.2);
            var w = (Vector2D) v.Clone();
            ReferenceEquals(v, w).ShouldBe(false);
            v.Equals(w).ShouldBe(true);
            v.IsEqual(w).ShouldBe(true);
            w.X.ShouldBe(v.X);
            w.Y.ShouldBe(v.Y);
        }

        [Test]
        public void ConstMulOpVector2D()
        {
            var a = 17.0;
            var b = 19.0;
            var d = 27.05;
            var u = new Vector2D(a, b);
            var w = u * d;
            w.X.ShouldBe(a * d);
            w.Y.ShouldBe(b * d);
        }

        [Test]
        public void ConstructorWithVector2D()
        {
            var a = 17.0;
            var b = 19.0;
            var u = new Vector2D();
            u.X = a;
            u.Y = b;
            var v = new Vector2D(u);
            v.X.ShouldBe(a);
            v.Y.ShouldBe(b);
        }

        [Test]
        public void Dimension_Is2()
        {
            var s = new Vector2D();
            s.Dimensions.ShouldBe(2);
        }

        [Test]
        public void Div()
        {
            var a = new Vector2D(1, 2);
            var b = 17.3;

            var r = a.Div(b);

            r.X.ShouldBe(a.X / b);
            r.Y.ShouldBe(a.Y / b);
        }

        [Test]
        public void Dot()
        {
            var a = 17.0;
            var b = 19.0;
            var d = 27.05;
            var e = 49.9;
            var u = new Vector2D(a, b);
            var v = new Vector2D(d, e);
            v.Dot(u).ShouldBe(a * d + b * e);
        }

        [Test]
        public void EmptyConstructor_NullVector2D()
        {
            var v = new Vector2D();
            v.X.ShouldBe(0);
            v.Y.ShouldBe(0);
        }

        [Test]
        public void Equals_SameRefVector2D_ReturnsTrue()
        {
            var v = new Vector2D(1, 2);
            var u = v;
            v.Equals(u).ShouldBe(true);
        }

        [Test]
        public void Equals_SameVector2D_ReturnsTrue()
        {
            var v = new Vector2D(1, 2);
            var u = new Vector2D(v);
            v.Equals(u).ShouldBe(true);
        }

        [Test]
        public void Equals_WihtNullptr_ReturnsFalse()
        {
            var v = new Vector2D(1, 2);
            Vector2D u = null;
            v.Equals(u).ShouldBe(false);
        }

        [Test]
        public void Euclidean()
        {
            var a = 17.0;
            var b = 19.0;
            var d = 27.05;
            var e = 49.9;
            var u = new Vector2D(a, b);
            var v = new Vector2D(d, e);
            var x = a - d;
            var y = b - e;
            v.EuclideanNorm(u).ShouldBe(System.Math.Sqrt(x * x + y * y));
        }

        [Test]
        public void Interpolate()
        {
            var v0 = new Vector2D(1, 2);
            var v1 = new Vector2D(4, 8);
            var expected = new Vector2D(2, 4);
            v0.Interpolate(v1, 1.0 / 3.0).ShouldBe(expected);
        }

        [Test]
        public void ModifiedNorm()
        {
            var a = 17.0;
            var b = 19.0;
            var d = 27.05;
            var e = 49.9;
            var u = new Vector2D(a, b);
            var v = new Vector2D(d, e);
            var x = a - d;
            var y = b - e;
            v.ModifiedNorm(u).ShouldBe(System.Math.Sqrt(x * x + y * y));
        }

        [Test]
        public void Mul()
        {
            var a = new Vector2D(1, 2);
            var b = 17.3;

            var r = a.Mul(b);

            r.X.ShouldBe(a.X * b);
            r.Y.ShouldBe(a.Y * b);
        }

        [Test]
        public void NegationOp()
        {
            var a = 17.0;
            var b = 19.0;
            var u = new Vector2D(a, b);
            var w = -u;
            w.X.ShouldBe(-a);
            w.Y.ShouldBe(-b);
        }

        [Test]
        public void Norm()
        {
            var a = -17.0;
            var b = -19.0;
            var v = new Vector2D(a, b);
            v.Norm().ShouldBe(System.Math.Sqrt(a * a + b * b));
        }

        [Test]
        public void Norm_CloseMaxDouble_returnsExpected()
        {
            var a = double.MaxValue / 2.0;
            var v = new Vector2D(a);
            v.Norm().ShouldBe(System.Math.Sqrt(2.0) * a);
        }

        [Test]
        public void Norm2()
        {
            var a = 17.0;
            var b = 19.0;
            var v = new Vector2D(a, b);
            v.Norm2().ShouldBe(a * a + b * b);
        }

        [Test]
        public void Normalize()
        {
            var a = 17.0;
            var b = 19.0;
            var v = new Vector2D(a, b);
            v.Normalize();
            v.Norm().ShouldBe(1.0, Comparison.Epsilon);
        }

        [Test]
        public void Normalize_ZeroVector2D_ZeroVector2D()
        {
            var v = new Vector2D();
            v.Normalize();
            v.Norm().ShouldBe(0.0);
        }

        [Test]
        public void Normalized()
        {
            var a = 17.0;
            var b = 19.0;
            var v = new Vector2D(a, b);
            var u = v.Normalized();
            u.Norm().ShouldBe(1.0, Comparison.Epsilon);
        }

        [Test]
        public void Normalized_ZeroVector2D_ReturnsZeroVector2D()
        {
            var v = new Vector2D();
            var u = v.Normalized();
            u.Norm().ShouldBe(0.0);
        }

        [Test]
        public void NotSameVector2DIsNotEqualOp_ReturnsTrue()
        {
            var v = new Vector2D(1, 2);
            var u = new Vector2D(1, 2.1);
            (v != u).ShouldBe(true);
        }

        [Test]
        public void OpEqual_WithSameVector_ReturnsTrue()
        {
            var v = new Vector2D(1, 2);
            var u = new Vector2D(v);
            (v == u).ShouldBe(true);
        }

        [Test]
        public void Rotate_DiagonalBy45_ReturnsE2()
        {
            var v = new Vector2D(System.Math.Sqrt(0.5));
            var w = v.Rotate(45.0 / 180 * System.Math.PI);
            w.ShouldBe(Vector2D.E2);
        }

        [Test]
        public void Sub()
        {
            var a = new Vector2D(1, 2);
            var b = new Vector2D(17.3, 19.2);

            var r = a.Sub(b);

            r.X.ShouldBe(a.X - b.X);
            r.Y.ShouldBe(a.Y - b.Y);
        }

        [Test]
        public void ToGpsPointToVector2D_ReturnsId()
        {
            var v = new Vector2D(123.45, 567.89);
            var center = Vector3D.One;
            var g = v.ToGpsPoint(center);
            var res = g.ToVector2D(center);
            res.EuclideanNorm(v).ShouldBeLessThan(1e-5);
        }

        [Test]
        public void Vector2DAddOpVector2D()
        {
            var a = 17.0;
            var b = 19.0;
            var d = 27.05;
            var e = 49.9;
            var u = new Vector2D(a, b);
            var v = new Vector2D(d, e);
            var w = u + v;

            w.X.ShouldBe(a + d);
            w.Y.ShouldBe(b + e);
        }

        [Test]
        public void Vector2DDivOpConst()
        {
            var a = 17.0;
            var b = 19.0;
            var d = 27.05;
            var u = new Vector2D(a, b);
            var w = u / d;
            w.X.ShouldBe(a / d);
            w.Y.ShouldBe(b / d);
        }

        [Test]
        public void Vector2DMulOpConst()
        {
            var a = 17.0;
            var b = 19.0;
            var d = 27.05;
            var u = new Vector2D(a, b);
            var w = d * u;
            w.X.ShouldBe(a * d);
            w.Y.ShouldBe(b * d);
        }

        [Test]
        public void Vector2DMulOpVector2D()
        {
            var a = 17.0;
            var b = 19.0;
            var d = 27.05;
            var e = 49.9;
            var u = new Vector2D(a, b);
            var v = new Vector2D(d, e);
            var w = u * v;
            w.ShouldBe(u.Dot(v));
        }

        [Test]
        public void Vector2DNaN()
        {
            Vector2D.NaN.X.ShouldBe(double.NaN);
            Vector2D.NaN.Y.ShouldBe(double.NaN);
        }

        [Test]
        public void Vector2DNegativeInfinity()
        {
            Vector2D.NegativeInfinity.X.ShouldBe(double.NegativeInfinity);
            Vector2D.NegativeInfinity.Y.ShouldBe(double.NegativeInfinity);
        }

        [Test]
        public void Vector2DPositiveInfinity()
        {
            Vector2D.PositiveInfinity.X.ShouldBe(double.PositiveInfinity);
            Vector2D.PositiveInfinity.Y.ShouldBe(double.PositiveInfinity);
        }

        [Test]
        public void Vector2DSubOpVector2D()
        {
            var a = 17.0;
            var b = 19.0;
            var d = 27.05;
            var e = 49.9;
            var u = new Vector2D(a, b);
            var v = new Vector2D(d, e);
            var w = u - v;

            w.X.ShouldBe(a - d);
            w.Y.ShouldBe(b - e);
        }

        [Test]
        public void Vector2DZero()
        {
            Vector2D.Zero.X.ShouldBe(0);
            Vector2D.Zero.Y.ShouldBe(0);
        }

        [Test]
        public void Vector2One()
        {
            Vector2D.One.X.ShouldBe(1);
            Vector2D.One.Y.ShouldBe(1);
        }
    }
}