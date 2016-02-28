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
        public double Latitude { get; set; } // theta
        public double Longitude { get; set; } // phi
        public double Elevation { get; set; } // radius

        public GpsPoint Interpolate(GpsPoint g, double x)
        {
            Vector3D a = this;
            Vector3D b = g;
            GpsPoint q = a*x + b*(1.0-x);
            q.Elevation = Elevation*x + g.Elevation*(1.0-x);
            return q;
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
    }
}