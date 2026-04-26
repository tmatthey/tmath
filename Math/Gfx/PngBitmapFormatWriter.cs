/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2025 Thierry Matthey
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

using System.IO;
#if NETSTANDARD2_0
using Drawing = System.DrawingCore;
#else
using Drawing = System.Drawing;
#endif

namespace Math.Gfx
{
    /// <summary>
    /// Writes the bitmap as a PNG using <see cref="System.Drawing"/>. This is the only writer
    /// that pulls in the System.Drawing dependency, so isolating it here keeps the PGM/PPM
    /// writers (and any future cross-platform alternatives) free of that coupling.
    /// </summary>
    public sealed class PngBitmapFormatWriter : IBitmapFormatWriter
    {
        public void Write(Stream stream, double[,] bitmap, IColorMapping colorMap)
        {
            var width = bitmap.GetLength(0);
            var height = bitmap.GetLength(1);
            var image = new Drawing.Bitmap(width, height);
            for (var j = 0; j < height; j++)
            {
                for (var i = 0; i < width; i++)
                {
                    var c = colorMap.Color(bitmap[i, j]);
                    image.SetPixel(i, height - j - 1, Drawing.Color.FromArgb(c.Red, c.Green, c.Blue));
                }
            }

            image.Save(stream, Drawing.Imaging.ImageFormat.Png);
        }
    }
}
