/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2024 Thierry Matthey
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
    public class Vector3DTests
    {
        [TestCase(1.123)]
        [TestCase(-1.123)]
        [TestCase(0.0)]
        public void ConstructorWithConstant_ConstantVector(double c)
        {
            var v = new Vector3D(c);
            v.X.ShouldBe(c);
            v.Y.ShouldBe(c);
            v.Z.ShouldBe(c);
        }

        [TestCase(0, -1, 1, 1, 1, -3, 0.1)]
        [TestCase(1, 0, 1, 1, 0, -3, 0.1)]
        [TestCase(1, -1, -1, 1, 1, 1.1, 0.1)]
        [TestCase(1, 0, 1, 1, -1, -12, 0.1)]
        public void Rotate_WithAxisAndAngle_AngleAsExpected(double vx, double vy, double vz,
            double rx, double ry, double rz,
            double a)
        {
            var r = new Vector3D(rx, ry, rz);
            var v = new Vector3D(vx, vy, vz) ^ r;
            var res = v.Rotate(r, a);
            var angle = v.Angle(res);
            angle.ShouldBe(Function.NormalizeAngle(a), 1e-13);
        }

        [TestCase(-3.5)]
        [TestCase(-3.0)]
        [TestCase(-0.1)]
        [TestCase(0)]
        [TestCase(0.1)]
        [TestCase(3.0)]
        [TestCase(3.5)]
        public void RotateE1_EqualToRotateWithAngle(double a)
        {
            var v = new Vector3D(1.13, 2.22, 3.31);
            var r = v.RotateE1(a);
            var e = v.Rotate(Vector3D.E1, a);
            r.ShouldBe(e);
        }

        [TestCase(-3.5)]
        [TestCase(-3.0)]
        [TestCase(-0.1)]
        [TestCase(0)]
        [TestCase(0.1)]
        [TestCase(3.0)]
        [TestCase(3.5)]
        public void RotateE2_EqualToRotateWithAngle(double a)
        {
            var v = new Vector3D(1.13, 2.22, 3.31);
            var r = v.RotateE2(a);
            var e = v.Rotate(Vector3D.E2, a);
            r.ShouldBe(e);
        }

        [TestCase(-3.5)]
        [TestCase(-3.0)]
        [TestCase(-0.1)]
        [TestCase(0)]
        [TestCase(0.1)]
        [TestCase(3.0)]
        [TestCase(3.5)]
        public void RotateE3_EqualToRotateWithAngle(double a)
        {
            var v = new Vector3D(1.13, 2.22, 3.31);
            var r = v.RotateE3(a);
            var e = v.Rotate(Vector3D.E3, a);
            r.ShouldBe(e);
        }

        [TestCase(false, false)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(true, true)]
        public void OpEqual_NullPointer_ReturnsExpected(bool a, bool b)
        {
            var x = a ? new Vector3D() : null;
            var y = b ? new Vector3D() : null;
            (x == y).ShouldBe(a == b);
        }

        [TestCase(false, false)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(true, true)]
        public void OpNotEqual_NullPointer_ReturnsExpected(bool a, bool b)
        {
            var x = a ? new Vector3D() : null;
            var y = b ? new Vector3D() : null;
            (x != y).ShouldBe(a != b);
        }

        [TestCase(-1)]
        [TestCase(3)]
        public void ArrayOp_WithOutOfBoundIndex_Throws(int i)
        {
            var v = new Vector3D();
            Should.Throw<ArgumentException>(() => v[i]);
        }

        [TestCase(0, 0, 0)]
        [TestCase(179, 0, 179)]
        [TestCase(180, 0, 180)]
        [TestCase(181, 0, 179)]
        [TestCase(-179, 0, 179)]
        [TestCase(-180, 0, 180)]
        [TestCase(-181, 0, 179)]
        public void Angle(double a, double b, double expected)
        {
            a = Conversion.DegToRad(a);
            b = Conversion.DegToRad(b);
            var v0 = Vector3D.E1.Rotate(Vector3D.E3, b);
            var v1 = Vector3D.E1.Rotate(Vector3D.E3, a + b);
            var angle = Conversion.RadToDeg(v0.Angle(v1));
            angle.ShouldBe(expected, 1e-13);
        }

        [Test]
        public void Add()
        {
            var a = new Vector3D(1, 2, 3);
            var b = new Vector3D(17.3, 19.2, 31.3);

            var r = a.Add(b);

            r.X.ShouldBe(a.X + b.X);
            r.Y.ShouldBe(a.Y + b.Y);
            r.Z.ShouldBe(a.Z + b.Z);
        }


        [Test]
        public void Angle_Eq_AngleAbs()
        {
            var a = new Vector3D(1, -1, 1);
            var b = new Vector3D(-1, -1, -1);
            var a1 = a.Angle(b);
            var a2 = b.Angle(a);
            var b1 = a.AngleAbs(b);
            var b2 = b.AngleAbs(a);
            a1.ShouldBe(a2);
            a2.ShouldBe(b1);
            b1.ShouldBe(b2);
        }

        [Test]
        public void AngleE1_E2_90Deg()
        {
            (3.0 * Vector3D.E1).Angle(-17 * Vector3D.E2).ShouldBe(System.Math.PI / 2.0);
        }

        [Test]
        public void Array_ReturnsExpected()
        {
            const double x = 0.1;
            const double y = 0.2;
            const double z = 0.3;
            var v = new Vector3D(x, y, z);
            var c = v.Array;
            c.Length.ShouldBe(v.Dimensions);
            c[0].ShouldBe(x);
            c[1].ShouldBe(y);
            c[2].ShouldBe(z);
        }

        [Test]
        public void ArrayOp_ReturnsExpected()
        {
            const double x = 0.1;
            const double y = 0.2;
            const double z = 0.3;
            var v = new Vector3D(x, y, z);
            v[0].ShouldBe(x);
            v[1].ShouldBe(y);
            v[2].ShouldBe(z);
        }

        [Test]
        public void Clone()
        {
            var v = new Vector3D(1.1, 2.2, 3.3);
            var w = v.Clone();
            ReferenceEquals(v, w).ShouldBe(false);
            v.Equals(w).ShouldBe(true);
            v.IsEqual(w).ShouldBe(true);
            w.X.ShouldBe(v.X);
            w.Y.ShouldBe(v.Y);
            w.Z.ShouldBe(v.Z);
        }

        [Test]
        public void ConstMulOpVector3D()
        {
            var a = 17.0;
            var b = 19.0;
            var c = 23.0;
            var d = 27.05;
            var u = new Vector3D(a, b, c);
            var w = u * d;
            w.X.ShouldBe(a * d);
            w.Y.ShouldBe(b * d);
            w.Z.ShouldBe(c * d);
        }

        [Test]
        public void ConstructorWithVector3D()
        {
            var a = 17.0;
            var b = 19.0;
            var c = 23.0;
            var u = new Vector3D {X = a, Y = b, Z = c};
            var v = new Vector3D(u);
            v.X.ShouldBe(a);
            v.Y.ShouldBe(b);
            v.Z.ShouldBe(c);
        }

        [Test]
        public void CrossNorm2xE1x3xE2_Returns6()
        {
            (2.0 * Vector3D.E1).CrossNorm(3.0 * Vector3D.E2).ShouldBe(6.0);
        }

        [Test]
        public void CrossProductE1xE1_ReturnsZero()
        {
            var e3 = Vector3D.E1 ^ Vector3D.E1;
            e3.ShouldBe(Vector3D.Zero);
        }

        [Test]
        public void CrossProductE1xE2_ReturnsE3()
        {
            var e3 = Vector3D.E1 ^ Vector3D.E2;
            e3.ShouldBe(Vector3D.E3);
        }

        [Test]
        public void CrossProductE2xE1_ReturnsMinusE3()
        {
            var e3 = Vector3D.E2 ^ Vector3D.E1;
            e3.ShouldBe(-Vector3D.E3);
        }

        [Test]
        public void Dimension_Is3()
        {
            var s = new Vector3D();
            s.Dimensions.ShouldBe(3);
        }

        [Test]
        public void Div()
        {
            var a = new Vector3D(1, 2, 3);
            var b = 17.3;

            var r = a.Div(b);

            r.X.ShouldBe(a.X / b);
            r.Y.ShouldBe(a.Y / b);
            r.Z.ShouldBe(a.Z / b);
        }

        [Test]
        public void Dot()
        {
            var a = 17.0;
            var b = 19.0;
            var c = 23.0;
            var d = 27.05;
            var e = 49.9;
            var f = -1.1;
            var u = new Vector3D(a, b, c);
            var v = new Vector3D(d, e, f);
            v.Dot(u).ShouldBe(a * d + b * e + c * f);
        }

        [Test]
        public void EmptyConstructor_NullVector3D()
        {
            var v = new Vector3D();
            v.X.ShouldBe(0);
            v.Y.ShouldBe(0);
            v.Z.ShouldBe(0);
        }

        [Test]
        public void Equals_SameRefVector3D_ReturnsTrue()
        {
            var v = new Vector3D(1, 2, 3);
            var u = v;
            v.Equals(u).ShouldBe(true);
        }

        [Test]
        public void Equals_SameVector3D_ReturnsTrue()
        {
            var v = new Vector3D(1, 2, 3);
            var u = new Vector3D(v);
            v.Equals(u).ShouldBe(true);
        }

        [Test]
        public void Equals_WithNullptr_ReturnsFalse()
        {
            var v = new Vector3D(1, 2, 3);
            v.Equals(null).ShouldBe(false);
        }

        [Test]
        public void Euclidean()
        {
            var a = 17.0;
            var b = 19.0;
            var c = 23.0;
            var d = 27.05;
            var e = 49.9;
            var f = -1.1;
            var u = new Vector3D(a, b, c);
            var v = new Vector3D(d, e, f);
            var x = a - d;
            var y = b - e;
            var z = c - f;
            v.EuclideanNorm(u).ShouldBe(System.Math.Sqrt(x * x + y * y + z * z));
        }

        [Test]
        public void Interpolate()
        {
            var v0 = new Vector3D(1, 2, -8);
            var v1 = new Vector3D(4, 8, -2);
            var expected = new Vector3D(2, 4, -6);
            v0.Interpolate(v1, 1.0 / 3.0).ShouldBe(expected);
        }

        [Test]
        public void ModifiedNorm()
        {
            var a = 17.0;
            var b = 19.0;
            var c = 23.0;
            var d = 27.05;
            var e = 49.9;
            var f = -1.1;
            var u = new Vector3D(a, b, c);
            var v = new Vector3D(d, e, f);
            var x = a - d;
            var y = b - e;
            var z = c - f;
            v.ModifiedNorm(u).ShouldBe(System.Math.Sqrt(x * x + y * y + z * z));
        }

        [Test]
        public void Mul()
        {
            var a = new Vector3D(1, 2, 3);
            var b = 17.3;

            var r = a.Mul(b);

            r.X.ShouldBe(a.X * b);
            r.Y.ShouldBe(a.Y * b);
            r.Z.ShouldBe(a.Z * b);
        }

        [Test]
        public void NegationOp()
        {
            var a = 17.0;
            var b = 19.0;
            var c = -31.0;
            var u = new Vector3D(a, b, c);
            var w = -u;
            w.X.ShouldBe(-a);
            w.Y.ShouldBe(-b);
            w.Z.ShouldBe(-c);
        }

        [Test]
        public void Norm()
        {
            var a = -17.0;
            var b = -19.0;
            var c = -23.0;
            var v = new Vector3D(a, b, c);
            v.Norm().ShouldBe(System.Math.Sqrt(a * a + b * b + c * c));
        }

        [Test]
        public void Norm_CloseMaxDouble_returnsExpected()
        {
            var a = double.MaxValue / 2.0;
            var v = new Vector3D(a);
            v.Norm().ShouldBe(System.Math.Sqrt(3.0) * a);
        }

        [Test]
        public void Norm2()
        {
            var a = 17.0;
            var b = 19.0;
            var c = 23.0;
            var v = new Vector3D(a, b, c);
            v.Norm2().ShouldBe(a * a + b * b + c * c);
        }

        [Test]
        public void Normalize()
        {
            var a = 17.0;
            var b = 19.0;
            var c = 23.0;
            var v = new Vector3D(a, b, c);
            v.Normalize();
            v.Norm().ShouldBe(1.0, Comparison.Epsilon);
        }

        [Test]
        public void Normalize_ZeroVector3D_ZeroVector3D()
        {
            var v = new Vector3D();
            v.Normalize();
            v.Norm().ShouldBe(0.0);
        }

        [Test]
        public void Normalized()
        {
            var a = 17.0;
            var b = 19.0;
            var c = 23.0;
            var v = new Vector3D(a, b, c);
            var u = v.Normalized();
            u.Norm().ShouldBe(1.0, Comparison.Epsilon);
        }

        [Test]
        public void Normalized_ZeroVector3D_returnsZeroVector3D()
        {
            var v = new Vector3D();
            var u = v.Normalized();
            u.Norm().ShouldBe(0.0);
        }

        [Test]
        public void OpEqual_WithSameVector_ReturnsTrue()
        {
            var v = new Vector3D(1, 2, 3);
            var u = v;
            (v == u).ShouldBe(true);
        }

        [Test]
        public void OpNotEqual_WithNotSameVector_ReturnsTrue()
        {
            var v = new Vector3D(1, 2, 3);
            var u = new Vector3D(1, 2, 3.1);
            (v != u).ShouldBe(true);
        }

        [Test]
        public void Sub()
        {
            var a = new Vector3D(1, 2, 3);
            var b = new Vector3D(17.3, 19.2, 31.3);

            var r = a.Sub(b);

            r.X.ShouldBe(a.X - b.X);
            r.Y.ShouldBe(a.Y - b.Y);
            r.Z.ShouldBe(a.Z - b.Z);
        }

        [Test]
        public void Vector3DAddOpVector3D()
        {
            var a = 17.0;
            var b = 19.0;
            var c = 23.0;
            var d = 27.05;
            var e = 49.9;
            var f = -1.1;
            var u = new Vector3D(a, b, c);
            var v = new Vector3D(d, e, f);
            var w = u + v;

            w.X.ShouldBe(a + d);
            w.Y.ShouldBe(b + e);
            w.Z.ShouldBe(c + f);
        }

        [Test]
        public void Vector3DDivOpConst()
        {
            var a = 17.0;
            var b = 19.0;
            var c = 23.0;
            var d = 27.05;
            var u = new Vector3D(a, b, c);
            var w = u / d;
            w.X.ShouldBe(a / d);
            w.Y.ShouldBe(b / d);
            w.Z.ShouldBe(c / d);
        }

        [Test]
        public void Vector3DMulOpConst()
        {
            var a = 17.0;
            var b = 19.0;
            var c = 23.0;
            var d = 27.05;
            var u = new Vector3D(a, b, c);
            var w = d * u;
            w.X.ShouldBe(a * d);
            w.Y.ShouldBe(b * d);
            w.Z.ShouldBe(c * d);
        }

        [Test]
        public void Vector3DNaN()
        {
            Vector3D.NaN.X.ShouldBe(double.NaN);
            Vector3D.NaN.Y.ShouldBe(double.NaN);
            Vector3D.NaN.Z.ShouldBe(double.NaN);
        }

        [Test]
        public void Vector3DNegativeInfinity()
        {
            Vector3D.NegativeInfinity.X.ShouldBe(double.NegativeInfinity);
            Vector3D.NegativeInfinity.Y.ShouldBe(double.NegativeInfinity);
            Vector3D.NegativeInfinity.Z.ShouldBe(double.NegativeInfinity);
        }

        [Test]
        public void Vector3DPositiveInfinity()
        {
            Vector3D.PositiveInfinity.X.ShouldBe(double.PositiveInfinity);
            Vector3D.PositiveInfinity.Y.ShouldBe(double.PositiveInfinity);
            Vector3D.PositiveInfinity.Z.ShouldBe(double.PositiveInfinity);
        }

        [Test]
        public void Vector3DSubOpVector3D()
        {
            var a = 17.0;
            var b = 19.0;
            var c = 23.0;
            var d = 27.05;
            var e = 49.9;
            var f = -1.1;
            var u = new Vector3D(a, b, c);
            var v = new Vector3D(d, e, f);
            var w = u - v;

            w.X.ShouldBe(a - d);
            w.Y.ShouldBe(b - e);
            w.Z.ShouldBe(c - f);
        }

        [Test]
        public void Vector3DZero()
        {
            Vector3D.Zero.X.ShouldBe(0);
            Vector3D.Zero.Y.ShouldBe(0);
            Vector3D.Zero.Z.ShouldBe(0);
        }


        [Test]
        public void Vector3MulOpVector3D()
        {
            var a = 17.0;
            var b = 19.0;
            var c = 23.0;
            var d = 27.05;
            var e = 49.9;
            var f = -1.1;
            var u = new Vector3D(a, b, c);
            var v = new Vector3D(d, e, f);
            var w = u * v;
            w.ShouldBe(u.Dot(v));
        }

        [Test]
        public void Vector3One()
        {
            Vector3D.One.X.ShouldBe(1);
            Vector3D.One.Y.ShouldBe(1);
            Vector3D.One.Z.ShouldBe(1);
        }
    }
}