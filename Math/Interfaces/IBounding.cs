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

namespace Math.Interfaces
{
    /// <summary>
    /// Mutable axis-aligned bounding region. Extends the read-only view in
    /// <see cref="IReadOnlyBounding{T}"/> with growth operations (Reset, Expand, ExpandLayer).
    /// Consumers that only need to read bounds should depend on <see cref="IReadOnlyBounding{T}"/>
    /// instead so they cannot accidentally widen the box (Interface Segregation).
    /// </summary>
    /// <typeparam name="T">Coordinate type (Vector2D, Vector3D, ...).</typeparam>
    public interface IBounding<T> : IReadOnlyBounding<T>
    {
        /// <summary>Resets the bounding region to the empty state.</summary>
        void Reset();

        /// <summary>Expands the bounding region to cover <paramref name="v"/>.</summary>
        void Expand(T v);

        /// <summary>Expands the bounding region to cover another bounding region.</summary>
        void Expand(IBounding<T> b);

        /// <summary>Adds an isotropic margin of width <paramref name="r"/> around the current region.</summary>
        void ExpandLayer(double r);
    }
}
