/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2024 Thierry Matthey
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
            var _lookup = gpsTrackRef.CreateLookup(10.0, gpsTrackRef.Center);
            var grid = _lookup.Grid;
            var max = 0;
            foreach (var list in grid)
                max = System.Math.Max(max, list.Count);
            var bitmap = new double[grid.GetLength(0), grid.GetLength(1)];
            foreach (var i in Enumerable.Range(0, grid.GetLength(0)))
            foreach (var j in Enumerable.Range(0, grid.GetLength(1)))
                bitmap[i, j] = grid[i, j].Count / (double) max;
            BitmapFileWriter.PGM(TestUtils.OutputPath() + "trackOne.pgm", bitmap);
        }

        [Test]
        public void WritePNG_HeatColorMapping()
        {
            var bitmap = new Bitmap(Vector2D.Zero, Vector2D.One * 300, 1);
            var width = bitmap.Pixels.GetLength(0) - 1;
            var height = bitmap.Pixels.GetLength(1) - 1;
            for (var i = 0; i <= width; i++)
            {
                var a = i / (double) width;
                for (var j = 0; j <= height; j++)
                {
                    bitmap.Pixels[i, j] = a;
                }
            }

            BitmapFileWriter.PNG(TestUtils.OutputPath() + "HeatColorMapping.png", bitmap.Pixels,
                HeatColorMapping.Default);
        }

        [Test]
        public void WritePNG_L()
        {
            var bitmap = new Bitmap(-Vector2D.One * 0.1, Vector2D.E1 + Vector2D.E2 * 2.0 + Vector2D.One * 0.1, 0.1);
            Draw.XiaolinWu(Vector2D.E2 * 2.0, Vector2D.Zero, bitmap.Add, 0.5);
            Draw.XiaolinWu(Vector2D.Zero, Vector2D.E1, bitmap.Add);
            BitmapFileWriter.PNG(TestUtils.OutputPath() + "L.png", bitmap.Pixels);
        }

        [Test]
        public void WritePNG_WritesTrackOneToDisk()
        {
            var gpsTrackRef = new GpsTrack(_gpsTrackExamples.TrackOne());
            var _lookup = gpsTrackRef.CreateLookup(10.0, gpsTrackRef.Center);
            var grid = _lookup.Grid;
            var max = 0;
            foreach (var list in grid)
                max = System.Math.Max(max, list.Count);
            var bitmap = new double[grid.GetLength(0), grid.GetLength(1)];
            foreach (var i in Enumerable.Range(0, grid.GetLength(0)))
            foreach (var j in Enumerable.Range(0, grid.GetLength(1)))
                bitmap[i, j] = grid[i, j].Count / (double) max;
            BitmapFileWriter.PNG(TestUtils.OutputPath() + "trackOne.png", bitmap);
        }

        [Test]
        public void WritePPM_HeatColorMapping()
        {
            var bitmap = new Bitmap(Vector2D.Zero, Vector2D.One * 300, 1);
            var width = bitmap.Pixels.GetLength(0) - 1;
            var height = bitmap.Pixels.GetLength(1) - 1;
            for (var i = 0; i <= width; i++)
            {
                var a = i / (double) width;
                for (var j = 0; j <= height; j++)
                {
                    bitmap.Pixels[i, j] = a;
                }
            }

            BitmapFileWriter.PPM(TestUtils.OutputPath() + "HeatColorMapping.ppm", bitmap.Pixels,
                HeatColorMapping.Default);
        }

        [Test]
        public void CubicBezierExample()
        {
            var P0 = new Vector2D(160, 120);
            var P1 = new Vector2D(200, 35);
            var P2 = new Vector2D(260, 220);
            var P3 = new Vector2D(40, 220);

            var bezier = new CubicBezier2D(P0, P1, P2, P3);

            var box = new BoundingRect();
            box.Expand(P0);
            box.Expand(P1);
            box.Expand(P2);
            box.Expand(P3);
            box.ExpandLayer(10);
            var bitmap = new RGBBitmap(box.Min, box.Max, 0.1, 1000);

            // Bounding rect
            var bb = bezier.Bounding() as BoundingRect;
            Draw.XiaolinWu(bb.Min, new Vector2D(bb.Max.X, bb.Min.Y), bitmap.Set);
            Draw.XiaolinWu(new Vector2D(bb.Max.X, bb.Min.Y), bb.Max, bitmap.Set);
            Draw.XiaolinWu(bb.Max, new Vector2D(bb.Min.X, bb.Max.Y), bitmap.Set);
            Draw.XiaolinWu(new Vector2D(bb.Min.X, bb.Max.Y), bb.Min, bitmap.Set);

            // Control points
            bitmap.Color = Color.Default.Green;
            Draw.XiaolinWu(P0, P1, bitmap.Set);
            Draw.XiaolinWu(P1, P2, bitmap.Set);
            Draw.XiaolinWu(P2, P3, bitmap.Set);

            var n = 100;
            var list = new List<Vector2D>();
            bitmap.Color = Color.Default.Red;
            for (var i = 0; i <= n; i++)
            {
                list.Add(bezier.Evaluate(i / (double) n));
            }

            // Bezier curve
            for (var i = 0; i + 1 < list.Count; i++)
            {
                Draw.XiaolinWu(list[i], list[i + 1], bitmap.Set);
            }

            BitmapFileWriter.PNG(TestUtils.OutputPath() + "CubicBezier2D.png", bitmap.RedPixels, bitmap.GreenPixels,
                bitmap.BluePixels);
        }
    }
}