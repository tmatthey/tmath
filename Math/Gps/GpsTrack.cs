/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2026 Thierry Matthey
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
using System.Collections.Generic;
using System.Linq;

namespace Math.Gps
{
    public class GpsTrack
    {
        private readonly Lazy<(Vector3D Center, double Angle)> _centerAndAngle;
        private readonly Lazy<(Vector3D Center, double Angle)> _minCircle;

        public GpsTrack(IList<GpsPoint> track)
        {
            Track = new List<GpsPoint>(track);
            _centerAndAngle = new Lazy<(Vector3D, double)>(CalculateCenter);
            _minCircle = new Lazy<(Vector3D, double)>(CalculateMinCircle);
        }

        public IReadOnlyList<GpsPoint> Track { get; }

        public Vector3D Center => _centerAndAngle.Value.Center;

        public double CenterAngle => _centerAndAngle.Value.Angle;

        public Vector3D MinCircleCenter => _minCircle.Value.Center;

        public double MinCircleAngle => _minCircle.Value.Angle;

        public FlatTrack CreateFlatTrack()
        {
            return new FlatTrack(Track, Center);
        }

        public FlatTrack CreateFlatTrack(GpsPoint center)
        {
            return new FlatTrack(Track, center);
        }

        private static GridLookup CreateLookup(double gridSize, FlatTrack flatTrack)
        {
            return new GridLookup(flatTrack, gridSize);
        }

        public GridLookup CreateLookup(double gridSize, GpsPoint center)
        {
            return CreateLookup(gridSize, CreateFlatTrack(center));
        }

        public GridLookup CreateLookup(double gridSize)
        {
            return CreateLookup(gridSize, Center);
        }

        private (Vector3D Center, double Angle) CalculateMinCircle()
        {
            if (Track.Count == 0)
                return (new Vector3D(double.NaN, double.NaN, double.NaN), double.NaN);

            var c = Geometry.MinCircleOnSphere(Track.Select(p => ((Vector3D) p).Normalized()).ToList());
            return (c.Center.Normalized() * Geodesy.EarthRadius, System.Math.Asin(c.Radius));
        }

        private (Vector3D Center, double Angle) CalculateCenter()
        {
            var a = new Vector3D();
            var angle = double.NaN;

            var d = 0.0;
            var n = 0;
            foreach (var g in Track)
            {
                Polar3D p = g;
                if (Comparison.IsPositive(p.R))
                {
                    d += p.R;
                    p.R = 1.0;
                    a += p;
                    n++;
                }
            }

            if (n > 0)
            {
                a.Normalize();
                a *= d / n;
                angle = a.Angle(Vector3D.E1);
            }
            else
            {
                a = new Vector3D(double.NaN);
            }

            return (a, angle);
        }
    }
}