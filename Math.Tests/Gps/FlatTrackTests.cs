/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2017 Thierry Matthey
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
using Math.Gps;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests.Gps
{
    [TestFixture]
    public class FlatTrackTests
    {
        private readonly GpsTrackExamples _gpsTrackExamples = new GpsTrackExamples();

        [Test]
        public void Constructor_TrackEqualCountAsInput()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint {Longitude = 179, Latitude = 1},
                new GpsPoint {Longitude = 179, Latitude = -1},
                new GpsPoint {Longitude = 181, Latitude = -1},
                new GpsPoint {Longitude = 181, Latitude = 1}
            };
            var center = new GpsPoint {Longitude = 180, Latitude = 0};
            var flatTrack = new FlatTrack(track, center);
            flatTrack.Track.Count.ShouldBe(track.Count);
        }

        [Test]
        public void Constructor_TwoPointsSameLatitude_MovedToEquator()
        {
            var center = new GpsPoint {Longitude = 178, Latitude = 30};
            var track = new List<GpsPoint>
            {
                new GpsPoint {Longitude = 179, Latitude = 30},
                center,
                new GpsPoint {Longitude = 177, Latitude = 30}
            };
            var flatTrack = new FlatTrack(track, center);
            flatTrack.Track.Count.ShouldBe(track.Count);
            flatTrack.Track[0].X.ShouldBe(-flatTrack.Track[2].X, 1e-7);
            flatTrack.Track[0].Y.ShouldBe(flatTrack.Track[2].Y, 1e-7);
            flatTrack.Track[1].X.ShouldBe(0.0, 1e-7);
            flatTrack.Track[1].Y.ShouldBe(0.0, 1e-7);
        }

        [Test]
        public void Constructor_TwoPointsSameMeridian_KeepsNorth()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint {Longitude = 51, Latitude = 60},
                new GpsPoint {Longitude = 51, Latitude = 61}
            };
            var center = new GpsPoint {Longitude = 51, Latitude = 60.5};
            var flatTrack = new FlatTrack(track, center);
            flatTrack.Track.Count.ShouldBe(track.Count);
            flatTrack.Track[0].X.ShouldBe(0.0, 1e-7);
            flatTrack.Track[0].Y.ShouldBe(-Geodesy.DistanceOneDeg*0.5);
            flatTrack.Track[1].X.ShouldBe(0.0, 1e-7);
            flatTrack.Track[1].Y.ShouldBe(Geodesy.DistanceOneDeg*0.5);
        }

        [Test]
        public void ConstructorFlatTrack_CreatesSameFlatTrack()
        {
            var track = new List<Vector2D>
            {
                new Vector2D(0, 0),
                new Vector2D(0, 1),
                new Vector2D(1, 1),
                new Vector2D(1, 0)
            };
            var flatTrack = new FlatTrack(track);
            var track2D = flatTrack.Track;
            track2D[0].X.ShouldBe(flatTrack.Min.X, 1e-7);
            track2D[0].Y.ShouldBe(flatTrack.Min.Y, 1e-7);
            track2D[1].X.ShouldBe(flatTrack.Min.X, 1e-7);
            track2D[1].Y.ShouldBe(flatTrack.Max.Y, 1e-7);
            track2D[2].X.ShouldBe(flatTrack.Max.X, 1e-7);
            track2D[2].Y.ShouldBe(flatTrack.Max.Y, 1e-7);
            track2D[3].X.ShouldBe(flatTrack.Max.X, 1e-7);
            track2D[3].Y.ShouldBe(flatTrack.Min.Y, 1e-7);
        }

        [Test]
        public void ConstructorQudraticTrack_CreatesCorrectTransformedCoordinatesEqualMinMax()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint {Longitude = 179, Latitude = 1},
                new GpsPoint {Longitude = 179, Latitude = -1},
                new GpsPoint {Longitude = 181, Latitude = -1},
                new GpsPoint {Longitude = 181, Latitude = 1}
            };
            var center = new GpsPoint {Longitude = 180, Latitude = 0};
            var flatTrack = new FlatTrack(track, center);
            var track2D = flatTrack.Track;
            track2D[0].X.ShouldBe(flatTrack.Min.X, 1e-7);
            track2D[0].Y.ShouldBe(flatTrack.Max.Y, 1e-7);
            track2D[1].X.ShouldBe(flatTrack.Min.X, 1e-7);
            track2D[1].Y.ShouldBe(flatTrack.Min.Y, 1e-7);
            track2D[2].X.ShouldBe(flatTrack.Max.X, 1e-7);
            track2D[2].Y.ShouldBe(flatTrack.Min.Y, 1e-7);
            track2D[3].X.ShouldBe(flatTrack.Max.X, 1e-7);
            track2D[3].Y.ShouldBe(flatTrack.Max.Y, 1e-7);
        }

        [Test]
        public void ConstructorQudraticTrackOneDegFromOrigin_TransformedCoordinatesDistanceHaveOneDegScale()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint {Longitude = 179, Latitude = 1},
                new GpsPoint {Longitude = 179, Latitude = -1},
                new GpsPoint {Longitude = 181, Latitude = -1},
                new GpsPoint {Longitude = 181, Latitude = 1}
            };
            var center = new GpsPoint {Longitude = 180, Latitude = 0};
            var flatTrack = new FlatTrack(track, center);
            var track2D = flatTrack.Track;
            track2D[0].X.ShouldBe(-Geodesy.DistanceOneDeg, 1e-7);
            track2D[0].Y.ShouldBe(Geodesy.DistanceOneDeg, 1e-7);
            track2D[1].X.ShouldBe(-Geodesy.DistanceOneDeg, 1e-7);
            track2D[1].Y.ShouldBe(-Geodesy.DistanceOneDeg, 1e-7);
            track2D[2].X.ShouldBe(Geodesy.DistanceOneDeg, 1e-7);
            track2D[2].Y.ShouldBe(-Geodesy.DistanceOneDeg, 1e-7);
            track2D[3].X.ShouldBe(Geodesy.DistanceOneDeg, 1e-7);
            track2D[3].Y.ShouldBe(Geodesy.DistanceOneDeg, 1e-7);
        }

        [Test]
        public void Euclidean_OfQudraticOneDeg_8OneDegDistance()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint {Longitude = 179, Latitude = 1},
                new GpsPoint {Longitude = 179, Latitude = -1},
                new GpsPoint {Longitude = 181, Latitude = -1},
                new GpsPoint {Longitude = 181, Latitude = 1},
                new GpsPoint {Longitude = 179, Latitude = 1}
            };
            var center = new GpsPoint {Longitude = 180, Latitude = 0};
            var flatTrack = new FlatTrack(track, center);
            var distance = flatTrack.Distance;
            distance.Count.ShouldBe(track.Count);
            distance.Last().ShouldBe(4*2*Geodesy.DistanceOneDeg, 1e-3);
        }

        [Test]
        public void Euclidean_WithTrackOne_ReturnsExpected()
        {
            var gpsTrack = new GpsTrack(_gpsTrackExamples.TrackOne());
            var track = gpsTrack.CreateFlatTrack();
            var d = 0.0;
            for (var i = 0; i + 1 < track.Track.Count; i++)
                d += track.Track[i].EuclideanNorm(track.Track[i + 1]);
            d.ShouldBe(8522.9, 1e-1);
        }

        [Test]
        public void Euclidean_WithTrackTwo_ReturnsExpected()
        {
            var gpsTrack = new GpsTrack(_gpsTrackExamples.TrackTwo());
            var track = gpsTrack.CreateFlatTrack();
            var d = 0.0;
            for (var i = 0; i + 1 < track.Track.Count; i++)
                d += track.Track[i].EuclideanNorm(track.Track[i + 1]);
            d.ShouldBe(9523.0, 1e-1);
        }

        [Test]
        public void TotalDistance_ReturnsDistanceLast()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint {Longitude = 179, Latitude = 1},
                new GpsPoint {Longitude = 179, Latitude = -1},
                new GpsPoint {Longitude = 181, Latitude = -1},
                new GpsPoint {Longitude = 181, Latitude = 1},
                new GpsPoint {Longitude = 179, Latitude = 1}
            };
            var center = new GpsPoint {Longitude = 180, Latitude = 0};
            var flatTrack = new FlatTrack(track, center);
            var total = flatTrack.TotalDistance;
            total.ShouldBe(flatTrack.Distance.Last());
        }
    }
}