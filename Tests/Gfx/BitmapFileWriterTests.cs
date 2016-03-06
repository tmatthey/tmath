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
using Math.Gfx;
using Math.Gps;
using Math.Tests.Gps;
using NUnit.Framework;

namespace Math.Tests.Gfx
{
    [TestFixture]
    public class BitmapFileWriterTests
    {
        private readonly GpsTrackExamples _gpsTrackExamples = new GpsTrackExamples();

        [Test]
        public void WritePGM_WritesTrackOneToDisk()
        {
            var gpsTrackRef = new GpsTrack(_gpsTrackExamples.TrackOne());
            gpsTrackRef.SetupLookup(gpsTrackRef.Center, 10.0);
            var grid = gpsTrackRef.Lookup.Grid;
            var max = 0;
            foreach (var list in grid)
                max = System.Math.Max(max, list.Count);
            var bitmap = new double[grid.GetLength(0), grid.GetLength(1)];
            foreach (var i in Enumerable.Range(0, grid.GetLength(0)))
                foreach (var j in Enumerable.Range(0, grid.GetLength(1)))
                    bitmap[i, j] = grid[i, j].Count / (double)max;
            BitmapFileWriter.PGM("trackOne.pgm", bitmap);
        }

        [Test]
        public void WritePNG_WritesTrackOneToDisk()
        {
            var gpsTrackRef = new GpsTrack(_gpsTrackExamples.TrackOne());
            gpsTrackRef.SetupLookup(gpsTrackRef.Center, 10.0);
            var grid = gpsTrackRef.Lookup.Grid;
            var max = 0;
            foreach (var list in grid)
                max = System.Math.Max(max, list.Count);
            var bitmap = new double[grid.GetLength(0), grid.GetLength(1)];
            foreach (var i in Enumerable.Range(0, grid.GetLength(0)))
                foreach (var j in Enumerable.Range(0, grid.GetLength(1)))
                    bitmap[i, j] = grid[i, j].Count / (double)max;
            BitmapFileWriter.PNG("trackOne.png", bitmap);
        }

        [Test]
        public void BitmapAdd_GreatesHeatmap()
        {
            var rawTracks = new List<List<GpsPoint>>();
            rawTracks.Add(_gpsTrackExamples.TrackOne().ToList());
            rawTracks.Add(_gpsTrackExamples.TrackTwo().ToList());
            rawTracks.Add(_gpsTrackExamples.TrackThree().ToList());
            rawTracks.Add(_gpsTrackExamples.TrackFour().ToList());
            rawTracks.Add(_gpsTrackExamples.TrackFive().ToList());
            var gpsTracks = new List<GpsTrack>();
            var center = new Vector3D();
            foreach (var gpsTrack in rawTracks.Select(track => new GpsTrack(track)))
            {
                center += gpsTrack.Center;
                gpsTracks.Add(gpsTrack);
            }
            center /= gpsTracks.Count;
            var tracks = new List<List<Vector2D>>();
            var min = new Vector2D();
            var max = new Vector2D();
            foreach (var track in gpsTracks)
            {
                var transformed = track.CreateTransformedTrack(center);
                tracks.Add(transformed.Track);
                min.X = System.Math.Min(min.X, transformed.Min.X);
                min.Y = System.Math.Min(min.Y, transformed.Min.Y);
                max.X = System.Math.Max(max.X, transformed.Max.X);
                max.Y = System.Math.Max(max.Y, transformed.Max.Y);
            }
            var bitmap = new BitmapAdd(min, max, 2.5);
            foreach (var track in tracks)
            {
                for (var i = 0; i + 1 < track.Count; i++)
                {
                    Draw.XiaolinWu(track[i], track[i+1], bitmap);
                }
            }
            var cMax = 0.0;
            foreach (var c in bitmap.Bitmap)
            {
                cMax = System.Math.Max(c, cMax);
            }
            foreach (var i in Enumerable.Range(0, bitmap.Bitmap.GetLength(0)))
                foreach (var j in Enumerable.Range(0, bitmap.Bitmap.GetLength(1)))
                {
                    var c = bitmap.Bitmap[i, j];
                    if (Comparison.IsPositive(c))
                        c = c / cMax / 0.95 + 0.05;
                    bitmap.Bitmap[i, j] = 1.0 - c;
                }
            
            BitmapFileWriter.PNG("heatMap.png", bitmap.Bitmap);
        }
    }
}
