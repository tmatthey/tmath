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
using Math.Gps;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests.Gps
{
    [TestFixture]
    public class IntersectionTests
    {
        private readonly GpsTrackExamples _gpsTrackExamples = new GpsTrackExamples();

        [Test]
        public void IntersectionGrid_CrossingPoleAndOnePolePoint_ReturnsIntersecting()
        {
            var gpsTrack1 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 70, Longitude = 90},
                    new GpsPoint {Latitude = 75, Longitude = -90}
                });
            var gpsTrack2 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 90, Longitude = 180}
                });
            Intersection.Grid(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Intersecting);
        }

        [Test]
        public void IntersectionGrid_DifferentTracks_ReturnsIntersecting()
        {
            var gpsTrack1 = new GpsTrack(_gpsTrackExamples.TrackOne());
            var gpsTrack2 = new GpsTrack(_gpsTrackExamples.TrackTwo());
            Intersection.Grid(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Intersecting);
        }

        [Test]
        public void IntersectionGrid_EmptyTracks_ReturnsUndefined()
        {
            var gpsTrack1 = new GpsTrack(new List<GpsPoint>());
            var gpsTrack2 = new GpsTrack(new List<GpsPoint>());
            Intersection.Grid(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Undefined);
        }

        [Test]
        public void IntersectionGrid_OnePolePointAndCrossingPole_ReturnsIntersecting()
        {
            var gpsTrack1 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 90, Longitude = 180}
                });
            var gpsTrack2 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 75, Longitude = 90},
                    new GpsPoint {Latitude = 70, Longitude = -90}
                });
            Intersection.Grid(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Intersecting);
        }

        [Test]
        public void IntersectionGrid_ReferenceTrackEmpty_ReturnsUndefined()
        {
            var gpsTrack1 = new GpsTrack(new List<GpsPoint>());
            var gpsTrack2 = new GpsTrack(new List<GpsPoint> {new GpsPoint {Latitude = 0, Longitude = 180}});
            Intersection.Grid(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Undefined);
        }

        [Test]
        public void IntersectionGrid_SameTrack_ReturnsIntersecting()
        {
            var gpsTrack1 = new GpsTrack(_gpsTrackExamples.TrackOne());
            var gpsTrack2 = new GpsTrack(_gpsTrackExamples.TrackOne());
            TestUtils.StartTimer();
            Intersection.Grid(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Intersecting);
            TestUtils.StopTimer();
        }

        [Test]
        public void IntersectionGrid_SameTrackWithTranslation_ReturnsIntersecting()
        {
            var track = _gpsTrackExamples.TrackOne();
            foreach (var pt in track)
            {
                pt.Longitude += 40.0;
            }

            var gpsTrack1 = new GpsTrack(track);
            var gpsTrack2 = new GpsTrack(_gpsTrackExamples.TrackOne());
            TestUtils.StartTimer();
            Intersection.Grid(gpsTrack1, gpsTrack2, 2000).ShouldBe(Intersection.Result.NotIntersecting);
            TestUtils.StopTimer();
        }

        [Test]
        public void IntersectionGrid_SimilarTrack_ReturnsIntersecting()
        {
            var gpsTrack1 = new GpsTrack(_gpsTrackExamples.TrackOne());
            var gpsTrack2 = new GpsTrack(_gpsTrackExamples.TrackTwo());
            TestUtils.StartTimer();
            Intersection.Grid(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Intersecting);
            TestUtils.StopTimer();
        }

        [Test]
        public void IntersectionGrid_TargetTrackEmpty_ReturnsUndefined()
        {
            var gpsTrack1 = new GpsTrack(new List<GpsPoint> {new GpsPoint {Latitude = 0, Longitude = 180}});
            var gpsTrack2 = new GpsTrack(new List<GpsPoint>());
            Intersection.Grid(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Undefined);
        }

        [Test]
        public void IntersectionGrid_TargetTracksInside_ReturnsIntersecting()
        {
            var gpsTrack1 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0, Longitude = 180},
                    new GpsPoint {Latitude = 1, Longitude = 180}
                });
            var gpsTrack2 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0.01, Longitude = 180},
                    new GpsPoint {Latitude = 0.99, Longitude = 180}
                });
            Intersection.Grid(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Intersecting);
        }

        [Test]
        public void IntersectionGrid_TargetTracksOutside_ReturnsIntersecting()
        {
            var gpsTrack1 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0.01, Longitude = 180},
                    new GpsPoint {Latitude = 0.99, Longitude = 180}
                });
            var gpsTrack2 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0, Longitude = 180},
                    new GpsPoint {Latitude = 1, Longitude = 180}
                });
            Intersection.Grid(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Intersecting);
        }

        [Test]
        public void IntersectionGrid_TracksCloseNotTouching_ReturnsNotIntersecting()
        {
            var gpsTrack1 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0, Longitude = 180},
                    new GpsPoint {Latitude = 1, Longitude = 180},
                    new GpsPoint {Latitude = 0, Longitude = 179},
                    new GpsPoint {Latitude = 1, Longitude = 179}
                });
            var gpsTrack2 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 1.1, Longitude = 180},
                    new GpsPoint {Latitude = 2, Longitude = 180},
                    new GpsPoint {Latitude = 1.1, Longitude = 179},
                    new GpsPoint {Latitude = 2, Longitude = 179}
                });
            Intersection.Grid(gpsTrack1, gpsTrack2)
                .ShouldBe(Intersection.Result.NotIntersecting);
        }

        [Test]
        public void IntersectionGrid_TracksCrossAPoleWithDefaultResolution_ReturnsIntersecting()
        {
            var gpsTrack1 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 89, Longitude = -90},
                    new GpsPoint {Latitude = 89, Longitude = 90}
                });
            var gpsTrack2 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 89, Longitude = 0},
                    new GpsPoint {Latitude = 89, Longitude = 180}
                });
            Intersection.Grid(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Intersecting);
        }

        [Test]
        public void IntersectionGrid_TracksCrossWith180Resolution_ReturnsIntersecting()
        {
            var gpsTrack1 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0, Longitude = 180},
                    new GpsPoint {Latitude = 1, Longitude = 181}
                });
            var gpsTrack2 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0, Longitude = 181},
                    new GpsPoint {Latitude = 1, Longitude = 180}
                });
            Intersection.Grid(gpsTrack1, gpsTrack2, 180).ShouldBe(Intersection.Result.Intersecting);
        }

        [Test]
        public void IntersectionGrid_TracksCrossWithDefaultResolution_ReturnsIntersecting()
        {
            var gpsTrack1 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0, Longitude = 180},
                    new GpsPoint {Latitude = 1, Longitude = 181}
                });
            var gpsTrack2 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0, Longitude = 181},
                    new GpsPoint {Latitude = 1, Longitude = 180}
                });
            Intersection.Grid(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Intersecting);
        }

        [Test]
        public void IntersectionGrid_TracksNotIntersecting_ReturnsNotIntersecting()
        {
            var gpsTrack1 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0, Longitude = 180},
                    new GpsPoint {Latitude = 1, Longitude = 180}
                });
            var gpsTrack2 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 1.5, Longitude = 180},
                    new GpsPoint {Latitude = 2, Longitude = 180}
                });
            Intersection.Grid(gpsTrack1, gpsTrack2)
                .ShouldBe(Intersection.Result.NotIntersecting);
        }

        [Test]
        public void IntersectionGrid_TracksOverlapping_ReturnsIntersecting()
        {
            var gpsTrack1 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0, Longitude = 180},
                    new GpsPoint {Latitude = 0.01, Longitude = 180}
                });
            var gpsTrack2 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0.005, Longitude = 180},
                    new GpsPoint {Latitude = 0.015, Longitude = 180}
                });
            Intersection.Grid(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Intersecting);
        }

        [Test]
        public void IntersectionGrid_TracksTouching_ReturnsIntersecting()
        {
            var gpsTrack1 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0, Longitude = 180},
                    new GpsPoint {Latitude = 1, Longitude = 180}
                });
            var gpsTrack2 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 1, Longitude = 180},
                    new GpsPoint {Latitude = 2, Longitude = 180}
                });
            Intersection.Grid(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Intersecting);
        }

        [Test]
        public void IntersectionGrid_WithOneSamePoint_ReturnsIntersecting()
        {
            var gpsTrack1 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0, Longitude = 180}
                });
            var gpsTrack2 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0, Longitude = 180}
                });
            Intersection.Grid(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Intersecting);
        }

        [Test]
        public void IntersectionGrid_WithSamePole_ReturnsIntersecting()
        {
            var gpsTrack1 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 90, Longitude = 180}
                });
            var gpsTrack2 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 90, Longitude = 180}
                });
            Intersection.Grid(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Intersecting);
        }

        [Test]
        public void IntersectionMinCircle_DifferentTracks_ReturnsIntersecting()
        {
            var gpsTrack1 = new GpsTrack(_gpsTrackExamples.TrackOne());
            var gpsTrack2 = new GpsTrack(_gpsTrackExamples.TrackTwo());
            Intersection.MinCircle(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Intersecting);
        }

        [Test]
        public void IntersectionMinCircle_EmptyTracks_ReturnsUndefined()
        {
            var gpsTrack1 = new GpsTrack(new List<GpsPoint>());
            var gpsTrack2 = new GpsTrack(new List<GpsPoint>());
            Intersection.MinCircle(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Undefined);
        }

        [Test]
        public void IntersectionMinCircle_ReferenceTrackEmpty_ReturnsUndefined()
        {
            var gpsTrack1 = new GpsTrack(new List<GpsPoint>());
            var gpsTrack2 = new GpsTrack(new List<GpsPoint> {new GpsPoint {Latitude = 0, Longitude = 180}});
            Intersection.MinCircle(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Undefined);
        }

        [Test]
        public void IntersectionMinCircle_SameTrack_ReturnsSame()
        {
            var gpsTrack1 = new GpsTrack(_gpsTrackExamples.TrackOne());
            var gpsTrack2 = new GpsTrack(_gpsTrackExamples.TrackOne());
            TestUtils.StartTimer();
            Intersection.MinCircle(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Same);
            TestUtils.StopTimer();
        }

        [Test]
        public void IntersectionMinCircle_TargetTrackEmpty_ReturnsUndefined()
        {
            var gpsTrack1 = new GpsTrack(new List<GpsPoint> {new GpsPoint {Latitude = 0, Longitude = 180}});
            var gpsTrack2 = new GpsTrack(new List<GpsPoint>());
            Intersection.MinCircle(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Undefined);
        }

        [Test]
        public void IntersectionMinCircle_TargetTracksInside_ReturnsInside()
        {
            var gpsTrack1 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0, Longitude = 180},
                    new GpsPoint {Latitude = 1, Longitude = 180}
                });
            var gpsTrack2 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0.01, Longitude = 180},
                    new GpsPoint {Latitude = 0.99, Longitude = 180}
                });
            Intersection.MinCircle(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Inside);
        }

        [Test]
        public void IntersectionMinCircle_TargetTracksOutside_ReturnsOutside()
        {
            var gpsTrack1 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0.01, Longitude = 180},
                    new GpsPoint {Latitude = 0.99, Longitude = 180}
                });
            var gpsTrack2 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0, Longitude = 180},
                    new GpsPoint {Latitude = 1, Longitude = 180}
                });
            Intersection.MinCircle(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Outside);
        }

        [Test]
        public void IntersectionMinCircle_TracksNotIntersecting_ReturnsNotIntersecting()
        {
            var gpsTrack1 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0, Longitude = 180},
                    new GpsPoint {Latitude = 1, Longitude = 180}
                });
            var gpsTrack2 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 1.5, Longitude = 180},
                    new GpsPoint {Latitude = 2, Longitude = 180}
                });
            Intersection.MinCircle(gpsTrack1, gpsTrack2)
                .ShouldBe(Intersection.Result.NotIntersecting);
        }

        [Test]
        public void IntersectionMinCircle_TracksOverlapping_ReturnsIntersecting()
        {
            var gpsTrack1 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0, Longitude = 180},
                    new GpsPoint {Latitude = 1, Longitude = 180}
                });
            var gpsTrack2 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0.01, Longitude = 180},
                    new GpsPoint {Latitude = 2, Longitude = 180}
                });
            Intersection.MinCircle(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Intersecting);
        }

        [Test]
        public void IntersectionMinCircle_TracksTouching_ReturnsIntersecting()
        {
            var gpsTrack1 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0, Longitude = 180},
                    new GpsPoint {Latitude = 1, Longitude = 180}
                });
            var gpsTrack2 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 1, Longitude = 180},
                    new GpsPoint {Latitude = 2, Longitude = 180}
                });
            Intersection.MinCircle(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Intersecting);
        }

        [Test]
        public void IntersectionMinCricle_SimilarTrack_ReturnsIntersecting()
        {
            var gpsTrack1 = new GpsTrack(_gpsTrackExamples.TrackOne());
            var gpsTrack2 = new GpsTrack(_gpsTrackExamples.TrackTwo());
            TestUtils.StartTimer();
            Intersection.MinCircle(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Intersecting);
            TestUtils.StopTimer();
        }

        [Test]
        public void IntersectionRect_DifferentTracks_ReturnsInside()
        {
            var gpsTrack1 = new GpsTrack(_gpsTrackExamples.TrackOne());
            var gpsTrack2 = new GpsTrack(_gpsTrackExamples.TrackTwo());
            Intersection.Rect(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Inside);
        }

        [Test]
        public void IntersectionRect_EmptyTracks_ReturnsUndefined()
        {
            var gpsTrack1 = new GpsTrack(new List<GpsPoint>());
            var gpsTrack2 = new GpsTrack(new List<GpsPoint>());
            Intersection.Rect(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Undefined);
        }

        [Test]
        public void IntersectionRect_ReferenceTrackEmpty_ReturnsUndefined()
        {
            var gpsTrack1 = new GpsTrack(new List<GpsPoint>());
            var gpsTrack2 = new GpsTrack(new List<GpsPoint> {new GpsPoint {Latitude = 0, Longitude = 180}});
            Intersection.Rect(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Undefined);
        }

        [Test]
        public void IntersectionRect_SameTrack_ReturnsSame()
        {
            var gpsTrack1 = new GpsTrack(_gpsTrackExamples.TrackOne());
            var gpsTrack2 = new GpsTrack(_gpsTrackExamples.TrackOne());
            TestUtils.StartTimer();
            Intersection.Rect(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Same);
            TestUtils.StopTimer();
        }

        [Test]
        public void IntersectionRect_SameTrackWithTranslation_ReturnsIntersecting()
        {
            var track = _gpsTrackExamples.TrackOne();
            foreach (var pt in track)
            {
                pt.Longitude += 40.0;
            }

            var gpsTrack1 = new GpsTrack(track);
            var gpsTrack2 = new GpsTrack(_gpsTrackExamples.TrackOne());
            TestUtils.StartTimer();
            Intersection.Rect(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.NotIntersecting);
            TestUtils.StopTimer();
        }

        [Test]
        public void IntersectionRect_SimilarTrack_ReturnsInside()
        {
            var gpsTrack1 = new GpsTrack(_gpsTrackExamples.TrackOne());
            var gpsTrack2 = new GpsTrack(_gpsTrackExamples.TrackTwo());
            TestUtils.StartTimer();
            Intersection.Rect(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Inside);
            TestUtils.StopTimer();
        }

        [Test]
        public void IntersectionRect_TargetTrackEmpty_ReturnsUndefined()
        {
            var gpsTrack1 = new GpsTrack(new List<GpsPoint> {new GpsPoint {Latitude = 0, Longitude = 180}});
            var gpsTrack2 = new GpsTrack(new List<GpsPoint>());
            Intersection.Rect(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Undefined);
        }

        [Test]
        public void IntersectionRect_TargetTracksInside_ReturnsInside()
        {
            var gpsTrack1 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0, Longitude = 180},
                    new GpsPoint {Latitude = 1, Longitude = 180}
                });
            var gpsTrack2 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0.01, Longitude = 180},
                    new GpsPoint {Latitude = 0.99, Longitude = 180}
                });
            Intersection.Rect(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Inside);
        }

        [Test]
        public void IntersectionRect_TargetTracksOutside_ReturnsOutside()
        {
            var gpsTrack1 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0.01, Longitude = 180},
                    new GpsPoint {Latitude = 0.99, Longitude = 180}
                });
            var gpsTrack2 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0, Longitude = 180},
                    new GpsPoint {Latitude = 1, Longitude = 180}
                });
            Intersection.Rect(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Outside);
        }

        [Test]
        public void IntersectionRect_TracksCloseNotTouching_ReturnsNotIntersecting()
        {
            var gpsTrack1 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0, Longitude = 180},
                    new GpsPoint {Latitude = 1, Longitude = 180},
                    new GpsPoint {Latitude = 0, Longitude = 179},
                    new GpsPoint {Latitude = 1, Longitude = 179}
                });
            var gpsTrack2 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 1.01, Longitude = 180},
                    new GpsPoint {Latitude = 2, Longitude = 180},
                    new GpsPoint {Latitude = 1.01, Longitude = 179},
                    new GpsPoint {Latitude = 2, Longitude = 179}
                });
            Intersection.Rect(gpsTrack1, gpsTrack2)
                .ShouldBe(Intersection.Result.NotIntersecting);
        }

        [Test]
        public void IntersectionRect_TracksNotFastIntersecting_ReturnsNotFastIntersecting()
        {
            var gpsTrack1 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0, Longitude = 180},
                    new GpsPoint {Latitude = 1, Longitude = 180}
                });
            var gpsTrack2 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 1.5, Longitude = 180},
                    new GpsPoint {Latitude = 2, Longitude = 180}
                });
            Intersection.Rect(gpsTrack1, gpsTrack2)
                .ShouldBe(Intersection.Result.NotIntersecting);
        }

        [Test]
        public void IntersectionRect_TracksOverlapping_ReturnsIntersecting()
        {
            var gpsTrack1 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0, Longitude = 180},
                    new GpsPoint {Latitude = 1, Longitude = 180}
                });
            var gpsTrack2 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0.01, Longitude = 180},
                    new GpsPoint {Latitude = 2, Longitude = 180}
                });
            Intersection.Rect(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Intersecting);
        }

        [Test]
        public void IntersectionRect_TracksTouching_ReturnsIntersecting()
        {
            var gpsTrack1 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0, Longitude = 180},
                    new GpsPoint {Latitude = 1, Longitude = 180}
                });
            var gpsTrack2 =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 1, Longitude = 180},
                    new GpsPoint {Latitude = 2, Longitude = 180}
                });
            Intersection.Rect(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Intersecting);
        }
    }
}