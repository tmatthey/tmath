﻿/*
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

namespace Math
{
    public class Vector3D
    {
        public static readonly Vector3D Zero = new Vector3D(0, 0, 0);
        public static readonly Vector3D E1 = new Vector3D(1, 0, 0);
        public static readonly Vector3D E2 = new Vector3D(0, 1, 0);
        public static readonly Vector3D E3 = new Vector3D(0, 0, 1);

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

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && IsEqual((Vector3D)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                return hashCode;
            }
        }

        public bool IsEqual(Vector3D v)
        {
            return IsEqual(v, Comparison.Epsilon);
        }

        public bool IsEqual(Vector3D v, double epsilon)
        {
            return (Comparison.IsEqual(X, v.X, epsilon) && Comparison.IsEqual(Y, v.Y, epsilon) & Comparison.IsEqual(Z, v.Z, epsilon));
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
                Div(d);
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

        public double Distance(Vector3D v)
        {
            return Norm(v.X - X, v.Y - Y, v.Z - Z);
        }

        public double Dot(Vector3D v)
        {
            return X * v.X + Y * v.Y + Z * v.Z;
        }

        public Vector3D Cross(Vector3D v)
        {
            return new Vector3D(Y * v.Z - Z * v.Y,
                Z * v.X - X * v.Z,
                X * v.Y - Y * v.X);
        }

        public double CrossNorm2(Vector3D v)
        {
            return Norm2(Y * v.Z - Z * v.Y,
                Z * v.X - X * v.Z,
                X * v.Y - Y * v.X);
        }

        public double CrossNorm(Vector3D v)
        {
            return System.Math.Sqrt(CrossNorm2(v));
        }

        public Vector3D Rotate(Vector3D v, double alpha)
        {
            var c = System.Math.Cos(alpha);
            var cm = 1.0 - c;
            var s = System.Math.Sin(alpha);
            var r = v.Normalized();
            return new Vector3D((c + r.X * r.X * cm) * X +
                                (r.X * r.Y * cm - r.Z * s) * Y +
                                (r.X * r.Z * cm + r.Y * s) * Z,

                                (r.X * r.Y * cm + r.Z * s) * X +
                                (c + r.Y * r.Y * cm) * Y +
                                (r.Y * r.Z * cm - r.X * s) * Z,

                                (r.X * r.Z * cm - r.Y * s) * X +
                                (r.Y * r.Z * cm + r.X * s) * Y +
                                (c + r.Z * r.Z * cm) * Z);
        }

        public double Angle(Vector3D v)
        {
            var cross = CrossNorm(v);
            var dot = Dot(v);
            return System.Math.Atan2(cross, dot);
        }

        public static Vector3D operator +(Vector3D v1, Vector3D v2)
        {
            var res = new Vector3D(v1);
            res.Add(v2);
            return res;
        }

        public static Vector3D operator -(Vector3D v1, Vector3D v2)
        {
            var res = new Vector3D(v1);
            res.Sub(v2);
            return res;
        }

        public static Vector3D operator -(Vector3D v)
        {
            var res = new Vector3D();
            res.Sub(v);
            return res;
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
            return v1.IsEqual(v2);
        }

        public static bool operator !=(Vector3D v1, Vector3D v2)
        {
            return !v1.IsEqual(v2);
        }

        public static Vector3D operator *(Vector3D v, double c)
        {
            var res = new Vector3D(v);
            res.Mul(c);
            return res;
        }

        public static Vector3D operator *(double c, Vector3D v)
        {
            var res = new Vector3D(v);
            res.Mul(c);
            return res;
        }

        public static Vector3D operator /(Vector3D v, double c)
        {
            var res = new Vector3D(v);
            res.Div(c);
            return res;
        }

        private void Add(Vector3D v)
        {
            X += v.X;
            Y += v.Y;
            Z += v.Z;
        }

        private void Sub(Vector3D v)
        {
            X -= v.X;
            Y -= v.Y;
            Z -= v.Z;
        }

        private void Mul(double c)
        {
            X *= c;
            Y *= c;
            Z *= c;
        }

        private void Div(double c)
        {
            X /= c;
            Y /= c;
            Z /= c;
        }

        static private double Norm2(double x, double y, double z)
        {
            return x * x + y * y + z * z;
        }

        static private double Norm(double x, double y, double z)
        {
            return System.Math.Sqrt(Norm2(x, y, z));
        }
    }
}