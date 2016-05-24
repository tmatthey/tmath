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
using Math.Gps;

namespace Math.Gfx
{
    public delegate Vector3D DelegateCalculateHeatMapCenter(IList<GpsTrack> gpsTracks, IList<Vector3D> allPoints);

    public class HeatMap
    {
        private readonly List<Vector3D> _allPoints;
        private readonly DelegateCalculateHeatMapCenter _calculateCenter;
        private readonly List<GpsTrack> _gpsTracks;

        public HeatMap(DelegateCalculateHeatMapCenter calculateCenter)
        {
            _gpsTracks = new List<GpsTrack>();
            _allPoints = new List<Vector3D>();
            _calculateCenter = calculateCenter;
        }

        public static Vector3D CalculateMinCircle(IList<GpsTrack> gpsTracks, IList<Vector3D> allPoints)
        {
            return Geometry.MinCircleOnSphere(allPoints).Center;
        }

        public static Vector3D CalculateCenter(IList<GpsTrack> gpsTracks, IList<Vector3D> allPoints)
        {
            var center = new Vector3D();
            return gpsTracks.Aggregate(center, (current, item) => current + item.Center)/gpsTracks.Count;
        }

        public void Add(IList<GpsPoint> track)
        {
            Add(new GpsTrack(track));
        }

        public void Add(GpsTrack track)
        {
            _gpsTracks.Add(track);
            _allPoints.AddRange(track.Track.Select(p => ((Vector3D) p).Normalized()).ToList());
        }

        public Bitmap Raw(double pixelSize)
        {
            var center = _calculateCenter(_gpsTracks, _allPoints);
            var tracks = new List<List<Vector2D>>();
            var size = new BoundingRect();
            foreach (var track in _gpsTracks)
            {
                var transformed = track.CreateTransformedTrack(center);
                tracks.Add(transformed.Track);
                size.Expand(transformed.Size);
            }
            var bitmap = new Bitmap(size.Min, size.Max, pixelSize);
            foreach (var track in tracks)
            {
                for (var i = 0; i + 1 < track.Count; i++)
                {
                    Draw.XiaolinWu(track[i], track[i + 1], bitmap.Add);
                }
            }
            return bitmap;
        }

        public double[,] Normalized(double pixelSize, double min, double max)
        {
            var bitmap = Raw(pixelSize);
            var cMax = 0.0;
            foreach (var c in bitmap.Pixels)
            {
                cMax = System.Math.Max(c, cMax);
            }
            foreach (var i in Enumerable.Range(0, bitmap.Pixels.GetLength(0)))
            {
                foreach (var j in Enumerable.Range(0, bitmap.Pixels.GetLength(1)))
                {
                    var c = bitmap.Pixels[i, j];
                    if (Comparison.IsPositive(c))
                        c = c/cMax/(max - min) + min;
                    bitmap.Pixels[i, j] = max - c;
                }
            }
            return bitmap.Pixels;
        }
    }
}