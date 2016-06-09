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
    public class Polar3D : IGeometryObject<Polar3D>, IBoundingFacade<Vector3D>
    {
        public static readonly Polar3D Zero = new Polar3D(0, 0, 0);

        public Polar3D()
        {
        }

        public Polar3D(double theta, double phi)
        {
            Theta = theta;
            Phi = phi;
            R = 1.0;
        }

        public Polar3D(double theta, double phi, double r)
        {
            Theta = theta;
            Phi = phi;
            R = r;
        }

        public double R { get; set; }
        public double Theta { get; set; }
        public double Phi { get; set; }

        public IBounding<Vector3D> Bounding()
        {
            return new BoundingBox(this);
        }

        public int Dimensions
        {
            get { return 3; }
        }

        public double[] Array
        {
            get { return new[] {Theta, Phi, R}; }
        }

        public double this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return Theta;
                    case 1:
                        return Phi;
                    case 2:
                        return R;
                }
                throw new IndexOutOfRangeException();
            }
        }

        public double EuclideanNorm(Polar3D d)
        {
            return ((Vector3D) this).EuclideanNorm(d);
        }

        public double ModifiedNorm(Polar3D d)
        {
            return EuclideanNorm(d);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && IsEqual((Polar3D) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = R.GetHashCode();
                hashCode = (hashCode*397) ^ Theta.GetHashCode();
                hashCode = (hashCode*397) ^ Phi.GetHashCode();
                return hashCode;
            }
        }

        public bool IsEqual(Polar3D p)
        {
            return IsEqual(p, Comparison.Epsilon);
        }

        public bool IsEqual(Polar3D p, double epsilon)
        {
            if (Comparison.IsZero(R, epsilon) && Comparison.IsZero(p.R, epsilon))
                return true;

            if (!Comparison.IsEqual(R, p.R, epsilon))
                return false;

            return Function.NormalizeAngle(Theta - p.Theta) < epsilon &&
                   Function.NormalizeAngle(Phi - p.Phi) < epsilon;
        }

        public static bool operator ==(Polar3D p1, Polar3D p2)
        {
            if ((object) p1 == null && (object) p2 == null)
                return true;
            if ((object) p1 == null || (object) p2 == null)
                return false;
            return p1.IsEqual(p2);
        }

        public static bool operator !=(Polar3D p1, Polar3D p2)
        {
            if ((object) p1 == null && (object) p2 == null)
                return false;
            if ((object) p1 == null || (object) p2 == null)
                return true;
            return !p1.IsEqual(p2);
        }

        public static implicit operator Vector3D(Polar3D p)
        {
            double sinTheta, cosTheta, sinPhi, cosPhi;
            Function.SinCos(p.Theta, out sinTheta, out cosTheta);
            Function.SinCos(p.Phi, out sinPhi, out cosPhi);

            return new Vector3D(
                p.R*sinTheta*cosPhi,
                p.R*sinTheta*sinPhi,
                p.R*cosTheta);
        }

        public static implicit operator Polar3D(Vector3D v)
        {
            var x2 = v.X*v.X;
            var y2 = v.Y*v.Y;
            var z2 = v.Z*v.Z;
            var r = System.Math.Sqrt(x2 + y2 + z2);
            var theta = 0.0;
            var phi = 0.0;
            if (Comparison.IsPositive(r))
            {
                var s = System.Math.Sqrt(x2 + y2);
                if (Comparison.IsPositive(s))
                {
                    theta = System.Math.Acos(System.Math.Min(System.Math.Max(v.Z/r, -1.0), 1.0));
                    phi = System.Math.Asin(System.Math.Min(System.Math.Max(v.Y/s, -1.0), 1.0));
                    if (v.X < 0.0)
                    {
                        phi = System.Math.PI - phi;
                    }
                    phi = Function.NormalizeAngle(phi);
                }
                else
                {
                    theta = (v.Z >= 0.0 ? 0.0 : System.Math.PI);
                    phi = 0.0;
                }
            }
            else
            {
                r = 0.0;
            }
            return new Polar3D {Theta = theta, Phi = phi, R = r};
        }
    }
}