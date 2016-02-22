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

namespace Math
{
    public class Vector2D
    {
        public static readonly Vector2D Zero = new Vector2D(0, 0);
        public static readonly Vector2D E1 = new Vector2D(1, 0);
        public static readonly Vector2D E2 = new Vector2D(0, 1);

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

        public double X { get; set; }
        public double Y { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && IsEqual((Vector2D)obj);
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

        public bool IsEqual(Vector2D v)
        {
            return IsEqual(v, Comparison.Epsilon);
        }

        public bool IsEqual(Vector2D v, double epsilon)
        {
            return (Comparison.IsEqual(X, v.X, epsilon) && Comparison.IsEqual(Y, v.Y, epsilon));
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

        public double Distance(Vector2D v)
        {
            return Norm(v.X - X, v.Y - Y);
        }

        public double Dot(Vector2D v)
        {
            return X * v.X + Y * v.Y;
        }

        public static Vector2D operator +(Vector2D v1, Vector2D v2)
        {
            var res = new Vector2D(v1);
            res.Add(v2);
            return res;
        }

        public static Vector2D operator -(Vector2D v1, Vector2D v2)
        {
            var res = new Vector2D(v1);
            res.Sub(v2);
            return res;
        }

        public static Vector2D operator -(Vector2D v)
        {
            var res = new Vector2D();
            res.Sub(v);
            return res;
        }

        public static double operator *(Vector2D v1, Vector2D v2)
        {
            return v1.Dot(v2);
        }

        public static bool operator ==(Vector2D v1, Vector2D v2)
        {
            return v1.IsEqual(v2);
        }

        public static bool operator !=(Vector2D v1, Vector2D v2)
        {
            return !v1.IsEqual(v2);
        }

        public static Vector2D operator *(Vector2D v, double c)
        {
            var res = new Vector2D(v);
            res.Mul(c);
            return res;
        }

        public static Vector2D operator *(double c, Vector2D v)
        {
            var res = new Vector2D(v);
            res.Mul(c);
            return res;
        }

        public static Vector2D operator /(Vector2D v, double c)
        {
            var res = new Vector2D(v);
            res.Div(c);
            return res;
        }

        private void Add(Vector2D v)
        {
            X += v.X;
            Y += v.Y;
        }

        private void Sub(Vector2D v)
        {
            X -= v.X;
            Y -= v.Y;
        }

        private void Mul(double c)
        {
            X *= c;
            Y *= c;
        }

        private void Div(double c)
        {
            X /= c;
            Y /= c;
        }

        static private double Norm2(double x, double y)
        {
            return x * x + y * y;
        }

        static private double Norm(double x, double y)
        {
            return System.Math.Sqrt(Norm2(x, y));
        }
    }
}