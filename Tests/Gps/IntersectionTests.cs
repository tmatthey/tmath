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
using System.Collections.Generic;
using System.Diagnostics;
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
        public void IntersectionGrid_DifferentTracks_ReturnsOverlapping()
        {
            var gpsTrack1 = new GpsTrack(_gpsTrackExamples.TrackOne());
            var gpsTrack2 = new GpsTrack(_gpsTrackExamples.TrackTwo());
            Intersection.Grid(gpsTrack1, gpsTrack2, 1000).ShouldBe(Intersection.Result.Overlapping);
        }

        [Test]
        public void IntersectionGrid_EmptyTracks_ReturnsUndefined()
        {
            var gpsTrack1 = new GpsTrack(new List<GpsPoint>());
            var gpsTrack2 = new GpsTrack(new List<GpsPoint>());
            Intersection.Grid(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Undefined);
        }

        [Test]
        public void IntersectionGrid_ReferenceTrackEmpty_ReturnsUndefined()
        {
            var gpsTrack1 = new GpsTrack(new List<GpsPoint>());
            var gpsTrack2 = new GpsTrack(new List<GpsPoint> {new GpsPoint {Latitude = 0, Longitude = 180}});
            Intersection.Grid(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Undefined);
        }

        [Test]
        public void IntersectionGrid_SameTrack_ReturnsOverlapping()
        {
            var gpsTrack1 = new GpsTrack(_gpsTrackExamples.TrackOne());
            var gpsTrack2 = new GpsTrack(_gpsTrackExamples.TrackOne());
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Intersection.Grid(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Overlapping);
            stopwatch.Stop();
            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
        }

        [Test]
        public void IntersectionGrid_SameTrackWithTranslation_ReturnsOverlapping()
        {
            var track = _gpsTrackExamples.TrackOne();
            foreach (var pt in track)
            {
                pt.Longitude += 40.0;
            }
            var gpsTrack1 = new GpsTrack(track);
            var gpsTrack2 = new GpsTrack(_gpsTrackExamples.TrackOne());
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Intersection.Grid(gpsTrack1, gpsTrack2, 2000).ShouldBe(Intersection.Result.NotIntersecting);
            stopwatch.Stop();
            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
        }

        [Test]
        public void IntersectionGrid_TargetTrackEmpty_ReturnsUndefined()
        {
            var gpsTrack1 = new GpsTrack(new List<GpsPoint> {new GpsPoint {Latitude = 0, Longitude = 180}});
            var gpsTrack2 = new GpsTrack(new List<GpsPoint>());
            Intersection.Grid(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Undefined);
        }

        [Test]
        public void IntersectionGrid_TargetTracksInside_ReturnsOverlapping()
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
            Intersection.Grid(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Overlapping);
        }

        [Test]
        public void IntersectionGrid_TargetTracksOutside_ReturnsOverlapping()
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
            Intersection.Grid(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Overlapping);
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
        public void IntersectionGrid_TracksOverlapping_ReturnsOverlapping()
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
            Intersection.Grid(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Overlapping);
        }

        [Test]
        public void IntersectionGrid_TracksTouching_ReturnsOverlapping()
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
            Intersection.Grid(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Overlapping);
        }

        [Test]
        public void IntersectionMinCircle_DifferentTracks_ReturnsOverlapping()
        {
            var gpsTrack1 = new GpsTrack(_gpsTrackExamples.TrackOne());
            var gpsTrack2 = new GpsTrack(_gpsTrackExamples.TrackTwo());
            Intersection.MinCircle(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Overlapping);
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
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Intersection.MinCircle(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Same);
            stopwatch.Stop();
            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
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
        public void IntersectionMinCircle_TracksOverlapping_ReturnsOverlapping()
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
            Intersection.MinCircle(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Overlapping);
        }

        [Test]
        public void IntersectionMinCircle_TracksTouching_ReturnsOverlapping()
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
            Intersection.MinCircle(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Overlapping);
        }

        [Test]
        public void IntersectionRect_DifferentTracks_ReturnsOverlapping()
        {
            var gpsTrack1 = new GpsTrack(_gpsTrackExamples.TrackOne());
            var gpsTrack2 = new GpsTrack(_gpsTrackExamples.TrackTwo());
            Intersection.Rect(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Overlapping);
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
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Intersection.Rect(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Same);
            stopwatch.Stop();
            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
        }

        [Test]
        public void IntersectionRect_SameTrackWithTranslation_ReturnsOverlapping()
        {
            var track = _gpsTrackExamples.TrackOne();
            foreach (var pt in track)
            {
                pt.Longitude += 40.0;
            }
            var gpsTrack1 = new GpsTrack(track);
            var gpsTrack2 = new GpsTrack(_gpsTrackExamples.TrackOne());
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Intersection.Rect(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.NotIntersecting);
            stopwatch.Stop();
            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
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
        public void IntersectionRect_TracksOverlapping_ReturnsOverlapping()
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
            Intersection.Rect(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Overlapping);
        }

        [Test]
        public void IntersectionRect_TracksTouching_ReturnsOverlapping()
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
            Intersection.Rect(gpsTrack1, gpsTrack2).ShouldBe(Intersection.Result.Overlapping);
        }
    }
}