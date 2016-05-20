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
using Math.Gfx;
using Math.Gps;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests.Gps
{
    public class ClusteringTests
    {
        private readonly GpsTrackExamples _gpsTrackExamples = new GpsTrackExamples();

        [Test]
        public void SignificantPoints_ListNullptr_ReturnsEmptyList()
        {
            Clustering.SignificantPoints(null).Count.ShouldBe(0);
        }

        [Test]
        public void SignificantPoints_ListEmpty_ReturnsEmptyList()
        {
            Clustering.SignificantPoints(new List<Vector2D>()).Count.ShouldBe(0);
        }

        [Test]
        public void SignificantPoints_ListWithOnePoint_ReturnsIndexZero()
        {
            var list = Clustering.SignificantPoints(new List<Vector2D> {Vector2D.E1});
            list.Count.ShouldBe(1);
            list[0].ShouldBe(0);
        }

        [Test]
        public void SignificantPoints_TrackOne_ReturnsList()
        {
            var track = new GpsTrack(_gpsTrackExamples.TrackOne());
            var list = Clustering.SignificantPoints(track.CreateTransformedTrack().Track);
            var heatMap = new HeatMap(HeatMap.CalculateCenter);
            heatMap.Add(track.Track);
            heatMap.Add(list.Select(i => track.Track[i]).ToList());
            var bitmap = heatMap.Normalized(2.5, 0.05, 1.0);
            BitmapFileWriter.PNG("SignificantPoints.png", bitmap);

            list.Count.ShouldBe(55);
        }

        [Test]
        public void SignificantPoints_TrackOneWithUnrestrictedMDLCostAdwantage_ReturnsStartAndEndPoint()
        {
            var track = new GpsTrack(_gpsTrackExamples.TrackOne());
            var list = Clustering.SignificantPoints(track.CreateTransformedTrack().Track, int.MaxValue/10);
            list.Count.ShouldBe(2);
            list[0].ShouldBe(0);
            list[1].ShouldBe(track.Track.Count - 1);
        }

        [Test]
        public void SignificantPoints_Corner_Returns3Points()
        {
            var track = new GpsTrack(new List<GpsPoint>
            {
                new GpsPoint {Latitude = 0, Longitude = 180},
                new GpsPoint {Latitude = 0, Longitude = 180.001},
                new GpsPoint {Latitude = 0, Longitude = 180.002},
                new GpsPoint {Latitude = 0, Longitude = 180.003},
                new GpsPoint {Latitude = 0, Longitude = 180.004},
                new GpsPoint {Latitude = 0, Longitude = 180.005},
                new GpsPoint {Latitude = 0.001, Longitude = 180.005},
                new GpsPoint {Latitude = 0.002, Longitude = 180.005},
                new GpsPoint {Latitude = 0.003, Longitude = 180.005},
                new GpsPoint {Latitude = 0.004, Longitude = 180.005},
                new GpsPoint {Latitude = 0.005, Longitude = 180.005}
            });
            var list = Clustering.SignificantPoints(track.CreateTransformedTrack().Track);
            list.Count.ShouldBe(3);
            list[0].ShouldBe(0);
            list[1].ShouldBe(5);
            list[2].ShouldBe(10);
        }

        [Test]
        public void SignificantPoints_10EqualPoints_ReturnsOneZero()
        {
            var track = new GpsTrack(new List<GpsPoint>
            {
                new GpsPoint {Latitude = 0, Longitude = 180},
                new GpsPoint {Latitude = 0, Longitude = 180},
                new GpsPoint {Latitude = 0, Longitude = 180},
                new GpsPoint {Latitude = 0, Longitude = 180},
                new GpsPoint {Latitude = 0, Longitude = 180},
                new GpsPoint {Latitude = 0, Longitude = 180},
                new GpsPoint {Latitude = 0, Longitude = 180},
                new GpsPoint {Latitude = 0, Longitude = 180},
                new GpsPoint {Latitude = 0, Longitude = 180},
                new GpsPoint {Latitude = 0, Longitude = 180}
            });
            var list = Clustering.SignificantPoints(track.CreateTransformedTrack().Track);
            list.Count.ShouldBe(1);
            list[0].ShouldBe(0);
        }
    }
}