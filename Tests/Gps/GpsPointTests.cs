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
using Math.Gps;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests.Gps
{
    [TestFixture]
    public class GpsPointTests
    {
        private readonly GpsTrackExamples _gpsTrackExamples = new GpsTrackExamples();

        [TestCase(0.0)]
        [TestCase(0.1)]
        [TestCase(0.2)]
        public void InterpolateFraction_ReturnsGpsWithCorrectAngle(double f)
        {
            var a = new GpsPoint {Longitude = 91.0, Latitude = 11.0, Elevation = 11.0};
            var b = new GpsPoint {Longitude = 93.0, Latitude = 12.0, Elevation = 12.0};
            var g = a.Interpolate(b, f);
            Vector3D x = g;
            Vector3D x0 = a;
            Vector3D x1 = b;
            var angle = x0.Angle(x1);
            g.Elevation.ShouldBe(a.Elevation*(1.0 - f) + b.Elevation*f, 1e-10);
            x0.Angle(x).ShouldBe(angle*f, 1e-10);
        }

        [TestCase(false, false)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(true, true)]
        public void OpEqual_NullPointer_ReturnsExpected(bool a, bool b)
        {
            var x = a ? new GpsPoint() : null;
            var y = b ? new GpsPoint() : null;
            (x == y).ShouldBe(a == b);
        }

        [TestCase(false, false)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(true, true)]
        public void OpNotEqual_NullPointer_ReturnsExpected(bool a, bool b)
        {
            var x = a ? new GpsPoint() : null;
            var y = b ? new GpsPoint() : null;
            (x != y).ShouldBe(a != b);
        }

        [TestCase(10.0, 20.0, 180, 80, 200)]
        [TestCase(10.0, -20.0, 180, 80, 160)]
        [TestCase(-10.0, 20.0, 180, 100, 200)]
        [TestCase(-10.0, -20.0, 180, 100, 160)]
        [TestCase(-10.5, -20.5, 180, 100, 159)]
        [TestCase(0.0, -0.1, 180, 90, 179)]
        [TestCase(0.0, 0.1, 180, 90, 180)]
        [TestCase(-0.1, 0.0, 180, 90, 180)]
        [TestCase(0.0, 0.0, 180, 90, 180)]
        [TestCase(0.1, 0.0, 180, 89, 180)]
        [TestCase(89.9, 50.0, 180, 0, 0)]
        [TestCase(89.9, -50.0, 180, 0, 0)]
        [TestCase(-89.9, 50.0, 180, 179, 0)]
        [TestCase(-89.9, -50.0, 180, 179, 0)]
        public void GridIndex(double latitude, double longitude, int resolution, int expectedI, int expectedJ)
        {
            var p = new GpsPoint {Latitude = latitude, Longitude = longitude};
            int i, j;
            p.GridIndex(resolution, out i, out j);
            i.ShouldBe(expectedI);
            j.ShouldBe(expectedJ);
        }

        [TestCase(10.0, 20.0, 180, 80, 200)]
        [TestCase(10.0, -20.0, 180, 80, 160)]
        [TestCase(-10.0, 20.0, 180, 100, 200)]
        [TestCase(-10.0, -20.0, 180, 100, 160)]
        [TestCase(-10.5, -20.5, 180, 100, 159)]
        [TestCase(0.0, -0.1, 180, 90, 179)]
        [TestCase(0.0, 0.1, 180, 90, 180)]
        [TestCase(-0.1, 0.0, 180, 90, 180)]
        [TestCase(0.0, 0.0, 180, 90, 180)]
        [TestCase(0.1, 0.0, 180, 89, 180)]
        [TestCase(89.9, 50.0, 180, 0, 0)]
        [TestCase(89.9, -50.0, 180, 0, 0)]
        [TestCase(-89.9, 50.0, 180, 179, 0)]
        [TestCase(-89.9, -50.0, 180, 179, 0)]
        public void GridLinearIndex(double latitude, double longitude, int resolution, int expectedI, int expectedJ)
        {
            var p = new GpsPoint {Latitude = latitude, Longitude = longitude};
            var expectedN = expectedJ*resolution + expectedI;
            p.GridLinearIndex(resolution).ShouldBe(expectedN);
        }

        [TestCase(-1)]
        [TestCase(3)]
        public void ArrayOp_WithOutOfBoundIndex_Throws(int i)
        {
            var v = new GpsPoint();
            Should.Throw<IndexOutOfRangeException>(() => { var a = v[i]; });
        }

        [Test]
        public void Array_ReturnsExpected()
        {
            const double x = 0.1;
            const double y = 0.2;
            const double z = 0.3;
            var v = new GpsPoint(x, y, z);
            var c = v.Array;
            c.Length.ShouldBe(v.Dimensions);
            c[0].ShouldBe(x);
            c[1].ShouldBe(y);
            c[2].ShouldBe(z);
        }

        [Test]
        public void ArrayOp_ReturnsExpected()
        {
            const double x = 0.1;
            const double y = 0.2;
            const double z = 0.3;
            var v = new GpsPoint(x, y, z);
            v[0].ShouldBe(x);
            v[1].ShouldBe(y);
            v[2].ShouldBe(z);
        }

        [Test]
        public void Dimension_Is3()
        {
            var v = new GpsPoint();
            v.Dimensions.ShouldBe(3);
        }

        [Test]
        public void Distance_ElevationEarthRadius_ReturnsHaversineDistance()
        {
            var a = new GpsPoint {Latitude = 0, Longitude = 0, Elevation = Geodesy.EarthRadius};
            var b = new GpsPoint {Latitude = 0, Longitude = 1, Elevation = Geodesy.EarthRadius};
            a.Distance(b).ShouldBe(Geodesy.Distance.Haversine(a.Latitude, a.Longitude, b.Latitude, b.Longitude)*2.0);
        }

        [Test]
        public void Distance_ElevationFromEarthRadius_ReturnsHaversineDistance()
        {
            var a = new GpsPoint {Latitude = 0, Longitude = 0, Elevation = Geodesy.EarthRadius};
            var b = new GpsPoint {Latitude = 0, Longitude = 1, Elevation = 0};
            a.Distance(b)
                .ShouldBe(
                    new Vector2D(Geodesy.Distance.Haversine(a.Latitude, a.Longitude, b.Latitude, b.Longitude),
                        Geodesy.EarthRadius).Norm());
        }

        [Test]
        public void Distance_ElevationToEarthRadius_ReturnsHaversineDistance()
        {
            var a = new GpsPoint {Latitude = 0, Longitude = 0, Elevation = 0.0};
            var b = new GpsPoint {Latitude = 0, Longitude = 1, Elevation = Geodesy.EarthRadius};
            a.Distance(b)
                .ShouldBe(
                    new Vector2D(Geodesy.Distance.Haversine(a.Latitude, a.Longitude, b.Latitude, b.Longitude),
                        Geodesy.EarthRadius).Norm());
        }

        [Test]
        public void Distance_NoElevation_ReturnsHaversineDistance()
        {
            var a = new GpsPoint {Latitude = 0, Longitude = 0};
            var b = new GpsPoint {Latitude = 0, Longitude = 1};
            a.Distance(b).ShouldBe(Geodesy.Distance.Haversine(a.Latitude, a.Longitude, b.Latitude, b.Longitude));
        }

        [Test]
        public void Equals_WithItself_ReturnsTrue()
        {
            var p = new GpsPoint(12, 14, 17);
            p.Equals(p).ShouldBe(true);
        }

        [Test]
        public void Equals_WithNull_ReturnsFalse()
        {
            var p = new GpsPoint(12, 14);
            GpsPoint q = null;
            p.Equals(q).ShouldBe(false);
        }

        [Test]
        public void Equals_WithSameVecotr_ReturnsTrue()
        {
            var p = new GpsPoint(12, 14, 17);
            var q = new GpsPoint(12, 14, 17);
            p.Equals(q).ShouldBe(true);
        }

        [Test]
        public void GpsPointNorthPole_ReturnsCorrectVector3D()
        {
            var g = new GpsPoint {Latitude = 90, Longitude = 0, Elevation = 0.0};
            Vector3D v = g;
            v.X.ShouldBe(0.0);
            v.Y.ShouldBe(0.0);
            v.Z.ShouldBe(Geodesy.EarthRadius);
        }

        [Test]
        public void GpsPointSouthPole_ReturnsCorrectVector3D()
        {
            var g = new GpsPoint(-90, 0);
            Vector3D v = g;
            v.X.ShouldBe(0.0);
            v.Y.ShouldBe(0.0);
            v.Z.ShouldBe(-Geodesy.EarthRadius);
        }

        [Test]
        public void HaversineDistance_WithTrackOne_ReturnsExpected()
        {
            var gpsTrack = new GpsTrack(_gpsTrackExamples.TrackOne());
            var d = 0.0;
            for (var i = 0; i + 1 < gpsTrack.Track.Count; i++)
                d += gpsTrack.Track[i].HaversineDistance(gpsTrack.Track[i + 1]);
            d.ShouldBe(8522.9, 1e-1);
        }

        [Test]
        public void HaversineDistance_WithTrackTwo_ReturnsExpected()
        {
            var gpsTrack = new GpsTrack(_gpsTrackExamples.TrackTwo());
            var d = 0.0;
            for (var i = 0; i + 1 < gpsTrack.Track.Count; i++)
                d += gpsTrack.Track[i].HaversineDistance(gpsTrack.Track[i + 1]);
            d.ShouldBe(9523.0, 1e-1);
        }

        [Test]
        public void InterpolateHalfFraction_ReturnsInterpolated()
        {
            var a = new GpsPoint {Longitude = 91.0, Latitude = 1.0, Elevation = 11.0};
            var b = new GpsPoint {Longitude = 93.0, Latitude = -1.0, Elevation = 12.0};
            var g = a.Interpolate(b, 0.5);
            g.Latitude.ShouldBe(0.0, 1e-10);
            g.Longitude.ShouldBe(92, 1e-10);
            g.Elevation.ShouldBe(11.5, 1e-10);
        }

        [Test]
        public void InterpolateOneFraction_ReturnsSecond()
        {
            var a = new GpsPoint {Longitude = 91.0, Latitude = 2.0, Elevation = 11.0};
            var b = new GpsPoint {Longitude = 93.0, Latitude = 4.0, Elevation = 12.0};
            var g = a.Interpolate(b, 1.0);
            g.Latitude.ShouldBe(b.Latitude, 1e-10);
            g.Longitude.ShouldBe(b.Longitude, 1e-10);
            g.Elevation.ShouldBe(b.Elevation, 1e-10);
        }

        [Test]
        public void InterpolateZeroFraction_ReturnsFirst()
        {
            var a = new GpsPoint {Longitude = 91.0, Latitude = 2.0, Elevation = 11.0};
            var b = new GpsPoint {Longitude = 93.0, Latitude = 4.0, Elevation = 12.0};
            var g = a.Interpolate(b, 0.0);
            g.Latitude.ShouldBe(a.Latitude, 1e-10);
            g.Longitude.ShouldBe(a.Longitude, 1e-10);
            g.Elevation.ShouldBe(a.Elevation, 1e-10);
        }

        [Test]
        public void IsEqual_WithCenterOfEarth_ReturnsTrue()
        {
            var p = new GpsPoint(12, 14, -Geodesy.EarthRadius);
            var q = new GpsPoint(12, 14.1, -Geodesy.EarthRadius);
            p.IsEqual(q).ShouldBe(true);
        }

        [Test]
        public void OpEqual_WithDiffrentRef_ReturnsTrue()
        {
            var p = new GpsPoint(12, 14, 17.1);
            var q = new GpsPoint(12, 14, 17.1);
            (p == q).ShouldBe(true);
        }

        [Test]
        public void OpNotEqual_NotEqualElevation_ReturnsTrue()
        {
            var p = new GpsPoint(12, 14, 17);
            var q = new GpsPoint(12, 14, 17.1);
            (p != q).ShouldBe(true);
        }

        [Test]
        public void OpNotEqual_NotEqualLat_ReturnsTrue()
        {
            var p = new GpsPoint(12, 14, 17.1);
            var q = new GpsPoint(12, 14.1, 17.1);
            (p != q).ShouldBe(true);
        }

        [Test]
        public void OpNotEqual_NotEqualLong_ReturnsTrue()
        {
            var p = new GpsPoint(12, 14, 17.1);
            var q = new GpsPoint(12, 14.1, 17.1);
            (p != q).ShouldBe(true);
        }

        [Test]
        public void Polar3DGpsPointPolar3DConversion_ReturnsId()
        {
            var p = new Polar3D(1, 1);
            GpsPoint g = p;
            Polar3D q = g;
            p.Theta.ShouldBe(q.Theta);
            p.Phi.ShouldBe(q.Phi);
            p.R.ShouldBe(q.R);
        }

        [Test]
        public void Vector3DGpsPointVector3DConversion_ReturnsId()
        {
            var v = new Vector3D(1, 1, 1);
            GpsPoint g = v;
            Vector3D w = g;
            v.X.ShouldBe(w.X, 1e-7);
            v.Y.ShouldBe(w.Y, 1e-7);
            v.Z.ShouldBe(w.Z, 1e-7);
        }
    }
}