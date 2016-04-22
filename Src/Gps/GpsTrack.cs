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

namespace Math.Gps
{
    public class GpsTrack
    {
        private double _angle;
        private Vector3D _axis;
        private Vector3D _center;

        public GpsTrack(IList<GpsPoint> track)
        {
            Track = track;
        }

        public IList<GpsPoint> Track { get; private set; }

        public Vector3D Center
        {
            get { return _center ?? (_center = CalculateCenter()); }
        }

        public Vector3D RotationAxis
        {
            get
            {
                if (_axis == null)
                {
                    CalculateRotation(Center, out _axis, out _angle);
                }
                return _axis;
            }
        }

        public double RotationAngle
        {
            get
            {
                if (_axis == null)
                {
                    CalculateRotation(Center, out _axis, out _angle);
                }
                return _angle;
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

        public static void CalculateRotation(Vector3D center, out Vector3D axis, out double angle)
        {
            axis = (center ^ -Vector3D.E1).Normalized();
            angle = center.Angle(-Vector3D.E1);
        }

        private Vector3D CalculateCenter()
        {
            var a = new Vector3D();

            if (Track != null)
            {
                var d = 0.0;
                var n = 0.0;
                foreach (var g in Track)
                {
                    Polar3D p = g;
                    if (Comparison.IsPositive(p.R))
                    {
                        n++;
                        d += p.R;
                        p.R = 1.0;
                        a += p;
                    }
                }
                if (n > 0)
                {
                    a.Normalize();
                    a *= d/n;
                }
            }
            return a;
        }
    }
}