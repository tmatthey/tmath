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

namespace Math.Gfx
{
    /// <summary>
    /// Path-based facade over <see cref="IBitmapFormatWriter"/> strategies. The four overloads
    /// are kept for source compatibility; new format implementations can plug in by adding a
    /// fresh <see cref="IBitmapFormatWriter"/> without modifying this type (Open/Closed).
    /// All <see cref="System.Drawing"/> usage in this assembly is delegated to the
    /// <c>Png*</c> strategies (<see cref="PngBitmapFormatWriter"/>,
    /// <see cref="PngTripleChannelBitmapWriter"/>) so that a future assembly split (DIP) only
    /// has to relocate those files.
    /// </summary>
    public static class BitmapFileWriter
    {
        public static readonly IBitmapFormatWriter Pgm = new PgmBitmapFormatWriter();
        public static readonly IBitmapFormatWriter Ppm = new PpmBitmapFormatWriter();
        public static readonly IBitmapFormatWriter Png = new PngBitmapFormatWriter();

        public static void PGM(string fileName, double[,] bitmap)
        {
            PGM(fileName, bitmap, GreyMapping.Default);
        }

        public static void PGM(string fileName, double[,] bitmap, IColorMapping colorMap)
        {
            WriteToFile(fileName, bitmap, colorMap, Pgm);
        }

        public static void PPM(string fileName, double[,] bitmap, IColorMapping colorMap)
        {
            WriteToFile(fileName, bitmap, colorMap, Ppm);
        }

        public static void PNG(string fileName, double[,] bitmap)
        {
            PNG(fileName, bitmap, GreyMapping.Default);
        }

        public static void PNG(string fileName, double[,] bitmap, IColorMapping colorMap)
        {
            WriteToFile(fileName, bitmap, colorMap, Png);
        }

        public static void PNG(string fileName, double[,] red, double[,] green, double[,] blue)
        {
            // The 3-channel PNG path mixes three intensity rasters through the GreenMapping grey
            // ramp, which is not expressible through the single-channel IBitmapFormatWriter
            // contract. Delegate to PngTripleChannelBitmapWriter so all System.Drawing usage in
            // this assembly stays confined to the Png* strategy files.
            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                PngTripleChannelBitmapWriter.Write(stream, red, green, blue);
            }
        }

        private static void WriteToFile(string fileName, double[,] bitmap, IColorMapping colorMap,
            IBitmapFormatWriter writer)
        {
            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                writer.Write(stream, bitmap, colorMap);
            }
        }
    }
}
