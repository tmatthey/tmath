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
    public class DrawLineTests
    {
        [Test]
        public void XiaolinWu()
        {
            var bitmap = new BitmapAdd(new Vector2D(0.0, 0.5), new Vector2D(2.9999, 0.9999), 1.0);
            Draw.XiaolinWu(new Vector2D(0.5, 0.5), new Vector2D(2.5, 0.5), bitmap);

            bitmap.Bitmap[0, 0].ShouldBe(0.0);
            bitmap.Bitmap[1, 0].ShouldBe(0.0);
            bitmap.Bitmap[2, 0].ShouldBe(0.0);
            bitmap.Bitmap[3, 0].ShouldBe(0.0);
            bitmap.Bitmap[4, 0].ShouldBe(0.0);
            bitmap.Bitmap[0, 1].ShouldBe(0.0);
            bitmap.Bitmap[1, 1].ShouldBe(0.0);
            bitmap.Bitmap[2, 1].ShouldBe(1.0);
            bitmap.Bitmap[3, 1].ShouldBe(1.0);
            bitmap.Bitmap[4, 1].ShouldBe(0.0);
            bitmap.Bitmap[0, 2].ShouldBe(0.0);
            bitmap.Bitmap[1, 2].ShouldBe(0.0);
            bitmap.Bitmap[2, 2].ShouldBe(0.0);
            bitmap.Bitmap[3, 2].ShouldBe(0.0);
            bitmap.Bitmap[4, 2].ShouldBe(0.0);
        }
    }
}