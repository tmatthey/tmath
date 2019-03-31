/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2019 Thierry Matthey
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
    public class RGBBitmapTests
    {
        [TestCase(2)]
        [TestCase(20)]
        [TestCase(200)]
        [TestCase(2000)]
        public void ConstructorWithMaxLength_MaxDimensions(int maxLength)
        {
            var bitmap = new RGBBitmap(new Vector2D(), new Vector2D(2000.0, 2000.0), 1.0, maxLength);
            bitmap.RedPixels.GetLength(0).ShouldBe(maxLength);
            bitmap.RedPixels.GetLength(1).ShouldBe(maxLength);
            bitmap.GreenPixels.GetLength(0).ShouldBe(maxLength);
            bitmap.GreenPixels.GetLength(1).ShouldBe(maxLength);
            bitmap.BluePixels.GetLength(0).ShouldBe(maxLength);
            bitmap.BluePixels.GetLength(1).ShouldBe(maxLength);
        }

        [Test]
        public void AddMagnitudePlot_Twice_ReturnsTwiceMax()
        {
            var bitmap = new RGBBitmap(new Vector2D(), new Vector2D(1.99, 0.99), 0.01);
            var x = 1;
            var y = 1;
            var c = 17.19;
            var cMax = 19.17;
            bitmap.AddMagnitude.Plot(x, y, c, cMax);
            bitmap.AddMagnitude.Plot(x, y, c, cMax);

            bitmap.PickRed(x, y).ShouldBe(2 * cMax);
            bitmap.PickGreen(x, y).ShouldBe(2 * cMax);
            bitmap.PickBlue(x, y).ShouldBe(2 * cMax);
        }

        [Test]
        public void AddPlot_Twice_ReturnsTwiceMax()
        {
            var bitmap = new RGBBitmap(new Vector2D(), new Vector2D(1.99, 0.99), 0.01);
            var x = 1;
            var y = 1;
            var c = 17.19;
            var cMax = 19.17;
            bitmap.Add.Plot(x, y, c, cMax);
            bitmap.Add.Plot(x, y, c, cMax);

            bitmap.PickRed(x, y).ShouldBe(2 * c);
            bitmap.PickGreen(x, y).ShouldBe(2 * c);
            bitmap.PickBlue(x, y).ShouldBe(2 * c);
        }

        [Test]
        public void Constructor_CorrectDimensions()
        {
            var bitmap = new RGBBitmap(new Vector2D(), new Vector2D(2.0, 1.0), 1.0);
            bitmap.RedPixels.GetLength(0).ShouldBe(3);
            bitmap.RedPixels.GetLength(0).ShouldBe(3);
            bitmap.GreenPixels.GetLength(1).ShouldBe(2);
            bitmap.GreenPixels.GetLength(0).ShouldBe(3);
            bitmap.BluePixels.GetLength(1).ShouldBe(2);
            bitmap.BluePixels.GetLength(0).ShouldBe(3);
        }

        [Test]
        public void Constructor_ZeroRGBBitmap()
        {
            var bitmap = new RGBBitmap(new Vector2D(), new Vector2D(1.99, 0.99), 1.0);
            foreach (var pt in bitmap.RedPixels)
            {
                pt.ShouldBe(0.0);
            }

            foreach (var pt in bitmap.GreenPixels)
            {
                pt.ShouldBe(0.0);
            }

            foreach (var pt in bitmap.BluePixels)
            {
                pt.ShouldBe(0.0);
            }
        }

        [Test]
        public void Pick_UnValidCoordsInitialBitMap_ReturnsNaN()
        {
            var bitmap = new RGBBitmap(new Vector2D(), new Vector2D(1.99, 0.99), 1.0);
            bitmap.PickRed(1000, 1).ShouldBe(double.NaN);
            bitmap.PickGreen(1000, 1).ShouldBe(double.NaN);
            bitmap.PickBlue(1000, 1).ShouldBe(double.NaN);
        }

        [Test]
        public void Pick_ValidCoordsInitialBitMap_ReturnsZero()
        {
            var bitmap = new RGBBitmap(new Vector2D(), new Vector2D(1.99, 0.99), 1.0);
            bitmap.PickRed(1, 1).ShouldBe(0.0);
            bitmap.PickGreen(1, 1).ShouldBe(0.0);
            bitmap.PickBlue(1, 1).ShouldBe(0.0);
        }

        [Test]
        public void PlotAdd_ReturnsValue()
        {
            var bitmap = new RGBBitmap(new Vector2D(), new Vector2D(1.99, 0.99), 0.01);
            var x = 1;
            var y = 1;
            var c = 17.19;
            bitmap.PixelAdd(x, y, c);

            bitmap.PickRed(x, y).ShouldBe(c);
            bitmap.PickGreen(x, y).ShouldBe(c);
            bitmap.PickBlue(x, y).ShouldBe(c);
        }

        [Test]
        public void PlotAdd_Twice_ReturnsDoubleValue()
        {
            var bitmap = new RGBBitmap(new Vector2D(), new Vector2D(1.99, 0.99), 0.01);
            var x = 1;
            var y = 1;
            var c = 17.19;
            bitmap.PixelAdd(x, y, c);
            bitmap.PixelAdd(x, y, c);

            bitmap.PickRed(x, y).ShouldBe(c * 2.0);
            bitmap.PickBlue(x, y).ShouldBe(c * 2.0);
            bitmap.PickGreen(x, y).ShouldBe(c * 2.0);
        }

        [Test]
        public void PlotSet_Twice_ReturnsValue()
        {
            var bitmap = new RGBBitmap(new Vector2D(), new Vector2D(1.99, 0.99), 0.01);
            var x = 1;
            var y = 1;
            var c = 17.19;
            bitmap.PixelSet(x, y, c);
            bitmap.PixelSet(x, y, c);

            bitmap.PickRed(x, y).ShouldBe(c);
            bitmap.PickGreen(x, y).ShouldBe(c);
            bitmap.PickBlue(x, y).ShouldBe(c);
        }

        [Test]
        public void SetMagnitudePlot_Twice_ReturnsMax()
        {
            var bitmap = new RGBBitmap(new Vector2D(), new Vector2D(1.99, 0.99), 0.01);
            var x = 1;
            var y = 1;
            var c = 17.19;
            var cMax = 19.17;
            bitmap.SetMagnitude.Plot(x, y, c, cMax);
            bitmap.SetMagnitude.Plot(x, y, c, cMax);

            bitmap.PickRed(x, y).ShouldBe(cMax);
            bitmap.PickGreen(x, y).ShouldBe(cMax);
            bitmap.PickBlue(x, y).ShouldBe(cMax);
        }

        [Test]
        public void SetPlot_Twice_ReturnsMax()
        {
            var bitmap = new RGBBitmap(new Vector2D(), new Vector2D(1.99, 0.99), 0.01);
            var x = 1;
            var y = 1;
            var c = 17.19;
            var cMax = 19.17;
            bitmap.Set.Plot(x, y, c, cMax);
            bitmap.Set.Plot(x, y, c, cMax);

            bitmap.PickRed(x, y).ShouldBe(c);
            bitmap.PickGreen(x, y).ShouldBe(c);
            bitmap.PickBlue(x, y).ShouldBe(c);
        }

        [Test]
        public void ConvertToBitmap()
        {
            var bitmap = new RGBBitmap(new Vector2D(), new Vector2D(1.99, 0.99), 0.01);
            var v = bitmap.ConvertToBitmap(new Vector2D(0.1, 0.5));
            v.X.ShouldBe(10);
            v.Y.ShouldBe(50);
        }
    }
}