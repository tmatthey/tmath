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

using Math.Interfaces;

namespace Math
{
    /// <summary>
    /// 3D bounding box
    /// </summary>
    public class BoundingBox : IBounding<Vector3D>, ICloneable<BoundingBox>, IIsEqual<BoundingBox>
    {
        /// <summary>
        /// Empty bounding box
        /// </summary>
        public BoundingBox()
        {
            Min = new Vector3D(double.PositiveInfinity);
            Max = new Vector3D(double.NegativeInfinity);
        }

        /// <summary>
        /// Bounding box with one point
        /// </summary>
        /// <param name="v"></param>
        public BoundingBox(Vector3D v)
        {
            Min = new Vector3D(v);
            Max = new Vector3D(v);
        }

        /// <summary>
        /// Bounding box 
        /// </summary>
        /// <param name="b"></param>
        public BoundingBox(BoundingBox b)
        {
            Min = new Vector3D(b.Min);
            Max = new Vector3D(b.Max);
        }

        /// <inheritdoc />
        public Vector3D Min { get; protected set; }

        /// <inheritdoc />
        public Vector3D Max { get; protected set; }

        /// <inheritdoc />
        public bool IsEmpty()
        {
            return Min.X > Max.X && Min.Y > Max.Y && Min.Z > Max.Z;
        }

        /// <inheritdoc />
        public void Reset()
        {
            Min.X = double.PositiveInfinity;
            Min.Y = double.PositiveInfinity;
            Min.Z = double.PositiveInfinity;
            Max.X = double.NegativeInfinity;
            Max.Y = double.NegativeInfinity;
            Max.Z = double.NegativeInfinity;
        }

        /// <inheritdoc />
        public void Expand(Vector3D v)
        {
            ExpandX(v.X);
            ExpandY(v.Y);
            ExpandZ(v.Z);
        }

        /// <inheritdoc />
        public void ExpandLayer(double r)
        {
            var min = (!IsEmpty() ? Min : Vector3D.Zero) - Vector3D.One * r;
            var max = (!IsEmpty() ? Max : Vector3D.Zero) + Vector3D.One * r;
            Expand(min);
            Expand(max);
        }

        /// <inheritdoc />
        public bool IsInside(Vector3D v)
        {
            return IsInside(v, Comparison.Epsilon);
        }

        /// <inheritdoc />
        public bool IsInside(Vector3D v, double eps)
        {
            if (IsEmpty())
                return false;

            return Comparison.IsLessEqual(Min.X, v.X, eps) && Comparison.IsLessEqual(Min.Y, v.Y, eps) &&
                   Comparison.IsLessEqual(Min.Z, v.Z, eps) &&
                   Comparison.IsLessEqual(v.X, Max.X, eps) && Comparison.IsLessEqual(v.Y, Max.Y, eps) &&
                   Comparison.IsLessEqual(v.Z, Max.Z, eps);
        }

        /// <inheritdoc />
        public void Expand(IBounding<Vector3D> b)
        {
            Expand(b.Min);
            Expand(b.Max);
        }

        /// <inheritdoc />
        public BoundingBox Clone()
        {
            return new BoundingBox(this);
        }

        /// <inheritdoc />
        public bool IsEqual(BoundingBox b)
        {
            return IsEqual(b, Comparison.Epsilon);
        }

        /// <inheritdoc />
        public bool IsEqual(BoundingBox b, double epsilon)
        {
            return Min.IsEqual(b.Min, epsilon) && Max.IsEqual(b.Max, epsilon);
        }

        /// <summary>
        /// Expand by X-coordinate
        /// </summary>
        /// <param name="x"></param>
        public void ExpandX(double x)
        {
            Min.X = ExpandMin(Min.X, x);
            Max.X = ExpandMax(Max.X, x);
        }

        /// <summary>
        /// Expand by Y-coordinate
        /// </summary>
        /// <param name="y"></param>
        public void ExpandY(double y)
        {
            Min.Y = ExpandMin(Min.Y, y);
            Max.Y = ExpandMax(Max.Y, y);
        }

        /// <summary>
        /// Expand by Z-coordinate
        /// </summary>
        /// <param name="z"></param>
        public void ExpandZ(double z)
        {
            Min.Z = ExpandMin(Min.Z, z);
            Max.Z = ExpandMax(Max.Z, z);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && IsEqual((BoundingBox) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Min.GetHashCode();
                hashCode = (hashCode * 397) ^ Max.GetHashCode();
                return hashCode;
            }
        }

        private double ExpandMin(double a, double b)
        {
            if (Comparison.IsNumber(b))
            {
                return Comparison.IsNumber(a) ? System.Math.Min(a, b) : b;
            }

            return a;
        }

        private double ExpandMax(double a, double b)
        {
            if (Comparison.IsNumber(b))
            {
                return Comparison.IsNumber(a) ? System.Math.Max(a, b) : b;
            }

            return a;
        }
    }
}