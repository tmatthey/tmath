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
    /// Aggregated contract for full vector-like types. Composed from smaller capabilities so that
    /// downstream code can ask for the narrowest interface it actually needs (Interface
    /// Segregation):
    /// <list type="bullet">
    /// <item><description><see cref="IGeometryObject{T}"/> - dimensionality + array indexing + INorm</description></item>
    /// <item><description><see cref="IBoundingFacade{T}"/> - axis-aligned bounding box</description></item>
    /// <item><description><see cref="IIsEqual{T}"/> - epsilon-tolerant equality</description></item>
    /// <item><description><see cref="INormalizable{T}"/> - length and unit-length scaling</description></item>
    /// <item><description><see cref="IInnerProduct{T}"/> - dot, cross-norm, angles</description></item>
    /// <item><description><see cref="IVectorArith{T}"/> - Add/Sub/Mul/Div</description></item>
    /// <item><description><see cref="IInterpolate{T}"/> - linear interpolation</description></item>
    /// </list>
    /// Existing implementations (Vector2D, Vector3D, GpsPoint, ...) already provide every
    /// member; the split is source-compatible.
    /// </summary>
    public interface IVector<T> :
        IGeometryObject<T>,
        IBoundingFacade<T>,
        IIsEqual<T>,
        INormalizable<T>,
        IInnerProduct<T>,
        IVectorArith<T>,
        IInterpolate<T>
    {
        /// <summary>X coordinate (mutable; preserved from the original IVector contract).</summary>
        double X { get; set; }
    }
}
