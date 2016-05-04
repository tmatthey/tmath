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
            return obj.GetType() == GetType() && IsEqual((Vector2D) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode*397) ^ Y.GetHashCode();
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
            return X*v.X + Y*v.Y;
        }

        public double Angle(Vector2D v)
        {
            var sin = X*v.Y - v.X*Y;
            var cos = X*v.X + Y*v.Y;
            return System.Math.Atan2(sin, cos);
        }

        public double AngleAbs(Vector2D v)
        {
            return System.Math.Abs(Angle(v));
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

        public static double PerpendicularDistance(Vector2D a, Vector2D b, Vector2D p)
        {
            var l = a.Distance(b);
            if (Comparison.IsZero(l))
            {
                return a.Distance(p);
            }
            return System.Math.Abs(Cross(b - a, p - a)/l);
        }

        public static double PerpendicularSegmentDistance(Vector2D x0, Vector2D x1, Vector2D p)
        {
            var dist = PerpendicularDistance(x0, x1, p);
            if (x0 == x1)
                return dist;

            if ((x1 - x0)*(p - x1) >= 0.0)
                return x1.Distance(p);
            if ((x0 - x1)*(p - x0) >= 0.0)
                return x0.Distance(p);

            return dist;
        }

        public static double PerpendicularParameter(Vector2D x0, Vector2D x1, Vector2D p)
        {
            var d = (x1 - x0).Norm2();
            if (Comparison.IsZero(d))
                return 0.0;
            var a = (p - x0);
            var b = (x1 - x0);
            return (a*b)/d;
        }

        public static double PerpendicularSegmentParameter(Vector2D x0, Vector2D x1, Vector2D p)
        {
            return System.Math.Max(System.Math.Min(PerpendicularParameter(x0, x1, p), 1.0), 0.0);
        }

        public static double Cross(Vector2D a, Vector2D b)
        {
            return a.X*b.Y - a.Y*b.X;
        }

        // Trajectory clustering: a partition-and-group framework
        // Jae-Gil Lee, Jiawei Han, Kyu-Young Whang
        // SIGMOD '07 Proceedings of the 2007 ACM SIGMOD international conference on Management of data 
        public static double TrajectoryHausdorffDistance(Vector2D a, Vector2D b, Vector2D c, Vector2D d,
            double wPerpendicular,
            double wParallel, double wAngular)
        {
            var l1 = a.Distance(b);
            var l2 = c.Distance(d);
            if (l1 > l2)
            {
                Utils.Swap(ref a, ref c);
                Utils.Swap(ref b, ref d);
                Utils.Swap(ref l1, ref l2);
            }
            var dPerpA = PerpendicularDistance(c, d, a);
            var dPerpB = PerpendicularDistance(c, d, b);
            var dPerpendicular = 0.0;
            if (Comparison.IsPositive(dPerpA + dPerpB))
            {
                dPerpendicular = (dPerpA*dPerpA + dPerpB*dPerpB)/(dPerpA + dPerpB);
            }
            var pa = PerpendicularParameter(c, d, a);
            var pb = PerpendicularParameter(c, d, b);
            if (pa > pb)
            {
                Utils.Swap(ref pa, ref pb);
            }

            var dParallel = System.Math.Min(System.Math.Abs(pa), System.Math.Abs(pb - 1.0))*l2;
            var angle = System.Math.Min((b - a).AngleAbs(d - c), System.Math.PI/2.0);
            var dAngular = l1*System.Math.Sin(angle);

            return wPerpendicular*dPerpendicular + wParallel*dParallel + wAngular*dAngular;
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

        private static double Norm2(double x, double y)
        {
            return x*x + y*y;
        }

        private static double Norm(double x, double y)
        {
            var a = System.Math.Max(System.Math.Abs(x), System.Math.Abs(y));
            if (Comparison.IsPositive(a))
            {
                x /= a;
                y /= a;
            }
            return System.Math.Sqrt(Norm2(x, y))*a;
        }
    }
}