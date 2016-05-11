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

namespace Math.Gfx
{
    public class Bitmap
    {
        private readonly Vector2D _min;
        private readonly int _nx;
        private readonly int _ny;
        private readonly double _pixelSize;

        public Bitmap(Vector2D min, Vector2D max, double pixelSize)
        {
            _pixelSize = pixelSize;
            var d = new Vector2D(pixelSize*0.5);
            _min = min;
            var v = max - min;
            _nx = (int) System.Math.Floor(System.Math.Max(v.X/pixelSize, 0.0) + 2.0 - 1e-9);
            _ny = (int) System.Math.Floor(System.Math.Max(v.Y/pixelSize, 0.0) + 2.0 - 1e-9);
            Pixels = new double[_nx, _ny];

            Add = new PlotWrapper(ConvertToBitmap, PixelAdd);
            Set = new PlotWrapper(ConvertToBitmap, PixelSet);
        }

        public PlotWrapper Add { get; private set; }
        public PlotWrapper Set { get; private set; }
        public double[,] Pixels { get; private set; }

        public void PixelAdd(int x, int y, double c)
        {
            if (0 <= x && 0 <= y && x < _nx && y < _ny)
            {
                Pixels[x, y] += c;
            }
        }

        public void PixelSet(int x, int y, double c)
        {
            if (0 <= x && 0 <= y && x < _nx && y < _ny)
            {
                Pixels[x, y] = c;
            }
        }

        public double Pick(int x, int y)
        {
            if (0 <= x && 0 <= y && x < _nx && y < _ny)
            {
                return Pixels[x, y];
            }
            return double.NaN;
        }

        public Vector2D ConvertToBitmap(Vector2D x)
        {
            return (x - _min)/_pixelSize;
        }
    }
}