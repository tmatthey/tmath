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
using Math.Tests.Gps;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests
{
    [TestFixture]
    public class GeometryTests
    {
        private readonly GpsTrackExamples _gpsTrackExamples = new GpsTrackExamples();

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        [TestCase(16)]
        [TestCase(17)]
        [TestCase(18)]
        [TestCase(19)]
        [TestCase(20)]
        public void MinCircle_4PlusNDiffrentPoints_returnsExpected(int n)
        {
            var points = new List<Vector2D>
            {
                new Vector2D(0, 0),
                new Vector2D(2, 0),
                new Vector2D(1, 1),
                new Vector2D(1, 0)
            };
            for (var i = 4; i < 4 + n; i++)
                points.Add(new Vector2D(1.0 + i/(n + 4.0)*0.99, 0.0));
            var c = Geometry.MinCircle(points);
            var expected = new Circle2D(new Vector2D(1, 0), 1);
            c.ShouldBe(expected);

            points.Reverse();
            c = Geometry.MinCircle(points);
            c.ShouldBe(expected);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        [TestCase(16)]
        [TestCase(17)]
        [TestCase(18)]
        [TestCase(19)]
        [TestCase(20)]
        public void MinCircle_3PlusNSpiralDiffrentPoints_returnsExpected(int n)
        {
            var points = new List<Vector2D> {new Vector2D(0, 0), new Vector2D(2, 0), new Vector2D(1, 1)};
            var center = new Vector2D(1.0, 0.0);
            for (var i = 0; i < n; i++)
            {
                var fraction = i/(double) n;
                var angle = fraction*System.Math.PI*2.0;
                points.Add(center + new Vector2D(System.Math.Sin(angle), System.Math.Cos(angle))*fraction);
            }
            var c = Geometry.MinCircle(points);
            var expected = new Circle2D(new Vector2D(1, 0), 1);
            c.ShouldBe(expected);

            points.Reverse();
            c = Geometry.MinCircle(points);
            c.ShouldBe(expected);
        }

        //

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        [TestCase(16)]
        [TestCase(17)]
        [TestCase(18)]
        [TestCase(19)]
        [TestCase(20)]
        public void MinCircleOnSphere_4PlusNDiffrentPoints_returnsExpected(int n)
        {
            var points = new List<Vector3D>
            {
                MakeVectorOnSphere(0, 0),
                MakeVectorOnSphere(2, 0),
                MakeVectorOnSphere(1, 1),
                MakeVectorOnSphere(1, 0)
            };
            for (var i = 4; i < 4 + n; i++)
                points.Add(MakeVectorOnSphere(1.0 + i/(n + 4.0)*0.99, 0.0));
            var c = Geometry.MinCircleOnSphere(points);
            c.Center.X.ShouldBe(1.0);
            c.Center.Y.ShouldBe(0.0);

            points.Reverse();
            c = Geometry.MinCircleOnSphere(points);
            c.Center.X.ShouldBe(1.0);
            c.Center.Y.ShouldBe(0.0);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        [TestCase(16)]
        [TestCase(17)]
        [TestCase(18)]
        [TestCase(19)]
        [TestCase(20)]
        [TestCase(2000)]
        public void MinCircleOnSphere_3PlusNSpiralDiffrentPoints_returnsExpected(int n)
        {
            var points = new List<Vector3D>
            {
                MakeVectorOnSphere(-1, 0),
                MakeVectorOnSphere(0, 1),
                MakeVectorOnSphere(1, 0)
            };
            var center = MakeVectorOnSphere(0.0, 0.0);
            for (var i = 0; i < n; i++)
            {
                var fraction = i/(double) n;
                var angle = fraction*System.Math.PI*2.0;
                points.Add(MakeVectorOnSphere(center.X + System.Math.Sin(angle)*fraction*.9,
                    center.Y + System.Math.Cos(angle)*fraction*.9));
            }
            var c = Geometry.MinCircleOnSphere(points);
            c.Center.X.ShouldBe(center.X);
            c.Center.Y.ShouldBe(center.Y);
            c.Radius.ShouldBe(1.0);

            points.Reverse();
            c = Geometry.MinCircleOnSphere(points);
            c.Center.X.ShouldBe(center.X);
            c.Center.Y.ShouldBe(center.Y);
            c.Radius.ShouldBe(1.0);
        }

        [TestCase(0.0, 1.1, true)]
        [TestCase(0.0, 1.0, true)]
        [TestCase(0.0, 0.9, true)]
        [TestCase(1.0, 1.1, true)]
        [TestCase(1.0, 1.0, true)]
        [TestCase(1.0, 0.9, true)]
        [TestCase(1.001, 1.1, false)]
        [TestCase(1.001, 1.0, false)]
        [TestCase(1.001, 0.9, false)]
        [TestCase(-1.001, 1.1, false)]
        [TestCase(-1.001, 1.0, false)]
        [TestCase(-1.001, 0.9, false)]
        public void CircleLineIntersect_returnsExpected(double x, double f, bool expected)
        {
            var center = new Vector3D(1, 0, 1);
            var a = new Vector3D(center.X, center.Y, 0.0);
            var b = new Vector3D(center.X + x*f, center.Y, f);
            var c = new Circle3D(center, Vector3D.E3, 1);
            Geometry.CircleLineIntersect(c, a, b).ShouldBe(expected);
        }

        private Vector3D MakeVectorOnSphere(double x, double y)
        {
            var r = 7;
            var z = System.Math.Sqrt(r*r - x*x - y*y);
            return new Vector3D(x, y, z);
        }

        [TestCase(0, 1, 1)]
        [TestCase(0, 2, 2)]
        [TestCase(-34, 1, 1)]
        [TestCase(-34, 2, 2)]
        [TestCase(0, 0, 0)]
        public void PerpendicularDistance_ReturnsExpected(double x, double y, double l)
        {
            var a = new Vector2D(1, 0);
            var b = new Vector2D(2, 0);
            var p = new Vector2D(x, y);
            Geometry.PerpendicularDistance(a, b, p).ShouldBe(l);
        }

        [TestCase(0, 1, -1)]
        [TestCase(0, 2, -1)]
        [TestCase(3, 1, 2)]
        [TestCase(3, 2, 2)]
        [TestCase(-34, 1, -35)]
        [TestCase(-34, 2, -35)]
        [TestCase(0, 0, -1)]
        public void PerpendicularParameter_ReturnsExpected(double x, double y, double l)
        {
            var a = new Vector2D(1, 0);
            var b = new Vector2D(2, 0);
            var p = new Vector2D(x, y);
            Geometry.PerpendicularParameter(a, b, p).ShouldBe(l);
        }

        [TestCase(1, 1, 1)]
        [TestCase(1, 2, 2)]
        [TestCase(1.5, 1, 1)]
        [TestCase(1.5, 2, 2)]
        [TestCase(2, 1, 1)]
        [TestCase(2, 2, 2)]
        [TestCase(1, -1, 1)]
        [TestCase(1, -2, 2)]
        [TestCase(1.5, -1, 1)]
        [TestCase(1.5, -2, 2)]
        [TestCase(2, -1, 1)]
        [TestCase(2, -2, 2)]
        [TestCase(0, 1, 1.4142135623731)]
        [TestCase(-1, 2, 2.82842712474619)]
        [TestCase(0, 0, 1)]
        [TestCase(3, 0, 1)]
        public void PerpendicularSegmentDistance_ReturnsExpected(double x, double y, double l)
        {
            var a = new Vector2D(1, 0);
            var b = new Vector2D(2, 0);
            var p = new Vector2D(x, y);
            Geometry.PerpendicularSegmentDistance(a, b, p).ShouldBe(l, 1e-13);
        }

        [TestCase(0, 1, 0)]
        [TestCase(0, 2, 0)]
        [TestCase(1, 1, 0)]
        [TestCase(1.12, 2, 0.12)]
        [TestCase(2, 1, 1)]
        [TestCase(3, 1, 1)]
        [TestCase(3, 2, 1)]
        public void PerpendicularSegmentParameter_ReturnsExpected(double x, double y, double l)
        {
            var a = new Vector2D(1, 0);
            var b = new Vector2D(2, 0);
            var p = new Vector2D(x, y);
            Geometry.PerpendicularSegmentParameter(a, b, p).ShouldBe(l, 1e-13);
        }

        [Test]
        public void CircleLineIntersect_coplanar_returnsFalse()
        {
            var c = new Circle3D(new Vector3D(1, 0, 1), Vector3D.E3, 1);
            Geometry.CircleLineIntersect(c, -Vector3D.E2, Vector3D.E2).ShouldBe(false);
        }

        [Test]
        public void ConvexHullJarvismarch_FiveGpsTracks()
        {
            var rawTracks = new List<List<GpsPoint>>();
            rawTracks.Add(_gpsTrackExamples.TrackOne().ToList());
            rawTracks.Add(_gpsTrackExamples.TrackTwo().ToList());
            rawTracks.Add(_gpsTrackExamples.TrackThree().ToList());
            rawTracks.Add(_gpsTrackExamples.TrackFour().ToList());
            rawTracks.Add(_gpsTrackExamples.TrackFive().ToList());
            var gpsTracks = new List<GpsTrack>();
            var center = new Vector3D();
            foreach (var gpsTrack in rawTracks.Select(track => new GpsTrack(track)))
            {
                center += gpsTrack.Center;
                gpsTracks.Add(gpsTrack);
            }
            center /= gpsTracks.Count;
            var points = new List<Vector2D>();
            foreach (var track in gpsTracks)
            {
                points.AddRange(track.CreateTransformedTrack(center).Track);
            }

            TestUtils.StartTimer();
            var result = Geometry.ConvexHullJarvismarch(points);
            TestUtils.StopTimer();
            result.Count.ShouldBe(34);
        }

        [Test]
        public void ConvexHullJarvismarch_Sample1()
        {
            var points = new List<Vector2D>
            {
                new Vector2D(4.4, 14),
                new Vector2D(6.7, 15.25),
                new Vector2D(6.9, 12.8),
                new Vector2D(2.1, 11.1),
                new Vector2D(9.5, 14.9),
                new Vector2D(13.2, 11.9),
                new Vector2D(10.3, 12.3),
                new Vector2D(6.8, 9.5),
                new Vector2D(3.3, 7.7),
                new Vector2D(0.6, 5.1),
                new Vector2D(5.3, 2.4),
                new Vector2D(8.45, 4.7),
                new Vector2D(11.5, 9.6),
                new Vector2D(13.8, 7.3),
                new Vector2D(12.9, 3.1),
                new Vector2D(11, 1.1)
            };

            var expected = new List<Vector2D>
            {
                new Vector2D(11, 1.1),
                new Vector2D(12.9, 3.1),
                new Vector2D(13.8, 7.3),
                new Vector2D(13.2, 11.9),
                new Vector2D(9.5, 14.9),
                new Vector2D(6.7, 15.25),
                new Vector2D(4.4, 14),
                new Vector2D(2.1, 11.1),
                new Vector2D(0.6, 5.1),
                new Vector2D(5.3, 2.4)
            };

            var result = Geometry.ConvexHullJarvismarch(points);
            result.Count.ShouldBe(expected.Count);
            for (var i = 0; i < result.Count; i++)
                result[i].ShouldBe(expected[i]);
        }

        [Test]
        public void ConvexHullJarvismarch_Sample2()
        {
            var points = new List<Vector2D>
            {
                new Vector2D(1, 0),
                new Vector2D(1, 1),
                new Vector2D(1, -1),
                new Vector2D(0.68957, 0.283647),
                new Vector2D(0.909487, 0.644276),
                new Vector2D(0.0361877, 0.803816),
                new Vector2D(0.583004, 0.91555),
                new Vector2D(-0.748169, 0.210483),
                new Vector2D(-0.553528, -0.967036),
                new Vector2D(0.316709, -0.153861),
                new Vector2D(-0.79267, 0.585945),
                new Vector2D(-0.700164, -0.750994),
                new Vector2D(0.452273, -0.604434),
                new Vector2D(-0.79134, -0.249902),
                new Vector2D(-0.594918, -0.397574),
                new Vector2D(-0.547371, -0.434041),
                new Vector2D(0.958132, -0.499614),
                new Vector2D(0.039941, 0.0990732),
                new Vector2D(-0.891471, -0.464943),
                new Vector2D(0.513187, -0.457062),
                new Vector2D(-0.930053, 0.60341),
                new Vector2D(0.656995, 0.854205)
            };

            var result = Geometry.ConvexHullJarvismarch(points);
            result.Count.ShouldBe(8);
        }

        [Test]
        public void ConvexHullJarvismarch_Sample3()
        {
            var points = new List<Vector2D>
            {
                new Vector2D(0.3215348546593775, 0.03629583077160248),
                new Vector2D(0.02402358131857918, -0.2356728797179394),
                new Vector2D(0.04590851212470659, -0.4156409924995536),
                new Vector2D(0.3218384001607433, 0.1379850698988746),
                new Vector2D(0.11506479756447, -0.1059521474930943),
                new Vector2D(0.2622539999543261, -0.29702873322836),
                new Vector2D(-0.161920957418085, -0.4055339716426413),
                new Vector2D(0.1905378631228002, 0.3698601009043493),
                new Vector2D(0.2387090918968516, -0.01629827079949742),
                new Vector2D(0.07495888748668034, -0.1659825110491202),
                new Vector2D(0.3319341836794598, -0.1821814101954749),
                new Vector2D(0.07703635755650362, -0.2499430638271785),
                new Vector2D(0.2069242999022122, -0.2232970760420869),
                new Vector2D(0.04604079532068295, -0.1923573186549892),
                new Vector2D(0.05054295812784038, 0.4754929463150845),
                new Vector2D(-0.3900589168910486, 0.2797829520700341),
                new Vector2D(0.3120693385713448, -0.0506329867529059),
                new Vector2D(0.01138812723698857, 0.4002504701728471),
                new Vector2D(0.009645149586391732, 0.1060251100976254),
                new Vector2D(-0.03597933197019559, 0.2953639456959105),
                new Vector2D(0.1818290866742182, 0.001454397571696298),
                new Vector2D(0.444056063372694, 0.2502497166863175),
                new Vector2D(-0.05301752458607545, -0.06553921621808712),
                new Vector2D(0.4823896228171788, -0.4776170002088109),
                new Vector2D(-0.3089226845734964, -0.06356112199235814),
                new Vector2D(-0.271780741188471, 0.1810810595574612),
                new Vector2D(0.4293626522918815, 0.2980897964891882),
                new Vector2D(-0.004796652127799228, 0.382663812844701),
                new Vector2D(0.430695573269106, -0.2995073500084759),
                new Vector2D(0.1799668387323309, -0.2973467472915973),
                new Vector2D(0.4932166845474547, 0.4928094162538735),
                new Vector2D(-0.3521487911717489, 0.4352656197131292),
                new Vector2D(-0.4907368011686362, 0.1865826865533206),
                new Vector2D(-0.1047924716070224, -0.247073392148198)
            };

            var result = Geometry.ConvexHullJarvismarch(points);
            result.Count.ShouldBe(6);
        }

        [Test]
        public void ConvexHullJarvismarch_TrackOne_returnExpected()
        {
            var gpsTrack = new GpsTrack(_gpsTrackExamples.TrackOne());
            var transform = gpsTrack.CreateTransformedTrack();
            TestUtils.StartTimer();
            var result = Geometry.ConvexHullJarvismarch(transform.Track);
            result.Count.ShouldBeLessThan(gpsTrack.Track.Count);
            TestUtils.StopTimer();
        }

        [Test]
        public void ConvexHullJarvismarch_WithCollinearPoints_returnsExpected()
        {
            var points = new List<Vector2D>
            {
                new Vector2D(0, 0),
                new Vector2D(0, 0.25),
                new Vector2D(0, 0.5),
                new Vector2D(0, 1),
                new Vector2D(0.5, 1.5),
                new Vector2D(0.25, 1.25),
                new Vector2D(1, 2),
                new Vector2D(1, 0)
            };
            var expected = new List<Vector2D>
            {
                new Vector2D(0, 0),
                new Vector2D(1, 0),
                new Vector2D(1, 2),
                new Vector2D(0, 1)
            };

            var result = Geometry.ConvexHullJarvismarch(points);

            result.Count.ShouldBe(expected.Count);
            for (var i = 0; i < result.Count; i++)
                result[i].ShouldBe(expected[i]);
        }

        [Test]
        public void ConvexHullJarvismarch_WithEmptyList_returnsEmpty()
        {
            var points = new List<Vector2D>();
            var expected = new List<Vector2D>();

            var result = Geometry.ConvexHullJarvismarch(points);

            result.Count.ShouldBe(expected.Count);
        }

        [Test]
        public void ConvexHullJarvismarch_WithFourPointsOnLine_returnsTwoPoints()
        {
            var points = new List<Vector2D>
            {
                new Vector2D(1, 0),
                new Vector2D(2, 0),
                new Vector2D(3, 0),
                new Vector2D(4, 0)
            };
            var expected = new List<Vector2D> {new Vector2D(1, 0), new Vector2D(4, 0)};

            var result = Geometry.ConvexHullJarvismarch(points);

            result.Count.ShouldBe(expected.Count);
            for (var i = 0; i < result.Count; i++)
                result[i].ShouldBe(expected[i]);
        }

        [Test]
        public void ConvexHullJarvismarch_WithFourSamePoint_returnsOnePoint()
        {
            var points = new List<Vector2D>
            {
                new Vector2D(4.4, 14),
                new Vector2D(4.4, 14),
                new Vector2D(4.4, 14),
                new Vector2D(4.4, 14)
            };
            var expected = new List<Vector2D> {new Vector2D(4.4, 14)};

            var result = Geometry.ConvexHullJarvismarch(points);

            result.Count.ShouldBe(expected.Count);
            for (var i = 0; i < result.Count; i++)
                result[i].ShouldBe(expected[i]);
        }

        [Test]
        public void ConvexHullJarvismarch_WithOnePoint_returnsOnePoint()
        {
            var points = new List<Vector2D> {new Vector2D(4.4, 14)};
            var expected = new List<Vector2D> {new Vector2D(4.4, 14)};

            var result = Geometry.ConvexHullJarvismarch(points);

            result.Count.ShouldBe(expected.Count);
            for (var i = 0; i < result.Count; i++)
                result[i].ShouldBe(expected[i]);
        }

        [Test]
        public void ConvexHullJarvismarch_WithTwoSamePoint_returnsOnePoint()
        {
            var points = new List<Vector2D> {new Vector2D(4.4, 14), new Vector2D(4.4, 14)};
            var expected = new List<Vector2D> {new Vector2D(4.4, 14)};

            var result = Geometry.ConvexHullJarvismarch(points);

            result.Count.ShouldBe(expected.Count);
            for (var i = 0; i < result.Count; i++)
                result[i].ShouldBe(expected[i]);
        }

        [Test]
        public void ConvexHullMonotoneChain_FiveGpsTracks()
        {
            var rawTracks = new List<List<GpsPoint>>();
            rawTracks.Add(_gpsTrackExamples.TrackOne().ToList());
            rawTracks.Add(_gpsTrackExamples.TrackTwo().ToList());
            rawTracks.Add(_gpsTrackExamples.TrackThree().ToList());
            rawTracks.Add(_gpsTrackExamples.TrackFour().ToList());
            rawTracks.Add(_gpsTrackExamples.TrackFive().ToList());
            var gpsTracks = new List<GpsTrack>();
            var center = new Vector3D();
            foreach (var gpsTrack in rawTracks.Select(track => new GpsTrack(track)))
            {
                center += gpsTrack.Center;
                gpsTracks.Add(gpsTrack);
            }
            center /= gpsTracks.Count;
            var points = new List<Vector2D>();
            foreach (var track in gpsTracks)
            {
                points.AddRange(track.CreateTransformedTrack(center).Track);
            }

            TestUtils.StartTimer();
            var result = Geometry.ConvexHullMonotoneChain(points);
            TestUtils.StopTimer();
            result.Count.ShouldBe(34);
        }

        [Test]
        public void ConvexHullMonotoneChain_Sample1()
        {
            var points = new List<Vector2D>
            {
                new Vector2D(4.4, 14),
                new Vector2D(6.7, 15.25),
                new Vector2D(6.9, 12.8),
                new Vector2D(2.1, 11.1),
                new Vector2D(9.5, 14.9),
                new Vector2D(13.2, 11.9),
                new Vector2D(10.3, 12.3),
                new Vector2D(6.8, 9.5),
                new Vector2D(3.3, 7.7),
                new Vector2D(0.6, 5.1),
                new Vector2D(5.3, 2.4),
                new Vector2D(8.45, 4.7),
                new Vector2D(11.5, 9.6),
                new Vector2D(13.8, 7.3),
                new Vector2D(12.9, 3.1),
                new Vector2D(11, 1.1)
            };

            var expected = new List<Vector2D>
            {
                new Vector2D(0.6, 5.1),
                new Vector2D(5.3, 2.4),
                new Vector2D(11, 1.1),
                new Vector2D(12.9, 3.1),
                new Vector2D(13.8, 7.3),
                new Vector2D(13.2, 11.9),
                new Vector2D(9.5, 14.9),
                new Vector2D(6.7, 15.25),
                new Vector2D(4.4, 14),
                new Vector2D(2.1, 11.1)
            };

            var result = Geometry.ConvexHullMonotoneChain(points);
            result.Count.ShouldBe(expected.Count);
            for (var i = 0; i < result.Count; i++)
                result[i].ShouldBe(expected[i]);
        }

        [Test]
        public void ConvexHullMonotoneChain_Sample2()
        {
            var points = new List<Vector2D>
            {
                new Vector2D(1, 0),
                new Vector2D(1, 1),
                new Vector2D(1, -1),
                new Vector2D(0.68957, 0.283647),
                new Vector2D(0.909487, 0.644276),
                new Vector2D(0.0361877, 0.803816),
                new Vector2D(0.583004, 0.91555),
                new Vector2D(-0.748169, 0.210483),
                new Vector2D(-0.553528, -0.967036),
                new Vector2D(0.316709, -0.153861),
                new Vector2D(-0.79267, 0.585945),
                new Vector2D(-0.700164, -0.750994),
                new Vector2D(0.452273, -0.604434),
                new Vector2D(-0.79134, -0.249902),
                new Vector2D(-0.594918, -0.397574),
                new Vector2D(-0.547371, -0.434041),
                new Vector2D(0.958132, -0.499614),
                new Vector2D(0.039941, 0.0990732),
                new Vector2D(-0.891471, -0.464943),
                new Vector2D(0.513187, -0.457062),
                new Vector2D(-0.930053, 0.60341),
                new Vector2D(0.656995, 0.854205)
            };

            var result = Geometry.ConvexHullMonotoneChain(points);
            result.Count.ShouldBe(8);
        }

        [Test]
        public void ConvexHullMonotoneChain_Sample3()
        {
            var points = new List<Vector2D>
            {
                new Vector2D(0.3215348546593775, 0.03629583077160248),
                new Vector2D(0.02402358131857918, -0.2356728797179394),
                new Vector2D(0.04590851212470659, -0.4156409924995536),
                new Vector2D(0.3218384001607433, 0.1379850698988746),
                new Vector2D(0.11506479756447, -0.1059521474930943),
                new Vector2D(0.2622539999543261, -0.29702873322836),
                new Vector2D(-0.161920957418085, -0.4055339716426413),
                new Vector2D(0.1905378631228002, 0.3698601009043493),
                new Vector2D(0.2387090918968516, -0.01629827079949742),
                new Vector2D(0.07495888748668034, -0.1659825110491202),
                new Vector2D(0.3319341836794598, -0.1821814101954749),
                new Vector2D(0.07703635755650362, -0.2499430638271785),
                new Vector2D(0.2069242999022122, -0.2232970760420869),
                new Vector2D(0.04604079532068295, -0.1923573186549892),
                new Vector2D(0.05054295812784038, 0.4754929463150845),
                new Vector2D(-0.3900589168910486, 0.2797829520700341),
                new Vector2D(0.3120693385713448, -0.0506329867529059),
                new Vector2D(0.01138812723698857, 0.4002504701728471),
                new Vector2D(0.009645149586391732, 0.1060251100976254),
                new Vector2D(-0.03597933197019559, 0.2953639456959105),
                new Vector2D(0.1818290866742182, 0.001454397571696298),
                new Vector2D(0.444056063372694, 0.2502497166863175),
                new Vector2D(-0.05301752458607545, -0.06553921621808712),
                new Vector2D(0.4823896228171788, -0.4776170002088109),
                new Vector2D(-0.3089226845734964, -0.06356112199235814),
                new Vector2D(-0.271780741188471, 0.1810810595574612),
                new Vector2D(0.4293626522918815, 0.2980897964891882),
                new Vector2D(-0.004796652127799228, 0.382663812844701),
                new Vector2D(0.430695573269106, -0.2995073500084759),
                new Vector2D(0.1799668387323309, -0.2973467472915973),
                new Vector2D(0.4932166845474547, 0.4928094162538735),
                new Vector2D(-0.3521487911717489, 0.4352656197131292),
                new Vector2D(-0.4907368011686362, 0.1865826865533206),
                new Vector2D(-0.1047924716070224, -0.247073392148198)
            };

            var result = Geometry.ConvexHullMonotoneChain(points);
            result.Count.ShouldBe(6);
        }

        [Test]
        public void ConvexHullMonotoneChain_TrackOne_returnExpected()
        {
            var gpsTrack = new GpsTrack(_gpsTrackExamples.TrackOne());
            var transform = gpsTrack.CreateTransformedTrack();
            TestUtils.StartTimer();
            var result = Geometry.ConvexHullMonotoneChain(transform.Track);
            result.Count.ShouldBeLessThan(gpsTrack.Track.Count);
            TestUtils.StopTimer();
        }

        [Test]
        public void ConvexHullMonotoneChain_WithCollinearPoints_returnsExpected()
        {
            var points = new List<Vector2D>
            {
                new Vector2D(0, 0),
                new Vector2D(0, 0.25),
                new Vector2D(0, 0.5),
                new Vector2D(0, 1),
                new Vector2D(0.5, 1.5),
                new Vector2D(0.25, 1.25),
                new Vector2D(1, 2),
                new Vector2D(1, 0)
            };
            var expected = new List<Vector2D>
            {
                new Vector2D(0, 0),
                new Vector2D(1, 0),
                new Vector2D(1, 2),
                new Vector2D(0, 1)
            };

            var result = Geometry.ConvexHullMonotoneChain(points);

            result.Count.ShouldBe(expected.Count);
            for (var i = 0; i < result.Count; i++)
                result[i].ShouldBe(expected[i]);
        }

        [Test]
        public void ConvexHullMonotoneChain_WithEmptyList_returnsEmpty()
        {
            var points = new List<Vector2D>();
            var expected = new List<Vector2D>();

            var result = Geometry.ConvexHullMonotoneChain(points);

            result.Count.ShouldBe(expected.Count);
        }

        [Test]
        public void ConvexHullMonotoneChain_WithFourPointsOnLine_returnsTwoPoints()
        {
            var points = new List<Vector2D>
            {
                new Vector2D(1, 0),
                new Vector2D(2, 0),
                new Vector2D(3, 0),
                new Vector2D(4, 0)
            };
            var expected = new List<Vector2D> {new Vector2D(1, 0), new Vector2D(4, 0)};

            var result = Geometry.ConvexHullMonotoneChain(points);

            result.Count.ShouldBe(expected.Count);
            for (var i = 0; i < result.Count; i++)
                result[i].ShouldBe(expected[i]);
        }

        [Test]
        public void ConvexHullMonotoneChain_WithFourSamePoint_returnsOnePoint()
        {
            var points = new List<Vector2D>
            {
                new Vector2D(4.4, 14),
                new Vector2D(4.4, 14),
                new Vector2D(4.4, 14),
                new Vector2D(4.4, 14)
            };
            var expected = new List<Vector2D> {new Vector2D(4.4, 14)};

            var result = Geometry.ConvexHullMonotoneChain(points);

            result.Count.ShouldBe(expected.Count);
            for (var i = 0; i < result.Count; i++)
                result[i].ShouldBe(expected[i]);
        }

        [Test]
        public void ConvexHullMonotoneChain_WithOnePoint_returnsOnePoint()
        {
            var points = new List<Vector2D> {new Vector2D(4.4, 14)};
            var expected = new List<Vector2D> {new Vector2D(4.4, 14)};

            var result = Geometry.ConvexHullMonotoneChain(points);

            result.Count.ShouldBe(expected.Count);
            for (var i = 0; i < result.Count; i++)
                result[i].ShouldBe(expected[i]);
        }

        [Test]
        public void ConvexHullMonotoneChain_WithTwoSamePoint_returnsOnePoint()
        {
            var points = new List<Vector2D> {new Vector2D(4.4, 14), new Vector2D(4.4, 14)};
            var expected = new List<Vector2D> {new Vector2D(4.4, 14)};

            var result = Geometry.ConvexHullMonotoneChain(points);

            result.Count.ShouldBe(expected.Count);
            for (var i = 0; i < result.Count; i++)
                result[i].ShouldBe(expected[i]);
        }

        [Test]
        public void MinCircle_EmptyList_returnsNaNCircle()
        {
            var points = new List<Vector2D>();
            var c = Geometry.MinCircle(points);
            c.Center.X.ShouldBe(double.NaN);
            c.Center.Y.ShouldBe(double.NaN);
            c.Radius.ShouldBe(double.NaN);
        }

        [Test]
        public void MinCircle_FourSamePoints_returnsPointRadiusZero()
        {
            var points = new List<Vector2D>
            {
                new Vector2D(1, 2),
                new Vector2D(1, 2),
                new Vector2D(1, 2),
                new Vector2D(1, 2)
            };
            var c = Geometry.MinCircle(points);
            c.Center.X.ShouldBe(1);
            c.Center.Y.ShouldBe(2);
            c.Radius.ShouldBe(0);
        }

        [Test]
        public void MinCircle_OnePoints_returnsPointRadiusZero()
        {
            var points = new List<Vector2D> {new Vector2D(1, 2)};
            var c = Geometry.MinCircle(points);
            c.Center.X.ShouldBe(1);
            c.Center.Y.ShouldBe(2);
            c.Radius.ShouldBe(0);
        }

        [Test]
        public void MinCircle_ThreeDiffrentPoints_returnsMidpoint()
        {
            var points = new List<Vector2D>
            {
                new Vector2D(1, 2),
                new Vector2D(3, 4),
                new Vector2D(5, 4),
                new Vector2D(1, 2)
            };
            var c = Geometry.MinCircle(points);
            var expected = Circle2D.Create(new Vector2D(1, 2), new Vector2D(3, 4), new Vector2D(5, 4));
            c.ShouldBe(expected);
        }

        [Test]
        public void MinCircle_TwoDiffrentPoints_returnsMidpoint()
        {
            var points = new List<Vector2D>
            {
                new Vector2D(1, 2),
                new Vector2D(3, 4),
                new Vector2D(3, 4),
                new Vector2D(1, 2)
            };
            var c = Geometry.MinCircle(points);
            c.Center.X.ShouldBe(2);
            c.Center.Y.ShouldBe(3);
            c.Radius.ShouldBe(System.Math.Sqrt(2.0));
        }

        [Test]
        public void MinCircle_WithFiveTracks_returnsExpected()
        {
            var rawTracks = new List<List<GpsPoint>>
            {
                _gpsTrackExamples.TrackOne().ToList(),
                _gpsTrackExamples.TrackTwo().ToList(),
                _gpsTrackExamples.TrackThree().ToList(),
                _gpsTrackExamples.TrackFour().ToList(),
                _gpsTrackExamples.TrackFive().ToList()
            };
            var gpsTracks = new List<GpsTrack>();
            var center = new Vector3D();
            foreach (var gpsTrack in rawTracks.Select(track => new GpsTrack(track)))
            {
                center += gpsTrack.Center;
                gpsTracks.Add(gpsTrack);
            }
            center /= gpsTracks.Count;
            var points = new List<Vector2D>();
            foreach (var track in gpsTracks)
            {
                points.AddRange(track.CreateTransformedTrack(center).Track);
            }

            TestUtils.StartTimer();
            var c = Geometry.MinCircle(points);
            TestUtils.StopTimer();
            c.Radius.ShouldBe(1243.50116557008, 1e-8);
        }

        [Test]
        public void MinCircleOnSphere_EmptyList_returnsNaNCircle()
        {
            var points = new List<Vector3D>();
            var c = Geometry.MinCircleOnSphere(points);
            c.Center.X.ShouldBe(double.NaN);
            c.Center.Y.ShouldBe(double.NaN);
            c.Radius.ShouldBe(double.NaN);
        }

        [Test]
        public void MinCircleOnSphere_FiveDiffrentPoints_returnsCorrectCircle()
        {
            var points = new List<Vector3D>
            {
                MakeVectorOnSphere(3, 3),
                MakeVectorOnSphere(-3, 3),
                MakeVectorOnSphere(0, 0),
                MakeVectorOnSphere(3, -3),
                MakeVectorOnSphere(-3, 3)
            };
            var c = Geometry.MinCircleOnSphere(points);
            var expected = Circle3D.Create(MakeVectorOnSphere(3, 3), MakeVectorOnSphere(3, -3),
                MakeVectorOnSphere(-3, -3));
            c.ShouldBe(expected);
        }

        [Test]
        public void MinCircleOnSphere_FiveGpsTracks()
        {
            var allGps = _gpsTrackExamples.TrackOne().ToList();
            allGps.AddRange(_gpsTrackExamples.TrackTwo().ToList());
            allGps.AddRange(_gpsTrackExamples.TrackThree().ToList());
            allGps.AddRange(_gpsTrackExamples.TrackFour().ToList());
            allGps.AddRange(_gpsTrackExamples.TrackFive().ToList());
            var gps = new GpsTrack(allGps);
            var points = new List<Vector3D>();
            foreach (var gpsPoint in gps.Track)
            {
                Polar3D p = gpsPoint;
                p.R = 1.0;
                points.Add(p);
            }
            var c = Geometry.MinCircleOnSphere(points);
            var c0 = c.Center.Normalized();
            var c1 = gps.Center.Normalized();
            var d = c0.Angle(c1)/System.Math.PI*Geodesy.EarthRadius;
            d.ShouldBeLessThan(c.Radius*Geodesy.EarthRadius);

            var n = 0;
            foreach (var p in points)
            {
                Geometry.CircleLineIntersect(c, Vector3D.Zero, p).ShouldBe(true);
                var l = c.Center.Distance(p);
                c.Radius.ShouldBeGreaterThanOrEqualTo(l);
                if (Comparison.IsEqual(c.Radius, l))
                    n++;
            }
            n.ShouldBeGreaterThan(1);
        }

        [Test]
        public void MinCircleOnSphere_FourSamePoints_returnsPointRadiusZero()
        {
            var points = new List<Vector3D>
            {
                MakeVectorOnSphere(1, 2),
                MakeVectorOnSphere(1, 2),
                MakeVectorOnSphere(1, 2),
                MakeVectorOnSphere(1, 2)
            };
            var c = Geometry.MinCircleOnSphere(points);
            c.Center.X.ShouldBe(1);
            c.Center.Y.ShouldBe(2);
            c.Radius.ShouldBe(0);
        }

        [Test]
        public void MinCircleOnSphere_OnePoints_returnsPointRadiusZero()
        {
            var points = new List<Vector3D> {MakeVectorOnSphere(1, 2)};
            var c = Geometry.MinCircleOnSphere(points);
            c.Center.X.ShouldBe(1);
            c.Center.Y.ShouldBe(2);
            c.Radius.ShouldBe(0);
        }

        [Test]
        public void MinCircleOnSphere_ThreeDiffrentPoints_returnsMidpoint()
        {
            var points = new List<Vector3D>
            {
                MakeVectorOnSphere(1, 2),
                MakeVectorOnSphere(3, 4),
                MakeVectorOnSphere(5, 4),
                MakeVectorOnSphere(1, 2)
            };
            var c = Geometry.MinCircleOnSphere(points);
            var expected = Circle3D.Create(MakeVectorOnSphere(1, 2), MakeVectorOnSphere(3, 4), MakeVectorOnSphere(5, 4));
            c.ShouldBe(expected);
        }

        [Test]
        public void MinCircleOnSphere_TwoDiffrentPoints_returnsMidpoint()
        {
            var a = MakeVectorOnSphere(1, 2);
            var b = MakeVectorOnSphere(3, 4);
            var points = new List<Vector3D>
            {
                a,
                b,
                b,
                a
            };
            var c = Geometry.MinCircleOnSphere(points);
            var center = (a + b)/2.0;
            c.Center.ShouldBe(center);
            c.Normal.ShouldBe(center);
            c.Radius.ShouldBe(a.Distance(b)/2.0);
        }

        [Test]
        public void TrajectoryHausdorffDistance_ParallelSameSegment_returnsExpected()
        {
            var a = new Vector2D(0, 0);
            var b = new Vector2D(3, 0);
            var c = new Vector2D(0, 2);
            var d = new Vector2D(3, 2);
            var dist = Geometry.TrajectoryHausdorffDistance(a, b, c, d, 1, 1, 1);
            dist.ShouldBe((2.0*2.0 + 2.0*2.0)/(2.0 + 2.0));
        }

        [Test]
        public void TrajectoryHausdorffDistance_ParallelSameSegmentOppositeDirection_returnsExpected()
        {
            var a = new Vector2D(0, 0);
            var b = new Vector2D(3, 0);
            var c = new Vector2D(3, 2);
            var d = new Vector2D(0, 2);
            var dist = Geometry.TrajectoryHausdorffDistance(a, b, c, d, 1, 1, 1);
            dist.ShouldBe((2.0*2.0 + 2.0*2.0)/(2.0 + 2.0) + 3.0);
        }

        [Test]
        public void TrajectoryHausdorffDistance_ParallelSegmentBig_returnsExpected()
        {
            var a = new Vector2D(-5, 2);
            var b = new Vector2D(4, 2);
            var c = new Vector2D(0, 0);
            var d = new Vector2D(3, 0);
            var dist = Geometry.TrajectoryHausdorffDistance(a, b, c, d, 1, 1, 1);
            dist.ShouldBe((2.0*2.0 + 2.0*2.0)/(2.0 + 2.0) + 1.0, 1e-12);
        }

        [Test]
        public void TrajectoryHausdorffDistance_ParallelSegmentBigOppositeDirection_returnsExpected()
        {
            var a = new Vector2D(4, 2);
            var b = new Vector2D(-5, 2);
            var c = new Vector2D(0, 0);
            var d = new Vector2D(3, 0);
            var dist = Geometry.TrajectoryHausdorffDistance(a, b, c, d, 1, 1, 1);
            dist.ShouldBe((2.0*2.0 + 2.0*2.0)/(2.0 + 2.0) + 3.0 + 1.0);
        }

        [Test]
        public void TrajectoryHausdorffDistance_ParallelSegmentSmall_returnsExpected()
        {
            var a = new Vector2D(0, 0);
            var b = new Vector2D(3, 0);
            var c = new Vector2D(-5, 2);
            var d = new Vector2D(4, 2);
            var dist = Geometry.TrajectoryHausdorffDistance(a, b, c, d, 1, 1, 1);
            dist.ShouldBe((2.0*2.0 + 2.0*2.0)/(2.0 + 2.0) + 1.0, 1e-12);
        }

        [Test]
        public void TrajectoryHausdorffDistance_ParallelSegmentSmallOppositeDirection_returnsExpected()
        {
            var a = new Vector2D(0, 0);
            var b = new Vector2D(3, 0);
            var c = new Vector2D(4, 2);
            var d = new Vector2D(-5, 2);
            var dist = Geometry.TrajectoryHausdorffDistance(a, b, c, d, 1, 1, 1);
            dist.ShouldBe((2.0*2.0 + 2.0*2.0)/(2.0 + 2.0) + 3.0 + 1.0);
        }

        [Test]
        public void TrajectoryHausdorffDistance_PointPoint_returnsExpected()
        {
            var a = new Vector2D(0, 0);
            var b = new Vector2D(1, 1);
            var dist = Geometry.TrajectoryHausdorffDistance(a, a, b, b, 1, 1, 1);
            dist.ShouldBe(a.Distance(b), 1e-13);
        }

        [Test]
        public void TrajectoryHausdorffDistance_PointSegment_returnsExpected()
        {
            var a = new Vector2D(1, 0);
            var b = new Vector2D(0, 1);
            var c = new Vector2D(2, 1);
            var dist = Geometry.TrajectoryHausdorffDistance(a, a, b, c, 1, 1, 1);
            dist.ShouldBe(2.0);
        }

        [Test]
        public void TrajectoryHausdorffDistance_SegmentPoint_returnsExpected()
        {
            var a = new Vector2D(1, 0);
            var b = new Vector2D(0, 1);
            var c = new Vector2D(2, 1);
            var dist = Geometry.TrajectoryHausdorffDistance(b, c, a, a, 1, 1, 1);
            dist.ShouldBe(2.0);
        }
    }
}