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
using Math.Gps;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests.Gps
{
    [TestFixture]
    public class GpsTrackTests
    {
        private readonly GpsTrackExamples _gpsTrackExamples = new GpsTrackExamples();

        [TestCase(1.0)]
        [TestCase(0.5)]
        [TestCase(0.1)]
        [TestCase(0.01)]
        public void MinCircleAngle_ReturnsExpected(double f)
        {
            var gpsTrack =
                new GpsTrack(new List<GpsPoint>
                {
                    new GpsPoint {Latitude = 0, Longitude = 180},
                    new GpsPoint {Latitude = 90*f, Longitude = 180}
                });
            gpsTrack.MinCircleAngle.ShouldBe(System.Math.PI*0.25*f, 1e-13);
        }

        [Test]
        public void Center_ReturnsExpected()
        {
            var gpsTrack = new GpsTrack(_gpsTrackExamples.TrackOne());
            var sum = new Vector3D();
            var d = 0.0;
            foreach (var g in gpsTrack.Track)
            {
                Vector3D v = g;
                sum += v.Normalized();
                d += v.Norm();
            }
            sum.Normalize();
            sum *= d/_gpsTrackExamples.TrackOne().Count;
            sum.X.ShouldBe(gpsTrack.Center.X, 1e-7);
            sum.Y.ShouldBe(gpsTrack.Center.Y, 1e-7);
            sum.Z.ShouldBe(gpsTrack.Center.Z, 1e-7);
        }

        [Test]
        public void Constructor_CorrectTrack()
        {
            var gpsTrack = new GpsTrack(_gpsTrackExamples.TrackOne());
            gpsTrack.Track.Count.ShouldBe(_gpsTrackExamples.TrackOne().Count);
        }

        [Test]
        public void EmptyTrack_CenterAndRotation_ReturnsNaN()
        {
            var gpsTrack = new GpsTrack(new List<GpsPoint>());
            gpsTrack.Center.X.ShouldBe(double.NaN);
            gpsTrack.Center.Y.ShouldBe(double.NaN);
            gpsTrack.Center.Z.ShouldBe(double.NaN);
            gpsTrack.RotationAxis.X.ShouldBe(double.NaN);
            gpsTrack.RotationAxis.Y.ShouldBe(double.NaN);
            gpsTrack.RotationAxis.Z.ShouldBe(double.NaN);
            gpsTrack.RotationAngle.ShouldBe(double.NaN);
        }

        [Test]
        public void EmptyTrack_MinCircleCenter_ReturnsNaN()
        {
            var gpsTrack = new GpsTrack(new List<GpsPoint>());
            var c = gpsTrack.MinCircleCenter;
            c.X.ShouldBe(double.NaN);
            c.Y.ShouldBe(double.NaN);
            c.Z.ShouldBe(double.NaN);
            gpsTrack.MinCircleAngle.ShouldBe(double.NaN);
        }

        [Test]
        public void MinCircleAngle_WithOnePoint_ReturnsExpected()
        {
            var gpsTrack = new GpsTrack(new List<GpsPoint> {new GpsPoint {Latitude = 0, Longitude = 180}});
            gpsTrack.MinCircleAngle.ShouldBe(0.0);
        }

        [Test]
        public void MinCircleCenter_SimilarAsCenter()
        {
            var gpsTrack = new GpsTrack(_gpsTrackExamples.TrackOne());
            var c = gpsTrack.Center;
            var d = gpsTrack.MinCircleCenter;
            var e = d.EuclideanNorm(c);
            e.ShouldBeLessThan(500.0);
            var cl = c.Norm();
            var dl = d.Norm();
            var f = System.Math.Abs(cl - dl);
            f.ShouldBeLessThan(1e-8);
        }

        [Test]
        public void RotationAngle_ReturnsExpected()
        {
            var gpsTrack = new GpsTrack(new List<GpsPoint> {new GpsPoint {Latitude = 0, Longitude = 0}});
            gpsTrack.RotationAngle.ShouldBe(0.0);
        }

        [Test]
        public void RotationAxis_ReturnsExpected()
        {
            var gpsTrack = new GpsTrack(new List<GpsPoint> {new GpsPoint {Latitude = 0, Longitude = 0}});
            gpsTrack.RotationAxis.ShouldBe(new Vector3D(0, 0, 0));
        }
    }
}