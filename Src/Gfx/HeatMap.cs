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
        private const int MaxLength = 2000;
        private readonly List<GpsTrack> _gpsTracks;

        public HeatMap()
        {
            _gpsTracks = new List<GpsTrack>();
        }


        public void Add(IList<GpsPoint> track)
        {
            Add(new GpsTrack(track));
        }

        public void Add(GpsTrack track)
        {
            _gpsTracks.Add(track);
        }

        public Bitmap Raw(double pixelSize, int maxLength = MaxLength)
        {
            var n = _gpsTracks.Sum(track => track.Track.Count);
            if (n == 0)
                return null;

            var center = new Vector3D();
            center =
                _gpsTracks.Aggregate(center,
                    (current1, track) => track.Track.Aggregate(current1, (current, pt) => current + pt))/n;

            var tracks = new List<List<Vector2D>>();
            var size = new BoundingRect();
            foreach (var track in _gpsTracks)
            {
                var transformed = track.CreateTransformedTrack(center);
                tracks.Add(transformed.Track);
                size.Expand(transformed.Size);
            }
            var bitmap = new Bitmap(size.Min, size.Max, pixelSize, maxLength);
            foreach (var track in tracks)
            {
                if (track.Any())
                {
                    var prev = bitmap.Add.Converter(track[0]);
                    Vector2D last = null;
                    for (var i = 1; i < track.Count; i++)
                    {
                        var a = last ?? prev;
                        var b = bitmap.Add.Converter(track[i]);
                        var l = a.EuclideanNorm(b);
                        var k = System.Math.Abs((int) a.X - (int) b.X) + System.Math.Abs((int) a.Y - (int) b.Y);
                        if (i + 1 < track.Count && (Comparison.IsLess(l, 2.0) || k < 2))
                        {
                            if (last == null)
                                last = prev;
                        }
                        else
                        {
                            Draw.XiaolinWu(a, b, bitmap.Add.Plot);
                            last = null;
                        }
                        prev = b;
                    }
                }
            }
            return bitmap;
        }

        public double[,] Normalized(double pixelSize, int maxLength = MaxLength)
        {
            var bitmap = Raw(pixelSize, maxLength);
            if (bitmap == null)
                return null;

            var max = bitmap.Pixels.Cast<double>().Aggregate(0.0, (current, c) => System.Math.Max(c, current));
            foreach (var i in Enumerable.Range(0, bitmap.Pixels.GetLength(0)))
            {
                foreach (var j in Enumerable.Range(0, bitmap.Pixels.GetLength(1)))
                {
                    var c = 0.0;
                    if (Comparison.IsPositive(max))
                    {
                        if (Comparison.IsEqual(bitmap.Pixels[i, j], max))
                        {
                            c = 1.0;
                        }
                        else if (Comparison.IsPositive(bitmap.Pixels[i, j]))
                        {
                            c = bitmap.Pixels[i, j]/max;
                        }
                    }
                    bitmap.Pixels[i, j] = c;
                }
            }
            return bitmap.Pixels;
        }
    }
}