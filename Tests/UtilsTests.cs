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

using System;
using System.Linq;
using Math.Gps;
using Math.Tests.Gps;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests
{
    [TestFixture]
    public class UtilsTests
    {
        private readonly GpsTrackExamples _gpsTrackExamples = new GpsTrackExamples();
        
        [Test]
        public void Swap()
        {
            var a = 2;
            var b = 3;
            Utils.Swap(ref a, ref b);
            a.ShouldBe(3);
            b.ShouldBe(2);
        }

        [Test]
        public void WritePGM_WritesTrackOneToDisk()
        {
            var gpsTrackRef = new GpsTrack(_gpsTrackExamples.TrackOne());
            gpsTrackRef.CreateLookup(gpsTrackRef.Center, 10.0);
            var grid = gpsTrackRef.Grid.Grid;
            var max = 0;
            foreach (var list in grid)
                max = System.Math.Max(max, list.Count);
            var bitmap = new double[grid.GetLength(0), grid.GetLength(1)];
            foreach (var i in Enumerable.Range(0, grid.GetLength(0)))
                foreach (var j in Enumerable.Range(0, grid.GetLength(1)))
                    bitmap[i, j] = grid[i, j].Count/(double)max;
            Utils.WritePGM("trackOne.pgm", bitmap);
        }
    }
}
