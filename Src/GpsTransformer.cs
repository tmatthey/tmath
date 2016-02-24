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
    public class GpsTransformer
    {
        public GpsTransformer(IList<GpsPoint> gpsTrack, Vector3D center)
        {
            Center = center;
            Vector3D axis;
            double angle;
            GpsTrack.CalculateRotation(center, out axis, out angle);
            RotationAxis = axis;
            RotationAngle = angle;
            Track = new List<Vector2D>();
            Min = new Vector2D(double.PositiveInfinity, double.PositiveInfinity);
            Max = new Vector2D(double.NegativeInfinity, double.NegativeInfinity);

            foreach (var g in gpsTrack)
            {
                GpsPoint u = ((Vector3D)g).Rotate(RotationAxis, RotationAngle);
                var v = new Vector2D(u.Longitude - 180.0, u.Latitude) * Geodesy.DistanceOneDeg;
                Track.Add(v);
                Min.X = System.Math.Min(v.X, Min.X);
                Min.Y = System.Math.Min(v.Y, Min.Y);
                Max.X = System.Math.Max(v.X, Max.X);
                Max.Y = System.Math.Max(v.Y, Max.Y);
            }
        }

        public Vector3D Center { get; private set; }
        public List<Vector2D> Track { get; private set; }
        public Vector3D RotationAxis { get; private set; }
        public double RotationAngle { get; private set; }
        public Vector2D Min { get; private set; }
        public Vector2D Max { get; private set; }
    }
}