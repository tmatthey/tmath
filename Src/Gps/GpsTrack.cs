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

using System.Collections.Generic;
using System.Linq;

namespace Math.Gps
{
    public class GpsTrack
    {
        private double _angle;
        private Vector3D _center;
        private double _minCircleAngle;
        private Vector3D _minCircleCenter;

        public GpsTrack(IList<GpsPoint> track)
        {
            Track = track;
        }

        public IList<GpsPoint> Track { get; private set; }

        public Vector3D Center
        {
            get
            {
                if (_center == null)
                {
                    CalculateCenter();
                }
                return _center;
            }
        }

        public double CenterAngle
        {
            get
            {
                if (_center == null)
                {
                    CalculateCenter();
                }
                return _angle;
            }
        }

        public Vector3D MinCircleCenter
        {
            get
            {
                if (_minCircleCenter == null)
                {
                    CalculateMinCircle();
                }
                return _minCircleCenter;
            }
        }

        public double MinCircleAngle
        {
            get
            {
                if (_minCircleCenter == null)
                {
                    CalculateMinCircle();
                }
                return _minCircleAngle;
            }
        }

        public Transformer TransformedTrack { get; private set; }
        public GridLookup Lookup { get; private set; }

        public Transformer CreateTransformedTrack()
        {
            return new Transformer(Track, Center);
        }

        public Transformer CreateTransformedTrack(GpsPoint center)
        {
            return new Transformer(Track, center);
        }

        public void SetupLookup(GpsPoint center, double gridSize)
        {
            TransformedTrack = CreateTransformedTrack(center);
            Lookup = new GridLookup(TransformedTrack, gridSize);
        }


        private void CalculateMinCircle()
        {
            var c = Geometry.MinCircleOnSphere(Track.Select(p => ((Vector3D) p).Normalized()).ToList());
            _minCircleCenter = c.Center.Normalized()*Geodesy.EarthRadius;
            _minCircleAngle = System.Math.Asin(c.Radius);
        }

        private void CalculateCenter()
        {
            var a = new Vector3D();
            _angle = double.NaN;

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
                a *= d/n;
                _angle = a.Angle(Vector3D.E1);
            }
            else
            {
                a = new Vector3D(double.NaN);
            }
            _center = a;
        }
    }
}