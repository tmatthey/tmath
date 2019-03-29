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

namespace Math.Gps
{
    public class GpsPoint : IGeometryObject<GpsPoint>, IBoundingFacade<Vector3D>, ICloneable, IIsEqual<GpsPoint>,
        IInterpolate<GpsPoint>
    {
        private double _longitude;

        public GpsPoint()
        {
        }

        public GpsPoint(double latitude, double longitude)
        {
            Latitude = latitude;
            _longitude = Function.NormalizeAngle180(longitude);
        }

        public GpsPoint(double latitude, double longitude, double elevation)
        {
            Latitude = latitude;
            _longitude = Function.NormalizeAngle180(longitude);
            Elevation = elevation;
        }

        public GpsPoint(GpsPoint g)
        {
            Latitude = g.Latitude;
            _longitude = g.Longitude;
            Elevation = g.Elevation;
        }

        public double Latitude { get; set; } // phi : [-90,90]

        public double Longitude
        {
            get => _longitude;
            set => _longitude = Function.NormalizeAngle180(value);
        } // theta :  (-180,180] 

        public double Elevation { get; set; } // radius

        public IBounding<Vector3D> Bounding()
        {
            return new BoundingBox(this);
        }

        public object Clone()
        {
            return new GpsPoint(this);
        }

        public int Dimensions => 3;

        public double[] Array => new[] {Latitude, Longitude, Elevation};

        public double this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return Latitude;
                    case 1:
                        return Longitude;
                    case 2:
                        return Elevation;
                }

                throw new ArgumentException();
            }
        }

        public double EuclideanNorm(GpsPoint d)
        {
            return ((Vector3D) this).EuclideanNorm(d);
        }

        public double ModifiedNorm(GpsPoint d, bool direction = true)
        {
            var f = (System.Math.Min(Elevation, d.Elevation) + Geodesy.EarthRadius) / Geodesy.EarthRadius;
            return new Vector2D(HaversineDistance(d), Elevation - d.Elevation).Norm() * f;
        }

        public GpsPoint Interpolate(GpsPoint g, double x)
        {
            Vector3D x0 = this;
            Vector3D x1 = g;
            var angle = x0.Angle(x1);
            var q = new GpsPoint(g);
            if (!Comparison.IsZero(angle))
            {
                var axis = x0 ^ x1;
                q = x0.Rotate(axis, angle * x);
            }

            q.Elevation = Elevation * (1.0 - x) + g.Elevation * x;
            return q;
        }

        public bool IsEqual(GpsPoint g)
        {
            return IsEqual(g, Comparison.Epsilon);
        }

        public bool IsEqual(GpsPoint g, double epsilon)
        {
            if (Comparison.IsZero(Geodesy.EarthRadius + Elevation, epsilon) &&
                Comparison.IsZero(Geodesy.EarthRadius + g.Elevation, epsilon))
                return true;

            if (!Comparison.IsEqual(Elevation, g.Elevation, epsilon))
                return false;

            return Function.NormalizeAngle(Conversion.DegToRad(Longitude - g.Longitude)) < epsilon &&
                   Function.NormalizeAngle(Conversion.DegToRad(Latitude - g.Latitude)) < epsilon;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && IsEqual((GpsPoint) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Elevation.GetHashCode();
                hashCode = (hashCode * 397) ^ Latitude.GetHashCode();
                hashCode = (hashCode * 397) ^ Longitude.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(GpsPoint g1, GpsPoint g2)
        {
            if ((object) g1 == null && (object) g2 == null)
                return true;
            if ((object) g1 == null || (object) g2 == null)
                return false;
            return g1.IsEqual(g2);
        }

        public static bool operator !=(GpsPoint g1, GpsPoint g2)
        {
            if ((object) g1 == null && (object) g2 == null)
                return false;
            if ((object) g1 == null || (object) g2 == null)
                return true;
            return !g1.IsEqual(g2);
        }

        public double HaversineDistance(GpsPoint g)
        {
            return Geodesy.Distance.Haversine(this, g);
        }

        public static implicit operator Polar3D(GpsPoint g)
        {
            return new Polar3D
            {
                Phi = Conversion.DegToRad(90.0 - g.Latitude),
                Theta = Conversion.DegToRad(g.Longitude),
                R = Geodesy.EarthRadius + g.Elevation
            };
        }

        public static implicit operator Vector3D(GpsPoint g)
        {
            return (Polar3D) g;
        }

        public static implicit operator GpsPoint(Polar3D p)
        {
            return new GpsPoint
            {
                Latitude = 90.0 - Conversion.RadToDeg(p.Phi),
                Longitude = Conversion.RadToDeg(p.Theta),
                Elevation = p.R - Geodesy.EarthRadius
            };
        }

        public static implicit operator GpsPoint(Vector3D v)
        {
            return (Polar3D) v;
        }

        public void GridIndex(int resolution, out int i, out int j)
        {
            i = System.Math.Min((int) ((90.0 - Latitude) / 180.0 * resolution), resolution - 1);
            j = (int) (i < 1 || i >= resolution - 1 ? 0 : (Longitude + 180.0) / 180.0 * resolution);
        }

        public int GridLinearIndex(int resolution)
        {
            int i, j;
            GridIndex(resolution, out i, out j);
            return j * resolution + i;
        }

        public Vector2D ToVector2D(Polar3D center)
        {
            var a3 = -center.Theta;
            var a2 = System.Math.PI * 0.5 - center.Phi;
            Vector3D w0 = this;
            var w1 = w0.RotateE3(a3);
            GpsPoint u = w1.RotateE2(a2);
            return new Vector2D(u.Longitude * Geodesy.DistanceOneDeg, u.Latitude * Geodesy.DistanceOneDeg);
        }
    }
}