/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2019 Thierry Matthey
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
using ICloneable = Math.Interfaces.ICloneable;

namespace Math
{
    public class Polar3D : IGeometryObject<Polar3D>, IBoundingFacade<Vector3D>, ICloneable, IIsEqual<Polar3D>
    {
        public static readonly Polar3D Zero = new Polar3D(0, 0, 0);
        private double _theta;

        public Polar3D()
        {
        }

        public Polar3D(double theta, double phi)
        {
            _theta = Function.NormalizeAnglePi(theta);
            Phi = phi;
            R = 1.0;
        }

        public Polar3D(double theta, double phi, double r)
        {
            _theta = Function.NormalizeAnglePi(theta);
            Phi = phi;
            R = r;
        }

        public Polar3D(Polar3D p)
        {
            _theta = p.Theta;
            Phi = p.Phi;
            R = p.R;
        }

        public double R { get; set; }

        public double Theta
        {
            get => _theta;
            set => _theta = Function.NormalizeAnglePi(value);
        } // Radian, Deg (-180,180] : azimuthal angle, longitude 

        public double Phi { get; set; } // Radian, Deg [0,180]   : polar angle, inclination, latitude 

        public IBounding<Vector3D> Bounding()
        {
            return new BoundingBox(this);
        }

        public object Clone()
        {
            return new Polar3D(this);
        }

        public int Dimensions => 3;

        public double[] Array => new[] {Theta, Phi, R};

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

                throw new ArgumentException();
            }
        }

        public double EuclideanNorm(Polar3D d)
        {
            return ((Vector3D) this).EuclideanNorm(d);
        }

        public double ModifiedNorm(Polar3D d, bool direction = true)
        {
            return EuclideanNorm(d);
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
                hashCode = (hashCode * 397) ^ Theta.GetHashCode();
                hashCode = (hashCode * 397) ^ Phi.GetHashCode();
                return hashCode;
            }
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
                p.R * sinPhi * cosTheta,
                p.R * sinPhi * sinTheta,
                p.R * cosPhi);
        }

        public static implicit operator Polar3D(Vector3D v)
        {
            var x2 = v.X * v.X;
            var y2 = v.Y * v.Y;
            var z2 = v.Z * v.Z;
            var r = System.Math.Sqrt(x2 + y2 + z2);
            var theta = 0.0;
            var phi = 0.0;
            if (Comparison.IsPositive(r))
            {
                var s = System.Math.Sqrt(x2 + y2);
                if (Comparison.IsPositive(s))
                {
                    phi = System.Math.Acos(System.Math.Min(System.Math.Max(v.Z / r, -1.0), 1.0));
                    theta = System.Math.Asin(System.Math.Min(System.Math.Max(v.Y / s, -1.0), 1.0));
                    if (v.X < 0.0)
                    {
                        theta = System.Math.PI - theta;
                    }

                    theta = Function.NormalizeAnglePi(theta);
                }
                else
                {
                    phi = v.Z >= 0.0 ? 0.0 : System.Math.PI;
                    theta = 0.0;
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