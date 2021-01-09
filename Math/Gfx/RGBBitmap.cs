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

namespace Math.Gfx
{
    public class RGBBitmap : IBitmap
    {
        private const int MaxLength = int.MaxValue;
        private readonly Bitmap _red;
        private readonly Bitmap _green;
        private readonly Bitmap _blue;


        public RGBBitmap(Vector2D min, Vector2D max, double pixelSize, int maxLength = MaxLength)
        {
            Color = Color.Default.Black;
            _red = new Bitmap(min, max, pixelSize, maxLength);
            _green = new Bitmap(min, max, pixelSize, maxLength);
            _blue = new Bitmap(min, max, pixelSize, maxLength);

            Add = new PlotWrapper(_red.ConvertToBitmap, PixelAdd);
            Set = new PlotWrapper(_red.ConvertToBitmap, PixelSet);
            AddMagnitude = new PlotWrapper(_red.ConvertToBitmap, PixelAddMagnitude);
            SetMagnitude = new PlotWrapper(_red.ConvertToBitmap, PixelSetMagnitude);
        }

        public Color Color { get; set; }

        public PlotWrapper Add { get; }
        public PlotWrapper Set { get; }
        public PlotWrapper AddMagnitude { get; }
        public PlotWrapper SetMagnitude { get; }
        public double[,] RedPixels => _red.Pixels;
        public double[,] GreenPixels => _green.Pixels;
        public double[,] BluePixels => _blue.Pixels;

        public void PixelAdd(int x, int y, double c)
        {
            if (IsInRange(x, y))
            {
                _red.Pixels[x, y] += c * (255 - Color.Red) / 255.0;
                _green.Pixels[x, y] += c * (255 - Color.Green) / 255.0;
                _blue.Pixels[x, y] += c * (255 - Color.Blue) / 255.0;
            }
        }

        public void PixelSet(int x, int y, double c)
        {
            if (IsInRange(x, y))
            {
                _red.Pixels[x, y] = c * (255 - Color.Red) / 255.0;
                _green.Pixels[x, y] = c * (255 - Color.Green) / 255.0;
                _blue.Pixels[x, y] = c * (255 - Color.Blue) / 255.0;
            }
        }

        public double PickRed(int x, int y)
        {
            return _red.Pick(x, y);
        }

        public double PickGreen(int x, int y)
        {
            return _green.Pick(x, y);
        }

        public double PickBlue(int x, int y)
        {
            return _blue.Pick(x, y);
        }

        public Vector2D ConvertToBitmap(Vector2D x)
        {
            return _red.ConvertToBitmap(x);
        }

        public bool IsInRange(int x, int y)
        {
            return _red.IsInRange(x, y);
        }

        private void PixelAdd(int x, int y, double c, double cMax)
        {
            PixelAdd(x, y, c);
        }

        private void PixelSet(int x, int y, double c, double cMax)
        {
            PixelSet(x, y, c);
        }

        private void PixelAddMagnitude(int x, int y, double c, double cMax)
        {
            PixelAdd(x, y, cMax);
        }

        private void PixelSetMagnitude(int x, int y, double c, double cMax)
        {
            PixelSet(x, y, cMax);
        }
    }
}