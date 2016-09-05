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
using Math;
using Math.Gps;

namespace Tools.Gps
{
    public class SegmentResult
    {
        public SegmentResult(List<TrackSegment> trackSegments, List<Vector2D> segment, Vector3D center)
        {
            TrackSegments = trackSegments;
            RepresentativeTrack = segment;
            Length = 0.0;
            RepresentativeGpsTrack = new List<GpsPoint>();

            Polar3D c = center;
            var a3 = -c.Theta;
            var a2 = System.Math.PI*0.5 - c.Phi;
            foreach (var v2 in segment)
            {
                Vector3D w0 = new GpsPoint
                {
                    Longitude = v2.X/Geodesy.DistanceOneDeg,
                    Latitude = v2.Y/Geodesy.DistanceOneDeg
                };
                var w1 = w0.RotateE2(-a2);
                GpsPoint u = w1.RotateE3(-a3);
                u.Elevation = 0.0;
                RepresentativeGpsTrack.Add(u);
            }

            for (var l = 0; l + 1 < segment.Count; l++)
            {
                Length += segment[l].EuclideanNorm(segment[l + 1]);
            }
        }

        public List<TrackSegment> TrackSegments { get; private set; }
        public List<Vector2D> RepresentativeTrack { get; private set; }
        public List<GpsPoint> RepresentativeGpsTrack { get; private set; }
        public double Length { get; private set; }
    }
}