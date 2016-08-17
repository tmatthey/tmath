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
                    bitmap[i, j] = grid[i, j].Count/(double) max;
            BitmapFileWriter.PGM(TestUtils.OutputPath() + "trackOne.pgm", bitmap);
        }

        [Test]
        public void WritePNG_HeatColorMapping()
        {
            var bitmap = new Bitmap(Vector2D.Zero, Vector2D.One*300, 1);
            var width = bitmap.Pixels.GetLength(0) - 1;
            var height = bitmap.Pixels.GetLength(1) - 1;
            for (var i = 0; i <= width; i++)
            {
                var a = i/(double) width;
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
            var bitmap = new Bitmap(-Vector2D.One*0.1, Vector2D.E1 + Vector2D.E2*2.0 + Vector2D.One*0.1, 0.1);
            Draw.XiaolinWu(Vector2D.E2*2.0, Vector2D.Zero, bitmap.Add, 0.5);
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
                    bitmap[i, j] = grid[i, j].Count/(double) max;
            BitmapFileWriter.PNG(TestUtils.OutputPath() + "trackOne.png", bitmap);
        }

        [Test]
        public void WritePPM_HeatColorMapping()
        {
            var bitmap = new Bitmap(Vector2D.Zero, Vector2D.One*300, 1);
            var width = bitmap.Pixels.GetLength(0) - 1;
            var height = bitmap.Pixels.GetLength(1) - 1;
            for (var i = 0; i <= width; i++)
            {
                var a = i/(double) width;
                for (var j = 0; j <= height; j++)
                {
                    bitmap.Pixels[i, j] = a;
                }
            }
            BitmapFileWriter.PPM(TestUtils.OutputPath() + "HeatColorMapping.ppm", bitmap.Pixels,
                HeatColorMapping.Default);
        }
    }
}