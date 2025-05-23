﻿/*
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

using System;
using Math.Gps;
using Math.Interfaces;

namespace Math
{
    public class Vector2D : IVector<Vector2D>, ICloneable<Vector2D>, IIsEqual<Vector2D>, IInterpolate<Vector2D>
    {
        public static readonly Vector2D Zero = new Vector2D(0, 0);
        public static readonly Vector2D One = new Vector2D(1, 1);
        public static readonly Vector2D E1 = new Vector2D(1, 0);
        public static readonly Vector2D E2 = new Vector2D(0, 1);
        public static readonly Vector2D NaN = new Vector2D(double.NaN, double.NaN);

        public static readonly Vector2D PositiveInfinity =
            new Vector2D(double.PositiveInfinity, double.PositiveInfinity);

        public static readonly Vector2D NegativeInfinity =
            new Vector2D(double.NegativeInfinity, double.NegativeInfinity);

        public Vector2D()
        {
        }

        public Vector2D(double c)
        {
            X = c;
            Y = c;
        }

        public Vector2D(Vector2D v)
        {
            X = v.X;
            Y = v.Y;
        }

        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double Y { get; set; }

        public Vector2D Clone()
        {
            return new Vector2D(this);
        }

        public double X { get; set; }

        public bool IsEqual(Vector2D v)
        {
            return IsEqual(v, Comparison.Epsilon);
        }

        public bool IsEqual(Vector2D v, double epsilon)
        {
            return Comparison.IsEqual(X, v.X, epsilon) && Comparison.IsEqual(Y, v.Y, epsilon);
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
            }

            return d;
        }

        public Vector2D Normalized()
        {
            return Normalized(Comparison.Epsilon);
        }

        public Vector2D Normalized(double epsilon)
        {
            var res = new Vector2D(this);
            res.Normalize(epsilon);
            return res;
        }

        public double Norm2()
        {
            return Norm2(X, Y);
        }

        public double Norm()
        {
            return Norm(X, Y);
        }

        public double EuclideanNorm(Vector2D v)
        {
            return Norm(v.X - X, v.Y - Y);
        }

        public double ModifiedNorm(Vector2D v, bool direction = true)
        {
            return EuclideanNorm(v);
        }

        public double Dot(Vector2D v)
        {
            return X * v.X + Y * v.Y;
        }

        public double CrossNorm(Vector2D v)
        {
            return System.Math.Abs(Cross(this, v));
        }

        public double Angle(Vector2D v)
        {
            return Function.NormalizeAnglePi(System.Math.Atan2(v.Y, v.X) - System.Math.Atan2(Y, X));
        }

        public double AngleAbs(Vector2D v)
        {
            return System.Math.Abs(Angle(v));
        }

        public Vector2D Add(Vector2D v)
        {
            return new Vector2D(this + v);
        }

        public Vector2D Sub(Vector2D v)
        {
            return new Vector2D(this - v);
        }

        public Vector2D Mul(double c)
        {
            return new Vector2D(this * c);
        }

        public Vector2D Div(double c)
        {
            return new Vector2D(this / c);
        }

        public Vector2D Interpolate(Vector2D v, double x)
        {
            return this * (1.0 - x) + v * x;
        }

        public int Dimensions => 2;

        public double[] Array => new[] {X, Y};

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
                }

                throw new ArgumentException();
            }
        }

        public IBounding<Vector2D> Bounding()
        {
            return new BoundingRect(this);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && IsEqual((Vector2D) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                return hashCode;
            }
        }

        public static Vector2D operator +(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector2D operator -(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector2D operator -(Vector2D v)
        {
            return new Vector2D(-v.X, -v.Y);
        }

        public static double operator *(Vector2D v1, Vector2D v2)
        {
            return v1.Dot(v2);
        }

        public static bool operator ==(Vector2D v1, Vector2D v2)
        {
            if ((object) v1 == null && (object) v2 == null)
                return true;
            if ((object) v1 == null || (object) v2 == null)
                return false;
            return v1.IsEqual(v2);
        }

        public static bool operator !=(Vector2D v1, Vector2D v2)
        {
            if ((object) v1 == null && (object) v2 == null)
                return false;
            if ((object) v1 == null || (object) v2 == null)
                return true;
            return !v1.IsEqual(v2);
        }

        public static Vector2D operator *(Vector2D v, double c)
        {
            return new Vector2D(v.X * c, v.Y * c);
        }

        public static Vector2D operator *(double c, Vector2D v)
        {
            return new Vector2D(v.X * c, v.Y * c);
        }

        public static Vector2D operator /(Vector2D v, double c)
        {
            return new Vector2D(v.X / c, v.Y / c);
        }

        public static double Cross(Vector2D a, Vector2D b)
        {
            return a.X * b.Y - a.Y * b.X;
        }

        public Vector2D Rotate(double angle)
        {
            var cosA = System.Math.Cos(angle);
            var sinA = System.Math.Sin(angle);

            return new Vector2D(cosA * X - sinA * Y, sinA * X + cosA * Y);
        }

        public GpsPoint ToGpsPoint(Polar3D center)
        {
            var a3 = -center.Theta;
            var a2 = System.Math.PI * 0.5 - center.Phi;
            Vector3D w0 = new GpsPoint
            {
                Longitude = X / Geodesy.DistanceOneDeg,
                Latitude = Y / Geodesy.DistanceOneDeg
            };
            var w1 = w0.RotateE2(-a2);
            GpsPoint res = w1.RotateE3(-a3);
            res.Elevation = 0.0;
            return res;
        }

        private static double Norm2(double x, double y)
        {
            return x * x + y * y;
        }

        private static double Norm(double x, double y)
        {
            var a = System.Math.Max(System.Math.Abs(x), System.Math.Abs(y));
            if (Comparison.IsPositive(a))
            {
                x /= a;
                y /= a;
            }

            return System.Math.Sqrt(Norm2(x, y)) * a;
        }
    }
}