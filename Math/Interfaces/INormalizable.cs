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
    /// Capability of having a (Euclidean) length and being scaled to unit length. Split out of
    /// the previous monolithic <see cref="IVector{T}"/> so callers that only need a "I have a
    /// magnitude" view of a value can ask for it without dragging in arithmetic and inner-product
    /// dependencies (Interface Segregation).
    /// </summary>
    /// <typeparam name="T">Concrete vector type returned by <see cref="Normalized()"/>.</typeparam>
    public interface INormalizable<T>
    {
        /// <summary>Squared norm of the vector.</summary>
        double Norm2();

        /// <summary>Norm of the vector.</summary>
        double Norm();

        /// <summary>Normalise this vector in place; returns the original length.</summary>
        double Normalize();

        /// <summary>Normalise this vector in place using <paramref name="epsilon"/>; returns the original length.</summary>
        double Normalize(double epsilon);

        /// <summary>Returns a normalised copy.</summary>
        T Normalized();

        /// <summary>Returns a normalised copy using <paramref name="epsilon"/>.</summary>
        T Normalized(double epsilon);
    }
}
