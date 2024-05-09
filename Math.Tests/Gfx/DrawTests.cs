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

using Math.Gfx;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests.Gfx
{
    [TestFixture]
    public class DrawTests
    {
        [TestCase(0.25, 0.6, 1.17)]
        [TestCase(0.78, 0.11, 3.59)]
        [TestCase(0.0, 0.0, 3.57)]
        [TestCase(0.5, 0.5, 3.56)]
        [TestCase(0.9999, 0.9999, 3.55)]
        public void Plot_WithSet_Inbetween(double dx, double dy, double c)
        {
            var bitmap = new Bitmap(new Vector2D(0.0, 0.0), new Vector2D(3.0, 3.0), 1.0);
            var a = new Vector2D(1.0 + dx, 1.0 + dy);

            Draw.Plot(a, bitmap.Set, c);

            bitmap.Pixels[0, 0].ShouldBe(0.0);
            bitmap.Pixels[0, 1].ShouldBe(0.0);
            bitmap.Pixels[0, 2].ShouldBe(0.0);
            bitmap.Pixels[1, 0].ShouldBe(0.0);
            bitmap.Pixels[1, 1].ShouldBe(c * (1.0 - dx) * (1.0 - dy), 1e-9);
            bitmap.Pixels[1, 2].ShouldBe(c * (1.0 - dx) * dy, 1e-9);
            bitmap.Pixels[2, 0].ShouldBe(0.0);
            bitmap.Pixels[2, 1].ShouldBe(c * dx * (1.0 - dy), 1e-9);
            bitmap.Pixels[2, 2].ShouldBe(c * dx * dy, 1e-9);
        }

        [TestCase(-0.45)]
        [TestCase(0.0)]
        [TestCase(0.45)]
        public void Bresenham_ZeroDistance_OnePoint(double d)
        {
            var bitmap = new Bitmap(new Vector2D(0.0, 0.0), new Vector2D(2.0, 2.0), 1.0);
            Draw.Bresenham(new Vector2D(1.0 + d, 1.0 + d), new Vector2D(1.0 + d, 1.0 + d), bitmap.Set);
            bitmap.Pixels[0, 0].ShouldBe(0.0);
            bitmap.Pixels[1, 0].ShouldBe(0.0);
            bitmap.Pixels[2, 0].ShouldBe(0.0);

            bitmap.Pixels[0, 1].ShouldBe(0.0);
            bitmap.Pixels[1, 1].ShouldBe(1.0);
            bitmap.Pixels[2, 1].ShouldBe(0.0);

            bitmap.Pixels[0, 2].ShouldBe(0.0);
            bitmap.Pixels[1, 2].ShouldBe(0.0);
            bitmap.Pixels[2, 2].ShouldBe(0.0);
        }

        [TestCase(-0.45, 0.45)]
        [TestCase(0.0, 0.1)]
        [TestCase(0.45, -0.45)]
        public void Bresenham_PointsSamePixel_OnePoint(double da, double db)
        {
            var bitmap = new Bitmap(new Vector2D(0.0, 0.0), new Vector2D(2.0, 2.0), 1.0);
            Draw.Bresenham(new Vector2D(1.0 + da, 1.0 + da), new Vector2D(1.0 + db, 1.0 + db), bitmap.Set);
            bitmap.Pixels[0, 0].ShouldBe(0.0);
            bitmap.Pixels[1, 0].ShouldBe(0.0);
            bitmap.Pixels[2, 0].ShouldBe(0.0);

            bitmap.Pixels[0, 1].ShouldBe(0.0);
            bitmap.Pixels[1, 1].ShouldBe(1.0);
            bitmap.Pixels[2, 1].ShouldBe(0.0);

            bitmap.Pixels[0, 2].ShouldBe(0.0);
            bitmap.Pixels[1, 2].ShouldBe(0.0);
            bitmap.Pixels[2, 2].ShouldBe(0.0);
        }

        [Test]
        public void Bresenham_DiagonalInbetween()
        {
            var bitmap = new Bitmap(new Vector2D(0.0, 0.0), new Vector2D(4.0, 4.0), 1.0);
            Draw.Bresenham(new Vector2D(0.55, 0.55), new Vector2D(3.45, 3.45), bitmap.Set);
            bitmap.Pixels[0, 0].ShouldBe(0.0);
            bitmap.Pixels[1, 0].ShouldBe(0.0);
            bitmap.Pixels[2, 0].ShouldBe(0.0);
            bitmap.Pixels[3, 0].ShouldBe(0.0);
            bitmap.Pixels[4, 0].ShouldBe(0.0);

            bitmap.Pixels[0, 1].ShouldBe(0.0);
            bitmap.Pixels[1, 1].ShouldBe(0.95, 1e-10);
            bitmap.Pixels[2, 1].ShouldBe(0.0);
            bitmap.Pixels[3, 1].ShouldBe(0.0);
            bitmap.Pixels[4, 1].ShouldBe(0.0);

            bitmap.Pixels[0, 2].ShouldBe(0.0);
            bitmap.Pixels[1, 2].ShouldBe(0.0);
            bitmap.Pixels[2, 2].ShouldBe(1.0);
            bitmap.Pixels[3, 2].ShouldBe(0.0);
            bitmap.Pixels[4, 2].ShouldBe(0.0);

            bitmap.Pixels[0, 3].ShouldBe(0.0);
            bitmap.Pixels[1, 3].ShouldBe(0.0);
            bitmap.Pixels[2, 3].ShouldBe(0.0);
            bitmap.Pixels[3, 3].ShouldBe(0.95, 1e-10);
            bitmap.Pixels[4, 3].ShouldBe(0.0);

            bitmap.Pixels[0, 4].ShouldBe(0.0);
            bitmap.Pixels[1, 4].ShouldBe(0.0);
            bitmap.Pixels[2, 4].ShouldBe(0.0);
            bitmap.Pixels[3, 4].ShouldBe(0.0);
            bitmap.Pixels[4, 4].ShouldBe(0.0);
        }

        [Test]
        public void Bresenham_DiagonalQ1()
        {
            var bitmap = new Bitmap(new Vector2D(0.0, 0.0), new Vector2D(4.0, 4.0), 1.0);
            Draw.Bresenham(new Vector2D(1.0, 1.0), new Vector2D(3.0, 3.0), bitmap.Set);
            bitmap.Pixels[0, 0].ShouldBe(0.0);
            bitmap.Pixels[1, 0].ShouldBe(0.0);
            bitmap.Pixels[2, 0].ShouldBe(0.0);
            bitmap.Pixels[3, 0].ShouldBe(0.0);
            bitmap.Pixels[4, 0].ShouldBe(0.0);

            bitmap.Pixels[0, 1].ShouldBe(0.0);
            bitmap.Pixels[1, 1].ShouldBe(0.5);
            bitmap.Pixels[2, 1].ShouldBe(0.0);
            bitmap.Pixels[3, 1].ShouldBe(0.0);
            bitmap.Pixels[4, 1].ShouldBe(0.0);

            bitmap.Pixels[0, 2].ShouldBe(0.0);
            bitmap.Pixels[1, 2].ShouldBe(0.0);
            bitmap.Pixels[2, 2].ShouldBe(1.0);
            bitmap.Pixels[3, 2].ShouldBe(0.0);
            bitmap.Pixels[4, 2].ShouldBe(0.0);

            bitmap.Pixels[0, 3].ShouldBe(0.0);
            bitmap.Pixels[1, 3].ShouldBe(0.0);
            bitmap.Pixels[2, 3].ShouldBe(0.0);
            bitmap.Pixels[3, 3].ShouldBe(0.5);
            bitmap.Pixels[4, 3].ShouldBe(0.0);

            bitmap.Pixels[0, 4].ShouldBe(0.0);
            bitmap.Pixels[1, 4].ShouldBe(0.0);
            bitmap.Pixels[2, 4].ShouldBe(0.0);
            bitmap.Pixels[3, 4].ShouldBe(0.0);
            bitmap.Pixels[4, 4].ShouldBe(0.0);
        }

        [Test]
        public void Bresenham_DiagonalQ2()
        {
            var bitmap = new Bitmap(new Vector2D(0.0, 0.0), new Vector2D(4.0, 4.0), 1.0);
            Draw.Bresenham(new Vector2D(1.0, 3.0), new Vector2D(3.0, 1.0), bitmap.Set);
            bitmap.Pixels[0, 0].ShouldBe(0.0);
            bitmap.Pixels[1, 0].ShouldBe(0.0);
            bitmap.Pixels[2, 0].ShouldBe(0.0);
            bitmap.Pixels[3, 0].ShouldBe(0.0);
            bitmap.Pixels[4, 0].ShouldBe(0.0);

            bitmap.Pixels[0, 1].ShouldBe(0.0);
            bitmap.Pixels[1, 1].ShouldBe(0.0);
            bitmap.Pixels[2, 1].ShouldBe(0.0);
            bitmap.Pixels[3, 1].ShouldBe(0.5);
            bitmap.Pixels[4, 1].ShouldBe(0.0);

            bitmap.Pixels[0, 2].ShouldBe(0.0);
            bitmap.Pixels[1, 2].ShouldBe(0.0);
            bitmap.Pixels[2, 2].ShouldBe(1.0);
            bitmap.Pixels[3, 2].ShouldBe(0.0);
            bitmap.Pixels[4, 2].ShouldBe(0.0);

            bitmap.Pixels[0, 3].ShouldBe(0.0);
            bitmap.Pixels[1, 3].ShouldBe(0.5);
            bitmap.Pixels[2, 3].ShouldBe(0.0);
            bitmap.Pixels[3, 3].ShouldBe(0.0);
            bitmap.Pixels[4, 3].ShouldBe(0.0);

            bitmap.Pixels[0, 4].ShouldBe(0.0);
            bitmap.Pixels[1, 4].ShouldBe(0.0);
            bitmap.Pixels[2, 4].ShouldBe(0.0);
            bitmap.Pixels[3, 4].ShouldBe(0.0);
            bitmap.Pixels[4, 4].ShouldBe(0.0);
        }

        [Test]
        public void Bresenham_DiagonalQ3()
        {
            var bitmap = new Bitmap(new Vector2D(0.0, 0.0), new Vector2D(4.0, 4.0), 1.0);
            Draw.Bresenham(new Vector2D(3.0, 3.0), new Vector2D(1.0, 1.0), bitmap.Set);
            bitmap.Pixels[0, 0].ShouldBe(0.0);
            bitmap.Pixels[1, 0].ShouldBe(0.0);
            bitmap.Pixels[2, 0].ShouldBe(0.0);
            bitmap.Pixels[3, 0].ShouldBe(0.0);
            bitmap.Pixels[4, 0].ShouldBe(0.0);

            bitmap.Pixels[0, 1].ShouldBe(0.0);
            bitmap.Pixels[1, 1].ShouldBe(0.5);
            bitmap.Pixels[2, 1].ShouldBe(0.0);
            bitmap.Pixels[3, 1].ShouldBe(0.0);
            bitmap.Pixels[4, 1].ShouldBe(0.0);

            bitmap.Pixels[0, 2].ShouldBe(0.0);
            bitmap.Pixels[1, 2].ShouldBe(0.0);
            bitmap.Pixels[2, 2].ShouldBe(1.0);
            bitmap.Pixels[3, 2].ShouldBe(0.0);
            bitmap.Pixels[4, 2].ShouldBe(0.0);

            bitmap.Pixels[0, 3].ShouldBe(0.0);
            bitmap.Pixels[1, 3].ShouldBe(0.0);
            bitmap.Pixels[2, 3].ShouldBe(0.0);
            bitmap.Pixels[3, 3].ShouldBe(0.5);
            bitmap.Pixels[4, 3].ShouldBe(0.0);

            bitmap.Pixels[0, 4].ShouldBe(0.0);
            bitmap.Pixels[1, 4].ShouldBe(0.0);
            bitmap.Pixels[2, 4].ShouldBe(0.0);
            bitmap.Pixels[3, 4].ShouldBe(0.0);
            bitmap.Pixels[4, 4].ShouldBe(0.0);
        }

        [Test]
        public void Bresenham_DiagonalQ4()
        {
            var bitmap = new Bitmap(new Vector2D(0.0, 0.0), new Vector2D(4.0, 4.0), 1.0);
            Draw.Bresenham(new Vector2D(3.0, 1.0), new Vector2D(1.0, 3.0), bitmap.Set);
            bitmap.Pixels[0, 0].ShouldBe(0.0);
            bitmap.Pixels[1, 0].ShouldBe(0.0);
            bitmap.Pixels[2, 0].ShouldBe(0.0);
            bitmap.Pixels[3, 0].ShouldBe(0.0);
            bitmap.Pixels[4, 0].ShouldBe(0.0);

            bitmap.Pixels[0, 1].ShouldBe(0.0);
            bitmap.Pixels[1, 1].ShouldBe(0.0);
            bitmap.Pixels[2, 1].ShouldBe(0.0);
            bitmap.Pixels[3, 1].ShouldBe(0.5);
            bitmap.Pixels[4, 1].ShouldBe(0.0);

            bitmap.Pixels[0, 2].ShouldBe(0.0);
            bitmap.Pixels[1, 2].ShouldBe(0.0);
            bitmap.Pixels[2, 2].ShouldBe(1.0);
            bitmap.Pixels[3, 2].ShouldBe(0.0);
            bitmap.Pixels[4, 2].ShouldBe(0.0);

            bitmap.Pixels[0, 3].ShouldBe(0.0);
            bitmap.Pixels[1, 3].ShouldBe(0.5);
            bitmap.Pixels[2, 3].ShouldBe(0.0);
            bitmap.Pixels[3, 3].ShouldBe(0.0);
            bitmap.Pixels[4, 3].ShouldBe(0.0);

            bitmap.Pixels[0, 4].ShouldBe(0.0);
            bitmap.Pixels[1, 4].ShouldBe(0.0);
            bitmap.Pixels[2, 4].ShouldBe(0.0);
            bitmap.Pixels[3, 4].ShouldBe(0.0);
            bitmap.Pixels[4, 4].ShouldBe(0.0);
        }

        [Test]
        public void Bresenham_Steep()
        {
            var bitmap = new Bitmap(new Vector2D(0.0, 0.0), new Vector2D(4.0, 4.0), 1.0);
            Draw.Bresenham(new Vector2D(1.0, 1.0), new Vector2D(3.0, 4.0), bitmap.Set);
            bitmap.Pixels[0, 0].ShouldBe(0.0);
            bitmap.Pixels[1, 0].ShouldBe(0.0);
            bitmap.Pixels[2, 0].ShouldBe(0.0);
            bitmap.Pixels[3, 0].ShouldBe(0.0);
            bitmap.Pixels[4, 0].ShouldBe(0.0);

            bitmap.Pixels[0, 1].ShouldBe(0.0);
            bitmap.Pixels[1, 1].ShouldBe(0.5);
            bitmap.Pixels[2, 1].ShouldBe(0.0);
            bitmap.Pixels[3, 1].ShouldBe(0.0);
            bitmap.Pixels[4, 1].ShouldBe(0.0);

            bitmap.Pixels[0, 2].ShouldBe(0.0);
            bitmap.Pixels[1, 2].ShouldBe(0.0);
            bitmap.Pixels[2, 2].ShouldBe(1.0);
            bitmap.Pixels[3, 2].ShouldBe(0.0);
            bitmap.Pixels[4, 2].ShouldBe(0.0);

            bitmap.Pixels[0, 3].ShouldBe(0.0);
            bitmap.Pixels[1, 3].ShouldBe(0.0);
            bitmap.Pixels[2, 3].ShouldBe(1.0);
            bitmap.Pixels[3, 3].ShouldBe(0.0);
            bitmap.Pixels[4, 3].ShouldBe(0.0);

            bitmap.Pixels[0, 4].ShouldBe(0.0);
            bitmap.Pixels[1, 4].ShouldBe(0.0);
            bitmap.Pixels[2, 4].ShouldBe(0.0);
            bitmap.Pixels[3, 4].ShouldBe(0.5);
            bitmap.Pixels[4, 4].ShouldBe(0.0);
        }

        [Test]
        public void Plot_TwiceWithAdd_OnInteger()
        {
            var bitmap = new Bitmap(new Vector2D(0.0, 0.0), new Vector2D(3.0, 3.0), 1.0);
            var a = new Vector2D(1.0);
            const double c = 1.17;
            Draw.Plot(a, bitmap.Add, c);
            Draw.Plot(a, bitmap.Add, c);

            bitmap.Pixels[0, 0].ShouldBe(0.0);
            bitmap.Pixels[0, 1].ShouldBe(0.0);
            bitmap.Pixels[0, 2].ShouldBe(0.0);
            bitmap.Pixels[1, 0].ShouldBe(0.0);
            bitmap.Pixels[1, 1].ShouldBe(c * 2.0);
            bitmap.Pixels[1, 2].ShouldBe(0.0);
            bitmap.Pixels[2, 0].ShouldBe(0.0);
            bitmap.Pixels[2, 1].ShouldBe(0.0);
            bitmap.Pixels[2, 2].ShouldBe(0.0);
        }

        [Test]
        public void Plot_TwiceWithSet_OnInteger()
        {
            var bitmap = new Bitmap(new Vector2D(0.0, 0.0), new Vector2D(3.0, 3.0), 1.0);
            var a = new Vector2D(1.0);
            const double c = 1.17;
            Draw.Plot(a, bitmap.Set, c);
            Draw.Plot(a, bitmap.Set, c);

            bitmap.Pixels[0, 0].ShouldBe(0.0);
            bitmap.Pixels[0, 1].ShouldBe(0.0);
            bitmap.Pixels[0, 2].ShouldBe(0.0);
            bitmap.Pixels[1, 0].ShouldBe(0.0);
            bitmap.Pixels[1, 1].ShouldBe(c);
            bitmap.Pixels[1, 2].ShouldBe(0.0);
            bitmap.Pixels[2, 0].ShouldBe(0.0);
            bitmap.Pixels[2, 1].ShouldBe(0.0);
            bitmap.Pixels[2, 2].ShouldBe(0.0);
        }

        [Test]
        public void Plot_WithAdd_OnInteger()
        {
            var bitmap = new Bitmap(new Vector2D(0.0, 0.0), new Vector2D(3.0, 3.0), 1.0);
            var a = new Vector2D(1.0);
            const double c = 1.17;
            Draw.Plot(a, bitmap.Add, c);

            bitmap.Pixels[0, 0].ShouldBe(0.0);
            bitmap.Pixels[0, 1].ShouldBe(0.0);
            bitmap.Pixels[0, 2].ShouldBe(0.0);
            bitmap.Pixels[1, 0].ShouldBe(0.0);
            bitmap.Pixels[1, 1].ShouldBe(c);
            bitmap.Pixels[1, 2].ShouldBe(0.0);
            bitmap.Pixels[2, 0].ShouldBe(0.0);
            bitmap.Pixels[2, 1].ShouldBe(0.0);
            bitmap.Pixels[2, 2].ShouldBe(0.0);
        }

        [Test]
        public void Plot_WithSet_OnInteger()
        {
            var bitmap = new Bitmap(new Vector2D(0.0, 0.0), new Vector2D(3.0, 3.0), 1.0);
            var a = new Vector2D(1.0);
            const double c = 1.17;
            Draw.Plot(a, bitmap.Set, c);

            bitmap.Pixels[0, 0].ShouldBe(0.0);
            bitmap.Pixels[0, 1].ShouldBe(0.0);
            bitmap.Pixels[0, 2].ShouldBe(0.0);
            bitmap.Pixels[1, 0].ShouldBe(0.0);
            bitmap.Pixels[1, 1].ShouldBe(c);
            bitmap.Pixels[1, 2].ShouldBe(0.0);
            bitmap.Pixels[2, 0].ShouldBe(0.0);
            bitmap.Pixels[2, 1].ShouldBe(0.0);
            bitmap.Pixels[2, 2].ShouldBe(0.0);
        }

        [Test]
        public void XiaolinWu_Inbetween()
        {
            const double eps = 10 * double.Epsilon;
            var bitmap = new Bitmap(new Vector2D(0.0, 0.0), new Vector2D(3.0, 2.0), 1.0);
            const double c = 1.17;
            Draw.XiaolinWu(new Vector2D(0.5 - eps, 0.5 - eps), new Vector2D(2.5 - eps, 0.5 - eps), bitmap.Add, c);

            bitmap.Pixels[0, 0].ShouldBe(0.0);
            bitmap.Pixels[1, 0].ShouldBe(0.5 * c);
            bitmap.Pixels[2, 0].ShouldBe(0.5 * c);
            bitmap.Pixels[3, 0].ShouldBe(0.0);
            bitmap.Pixels[0, 1].ShouldBe(0.0);
            bitmap.Pixels[1, 1].ShouldBe(0.5 * c);
            bitmap.Pixels[2, 1].ShouldBe(0.5 * c);
            bitmap.Pixels[3, 1].ShouldBe(0.0);
            bitmap.Pixels[0, 2].ShouldBe(0.0);
            bitmap.Pixels[1, 2].ShouldBe(0.0);
            bitmap.Pixels[2, 2].ShouldBe(0.0);
            bitmap.Pixels[3, 2].ShouldBe(0.0);
        }

        [Test]
        public void XiaolinWu_OnIntegerHorizontal()
        {
            var bitmap = new Bitmap(new Vector2D(0.0, 0.0), new Vector2D(4.0, 2.0), 1.0);
            const double c = 1.17;
            Draw.XiaolinWu(new Vector2D(1.0, 1.0), new Vector2D(3.0, 1.0), bitmap.Add, c);

            bitmap.Pixels[0, 0].ShouldBe(0.0);
            bitmap.Pixels[1, 0].ShouldBe(0.0);
            bitmap.Pixels[2, 0].ShouldBe(0.0);
            bitmap.Pixels[3, 0].ShouldBe(0.0);
            bitmap.Pixels[4, 0].ShouldBe(0.0);
            bitmap.Pixels[0, 1].ShouldBe(0.0);
            bitmap.Pixels[1, 1].ShouldBe(0.5 * c);
            bitmap.Pixels[2, 1].ShouldBe(1.0 * c);
            bitmap.Pixels[3, 1].ShouldBe(0.5 * c);
            bitmap.Pixels[4, 1].ShouldBe(0.0);
            bitmap.Pixels[0, 2].ShouldBe(0.0);
            bitmap.Pixels[1, 2].ShouldBe(0.0);
            bitmap.Pixels[2, 2].ShouldBe(0.0);
            bitmap.Pixels[3, 2].ShouldBe(0.0);
            bitmap.Pixels[4, 2].ShouldBe(0.0);
        }

        [Test]
        public void XiaolinWu_OnIntegerVertical()
        {
            var bitmap = new Bitmap(new Vector2D(0.0, 0.0), new Vector2D(2.0, 4.0), 1.0);
            const double c = 1.17;
            Draw.XiaolinWu(new Vector2D(1.0, 1.0), new Vector2D(1.0, 3.0), bitmap.Set, c);

            bitmap.Pixels[0, 0].ShouldBe(0.0);
            bitmap.Pixels[1, 0].ShouldBe(0.0);
            bitmap.Pixels[2, 0].ShouldBe(0.0);
            bitmap.Pixels[0, 1].ShouldBe(0.0);
            bitmap.Pixels[1, 1].ShouldBe(0.5 * c);
            bitmap.Pixels[2, 1].ShouldBe(0.0);
            bitmap.Pixels[0, 2].ShouldBe(0.0);
            bitmap.Pixels[1, 2].ShouldBe(1.0 * c);
            bitmap.Pixels[2, 2].ShouldBe(0.0);
            bitmap.Pixels[0, 3].ShouldBe(0.0);
            bitmap.Pixels[1, 3].ShouldBe(0.5 * c);
            bitmap.Pixels[2, 3].ShouldBe(0.0);
            bitmap.Pixels[0, 4].ShouldBe(0.0);
            bitmap.Pixels[1, 4].ShouldBe(0.0);
            bitmap.Pixels[2, 4].ShouldBe(0.0);
        }
    }
}