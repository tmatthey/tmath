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
using System.Linq;
using Math.Gps;

namespace Math.Gfx
{
    public delegate Vector3D DelegateCalculateHeatMapCenter(IList<GpsTrack> gpsTracks, IList<Vector3D> allPoints);

    public class HeatMap
    {
        private const int MaxLength = 2000;
        private readonly List<List<GpsPoint>> _tracks;

        public HeatMap()
        {
            _tracks = new List<List<GpsPoint>>();
        }


        public void Add(IList<GpsPoint> track)
        {
            _tracks.Add(track.ToList());
        }

        public void Add(GpsTrack track)
        {
            _tracks.Add(track.Track.ToList());
        }

        public Bitmap Raw(double pixelSize, int maxLength = MaxLength)
        {
            var flatTracks = new FlatTrackCluster(_tracks);
            if (flatTracks.Size.IsEmpty())
                return null;

            var bitmap = new Bitmap(flatTracks.Size.Min, flatTracks.Size.Max, pixelSize, maxLength);
            foreach (var track in flatTracks.FlatTracks)
            {
                if (track.Track.Any())
                {
                    var prev = bitmap.Add.Converter(track.Track[0]);
                    Vector2D last = null;
                    for (var i = 1; i < track.Track.Count; i++)
                    {
                        var a = last ?? prev;
                        var b = bitmap.Add.Converter(track.Track[i]);
                        var l = a.EuclideanNorm(b);
                        var k = System.Math.Abs((int) a.X - (int) b.X) + System.Math.Abs((int) a.Y - (int) b.Y);
                        if (i + 1 < track.Track.Count && (Comparison.IsLess(l, 2.0) || k < 2))
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
                            c = bitmap.Pixels[i, j] / max;
                        }
                    }

                    bitmap.Pixels[i, j] = c;
                }
            }

            return bitmap.Pixels;
        }

        public double[,] Median(double pixelSize, int maxLength = MaxLength)
        {
            var bitmap = Raw(pixelSize, maxLength);
            if (bitmap == null)
                return null;

            var list =
                bitmap.Pixels.Cast<double>()
                    .Where(pixel => Comparison.IsLess(0.0, pixel))
                    .OrderBy(num => num)
                    .Distinct()
                    .ToList();
            double n = list.Count;
            if (n > 0)
            {
                foreach (var i in Enumerable.Range(0, bitmap.Pixels.GetLength(0)))
                {
                    foreach (var j in Enumerable.Range(0, bitmap.Pixels.GetLength(1)))
                    {
                        var pixel = bitmap.Pixels[i, j];
                        if (Comparison.IsLess(0.0, pixel))
                        {
                            var c = (list.IndexOf(pixel) + 1) / n;
                            bitmap.Pixels[i, j] = c;
                        }
                    }
                }
            }

            return bitmap.Pixels;
        }

        public double[,] Log(double pixelSize, int maxLength = MaxLength)
        {
            var bitmap = Raw(pixelSize, maxLength);
            if (bitmap == null)
                return null;

            var max = bitmap.Pixels.Cast<double>().Aggregate(0.0, (current, c) => System.Math.Max(c, current));
            if (Comparison.IsPositive(max))
            {
                var b = System.Math.Exp(System.Math.Log(max) / 0.9);
                foreach (var i in Enumerable.Range(0, bitmap.Pixels.GetLength(0)))
                {
                    foreach (var j in Enumerable.Range(0, bitmap.Pixels.GetLength(1)))
                    {
                        double c;
                        var pixel = bitmap.Pixels[i, j];
                        if (Comparison.IsEqual(pixel, max))
                        {
                            c = 1.0;
                        }
                        else if (Comparison.IsLess(pixel, 1.0))
                        {
                            c = pixel * 0.1;
                        }
                        else
                        {
                            c = System.Math.Log(pixel, b) + 0.1;
                        }

                        bitmap.Pixels[i, j] = c;
                    }
                }
            }

            return bitmap.Pixels;
        }
    }
}