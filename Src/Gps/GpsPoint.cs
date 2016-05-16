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

namespace Math.Gps
{
    public class GpsPoint
    {
        public GpsPoint()
        {
        }

        public GpsPoint(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public GpsPoint(double latitude, double longitude, double elevation)
        {
            Latitude = latitude;
            Longitude = longitude;
            Elevation = elevation;
        }

        public GpsPoint(GpsPoint g)
        {
            Latitude = g.Latitude;
            Longitude = g.Longitude;
            Elevation = g.Elevation;
        }

        public double Latitude { get; set; } // theta
        public double Longitude { get; set; } // phi
        public double Elevation { get; set; } // radius

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
                hashCode = (hashCode*397) ^ Latitude.GetHashCode();
                hashCode = (hashCode*397) ^ Longitude.GetHashCode();
                return hashCode;
            }
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

        public GpsPoint Interpolate(GpsPoint g, double x)
        {
            Vector3D x0 = this;
            Vector3D x1 = g;
            var angle = x0.Angle(x1);
            var q = new GpsPoint(g);
            if (!Comparison.IsZero(angle))
            {
                var axis = x0 ^ x1;
                q = x0.Rotate(axis, angle*x);
            }
            q.Elevation = Elevation*(1.0 - x) + g.Elevation*x;
            return q;
        }

        public double HaversineDistance(GpsPoint g)
        {
            return Geodesy.Distance.Haversine(this, g);
        }

        public static implicit operator Polar3D(GpsPoint g)
        {
            return new Polar3D
            {
                Theta = Conversion.DegToRad(90.0 - g.Latitude),
                Phi = Conversion.DegToRad(g.Longitude),
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
                Latitude = 90.0 - Conversion.RadToDeg(p.Theta),
                Longitude = Conversion.RadToDeg(p.Phi),
                Elevation = p.R - Geodesy.EarthRadius
            };
        }

        public static implicit operator GpsPoint(Vector3D v)
        {
            return (Polar3D) v;
        }

        public void GridIndex(int resolution, out int i, out int j)
        {
            i = System.Math.Min((int) ((90.0 - Latitude)/180.0*resolution), resolution - 1);
            j = (int) (i < 1 || i >= resolution - 1 ? 0 : (Longitude + 180.0)/180.0*resolution);
        }

        public int GridLinearIndex(int resolution)
        {
            int i, j;
            GridIndex(resolution, out i, out j);
            return j*resolution + i;
        }
    }
}