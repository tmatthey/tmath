/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2017 Thierry Matthey
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
    public class BoundingRect : IBounding<Vector2D>, ICloneable, IIsEqual<BoundingRect>
    {
        public BoundingRect()
        {
            Min = new Vector2D(double.PositiveInfinity);
            Max = new Vector2D(double.NegativeInfinity);
        }

        public BoundingRect(Vector2D v)
        {
            Min = new Vector2D(v);
            Max = new Vector2D(v);
        }

        public BoundingRect(BoundingRect b)
        {
            Min = new Vector2D(b.Min);
            Max = new Vector2D(b.Max);
        }

        public Vector2D Min { get; protected set; }
        public Vector2D Max { get; protected set; }

        public bool IsEmpty()
        {
            return Min.X > Max.X && Min.Y > Max.Y;
        }

        public void Reset()
        {
            Min.X = double.PositiveInfinity;
            Min.Y = double.PositiveInfinity;
            Max.X = double.NegativeInfinity;
            Max.Y = double.NegativeInfinity;
        }

        public void Expand(Vector2D v)
        {
            ExpandX(v.X);
            ExpandY(v.Y);
        }

        public void Expand(IBounding<Vector2D> b)
        {
            Expand(b.Min);
            Expand(b.Max);
        }

        public void ExpandLayer(double r)
        {
            var min = (!IsEmpty() ? Min : Vector2D.Zero) - Vector2D.One*r;
            var max = (!IsEmpty() ? Max : Vector2D.Zero) + Vector2D.One*r;
            Expand(min);
            Expand(max);
        }

        public bool IsInside(Vector2D v)
        {
            return IsInside(v, Comparison.Epsilon);
        }

        public bool IsInside(Vector2D v, double eps)
        {
            if (IsEmpty())
                return false;

            return Comparison.IsLessEqual(Min.X, v.X, eps) && Comparison.IsLessEqual(Min.Y, v.Y, eps) &&
                   Comparison.IsLessEqual(v.X, Max.X, eps) && Comparison.IsLessEqual(v.Y, Max.Y, eps);
        }

        public object Clone()
        {
            return new BoundingRect(this);
        }

        public bool IsEqual(BoundingRect b)
        {
            return IsEqual(b, Comparison.Epsilon);
        }

        public bool IsEqual(BoundingRect b, double epsilon)
        {
            return Min.IsEqual(b.Min, epsilon) && Max.IsEqual(b.Max, epsilon);
        }

        public void ExpandX(double x)
        {
            Min.X = ExpandMin(Min.X, x);
            Max.X = ExpandMax(Max.X, x);
        }

        public void ExpandY(double y)
        {
            Min.Y = ExpandMin(Min.Y, y);
            Max.Y = ExpandMax(Max.Y, y);
        }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && IsEqual((BoundingRect) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Min.GetHashCode();
                hashCode = (hashCode*397) ^ Max.GetHashCode();
                return hashCode;
            }
        }

        private static double ExpandMin(double a, double b)
        {
            if (Comparison.IsNumber(b))
            {
                return Comparison.IsNumber(a) ? System.Math.Min(a, b) : b;
            }
            return a;
        }

        private static double ExpandMax(double a, double b)
        {
            if (Comparison.IsNumber(b))
            {
                return Comparison.IsNumber(a) ? System.Math.Max(a, b) : b;
            }
            return a;
        }
    }
}