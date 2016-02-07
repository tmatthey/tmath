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
    public class Vector3D
    {
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

        public void Add(Vector3D v)
        {
            X += v.X;
            Y += v.Y;
            Z += v.Z;
        }

        public void Set(Vector3D v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }

        public void Set(double c)
        {
            X = c;
            Y = c;
            Z = c;
        }

        public void Sub(Vector3D v)
        {
            X -= v.X;
            Y -= v.Y;
            Z -= v.Z;
        }

        public void Add(double c)
        {
            X += c;
            Y += c;
            Z += c;
        }

        public void Sub(double c)
        {
            X -= c;
            Y -= c;
            Z -= c;
        }

        public void Mul(double c)
        {
            X *= c;
            Y *= c;
            Z *= c;
        }

        public void Div(double c)
        {
            X /= c;
            Y /= c;
            Z /= c;
        }

        public double Normalize()
        {
            var d = Norm();
            if (Comparison.IsPositive(d))
            {
                Div(d);
            }
            return d;
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

        public Vector3D Normalized()
        {
            var res = new Vector3D(this);
            res.Normalize();
            return res;
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

        public static double operator *(Vector3D v1, Vector3D v2)
        {
            return v1.Dot(v2);
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