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

using System.IO;
using System.Text;
#if NETCOREAPP1_1
using Drawing = System.DrawingCore;
#elif NETSTANDARD1_5
using Drawing = System.DrawingCore;
#else
using Drawing = System.Drawing;
#endif

namespace Math.Gfx
{
    public static class BitmapFileWriter
    {
        public static void PGM(string fileName, double[,] bitmap)
        {
            PGM(fileName, bitmap, GreyMapping.Default);
        }

        public static void PGM(string fileName, double[,] bitmap, IColorMapping colorMap)
        {
            var width = bitmap.GetLength(0);
            var height = bitmap.GetLength(1);
            var header = "P5\n" + width + " " + height + "\n255\n";
            var writer = new BinaryWriter(new FileStream(fileName, FileMode.Create));
            writer.Write(Encoding.ASCII.GetBytes(header));
            for (var j = height - 1; j >= 0; j--)
            {
                for (var i = 0; i < width; i++)
                {
                    writer.Write(colorMap.Grey(bitmap[i, j]));
                }
            }
        }

        public static void PPM(string fileName, double[,] bitmap, IColorMapping colorMap)
        {
            var width = bitmap.GetLength(0);
            var height = bitmap.GetLength(1);
            var header = "P6\n" + width + " " + height + "\n255\n";
            var writer = new BinaryWriter(new FileStream(fileName, FileMode.Create));
            writer.Write(Encoding.ASCII.GetBytes(header));
            for (var j = height - 1; j >= 0; j--)
            {
                for (var i = 0; i < width; i++)
                {
                    var col = colorMap.Color(bitmap[i, j]);
                    writer.Write(col.Red);
                    writer.Write(col.Green);
                    writer.Write(col.Blue);
                }
            }
        }

        public static void PNG(string fileName, double[,] bitmap)
        {
            PNG(fileName, bitmap, GreyMapping.Default);
        }

        public static void PNG(string fileName, double[,] bitmap, IColorMapping colorMap)
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

            image.Save(new FileStream(fileName, FileMode.Create), Drawing.Imaging.ImageFormat.Png);
        }
        public static void PNG(string fileName, double[,] red, double[,] green, double[,] blue)
        {
            var width = red.GetLength(0);
            var height = red.GetLength(1);
            var image = new Drawing.Bitmap(width, height);
            var colorMap = GreenMapping.Default;
            for (var j = 0; j < height; j++)
            {
                for (var i = 0; i < width; i++)
                {
                    image.SetPixel(i, height - j - 1, Drawing.Color.FromArgb(colorMap.Grey(red[i, j]), colorMap.Grey(green[i, j]), colorMap.Grey(blue[i, j])));
                }
            }

            image.Save(new FileStream(fileName, FileMode.Create), Drawing.Imaging.ImageFormat.Png);
        }
    }
}