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

using Math.Gfx;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests.Gfx
{
    [TestFixture]
    public class DrawTests
    {
        [Test]
        public void Plot_OnInteger()
        {
            var bitmap = new Bitmap(new Vector2D(0.0, 0.0), new Vector2D(3.0, 3.0), 1.0);
            var a = new Vector2D(1.0);
            const double c = 1.17;
            Draw.Plot(a, c, bitmap.Set);

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
            var eps = 10*double.Epsilon;
            var bitmap = new Bitmap(new Vector2D(0.0, 0.0), new Vector2D(3.0, 2.0), 1.0);
            Draw.XiaolinWu(new Vector2D(0.5 - eps, 0.5 - eps), new Vector2D(2.5 - eps, 0.5 - eps), bitmap.Add);

            bitmap.Pixels[0, 0].ShouldBe(0.0);
            bitmap.Pixels[1, 0].ShouldBe(0.5);
            bitmap.Pixels[2, 0].ShouldBe(0.5);
            bitmap.Pixels[3, 0].ShouldBe(0.0);
            bitmap.Pixels[0, 1].ShouldBe(0.0);
            bitmap.Pixels[1, 1].ShouldBe(0.5);
            bitmap.Pixels[2, 1].ShouldBe(0.5);
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
            Draw.XiaolinWu(new Vector2D(1.0, 1.0), new Vector2D(3.0, 1.0), bitmap.Add);

            bitmap.Pixels[0, 0].ShouldBe(0.0);
            bitmap.Pixels[1, 0].ShouldBe(0.0);
            bitmap.Pixels[2, 0].ShouldBe(0.0);
            bitmap.Pixels[3, 0].ShouldBe(0.0);
            bitmap.Pixels[4, 0].ShouldBe(0.0);
            bitmap.Pixels[0, 1].ShouldBe(0.0);
            bitmap.Pixels[1, 1].ShouldBe(0.5);
            bitmap.Pixels[2, 1].ShouldBe(1.0);
            bitmap.Pixels[3, 1].ShouldBe(0.5);
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
            Draw.XiaolinWu(new Vector2D(1.0, 1.0), new Vector2D(1.0, 3.0), bitmap.Set);

            bitmap.Pixels[0, 0].ShouldBe(0.0);
            bitmap.Pixels[1, 0].ShouldBe(0.0);
            bitmap.Pixels[2, 0].ShouldBe(0.0);
            bitmap.Pixels[0, 1].ShouldBe(0.0);
            bitmap.Pixels[1, 1].ShouldBe(0.5);
            bitmap.Pixels[2, 1].ShouldBe(0.0);
            bitmap.Pixels[0, 2].ShouldBe(0.0);
            bitmap.Pixels[1, 2].ShouldBe(1.0);
            bitmap.Pixels[2, 2].ShouldBe(0.0);
            bitmap.Pixels[0, 3].ShouldBe(0.0);
            bitmap.Pixels[1, 3].ShouldBe(0.5);
            bitmap.Pixels[2, 3].ShouldBe(0.0);
            bitmap.Pixels[0, 4].ShouldBe(0.0);
            bitmap.Pixels[1, 4].ShouldBe(0.0);
            bitmap.Pixels[2, 4].ShouldBe(0.0);
        }
    }
}