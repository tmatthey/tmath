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
using Math.Clustering;
using Math.Gps;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests.Clustering
{
    [TestFixture]
    public class GpsSegmentClusteringTests
    {
        [Test]
        public void FindGlobalCommonSegments_OneTrack_ReturnsIdentity()
        {
            var list = new List<List<GpsPoint>>
            {
                new List<GpsPoint> {new GpsPoint(0, 0), new GpsPoint(0.01, 0.01)}
            };
            var res = GpsSegmentClustering.FindGlobalCommonSegments(list, 1);
            res.Count.ShouldBe(1);
            res[0].Count.ShouldBe(1);
            res[0][0].RepresentativeGpsTrack.First().ShouldBe(list.First().First());
            res[0][0].RepresentativeGpsTrack.Last().ShouldBe(list.First().Last());
        }

        [Test]
        public void FindGlobalCommonSegments_OneTrack_ReturnsRepresentativeTracksWithSameDistances()
        {
            var list = new List<List<GpsPoint>>
            {
                new List<GpsPoint> {new GpsPoint(0, 0), new GpsPoint(0.01, 0.01)}
            };
            var res = GpsSegmentClustering.FindGlobalCommonSegments(list, 1);
            res.Count.ShouldBe(1);
            res[0].Count.ShouldBe(1);
            var distGps =
                res[0][0].RepresentativeGpsTrack.First().HaversineDistance(res[0][0].RepresentativeGpsTrack.Last());
            var distPlane = res[0][0].RepresentativeTrack.First().EuclideanNorm(res[0][0].RepresentativeTrack.Last());
            distPlane.ShouldBe(distGps, 1e-6);
        }

        [Test]
        public void FindGlobalCommonSegments_ThreeDisjunctTrackClusters_ReturnsThreeClusters()
        {
            var list = new List<List<GpsPoint>>
            {
                new List<GpsPoint> {new GpsPoint(0.00000001, 0), new GpsPoint(0, 0.001)},
                new List<GpsPoint> {new GpsPoint(0, 0), new GpsPoint(0, 0.001)},
                new List<GpsPoint> {new GpsPoint(5, 5), new GpsPoint(5, 5.001)},
                new List<GpsPoint> {new GpsPoint(5.00000001, 5), new GpsPoint(5, 5.001)},
                new List<GpsPoint> {new GpsPoint(40, 50), new GpsPoint(40.1, 50)}
            };
            var res = GpsSegmentClustering.FindGlobalCommonSegments(list, 1, 5000, 20, 5, 5000);
            res.Count.ShouldBe(3);
            res[0].Count.ShouldBe(1);
            res[1].Count.ShouldBe(1);
            res[2].Count.ShouldBe(1);
        }

        [Test]
        public void FindGlobalCommonSegments_TwoCloseTracks_ReturnsOneSegments()
        {
            var list = new List<List<GpsPoint>>
            {
                new List<GpsPoint> {new GpsPoint(0.0001, 0), new GpsPoint(0.0001, 0.0001), new GpsPoint(0.0001, 0.0002)},
                new List<GpsPoint>
                {
                    new GpsPoint(-0.0001, 0),
                    new GpsPoint(-0.0001, 0.0001),
                    new GpsPoint(-0.0001, 0.0002)
                }
            };
            var res = GpsSegmentClustering.FindGlobalCommonSegments(list, 2, 100, 20, 5, 100);
            res.Count.ShouldBe(1);
            res[0].Count.ShouldBe(1);
            res[0][0].RepresentativeGpsTrack.First().ShouldBe(new GpsPoint(0, 0));
            res[0][0].RepresentativeGpsTrack.Last().ShouldBe(new GpsPoint(0, 0.0002));
        }

        [Test]
        public void FindGlobalCommonSegments_TwoDisjunctTracks_ReturnsTwoSegments()
        {
            var list = new List<List<GpsPoint>>
            {
                new List<GpsPoint> {new GpsPoint(0, 0), new GpsPoint(0.01, 0.01)},
                new List<GpsPoint> {new GpsPoint(5, 5), new GpsPoint(5.01, 5.01)}
            };
            var res = GpsSegmentClustering.FindGlobalCommonSegments(list, 1);
            res.Count.ShouldBe(2);
            res[0].Count.ShouldBe(1);
            res[0][0].RepresentativeGpsTrack.First().ShouldBe(list[0].First());
            res[0][0].RepresentativeGpsTrack.Last().ShouldBe(list[0].Last());
            res[1].Count.ShouldBe(1);
            res[1][0].RepresentativeGpsTrack.First().ShouldBe(list[1].First());
            res[1][0].RepresentativeGpsTrack.Last().ShouldBe(list[1].Last());
        }

        [Test]
        public void FindLocalCommonSegments_OneTrack_ReturnsIdentity()
        {
            var list = new List<List<GpsPoint>>
            {
                new List<GpsPoint> {new GpsPoint(0, 0), new GpsPoint(0.01, 0.01)}
            };
            var res = GpsSegmentClustering.FindLocalCommonSegments(list, 1);
            res.Count.ShouldBe(1);
            res[0].RepresentativeGpsTrack.First().ShouldBe(list.First().First());
            res[0].RepresentativeGpsTrack.Last().ShouldBe(list.First().Last());
        }

        [Test]
        public void FindLocalCommonSegments_TwoSameTrackWith4Points_ReturnsTwoTrackSegment()
        {
            var list = new List<List<GpsPoint>>
            {
                new List<GpsPoint>
                {
                    new GpsPoint(0, 0),
                    new GpsPoint(0.001, 0.001),
                    new GpsPoint(0.002, 0.002),
                    new GpsPoint(0.003, 0.003)
                },
                new List<GpsPoint>
                {
                    new GpsPoint(0, 0),
                    new GpsPoint(0.001, 0.001),
                    new GpsPoint(0.002, 0.002),
                    new GpsPoint(0.003, 0.003)
                }
            };
            var res = GpsSegmentClustering.FindLocalCommonSegments(list, 1, 20, 20, 5, 500.0);
            res[0].TrackSegments.Count.ShouldBe(2);

            res[0].TrackSegments[0].Indices.Count.ShouldBe(4);
            res[0].TrackSegments[0].First.ShouldBe(0);
            res[0].TrackSegments[0].Last.ShouldBe(3);
            res[0].TrackSegments[0].SegmentFirst.ShouldBe(0);
            res[0].TrackSegments[0].SegmentLast.ShouldBe(1);
            res[0].TrackSegments[0].Id.ShouldBe(0);
            res[0].TrackSegments[0].Length.ShouldBe(471.463927706797, 1e-7);
            res[0].TrackSegments[0].Coverage.ShouldBe(1.0);
            res[0].TrackSegments[0].Common.ShouldBe(1.0);
            res[0].TrackSegments[0].Direction.ShouldBe(3.0);

            res[0].TrackSegments[1].Indices.Count.ShouldBe(4);
            res[0].TrackSegments[1].First.ShouldBe(0);
            res[0].TrackSegments[1].Last.ShouldBe(3);
            res[0].TrackSegments[1].SegmentFirst.ShouldBe(0);
            res[0].TrackSegments[1].SegmentLast.ShouldBe(1);
            res[0].TrackSegments[1].Id.ShouldBe(1);
            res[0].TrackSegments[1].Length.ShouldBe(471.463927706797, 1e-7);
            res[0].TrackSegments[1].Coverage.ShouldBe(1.0);
            res[0].TrackSegments[1].Common.ShouldBe(1.0);
            res[0].TrackSegments[1].Direction.ShouldBe(3.0);
        }

        [Test]
        public void FindTrackClusters_EmptyList_ReturnsEmpty()
        {
            var list = new List<List<GpsPoint>>();
            GpsSegmentClustering.FindTrackClusters(list).Count.ShouldBe(0);
        }

        [Test]
        public void FindTrackClusters_TwoDisjunctTracks_ReturnsTwoClusters()
        {
            var list = new List<List<GpsPoint>>
            {
                new List<GpsPoint> {new GpsPoint(0, 0), new GpsPoint(1, 1)},
                new List<GpsPoint> {new GpsPoint(5, 5), new GpsPoint(6, 6)}
            };
            GpsSegmentClustering.FindTrackClusters(list).Count.ShouldBe(2);
        }
    }
}