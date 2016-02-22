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

namespace Math
{
    public class GpsTrack
    {
        private Vector3D _lookupRotationAxis;
        private double _lookupRoationAngle;
        private double _lookupGridSize;
        private GpsPoint _lookupCenter;
        private Vector2D _lookupMin;
        private Vector2D _lookupMax;
        private List<int>[,] _lookupGrid;

        public GpsTrack(IList<GpsPoint> track)
        {
            Track = track;
            Center = CalculateCenter();
            Vector3D axis;
            double angle;
            CalculateRotation(Center, out axis, out angle);
            RotationAxis = axis;
            RotationAngle = angle;
        }

        public IList<GpsPoint> Track { get; private set; }
        public Vector3D Center { get; private set; }
        public Vector3D RotationAxis { get; private set; }
        public double RotationAngle { get; private set; }
        public List<Vector2D> _lookupPoints { get; private set; }

        public void CreateLookup(double gridSize)
        {
            CreateLookup(Center, gridSize);
        }

        public void CreateLookup(GpsPoint center, double gridSize)
        {
            _lookupGridSize = gridSize;
            _lookupCenter = center;
            CalculateRotation(center, out _lookupRotationAxis, out _lookupRoationAngle);
            _lookupPoints = new List<Vector2D>();
            _lookupMin = new Vector2D(double.PositiveInfinity, double.PositiveInfinity);
            _lookupMax = new Vector2D(double.NegativeInfinity, double.NegativeInfinity);

            foreach (var g in Track)
            {
                GpsPoint u = Rotate(g, _lookupRotationAxis, _lookupRoationAngle);
                var v = new Vector2D(u.Longitude, u.Latitude);
                _lookupPoints.Add(v);
                _lookupMin.X = System.Math.Min(v.X, _lookupMin.X);
                _lookupMin.Y = System.Math.Min(v.Y, _lookupMin.Y);
                _lookupMax.X = System.Math.Max(v.X, _lookupMax.X);
                _lookupMax.Y = System.Math.Max(v.Y, _lookupMax.Y);
            }
            var d = (_lookupMax - _lookupMin) * Geodesy.DistanceOneDeg;
            var nx = (int)(d.X / gridSize) + 1;
            var ny = (int)(d.Y  / gridSize) + 1;
            _lookupGrid = new List<int>[nx, ny];
            for (var i = 0; i < nx; ++i)
                for (var j = 0; j < ny; j++)
                    _lookupGrid[i, j] = new List<int>();
            for (var k = 0; k < _lookupPoints.Count; k++)
            {
                var v = (_lookupPoints[k] - _lookupMin) * Geodesy.DistanceOneDeg;
                var i = (int)(v.X / gridSize);
                var j = (int)(v.Y / gridSize);
                _lookupGrid[i, j].Add(k);
            }
        }

        public Vector3D Rotate(GpsPoint g)
        {
            return (Rotate(g, RotationAxis, RotationAngle));
        }

        public IList<Vector3D> Rotate(IList<GpsPoint> track)
        {
            var res = new List<Vector3D>();
            foreach (var g in track)
            {
                res.Add(Rotate(g));
            }
            return res;
        }

        static private Vector3D Rotate(GpsPoint g, Vector3D axis, double angle)
        {
            return ((Vector3D)g).Rotate(axis, angle);
        }

        private static void CalculateRotation(Vector3D center, out Vector3D axis, out double angle)
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
                    a *= d / n;
                }
            }
            return a;
        }
    }
}