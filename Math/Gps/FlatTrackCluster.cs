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

using System.Collections.Generic;

namespace Math.Gps
{
    public class FlatTrackCluster
    {
        public FlatTrackCluster(List<List<GpsPoint>> gpsTracks)
        {
            Center = CalculateCenter(gpsTracks);
            Tracks = new List<List<Vector2D>>();
            FlatTracks = new List<FlatTrack>();
            Size = new BoundingRect();
            foreach (var track in gpsTracks)
            {
                var gpsTrack = new GpsTrack(track);
                var flatTrack = gpsTrack.CreateFlatTrack(Center);
                Size.Expand(flatTrack.Size);
                Tracks.Add(flatTrack.Track);
                FlatTracks.Add(flatTrack);
            }
        }

        public BoundingRect Size { get; protected set; }
        public Vector3D Center { get; protected set; }
        public List<FlatTrack> FlatTracks { get; protected set; }
        public List<List<Vector2D>> Tracks { get; protected set; }

        private static Vector3D CalculateCenter(IEnumerable<List<GpsPoint>> gpsTracks)
        {
            var center = new Vector3D();
            var d = 0.0;
            var n = 0;
            foreach (var track in gpsTracks)
            {
                foreach (var point in track)
                {
                    Polar3D p = point;
                    if (Comparison.IsPositive(p.R))
                    {
                        d += p.R;
                        p.R = 1.0;
                        center += p;
                        n++;
                    }
                }
            }

            return n > 0 ? center.Normalized() * d / n : new Vector3D(double.NaN);
        }
    }
}