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
    public class BitmapTests
    {
        [Test]
        public void Constructor_WithCorrectDimesions()
        {
            var bitmap = new Bitmap(new Vector2D(), new Vector2D(2.0, 1.0), 1.0);
            bitmap.Pixels.GetLength(0).ShouldBe(3);
            bitmap.Pixels.GetLength(1).ShouldBe(2);
        }

        [Test]
        public void Constructor_ZeroBitmap()
        {
            var bitmap = new Bitmap(new Vector2D(), new Vector2D(1.99, 0.99), 1.0);
            foreach (var pt in bitmap.Pixels)
            {
                pt.ShouldBe(0.0);
            }
        }

        [Test]
        public void Pick_UnValidCoordsInitialBitMap_ReturnsNaN()
        {
            var bitmap = new Bitmap(new Vector2D(), new Vector2D(1.99, 0.99), 1.0);
            bitmap.Pick(1000, 1).ShouldBe(double.NaN);
        }

        [Test]
        public void Pick_ValidCoordsInitialBitMap_ReturnsZero()
        {
            var bitmap = new Bitmap(new Vector2D(), new Vector2D(1.99, 0.99), 1.0);
            bitmap.Pick(1, 1).ShouldBe(0.0);
        }

        [Test]
        public void PlotAdd_ReturnsValue()
        {
            var bitmap = new Bitmap(new Vector2D(), new Vector2D(1.99, 0.99), 0.01);
            var x = 1;
            var y = 1;
            var c = 17.19;
            bitmap.PlotAdd(x, y, c);

            bitmap.Pick(x, y).ShouldBe(c);
        }

        [Test]
        public void PlotAdd_Twice_ReturnsDoubleValue()
        {
            var bitmap = new Bitmap(new Vector2D(), new Vector2D(1.99, 0.99), 0.01);
            var x = 1;
            var y = 1;
            var c = 17.19;
            bitmap.PlotAdd(x, y, c);
            bitmap.PlotAdd(x, y, c);

            bitmap.Pick(x, y).ShouldBe(c*2.0);
        }

        [Test]
        public void PlotSet_Twice_ReturnsValue()
        {
            var bitmap = new Bitmap(new Vector2D(), new Vector2D(1.99, 0.99), 0.01);
            var x = 1;
            var y = 1;
            var c = 17.19;
            bitmap.PlotSet(x, y, c);
            bitmap.PlotSet(x, y, c);

            bitmap.Pick(x, y).ShouldBe(c);
        }
    }
}