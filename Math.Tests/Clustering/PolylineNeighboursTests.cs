/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2019 Thierry Matthey
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
using Math.Clustering;
using Math.Gps;
using Math.Tests.Gps;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests.Clustering
{
    [TestFixture]
    public class PolylineNeighboursTests
    {
        private readonly GpsTrackExamples _gpsTrackExamples = new GpsTrackExamples();

        [Test]
        public void Neighbours_5TracksSameStartEnd_OneCluster()
        {
            var list = new List<List<GpsPoint>>
            {
                _gpsTrackExamples.TrackOne().ToList(),
                _gpsTrackExamples.TrackTwo().ToList(),
                _gpsTrackExamples.TrackThree().ToList(),
                _gpsTrackExamples.TrackFour().ToList(),
                _gpsTrackExamples.TrackFive().ToList()
            };

            var res = PolylineNeighbours.Cluster(list, 100);
            res.Count.ShouldBe(1);
            res[0].Count.ShouldBe(5);
            res[0][0].ShouldBe(0);
            res[0][1].ShouldBe(1);
            res[0][2].ShouldBe(2);
            res[0][3].ShouldBe(3);
            res[0][4].ShouldBe(4);
        }

        [Test]
        public void Neighbours_5TracksSameStartEndWithSmallEps_OneCluster()
        {
            var list = new List<List<GpsPoint>>
            {
                _gpsTrackExamples.TrackOne().ToList(),
                _gpsTrackExamples.TrackTwo().ToList(),
                _gpsTrackExamples.TrackThree().ToList(),
                _gpsTrackExamples.TrackFour().ToList(),
                _gpsTrackExamples.TrackFive().ToList()
            };

            var res = PolylineNeighbours.Cluster(list, 1);
            res.Count.ShouldBe(1);
            res[0].Count.ShouldBe(5);
            res[0][0].ShouldBe(0);
            res[0][1].ShouldBe(1);
            res[0][2].ShouldBe(2);
            res[0][3].ShouldBe(3);
            res[0][4].ShouldBe(4);
        }

        [Test]
        public void Neighbours_EmptyList_ReturnsEmpty()
        {
            var list = new List<List<Vector2D>>();
            var res = PolylineNeighbours.Cluster(list);
            res.Count.ShouldBe(0);
        }

        [Test]
        public void Neighbours_OnePolyline_ReturnsOne()
        {
            var list = new List<List<Vector2D>> {new List<Vector2D> {new Vector2D()}};
            var res = PolylineNeighbours.Cluster(list);
            res.Count.ShouldBe(1);
            res[0].Count.ShouldBe(1);
            res[0][0].ShouldBe(0);
        }

        [Test]
        public void Neighbours_TwoPolylineAway_ReturnsTwo()
        {
            var list = new List<List<Vector2D>> {new List<Vector2D> {Vector2D.Zero}, new List<Vector2D> {Vector2D.One}};
            var res = PolylineNeighbours.Cluster(list, 0.1);
            res.Count.ShouldBe(2);
            res[0].Count.ShouldBe(1);
            res[1].Count.ShouldBe(1);
            res[0][0].ShouldBe(0);
            res[1][0].ShouldBe(1);
        }

        [Test]
        public void Neighbours_TwoPolylineClose_ReturnsOne()
        {
            var list = new List<List<Vector2D>> {new List<Vector2D> {Vector2D.Zero}, new List<Vector2D> {Vector2D.One}};
            var res = PolylineNeighbours.Cluster(list, 2.0);
            res.Count.ShouldBe(1);
            res[0].Count.ShouldBe(2);
            res[0][0].ShouldBe(0);
            res[0][1].ShouldBe(1);
        }
    }
}