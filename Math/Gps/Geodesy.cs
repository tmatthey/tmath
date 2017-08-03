/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2017 Thierry Matthey
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

using System.Collections.Generic;
using System.Linq;

namespace Math.Gps
{
    public static class Geodesy
    {
        public static readonly double EarthRadius = 6367000.0;
        public static readonly double DistanceOneDeg = EarthRadius*System.Math.PI/180.0;

        public static class Distance
        {
            // Latitude and longitude are in degrees, output in meter
            public static double Haversine(double lat1, double long1, double lat2, double long2)
            {
                lat1 = Conversion.DegToRad(lat1);
                long1 = Conversion.DegToRad(long1);
                lat2 = Conversion.DegToRad(lat2);
                long2 = Conversion.DegToRad(long2);

                var dlong = long2 - long1;
                var dlat = lat2 - lat1;
                var a = System.Math.Pow(System.Math.Sin(dlat/2.0), 2) +
                        System.Math.Cos(lat1)*System.Math.Cos(lat2)*System.Math.Pow(System.Math.Sin(dlong/2.0), 2);
                var c = 2*System.Math.Atan2(System.Math.Sqrt(a), System.Math.Sqrt(1 - a));
                var d = EarthRadius*c;

                return d;
            }

            public static double Haversine(GpsPoint g, GpsPoint q)
            {
                return Haversine(g.Latitude, g.Longitude, q.Latitude, q.Longitude);
            }

            public static double HaversineTotal(IList<GpsPoint> track)
            {
                var d = 0.0;
                for (var i = 0; i + 1 < track.Count; i++)
                {
                    d += Haversine(track[i], track[i + 1]);
                }
                return d;
            }

            public static List<double> Haversine(IList<GpsPoint> track)
            {
                var d = new List<double>();
                for (var i = 0; i + 1 < track.Count; i++)
                {
                    d.Add(Haversine(track[i], track[i + 1]));
                }
                return d;
            }

            public static List<double> HaversineAccumulated(IList<GpsPoint> track)
            {
                var d = new List<double>();
                if (track.Any())
                    d.Add(0.0);
                for (var i = 0; i + 1 < track.Count; i++)
                {
                    d.Add(d.Last() + Haversine(track[i], track[i + 1]));
                }
                return d;
            }
        }
    }
}