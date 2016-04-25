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
        public enum IntersectingEnum
        {
            Undefined,
            NotIntersecting,
            Same,
            Inside,
            Outside,
            Overlapping
        }

        private double _angle;
        private Vector3D _axis;
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

        public IntersectingEnum IsCricleIntersecting(GpsTrack target)
        {
            if (double.IsNaN(MinCircleAngle) || double.IsNaN(target.MinCircleAngle))
                return IntersectingEnum.Undefined;

            var r1 = MinCircleAngle*2.0*Geodesy.EarthRadius;
            var r2 = target.MinCircleAngle*2.0*Geodesy.EarthRadius;
            if (Center.Distance(target.Center) < 1.0 && Comparison.IsEqual(r1, r2, 1.0))
                return IntersectingEnum.Same;

            var r12 = System.Math.Abs(MinCircleCenter.Angle(target.MinCircleCenter))*2.0*Geodesy.EarthRadius;
            if (Comparison.IsLessEqual(r12 + r1, r2, 1.0))
                return IntersectingEnum.Outside;

            if (Comparison.IsLessEqual(r12 + r2, r1, 1.0))
                return IntersectingEnum.Inside;

            if (Comparison.IsLessEqual(r12, r1 + r2, 1.0))
                return IntersectingEnum.Overlapping;

            return IntersectingEnum.NotIntersecting;
        }

        public IntersectingEnum IsRectIntersecting(GpsTrack target)
        {
            if (double.IsNaN(RotationAngle) || double.IsNaN(target.RotationAngle))
                return IntersectingEnum.Undefined;

            var t1 = CreateTransformedTrack();
            var t2 = target.CreateTransformedTrack();
            var r1 = t1.Size.Min.Distance(t1.Size.Max);
            var r2 = t2.Size.Min.Distance(t2.Size.Max);
            var r12 = System.Math.Abs(Center.Angle(target.Center))*2.0*Geodesy.EarthRadius;
            if (!Comparison.IsLessEqual(r12, r1 + r2, 1.0))
                return IntersectingEnum.NotIntersecting;

            t2 = target.CreateTransformedTrack(Center);
            if (t1.Size.Min.Distance(t2.Size.Min) < 1.0 && t1.Size.Max.Distance(t2.Size.Max) < 1.0)
                return IntersectingEnum.Same;

            var insideMin = t1.Size.IsInside(t2.Size.Min);
            var insideMax = t1.Size.IsInside(t2.Size.Max);
            var outsideMin = t2.Size.IsInside(t1.Size.Min);
            var outsideMax = t2.Size.IsInside(t1.Size.Max);

            if (insideMax && insideMin)
            {
                return IntersectingEnum.Inside;
            }
            if (outsideMin && outsideMax)
            {
                return IntersectingEnum.Outside;
            }

            if (insideMax || insideMin || outsideMin || outsideMax)
                return IntersectingEnum.Overlapping;

            return IntersectingEnum.NotIntersecting;
        }

        public static void CalculateRotation(Vector3D center, out Vector3D axis, out double angle)
        {
            axis = (center ^ -Vector3D.E1).Normalized();
            angle = center.Angle(-Vector3D.E1);
        }

        private void CalculateMinCircle()
        {
            var c = Geometry.MinCircleOnSphere(Track.Select(p => ((Vector3D) p).Normalized()).ToList());
            _minCircleCenter = c.Center.Normalized()*Geodesy.EarthRadius;
            _minCircleAngle = System.Math.Asin(c.Radius);
        }

        private Vector3D CalculateCenter()
        {
            var a = new Vector3D(double.NaN);

            var d = 0.0;
            var n = 0.0;
            foreach (var g in Track)
            {
                Polar3D p = g;
                if (Comparison.IsPositive(p.R))
                {
                    d += p.R;
                    p.R = 1.0;
                    if (n < 0.5)
                    {
                        a = p;
                    }
                    else
                    {
                        a += p;
                    }
                    n++;
                }
            }
            if (n > 0)
            {
                a.Normalize();
                a *= d/n;
            }

            return a;
        }
    }
}