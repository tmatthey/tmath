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
    public class Vector2DTests
    {
        [Test]
        public void Vector2D_EmptyConstructor_NullVector()
        {
            var v = new Vector2D();
            v.X.ShouldBe(0);
            v.Y.ShouldBe(0);
        }

        [TestCase(1.123)]
        [TestCase(-1.123)]
        [TestCase(0.0)]
        public void Vector2D_ConstructorWithConstant_ConstantVector(double c)
        {
            var v = new Vector2D(c);
            v.X.ShouldBe(c);
            v.Y.ShouldBe(c);
        }

        [Test]
        public void Vector2D_ConstructorWithVector2D_Vector2D()
        {
            var a = 17.0;
            var b = 19.0;
            var u = new Vector2D();
            u.X = a;
            u.Y = b;
            var v = new Vector2D(u);
            u.X.ShouldBe(a);
            u.Y.ShouldBe(b);
        }

        [Test]
        public void Vector2D_Norm2()
        {
            var a = 17.0;
            var b = 19.0;
            var v = new Vector2D(a, b);
            v.Norm2().ShouldBe(a * a + b * b);
        }

        [Test]
        public void Vector2D_Norm()
        {
            var a = -17.0;
            var b = -19.0;
            var v = new Vector2D(a, b);
            v.Norm().ShouldBe(System.Math.Sqrt(a * a + b * b));
        }

        [Test]
        public void Vector2D_Distance()
        {
            var a = 17.0;
            var b = 19.0;
            var d = 27.05;
            var e = 49.9;
            var u = new Vector2D(a, b);
            var v = new Vector2D(d, e);
            var x = a - d;
            var y = b - e;
            v.Distance(u).ShouldBe(System.Math.Sqrt(x * x + y * y));
        }

        [Test]
        public void Vector2D_Dot()
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
        public void Vector2D_Normalize()
        {
            var a = 17.0;
            var b = 19.0;
            var v = new Vector2D(a, b);
            v.Normalize();
            v.Norm().ShouldBe(1.0, Comparison.Epsilon);
        }

        [Test]
        public void Vector2D_ZeroVector_Normalize()
        {
            var v = new Vector2D();
            v.Normalize();
            v.Norm().ShouldBe(0.0);
        }

        [Test]
        public void Vector2D_Normalized()
        {
            var a = 17.0;
            var b = 19.0;
            var v = new Vector2D(a, b);
            var u = v.Normalized();
            u.Norm().ShouldBe(1.0, Comparison.Epsilon);
        }

        [Test]
        public void Vector2D_ZeroVector_Normalized()
        {
            var v = new Vector2D();
            var u = v.Normalized();
            u.Norm().ShouldBe(0.0);
        }

        [Test]
        public void Vector2D_Vector2DAddOpVector2D()
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
        public void Vector2D_Vector2DSubOpVector2D()
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
        public void Vector2D_Vector3MulOpVector2D()
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
        public void Vector2D_Vector2DMulOpConst()
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
        public void Vector2D_ConstMulOpVector2D()
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
        public void Vector2D_Vector2DDivOpConst()
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
        public void Vector2D_SameVectorIsEqualOp_ReturnsTrue()
        {
            var v = new Vector2D(1, 2);
            var u = v;
            (v == u).ShouldBe(true);
        }

        [Test]
        public void Vector2D_NotSameVectorIsNotEqualOp_ReturnsTrue()
        {
            var v = new Vector2D(1, 2);
            var u = new Vector2D(1, 2.1);
            (v != u).ShouldBe(true);
        }
    }
}