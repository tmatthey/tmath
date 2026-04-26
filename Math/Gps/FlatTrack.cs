/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2025 Thierry Matthey
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
    public class FlatTrack
    {
        private readonly Lazy<(List<double> Distance, List<double> Displacement)> _distances;

        public FlatTrack(IEnumerable<GpsPoint> gpsTrack, Vector3D center)
        {
            Size = new BoundingRect();
            Track = new List<Vector2D>();

            Polar3D c = center;
            foreach (var g in gpsTrack)
            {
                var v = g.ToVector2D(c);
                Track.Add(v);
                Size.Expand(v);
            }

            _distances = new Lazy<(List<double>, List<double>)>(CalculateDistance);
        }

        public FlatTrack(IEnumerable<Vector2D> track)
        {
            Size = new BoundingRect();
            Track = track.ToList();
            foreach (var pt in Track)
            {
                Size.Expand(pt);
            }

            _distances = new Lazy<(List<double>, List<double>)>(CalculateDistance);
        }

        public List<Vector2D> Track { get; }

        public Vector2D Min => Size.Min;

        public Vector2D Max => Size.Max;

        public IList<double> Distance => _distances.Value.Distance;

        public IList<double> Displacement => _distances.Value.Displacement;

        public double TotalDistance => _distances.Value.Distance.LastOrDefault();

        public BoundingRect Size { get; }

        private (List<double> Distance, List<double> Displacement) CalculateDistance()
        {
            var distance = new List<double>();
            var displacement = new List<double>();
            var d = 0.0;
            for (var i = 0; i < Track.Count; i++)
            {
                var ds = 0.0;
                if (i > 0)
                {
                    ds = Track[i - 1].EuclideanNorm(Track[i]);
                }

                d += ds;
                distance.Add(d);
                displacement.Add(ds);
            }

            return (distance, displacement);
        }
    }
}