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
    public class Vector3DTests
    {
        [Test]
        public void Vector3D_EmptyConstructor_NullVector()
        {
            var v = new Vector3D();
            v.X.ShouldBe(0);
            v.Y.ShouldBe(0);
            v.Z.ShouldBe(0);
        }

        [TestCase(1.123)]
        [TestCase(-1.123)]
        [TestCase(0.0)]
        public void Vector3D_ConstructorWithConstant_ConstantVector(double c)
        {
            var v = new Vector3D(c);
            v.X.ShouldBe(c);
            v.Y.ShouldBe(c);
            v.Z.ShouldBe(c);
        }

        [Test]
        public void Vector3D_ConstructorWithVector3D_Vector3D()
        {
            var a = 17.0;
            var b = 19.0;
            var c = 23.0;
            var u = new Vector3D();
            u.X = a;
            u.Y = b;
            u.Z = c;
            var v = new Vector3D(u);
            u.X.ShouldBe(a);
            u.Y.ShouldBe(b);
            u.Z.ShouldBe(c);
        }



        [Test]
        public void Vector3D_Norm2()
        {
            var a = 17.0;
            var b = 19.0;
            var c = 23.0;
            var v = new Vector3D(a, b, c);
            v.Norm2().ShouldBe(a * a + b * b + c * c);
        }

        [Test]
        public void Vector3D_Norm()
        {
            var a = -17.0;
            var b = -19.0;
            var c = -23.0;
            var v = new Vector3D(a, b, c);
            v.Norm().ShouldBe(System.Math.Sqrt(a * a + b * b + c * c));
        }

        [Test]
        public void Vector3D_Distance()
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
            v.Distance(u).ShouldBe(System.Math.Sqrt(x * x + y * y + z * z));
        }

        [Test]
        public void Vector3D_Dot()
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
        public void Vector3D_Normalize()
        {
            var a = 17.0;
            var b = 19.0;
            var c = 23.0;
            var v = new Vector3D(a, b, c);
            v.Normalize();
            v.Norm().ShouldBe(1.0, Comparison.Epsilon);
        }

        [Test]
        public void Vector3D_ZeroVector_Normalize()
        {
            var v = new Vector3D();
            v.Normalize();
            v.Norm().ShouldBe(0.0);
        }

        [Test]
        public void Vector3D_Normalized()
        {
            var a = 17.0;
            var b = 19.0;
            var c = 23.0;
            var v = new Vector3D(a, b, c);
            var u = v.Normalized();
            u.Norm().ShouldBe(1.0, Comparison.Epsilon);
        }

        [Test]
        public void Vector3D_ZeroVector_Normalized()
        {
            var v = new Vector3D();
            var u = v.Normalized();
            u.Norm().ShouldBe(0.0);
        }

        [Test]
        public void Vector3D_Vector3DAddOpVector3D()
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
        public void Vector3D_Vector3DSubOpVector3D()
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
        public void Vector3D_Vector3MulOpVector3D()
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
        public void Vector3D_Vector3DMulOpConst()
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
        public void Vector3D_ConstMulOpVector3D()
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
        public void Vector3D_Vector3DDivOpConst()
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
        public void Vector3D_SameVectorIsEqualOp_ReturnsTrue()
        {
            var v = new Vector3D(1, 2, 3);
            var u = v;
            (v == u).ShouldBe(true);
        }
        [Test]
        public void Vector3D_NotSameVectorIsNotEqualOp_ReturnsTrue()
        {
            var v = new Vector3D(1, 2, 3);
            var u = new Vector3D(1, 2, 3.1);
            (v != u).ShouldBe(true);
        }

        [Test]
        public void Vector3D_CrossProductE1xE2_ReturnsE3()
        {
            var e3 = (Vector3D.E1 ^ Vector3D.E2);
            e3.ShouldBe(Vector3D.E3);
        }
        [Test]
        public void Vector3D_CrossProductE1xE1_ReturnsZero()
        {
            var e3 = (Vector3D.E1 ^ Vector3D.E1);
            e3.ShouldBe(Vector3D.Zero);
        }
        [Test]
        public void Vector3D_CrossProductE2xE1_ReturnsMinusE3()
        {
            var e3 = (Vector3D.E2 ^ Vector3D.E1);
            e3.ShouldBe(-Vector3D.E3);
        }
    }
}