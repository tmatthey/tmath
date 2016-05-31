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
    public class Transformer
    {
        public Transformer(IEnumerable<GpsPoint> gpsTrack, Vector3D center)
        {
            Center = center;
            Vector3D axis;
            double angle;
            GpsTrack.CalculateRotation(center, out axis, out angle);
            RotationAxis = axis;
            RotationAngle = angle;
            Size = new BoundingRect();
            Track = new List<Vector2D>();

            foreach (var g in gpsTrack)
            {
                GpsPoint u = ((Vector3D) g).Rotate(RotationAxis, RotationAngle);
                var v = new Vector2D(u.Longitude - 180.0, u.Latitude)*Geodesy.DistanceOneDeg;
                Track.Add(v);
                Size.Expand(v);
            }

            Distance = new List<double>();
            Displacement = new List<double>();
            var d = 0.0;
            for (var i = 0; i < Track.Count; i++)
            {
                var ds = 0.0;
                if (i > 0)
                {
                    ds = Track[i - 1].EuclideanNorm(Track[i]);
                }
                d += ds;
                Distance.Add(d);
                Displacement.Add(ds);
            }
        }

        public Vector3D Center { get; private set; }
        public List<Vector2D> Track { get; private set; }
        public Vector3D RotationAxis { get; private set; }
        public double RotationAngle { get; private set; }

        public Vector2D Min
        {
            get { return Size.Min; }
        }

        public Vector2D Max
        {
            get { return Size.Max; }
        }

        public IList<double> Distance { get; private set; }
        public IList<double> Displacement { get; private set; }

        public double TotalDistance
        {
            get { return Distance.LastOrDefault(); }
        }

        public BoundingRect Size { get; private set; }
    }
}