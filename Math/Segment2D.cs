/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016 Thierry Matthey
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

using System;
using Math.Interfaces;

namespace Math
{
    public class Segment2D : ISegment<Vector2D>, Interfaces.ICloneable, IIsEqual<Segment2D>
    {
        public Segment2D()
        {
            A = new Vector2D();
            B = new Vector2D();
        }

        public Segment2D(Segment2D d)
        {
            A = new Vector2D(d.A);
            B = new Vector2D(d.B);
        }

        public Segment2D(Vector2D a, Vector2D b)
        {
            A = new Vector2D(a);
            B = new Vector2D(b);
        }

        public object Clone()
        {
            return new Segment2D(this);
        }

        public bool IsEqual(Segment2D s)
        {
            return IsEqual(s, Comparison.Epsilon);
        }

        public bool IsEqual(Segment2D s, double epsilon)
        {
            return A.IsEqual(s.A, epsilon) && B.IsEqual(s.B, epsilon);
        }

        public Vector2D A { get; set; }
        public Vector2D B { get; set; }

        public double Length()
        {
            return A.EuclideanNorm(B);
        }

        public Vector2D Vector()
        {
            return B - A;
        }

        public bool IsIntersecting(ISegment<Vector2D> s, double eps = Comparison.Epsilon)
        {
            return Comparison.IsLessEqual(EuclideanNorm(s), eps, 0);
        }

        public double EuclideanNorm(ISegment<Vector2D> d)
        {
            var a0 = Geometry.PerpendicularSegmentDistance(A, B, d.A);
            var a1 = Geometry.PerpendicularSegmentDistance(A, B, d.B);
            var b0 = Geometry.PerpendicularSegmentDistance(d.A, d.B, A);
            var b1 = Geometry.PerpendicularSegmentDistance(d.A, d.B, B);
            var l = System.Math.Min(System.Math.Min(a0, a1), System.Math.Min(b0, b1));
            if (Comparison.IsZero(l))
                return 0.0;
            if (Comparison.IsZero(Length()) || Comparison.IsZero(d.Length()))
                return l;

            var a = B - A;
            var b = d.B - d.A;
            var ab = Vector2D.Cross(a, b);
            if (Comparison.IsZero(ab))
                return l;

            var c = d.A - A;
            var s = Vector2D.Cross(c, b)/ab;
            var t = Vector2D.Cross(c, a)/ab;

            if (Comparison.IsLessEqual(s, 0.0) || Comparison.IsLessEqual(1.0, s) ||
                Comparison.IsLessEqual(t, 0.0) || Comparison.IsLessEqual(1.0, t))
                return l;

            return 0.0;
        }

        public double ModifiedNorm(ISegment<Vector2D> d, bool direction = true)
        {
            return Geometry.TrajectoryHausdorffDistance(this, d, direction);
        }

        public int Dimensions
        {
            get { return A.Dimensions; }
        }

        public double[] Array
        {
            get { return new[] {A.X, A.Y, B.X, B.Y}; }
        }

        public double this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return A.X;
                    case 1:
                        return A.Y;
                    case 2:
                        return B.X;
                    case 3:
                        return B.Y;
                }
                throw new ArgumentException();
            }
        }

        public IBounding<Vector2D> Bounding()
        {
            var b = new BoundingRect(A);
            b.Expand(B);
            return b;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && IsEqual((Segment2D) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = A.GetHashCode();
                hashCode = (hashCode*397) ^ B.GetHashCode();
                return hashCode;
            }
        }
    }
}