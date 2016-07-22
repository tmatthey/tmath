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
using Math.Gps;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests.Gps
{
    [TestFixture]
    public class TransformerTests
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
            var transformed = new Transformer(track, center);
            transformed.Track.Count.ShouldBe(track.Count);
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
            var transformed = new Transformer(track, center);
            transformed.Track.Count.ShouldBe(track.Count);
            transformed.Track[0].X.ShouldBe(-transformed.Track[2].X, 1e-7);
            transformed.Track[0].Y.ShouldBe(transformed.Track[2].Y, 1e-7);
            transformed.Track[1].X.ShouldBe(0.0, 1e-7);
            transformed.Track[1].Y.ShouldBe(0.0, 1e-7);
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
            var transformed = new Transformer(track, center);
            transformed.Track.Count.ShouldBe(track.Count);
            transformed.Track[0].X.ShouldBe(0.0, 1e-7);
            transformed.Track[0].Y.ShouldBe(-Geodesy.DistanceOneDeg*0.5);
            transformed.Track[1].X.ShouldBe(0.0, 1e-7);
            transformed.Track[1].Y.ShouldBe(Geodesy.DistanceOneDeg*0.5);
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
            var transformed = new Transformer(track, center);
            var track2D = transformed.Track;
            track2D[0].X.ShouldBe(transformed.Min.X, 1e-7);
            track2D[0].Y.ShouldBe(transformed.Max.Y, 1e-7);
            track2D[1].X.ShouldBe(transformed.Min.X, 1e-7);
            track2D[1].Y.ShouldBe(transformed.Min.Y, 1e-7);
            track2D[2].X.ShouldBe(transformed.Max.X, 1e-7);
            track2D[2].Y.ShouldBe(transformed.Min.Y, 1e-7);
            track2D[3].X.ShouldBe(transformed.Max.X, 1e-7);
            track2D[3].Y.ShouldBe(transformed.Max.Y, 1e-7);
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
            var transformed = new Transformer(track, center);
            var track2D = transformed.Track;
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
            var transformed = new Transformer(track, center);
            var distance = transformed.Distance;
            distance.Count.ShouldBe(track.Count);
            distance.Last().ShouldBe(4*2*Geodesy.DistanceOneDeg, 1e-3);
        }

        [Test]
        public void Euclidean_WithTrackOne_ReturnsExpected()
        {
            var gpsTrack = new GpsTrack(_gpsTrackExamples.TrackOne());
            var track = gpsTrack.CreateTransformedTrack();
            var d = 0.0;
            for (var i = 0; i + 1 < track.Track.Count; i++)
                d += track.Track[i].EuclideanNorm(track.Track[i + 1]);
            d.ShouldBe(8522.9, 1e-1);
        }

        [Test]
        public void Euclidean_WithTrackTwo_ReturnsExpected()
        {
            var gpsTrack = new GpsTrack(_gpsTrackExamples.TrackTwo());
            var track = gpsTrack.CreateTransformedTrack();
            var d = 0.0;
            for (var i = 0; i + 1 < track.Track.Count; i++)
                d += track.Track[i].EuclideanNorm(track.Track[i + 1]);
            d.ShouldBe(9523.0, 1e-1);
        }
    }
}