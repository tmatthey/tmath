/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2018 Thierry Matthey
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
using Math.Gps;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests.Gps
{
    [TestFixture]
    public class NeighbourDistanceCalculatorTests
    {
        private readonly GpsTrackExamples _gpsTrackExamples = new GpsTrackExamples();

        [Test]
        public void Analyze_LessSimpleVector2DLists()
        {
            var refLine = new List<Vector2D> {new Vector2D(0, 0), new Vector2D(5, 0), new Vector2D(10, 0)};
            var curLine = new List<Vector2D> {new Vector2D(1, 1), new Vector2D(9, 1)};
            var analyzer = new NeighbourDistanceCalculator(refLine);
            var res = analyzer.Analyze(curLine, 2.0);
            res.Neighbours.Count.ShouldBe(2);
            res.Neighbours[0].Count.ShouldBe(1);
            res.Neighbours[1].Count.ShouldBe(1);
            res.Neighbours[0][0].Fraction.ShouldBe(0.2);
            res.Neighbours[0][0].RefDistance.ShouldBe(1);
            res.Neighbours[1][0].Fraction.ShouldBe(0.8);
            res.Neighbours[1][0].RefDistance.ShouldBe(9);
        }

        [Test]
        public void Analyze_SimpleVector2DLists()
        {
            var refLine = new List<Vector2D> {new Vector2D(0, 0), new Vector2D(10, 0)};
            var curLine = new List<Vector2D> {new Vector2D(1, 1), new Vector2D(9, 1)};
            var analyzer = new NeighbourDistanceCalculator(refLine);
            var res = analyzer.Analyze(curLine, 2.0);
            res.Neighbours.Count.ShouldBe(2);
            res.Neighbours[0].Count.ShouldBe(1);
            res.Neighbours[1].Count.ShouldBe(1);
            res.Neighbours[0][0].Fraction.ShouldBe(0.1);
            res.Neighbours[0][0].RefDistance.ShouldBe(1);
            res.Neighbours[1][0].Fraction.ShouldBe(0.9);
            res.Neighbours[1][0].RefDistance.ShouldBe(9);
        }

        [Test]
        public void Analyze_WithFlatTrack_ReturnsSameListAsGpsImplementation()
        {
            TestUtils.StartTimer();
            var one = new GpsTrack(_gpsTrackExamples.TrackOne());
            var two = new GpsTrack(_gpsTrackExamples.TrackTwo());
            var analyzer1 = new NeighbourDistanceCalculator(one.CreateFlatTrack(), 4.56);
            var current1 = analyzer1.Analyze(two.CreateFlatTrack(one.Center), 50.0);
            TestUtils.StopTimer();
            TestUtils.StartTimer();
            var analyzer2 = new NeighbourGpsDistanceCalculator(_gpsTrackExamples.TrackOne(), 4.56);
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
        public void Analyze_WithVector2D_ReturnsSameListAsGpsImplementation()
        {
            TestUtils.StartTimer();
            var one = new GpsTrack(_gpsTrackExamples.TrackOne());
            var two = new GpsTrack(_gpsTrackExamples.TrackTwo());
            var analyzer1 = new NeighbourDistanceCalculator(one.CreateFlatTrack().Track);
            var current1 = analyzer1.Analyze(two.CreateFlatTrack(one.Center).Track, 50.0);
            TestUtils.StopTimer();
            TestUtils.StartTimer();
            var analyzer2 = new NeighbourGpsDistanceCalculator(_gpsTrackExamples.TrackOne());
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
    }
}