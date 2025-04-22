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
    /// Interface vector
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IVector<T> : IGeometryObject<T>, IBoundingFacade<T>
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        double X { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        /// <summary>
        /// Is equal
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        bool IsEqual(T v);

        /// <summary>
        /// Is equal with epsilon
        /// </summary>
        /// <param name="v"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        bool IsEqual(T v, double epsilon);

        /// <summary>
        /// Normalize vector
        /// </summary>
        /// <returns>Length of vector</returns>
        double Normalize();

        /// <summary>
        /// Normalize vector with epsilon
        /// </summary>
        /// <param name="epsilon"></param>
        /// <returns>Length of vector</returns>
        double Normalize(double epsilon);

        /// <summary>
        /// Return normalized vector
        /// </summary>
        /// <returns></returns>
        T Normalized();

        /// <summary>
        /// Return normalized vector with epsilon
        /// </summary>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        T Normalized(double epsilon);

        /// <summary>
        /// Squared norm of vector
        /// </summary>
        /// <returns></returns>
        double Norm2();

        /// <summary>
        /// Norm
        /// </summary>
        /// <returns></returns>
        double Norm();

        /// <summary>
        /// Dot product
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        double Dot(T v);

        /// <summary>
        /// Norm of cross product
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        double CrossNorm(T v);

        /// <summary>
        /// Angle between two vectors
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        double Angle(T v);

        /// <summary>
        /// Unsigned angle between two vectors
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        double AngleAbs(T v);

        /// <summary>
        /// Add vector
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        T Add(T v);

        /// <summary>
        /// Subtract vector
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        T Sub(T v);

        /// <summary>
        /// Multiply with a scalar
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        T Mul(double c);

        /// <summary>
        /// Divide with a scalar
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        T Div(double c);

        /// <summary>
        /// Interpolate between two vectors
        /// </summary>
        /// <param name="v"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        T Interpolate(T v, double x);
    }
}