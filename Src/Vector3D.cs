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
    public class Vector3D : IVector<Vector3D>
    {
        public static readonly Vector3D Zero = new Vector3D(0, 0, 0);
        public static readonly Vector3D One = new Vector3D(1, 1, 1);
        public static readonly Vector3D E1 = new Vector3D(1, 0, 0);
        public static readonly Vector3D E2 = new Vector3D(0, 1, 0);
        public static readonly Vector3D E3 = new Vector3D(0, 0, 1);
        public static readonly Vector3D NaN = new Vector3D(double.NaN, double.NaN, double.NaN);

        public static readonly Vector3D PositiveInfinity = new Vector3D(double.PositiveInfinity, double.PositiveInfinity,
            double.PositiveInfinity);

        public static readonly Vector3D NegativeInfinity = new Vector3D(double.NegativeInfinity, double.NegativeInfinity,
            double.NegativeInfinity);

        public Vector3D()
        {
        }

        public Vector3D(double c)
        {
            X = c;
            Y = c;
            Z = c;
        }

        public Vector3D(Vector3D v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }

        public Vector3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double Y { get; set; }
        public double Z { get; set; }

        public double X { get; set; }

        public bool IsEqual(Vector3D v)
        {
            return IsEqual(v, Comparison.Epsilon);
        }

        public bool IsEqual(Vector3D v, double epsilon)
        {
            return Comparison.IsEqual(X, v.X, epsilon) && Comparison.IsEqual(Y, v.Y, epsilon) &&
                   Comparison.IsEqual(Z, v.Z, epsilon);
        }

        public double Normalize()
        {
            return Normalize(Comparison.Epsilon);
        }

        public double Normalize(double epsilon)
        {
            var d = Norm();
            if (Comparison.IsPositive(d, epsilon))
            {
                X /= d;
                Y /= d;
                Z /= d;
            }
            return d;
        }

        public Vector3D Normalized()
        {
            return Normalized(Comparison.Epsilon);
        }

        public Vector3D Normalized(double epsilon)
        {
            var res = new Vector3D(this);
            res.Normalize(epsilon);
            return res;
        }

        public double Norm2()
        {
            return Norm2(X, Y, Z);
        }

        public double Norm()
        {
            return Norm(X, Y, Z);
        }

        public double EuclideanNorm(Vector3D v)
        {
            return Norm(v.X - X, v.Y - Y, v.Z - Z);
        }

        public double ModifiedNorm(Vector3D v, bool direction = true)
        {
            return EuclideanNorm(v);
        }

        public double Dot(Vector3D v)
        {
            return X*v.X + Y*v.Y + Z*v.Z;
        }

        public double CrossNorm(Vector3D v)
        {
            return System.Math.Sqrt(CrossNorm2(v));
        }

        public double Angle(Vector3D v)
        {
            var cross = CrossNorm(v);
            var dot = Dot(v);
            return Function.NormalizeAnglePi(System.Math.Atan2(cross, dot));
        }

        public double AngleAbs(Vector3D v)
        {
            return System.Math.Abs(Angle(v));
        }

        public Vector3D Add(Vector3D v)
        {
            return new Vector3D(this + v);
        }

        public Vector3D Sub(Vector3D v)
        {
            return new Vector3D(this - v);
        }

        public Vector3D Mul(double c)
        {
            return new Vector3D(this*c);
        }

        public Vector3D Div(double c)
        {
            return new Vector3D(this/c);
        }

        public int Dimensions
        {
            get { return 3; }
        }

        public double[] Array
        {
            get { return new[] {X, Y, Z}; }
        }


        public double this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return X;
                    case 1:
                        return Y;
                    case 2:
                        return Z;
                }
                throw new IndexOutOfRangeException();
            }
        }

        public IBounding<Vector3D> Bounding()
        {
            return new BoundingBox(this);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && IsEqual((Vector3D) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode*397) ^ Y.GetHashCode();
                hashCode = (hashCode*397) ^ Z.GetHashCode();
                return hashCode;
            }
        }

        public Vector3D Cross(Vector3D v)
        {
            return new Vector3D(Y*v.Z - Z*v.Y,
                Z*v.X - X*v.Z,
                X*v.Y - Y*v.X);
        }

        public double CrossNorm2(Vector3D v)
        {
            return Norm2(Y*v.Z - Z*v.Y,
                Z*v.X - X*v.Z,
                X*v.Y - Y*v.X);
        }

        public Vector3D Rotate(Vector3D v, double alpha)
        {
            var c = System.Math.Cos(alpha);
            var cm = 1.0 - c;
            var s = System.Math.Sin(alpha);
            var r = v.Normalized();
            return new Vector3D((c + r.X*r.X*cm)*X +
                                (r.X*r.Y*cm - r.Z*s)*Y +
                                (r.X*r.Z*cm + r.Y*s)*Z,
                (r.X*r.Y*cm + r.Z*s)*X +
                (c + r.Y*r.Y*cm)*Y +
                (r.Y*r.Z*cm - r.X*s)*Z,
                (r.X*r.Z*cm - r.Y*s)*X +
                (r.Y*r.Z*cm + r.X*s)*Y +
                (c + r.Z*r.Z*cm)*Z);
        }

        public Vector3D RotateE1(double alpha)
        {
            var c = System.Math.Cos(alpha);
            var s = System.Math.Sin(alpha);
            return new Vector3D(X, c*Y - s*Z, s*Y + c*Z);
        }

        public Vector3D RotateE2(double alpha)
        {
            var c = System.Math.Cos(alpha);
            var s = System.Math.Sin(alpha);
            return new Vector3D(c*X + s*Z, Y, -s*X + c*Z);
        }

        public Vector3D RotateE3(double alpha)
        {
            var c = System.Math.Cos(alpha);
            var s = System.Math.Sin(alpha);
            return new Vector3D(c*X - s*Y, s*X + c*Y, Z);
        }


        public static Vector3D operator +(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vector3D operator -(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static Vector3D operator -(Vector3D v)
        {
            return new Vector3D(-v.X, -v.Y, -v.Z);
        }

        public static double operator *(Vector3D v1, Vector3D v2)
        {
            return v1.Dot(v2);
        }

        public static Vector3D operator ^(Vector3D v1, Vector3D v2)
        {
            return v1.Cross(v2);
        }

        public static bool operator ==(Vector3D v1, Vector3D v2)
        {
            if ((object) v1 == null && (object) v2 == null)
                return true;
            if ((object) v1 == null || (object) v2 == null)
                return false;
            return v1.IsEqual(v2);
        }

        public static bool operator !=(Vector3D v1, Vector3D v2)
        {
            if ((object) v1 == null && (object) v2 == null)
                return false;
            if ((object) v1 == null || (object) v2 == null)
                return true;
            return !v1.IsEqual(v2);
        }

        public static Vector3D operator *(Vector3D v, double c)
        {
            return new Vector3D(v.X*c, v.Y*c, v.Z*c);
        }

        public static Vector3D operator *(double c, Vector3D v)
        {
            return new Vector3D(v.X*c, v.Y*c, v.Z*c);
        }

        public static Vector3D operator /(Vector3D v, double c)
        {
            return new Vector3D(v.X/c, v.Y/c, v.Z/c);
        }

        private static double Norm2(double x, double y, double z)
        {
            return x*x + y*y + z*z;
        }

        private static double Norm(double x, double y, double z)
        {
            var a = System.Math.Max(System.Math.Max(System.Math.Abs(x), System.Math.Abs(y)), System.Math.Abs(z));
            if (Comparison.IsPositive(a))
            {
                x /= a;
                y /= a;
                z /= a;
            }
            return System.Math.Sqrt(Norm2(x, y, z))*a;
        }
    }
}