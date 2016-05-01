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
    public class BitmapAdd : IBitmap
    {
        private readonly Vector2D _min;
        private readonly int _nx;
        private readonly int _ny;
        private readonly double _pixelSize;

        public BitmapAdd(Vector2D min, Vector2D max, double pixelSize)
        {
            _pixelSize = pixelSize;
            var d = new Vector2D(pixelSize*0.5);
            _min = min - d;
            var v = (max + d) - _min;
            _nx = (int) System.Math.Floor(v.X/pixelSize + 0.5) + 1;
            _ny = (int) System.Math.Floor(v.Y/pixelSize + 0.5) + 1;
            Bitmap = new double[_nx, _ny];
        }

        public double[,] Bitmap { get; private set; }

        public void Plot(double x, double y, double c)
        {
            var i = (int) x;
            var j = (int) y;
            if (0 <= i && 0 <= j && i < _nx && j < _ny)
            {
                Bitmap[i, j] += c;
            }
        }

        public void Plot(Vector2D x, double c)
        {
            var v = ConvertToBitmap(x);
            Plot(v.X, v.Y, c);
        }

        public double Pick(double x, double y)
        {
            var i = (int) x;
            var j = (int) y;
            if (0 <= i && 0 <= j && i < _nx && j < _ny)
            {
                return Bitmap[i, j];
            }
            return double.NaN;
        }

        public double Pick(Vector2D x)
        {
            var v = ConvertToBitmap(x);
            return Pick(v.X, v.Y);
        }

        public Vector2D ConvertToBitmap(Vector2D x)
        {
            return (x - _min)/_pixelSize;
        }
    }
}