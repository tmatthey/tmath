﻿/*
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
using Math.Gps;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests.Gps
{
    [TestFixture]
    public class GpsTransformerTests
    {
        [Test]
        public void GpsTransformer_Constructor_TrackEqualCountAsInput()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint {Longitude = 179, Latitude = 1},
                new GpsPoint {Longitude = 179, Latitude = -1},
                new GpsPoint {Longitude = 181, Latitude = -1},
                new GpsPoint {Longitude = 181, Latitude = 1}
            };
            var center = new GpsPoint {Longitude = 180, Latitude = 0};
            var transformed = new GpsTransformer(track, center);
            transformed.Track.Count.ShouldBe(track.Count);
        }

        [Test]
        public void GpsTransformer_ConstructorQudraticTrack_CreatesCorrectTransformedCoordinatesEqualMinMax()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint {Longitude = 179, Latitude = 1},
                new GpsPoint {Longitude = 179, Latitude = -1},
                new GpsPoint {Longitude = 181, Latitude = -1},
                new GpsPoint {Longitude = 181, Latitude = 1}
            };
            var center = new GpsPoint {Longitude = 180, Latitude = 0};
            var transformed = new GpsTransformer(track, center);
            var track2D = transformed.Track;
            track2D[0].X.ShouldBe(transformed.Min.X);
            track2D[0].Y.ShouldBe(transformed.Max.Y);
            track2D[1].X.ShouldBe(transformed.Min.X);
            track2D[1].Y.ShouldBe(transformed.Min.Y);
            track2D[2].X.ShouldBe(transformed.Max.X);
            track2D[2].Y.ShouldBe(transformed.Min.Y);
            track2D[3].X.ShouldBe(transformed.Max.X);
            track2D[3].Y.ShouldBe(transformed.Max.Y);
        }

        [Test]
        public void
            GpsTransformer_ConstructorQudraticTrackOneDegFromOrigin_TransformedCoordinatesDistanceHaveOneDegScale()
        {
            var track = new List<GpsPoint>
            {
                new GpsPoint {Longitude = 179, Latitude = 1},
                new GpsPoint {Longitude = 179, Latitude = -1},
                new GpsPoint {Longitude = 181, Latitude = -1},
                new GpsPoint {Longitude = 181, Latitude = 1}
            };
            var center = new GpsPoint {Longitude = 180, Latitude = 0};
            var transformed = new GpsTransformer(track, center);
            var track2D = transformed.Track;
            track2D[0].X.ShouldBe(-Geodesy.DistanceOneDeg);
            track2D[0].Y.ShouldBe(Geodesy.DistanceOneDeg);
            track2D[1].X.ShouldBe(-Geodesy.DistanceOneDeg);
            track2D[1].Y.ShouldBe(-Geodesy.DistanceOneDeg);
            track2D[2].X.ShouldBe(Geodesy.DistanceOneDeg);
            track2D[2].Y.ShouldBe(-Geodesy.DistanceOneDeg);
            track2D[3].X.ShouldBe(Geodesy.DistanceOneDeg);
            track2D[3].Y.ShouldBe(Geodesy.DistanceOneDeg);
        }
    }
}