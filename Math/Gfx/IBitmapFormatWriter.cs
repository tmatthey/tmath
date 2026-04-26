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
    /// Strategy interface for serialising a <c>double[,]</c> intensity raster to a stream in a
    /// specific bitmap container format (PGM, PPM, PNG, ...). Implementations are expected to be
    /// stateless and thread-safe so that <see cref="BitmapFileWriter"/> can pick a writer based
    /// on file extension or caller intent without coupling the call site to a specific format.
    /// </summary>
    public interface IBitmapFormatWriter
    {
        /// <summary>
        /// Writes <paramref name="bitmap"/> to <paramref name="stream"/> using
        /// <paramref name="colorMap"/> to translate intensities into pixel values. The caller
        /// retains ownership of the stream and is responsible for disposing it.
        /// </summary>
        void Write(Stream stream, double[,] bitmap, IColorMapping colorMap);
    }
}
