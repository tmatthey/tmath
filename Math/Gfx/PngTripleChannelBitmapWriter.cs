/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2026 Thierry Matthey
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
using Drawing = System.DrawingCore;

namespace Math.Gfx
{
    /// <summary>
    /// Three-channel PNG writer: each input raster supplies one of R, G, B mixed through the
    /// shared <see cref="GreenMapping"/> grey ramp. Kept as a separate writer so that all
    /// <see cref="System.Drawing"/> usage in the Math assembly is confined to the two
    /// <c>Png*BitmapWriter</c> files - a future split into a separate
    /// <c>Math.Gfx.SystemDrawing</c> assembly only has to relocate this file and
    /// <see cref="PngBitmapFormatWriter"/>.
    /// </summary>
    public static class PngTripleChannelBitmapWriter
    {
        public static void Write(Stream stream, double[,] red, double[,] green, double[,] blue)
        {
            var width = red.GetLength(0);
            var height = red.GetLength(1);
            var image = new Drawing.Bitmap(width, height);
            var colorMap = GreenMapping.Default;
            for (var j = 0; j < height; j++)
            {
                for (var i = 0; i < width; i++)
                {
                    image.SetPixel(i, height - j - 1,
                        Drawing.Color.FromArgb(colorMap.Grey(red[i, j]), colorMap.Grey(green[i, j]),
                            colorMap.Grey(blue[i, j])));
                }
            }

            image.Save(stream, Drawing.Imaging.ImageFormat.Png);
        }
    }
}
