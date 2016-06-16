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

using Math.Interfaces;

namespace Math
{
    public class Circle2D : INorm<Circle2D>, IDimension
    {
        public Circle2D()
        {
            Center = new Vector2D();
        }

        public Circle2D(double radius)
        {
            Radius = radius;
            Center = new Vector2D();
        }

        public Circle2D(Vector2D center, double radius)
        {
            Radius = radius;
            Center = new Vector2D(center);
        }

        public Vector2D Center { get; set; }
        public double Radius { get; set; }

        public int Dimensions
        {
            get { return Center.Dimensions; }
        }

        public double EuclideanNorm(Circle2D c)
        {
            var d = Center.EuclideanNorm(c.Center) - Radius - c.Radius;
            return System.Math.Max(d, 0.0);
        }

        public double ModifiedNorm(Circle2D c, bool direction = true)
        {
            return EuclideanNorm(c);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && IsEqual((Circle2D) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Center.GetHashCode();
                hashCode = (hashCode*397) ^ Radius.GetHashCode();
                return hashCode;
            }
        }

        public bool IsEqual(Circle2D c)
        {
            return IsEqual(c, Comparison.Epsilon);
        }

        public bool IsEqual(Circle2D c, double epsilon)
        {
            return Center.IsEqual(c.Center, epsilon) && Comparison.IsEqual(Radius, c.Radius, epsilon);
        }

        public static bool operator ==(Circle2D c1, Circle2D c2)
        {
            if ((object) c1 == null && (object) c2 == null)
                return true;
            if ((object) c1 == null || (object) c2 == null)
                return false;
            return c1.IsEqual(c2);
        }

        public static bool operator !=(Circle2D c1, Circle2D c2)
        {
            if ((object) c1 == null && (object) c2 == null)
                return false;
            if ((object) c1 == null || (object) c2 == null)
                return true;
            return !c1.IsEqual(c2);
        }

        public bool IsInside(Vector2D p, double epsilon)
        {
            return Center.EuclideanNorm(p) <= Radius + epsilon;
        }

        public bool IsInside(Vector2D p)
        {
            return IsInside(p, Comparison.Epsilon);
        }

        public static Circle2D Create(Vector2D a)
        {
            return new Circle2D(a, 0.0);
        }

        public static Circle2D Create(Vector2D a, Vector2D b)
        {
            return new Circle2D((a + b)*0.5, a.EuclideanNorm(b)*0.5);
        }

        public static Circle2D Create(Vector2D a, Vector2D b, Vector2D c)
        {
            var d0 = a.EuclideanNorm(b);
            var d1 = b.EuclideanNorm(c);
            var d2 = c.EuclideanNorm(a);
            if (Comparison.IsEqual((b - a)*(c - a), 1) || Comparison.IsZero(d0) || Comparison.IsZero(d1) ||
                Comparison.IsZero(d2))
            {
                return d0 >= d1 && d0 >= d2 ? Create(a, b) : d1 >= d0 && d1 >= d2 ? Create(b, c) : Create(c, a);
            }

            var offset = b.Norm2();
            var bc = (a.Norm2() - offset)/2.0;
            var cd = (offset - c.Norm2())/2.0;
            var det = (a.X - b.X)*(b.Y - c.Y) - (b.X - c.X)*(a.Y - b.Y);
            var center = new Vector2D((bc*(b.Y - c.Y) - cd*(a.Y - b.Y))/det,
                (cd*(a.X - b.X) - bc*(b.X - c.X))/det);
            var radius = b.EuclideanNorm(center);

            return new Circle2D(center, radius);
        }
    }
}