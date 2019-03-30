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

namespace Math.Interfaces
{
    /// <summary>
    /// Interface of bounding box or rectangle
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBounding<T>
    {
        /// <summary>
        /// Min
        /// </summary>
        T Min { get; }

        /// <summary>
        /// Max
        /// </summary>
        T Max { get; }

        /// <summary>
        /// Is empty bounding box
        /// </summary>
        /// <returns></returns>
        bool IsEmpty();

        /// <summary>
        /// Reset
        /// </summary>
        void Reset();

        /// <summary>
        /// Expand bounding box with vector / point
        /// </summary>
        /// <param name="v">vector / point</param>
        void Expand(T v);

        /// <summary>
        /// Expanding with another bounding box
        /// </summary>
        /// <param name="b"></param>
        void Expand(IBounding<T> b);

        /// <summary>
        /// Add layer aorund current bounding box
        /// </summary>
        /// <param name="r">radius</param>
        void ExpandLayer(double r);

        /// <summary>
        /// Is point inside
        /// </summary>
        /// <param name="v">vector / point</param>
        /// <returns></returns>
        bool IsInside(T v);

        /// <summary>
        /// Is point inside
        /// </summary>
        /// <param name="v">point / vector</param>
        /// <param name="eps">epsilon</param>
        /// <returns></returns>
        bool IsInside(T v, double eps);
    }
}