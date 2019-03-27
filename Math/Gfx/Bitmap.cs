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

namespace Math.Gfx
{
    public class Bitmap
    {
        private const int MaxLength = int.MaxValue;
        private const double Eps = 1e-9;
        private readonly Vector2D _min;
        private readonly int _nx;
        private readonly int _ny;
        private readonly double _pixelSize;

        public Bitmap(Vector2D min, Vector2D max, double pixelSize, int maxLength = MaxLength)
        {
            var v = max - min;

            var a = System.Math.Abs(maxLength - 2.0) + Eps;
            var maxPixel = System.Math.Max(v.X / a, v.Y / a);
            _min = new Vector2D(min);
            _pixelSize = System.Math.Max(pixelSize, maxPixel);

            _nx = (int) System.Math.Floor(System.Math.Max(v.X / _pixelSize, 0.0) + 2.0 - Eps);
            _ny = (int) System.Math.Floor(System.Math.Max(v.Y / _pixelSize, 0.0) + 2.0 - Eps);
            Pixels = new double[_nx, _ny];

            Add = new PlotWrapper(ConvertToBitmap, PixelAdd);
            Set = new PlotWrapper(ConvertToBitmap, PixelSet);
            AddMagnitude = new PlotWrapper(ConvertToBitmap, PixelAddMagnitude);
            SetMagnitude = new PlotWrapper(ConvertToBitmap, PixelSetMagnitude);
        }

        public PlotWrapper Add { get; private set; }
        public PlotWrapper Set { get; private set; }
        public PlotWrapper AddMagnitude { get; private set; }
        public PlotWrapper SetMagnitude { get; private set; }
        public double[,] Pixels { get; }

        public void PixelAdd(int x, int y, double c)
        {
            if (IsInRange(x, y))
            {
                Pixels[x, y] += c;
            }
        }

        public void PixelSet(int x, int y, double c)
        {
            if (IsInRange(x, y))
            {
                Pixels[x, y] = c;
            }
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

        public double Pick(int x, int y)
        {
            return IsInRange(x, y) ? Pixels[x, y] : double.NaN;
        }

        public Vector2D ConvertToBitmap(Vector2D x)
        {
            return (x - _min) / _pixelSize;
        }

        private bool IsInRange(int x, int y)
        {
            return 0 <= x && 0 <= y && x < _nx && y < _ny;
        }
    }
}