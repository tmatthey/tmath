﻿/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016 Thierry Matthey
 *
 * Permission is hereby granted, free of charge, to any person
 * obtaining x0 copy of this software and associated documentation
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

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace Math.Gfx
{
    static public class BitmapFileWriter
    {
        public static void PGM(string fileName, double[,] bitmap)
        {
            var width = bitmap.GetLength(0);
            var height = bitmap.GetLength(1);
            var header = "P5\n" + width + " " + height + "\n255\n";
            var writer = new BinaryWriter(new FileStream(fileName, FileMode.Create));
            writer.Write(Encoding.ASCII.GetBytes(header));
            for (var j = 0; j < height; j++)
            {
                for (var i = width-1; i >=0; i--)
                {
                    var c = (byte)(System.Math.Max(System.Math.Min(1.0, bitmap[i, j]), 0.0) * 255.0);
                    writer.Write(c);
                }
            }
            writer.Close();
        }

        public static void PNG(string fileName, double[,] bitmap)
        {
            var width = bitmap.GetLength(0);
            var height = bitmap.GetLength(1);
            var image = new Bitmap(width, height);
            for (var j = 0; j < height; j++)
            {
                for (var i = 0; i < width; i++)
                {
                    var c = (byte)(System.Math.Max(System.Math.Min(1.0, bitmap[i, j]), 0.0) * 255.0);
                    image.SetPixel(width-i-1, j, Color.FromArgb(c, c, c));
                }
            }
            image.Save(new FileStream(fileName, FileMode.Create), ImageFormat.Png);
        }
    }
}