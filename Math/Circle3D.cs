/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2024 Thierry Matthey
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
    public class Circle3D : IDimension, ICloneable<Circle3D>, IIsEqual<Circle3D>
    {
        public Circle3D()
        {
            Center = new Vector3D();
            Normal = new Vector3D();
        }

        public Circle3D(double radius)
        {
            Radius = radius;
            Center = new Vector3D();
            Normal = new Vector3D(Vector3D.E3);
        }

        public Circle3D(Vector3D center, double radius)
        {
            Radius = radius;
            Center = new Vector3D(center);
            Normal = new Vector3D(center);
        }

        public Circle3D(Vector3D center, Vector3D normal, double radius)
        {
            Radius = radius;
            Center = new Vector3D(center);
            Normal = new Vector3D(normal);
        }

        public Circle3D(Circle3D c)
        {
            Radius = c.Radius;
            Center = new Vector3D(c.Center);
            Normal = new Vector3D(c.Normal);
        }

        public Vector3D Center { get; set; }
        public Vector3D Normal { get; set; }
        public double Radius { get; set; }

        public Circle3D Clone()
        {
            return new Circle3D(this);
        }

        public int Dimensions => Center.Dimensions;

        public bool IsEqual(Circle3D c)
        {
            return IsEqual(c, Comparison.Epsilon);
        }

        public bool IsEqual(Circle3D c, double epsilon)
        {
            if (!Center.IsEqual(c.Center, epsilon) || !Comparison.IsEqual(Radius, c.Radius, epsilon))
                return false;
            return Comparison.IsZero(c.Normal.CrossNorm(Normal), epsilon);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && IsEqual((Circle3D) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Center.GetHashCode();
                hashCode = (hashCode * 397) ^ Radius.GetHashCode();
                hashCode = (hashCode * 397) ^ Normal.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Circle3D c1, Circle3D c2)
        {
            if ((object) c1 == null && (object) c2 == null)
                return true;
            if ((object) c1 == null || (object) c2 == null)
                return false;
            return c1.IsEqual(c2);
        }

        public static bool operator !=(Circle3D c1, Circle3D c2)
        {
            if ((object) c1 == null && (object) c2 == null)
                return false;
            if ((object) c1 == null || (object) c2 == null)
                return true;
            return !c1.IsEqual(c2);
        }

        public static Circle3D Create(Vector3D a)
        {
            return new Circle3D(a, 0.0);
        }

        public static Circle3D Create(Vector3D a, Vector3D b)
        {
            return new Circle3D((a + b) * 0.5, a.EuclideanNorm(b) * 0.5);
        }

        public static Circle3D Create(Vector3D a, Vector3D b, Vector3D c)
        {
            var d0 = a.EuclideanNorm(b);
            var d1 = b.EuclideanNorm(c);
            var d2 = c.EuclideanNorm(a);
            if (Comparison.IsEqual((b - a) * (c - a), 1) || Comparison.IsZero(d0) || Comparison.IsZero(d1) ||
                Comparison.IsZero(d2))
            {
                return d0 >= d1 && d0 >= d2 ? Create(a, b) : d1 >= d0 && d1 >= d2 ? Create(b, c) : Create(c, a);
            }

            var t = b - a;
            var u = c - a;
            var v = c - b;

            var w = t ^ u;
            var nl2 = w.Norm2();

            var inl2 = 1.0 / (2.0 * nl2);
            var tt = t * t;
            var uu = u * u;

            var center = a + (u * tt * (u * v) - t * uu * (t * v)) * inl2;
            var radius = System.Math.Sqrt(tt * uu * (v * v) * inl2 * 0.5);
            return new Circle3D(center, w.Normalized(), radius);
        }
    }
}