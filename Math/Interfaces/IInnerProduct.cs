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

namespace Math.Interfaces
{
    /// <summary>
    /// Inner-product / angular operations on a vector type. Lifted out of <see cref="IVector{T}"/>
    /// so callers that only need pairwise dot products and angles can depend on this narrower
    /// surface (Interface Segregation).
    /// </summary>
    public interface IInnerProduct<in T>
    {
        /// <summary>Dot product.</summary>
        double Dot(T v);

        /// <summary>Norm of the cross product (well-defined in 2D and 3D).</summary>
        double CrossNorm(T v);

        /// <summary>Signed angle between this vector and <paramref name="v"/>.</summary>
        double Angle(T v);

        /// <summary>Unsigned angle between this vector and <paramref name="v"/>.</summary>
        double AngleAbs(T v);
    }
}
