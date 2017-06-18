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

using Math.Gps;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests.Gps
{
    [TestFixture]
    public class NeighbourGpsDistanceCalculatorTests
    {
        private readonly GpsTrackExamples _gpsTrackExamples = new GpsTrackExamples();

        [Test]
        public void Analyze_WithDifferentGridSize_ReturnsSameList()
        {
            TestUtils.StartTimer();
            var analyzer1 = new NeighbourGpsDistanceCalculator(_gpsTrackExamples.TrackOne(), 4.56);
            var current1 = analyzer1.Analyze(_gpsTrackExamples.TrackTwo(), 50.0);
            TestUtils.StopTimer();
            TestUtils.StartTimer();
            var analyzer2 = new NeighbourGpsDistanceCalculator(_gpsTrackExamples.TrackOne(), 51.37);
            var current2 = analyzer2.Analyze(_gpsTrackExamples.TrackTwo(), 50.0);
            TestUtils.StopTimer();

            current1.Neighbours.Count.ShouldBe(current2.Neighbours.Count);
            for (var i = 0; i < System.Math.Min(current1.Neighbours.Count, current2.Neighbours.Count); i++)
            {
                current1.Neighbours[i].Count.ShouldBe(current2.Neighbours[i].Count);
                for (var j = 0; j < System.Math.Min(current1.Neighbours[i].Count, current2.Neighbours[i].Count); j++)
                {
                    current1.Neighbours[i][j].IsEqual(current2.Neighbours[i][j]).ShouldBe(true);
                }
            }
        }

        [Test]
        public void ListOfNeighbours_InRange()
        {
            TestUtils.StartTimer();
            var analyzer = new NeighbourGpsDistanceCalculator(_gpsTrackExamples.TrackOne());
            var current = analyzer.Analyze(_gpsTrackExamples.TrackTwo(), 50.0);
            TestUtils.StopTimer();
            foreach (var point in current.Neighbours)
            {
                foreach (var p in point)
                {
                    var r = p.Reference;
                    var c = p.Current;
                    r.ShouldBeGreaterThanOrEqualTo(0);
                    r.ShouldBeLessThan(analyzer.ReferenceFlattendTrack.Track.Count);
                    c.ShouldBeGreaterThanOrEqualTo(0);
                    c.ShouldBeLessThan(current.FlattendTrack.Track.Count);
                }
            }
        }

        [Test]
        public void TotalDistance_ReturnsExpected()
        {
            var one = _gpsTrackExamples.TrackOne();
            var analyzer = new NeighbourGpsDistanceCalculator(one);
            var two = _gpsTrackExamples.TrackTwo();
            var current = analyzer.Analyze(two, 50.0);
            analyzer.ReferenceFlattendTrack.TotalDistance.ShouldBe(
                Geodesy.Distance.HaversineTotal(one), 1e-1);
            current.TotalDistance.ShouldBe(Geodesy.Distance.HaversineTotal(two), 1e-1);
        }
    }
}