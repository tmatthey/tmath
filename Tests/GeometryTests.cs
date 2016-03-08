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

        [Test]
        public void ConvexHull_WithEmptyList_returnsEmpty()
        {
            var points = new List<Vector2D>();
            var expected = new List<Vector2D>();

            var result = Geometry.ConvexHull(points);

            result.Count.ShouldBe(expected.Count);

        }

        [Test]
        public void ConvexHull_WithOnePoint_returnsOnePoint()
        {
            var points = new List<Vector2D> { new Vector2D(4.4, 14) };
            var expected = new List<Vector2D> { new Vector2D(4.4, 14) };

            var result = Geometry.ConvexHull(points);

            result.Count.ShouldBe(expected.Count);
            for (var i = 0; i < result.Count; i++)
                result[i].ShouldBe(expected[i]);
        }

        [Test]
        public void ConvexHull_WithTwoSamePoint_returnsOnePoint()
        {
            var points = new List<Vector2D> { new Vector2D(4.4, 14), new Vector2D(4.4, 14) };
            var expected = new List<Vector2D> { new Vector2D(4.4, 14) };

            var result = Geometry.ConvexHull(points);

            result.Count.ShouldBe(expected.Count);
            for (var i = 0; i < result.Count; i++)
                result[i].ShouldBe(expected[i]);
        }

        [Test]
        public void ConvexHull_WithFourSamePoint_returnsOnePoint()
        {
            var points = new List<Vector2D> { new Vector2D(4.4, 14), new Vector2D(4.4, 14), new Vector2D(4.4, 14), new Vector2D(4.4, 14) };
            var expected = new List<Vector2D> { new Vector2D(4.4, 14) };

            var result = Geometry.ConvexHull(points);

            result.Count.ShouldBe(expected.Count);
            for (var i = 0; i < result.Count; i++)
                result[i].ShouldBe(expected[i]);
        }

        [Test]
        public void ConvexHull_WithFourPointsOnLine_returnsTwoPoints()
        {
            var points = new List<Vector2D> { new Vector2D(1, 0), new Vector2D(2, 0), new Vector2D(3, 0), new Vector2D(4, 0) };
            var expected = new List<Vector2D> { new Vector2D(1, 0), new Vector2D(4, 0) };

            var result = Geometry.ConvexHull(points);

            result.Count.ShouldBe(expected.Count);
            for (var i = 0; i < result.Count; i++)
                result[i].ShouldBe(expected[i]);
        }

        [Test]
        public void ConvexHull_Sample1()
        {
            var points = new List<Vector2D> { new Vector2D(4.4, 14), new Vector2D(6.7, 15.25), new Vector2D(6.9, 12.8), new Vector2D(2.1, 11.1), 
                                                new Vector2D(9.5, 14.9), new Vector2D(13.2, 11.9), new Vector2D(10.3, 12.3), new Vector2D(6.8, 9.5), new Vector2D(3.3, 7.7), 
                                                new Vector2D(0.6, 5.1), new Vector2D(5.3, 2.4),  new Vector2D(8.45, 4.7), new Vector2D(11.5, 9.6), new Vector2D(13.8, 7.3), 
                                                new Vector2D(12.9, 3.1), new Vector2D(11, 1.1) };

            var expected = new List<Vector2D> {new Vector2D(0.6, 5.1), new Vector2D(5.3, 2.4), 
                                                new Vector2D(11, 1.1), new Vector2D(12.9, 3.1), new Vector2D(13.8, 7.3), 
                                                new Vector2D(13.2, 11.9), new Vector2D(9.5, 14.9), new Vector2D(6.7, 15.25), 
                                                new Vector2D(4.4, 14), new Vector2D(2.1, 11.1)};

            var result = Geometry.ConvexHull(points);
            result.Count.ShouldBe(expected.Count);
            for (var i = 0; i < result.Count; i++)
                result[i].ShouldBe(expected[i]);
        }

        [Test]
        public void ConvexHull_Sample2()
        {
            var points = new List<Vector2D> { new Vector2D(1, 0), new Vector2D(1, 1), new Vector2D(1, -1), new Vector2D(0.68957, 0.283647), new Vector2D(0.909487, 0.644276), 
                                                new Vector2D(0.0361877, 0.803816), new Vector2D(0.583004, 0.91555), new Vector2D(-0.748169, 0.210483), 
                                                new Vector2D(-0.553528, -0.967036), new Vector2D(0.316709, -0.153861), new Vector2D(-0.79267, 0.585945),
                                                new Vector2D(-0.700164, -0.750994), new Vector2D(0.452273, -0.604434), new Vector2D(-0.79134, -0.249902), 
                                                new Vector2D(-0.594918, -0.397574), new Vector2D(-0.547371, -0.434041), new Vector2D(0.958132, -0.499614), 
                                                new Vector2D(0.039941, 0.0990732), new Vector2D(-0.891471, -0.464943), new Vector2D(0.513187, -0.457062), 
                                                new Vector2D(-0.930053, 0.60341), new Vector2D(0.656995, 0.854205)};

            var expected = new List<Vector2D> { new Vector2D(1, -1), new Vector2D(1, 1), new Vector2D(0.583004, 0.91555), new Vector2D(0.0361877, 0.803816), 
                                                new Vector2D(-0.930053, 0.60341), new Vector2D(-0.891471, -0.464943), new Vector2D(-0.700164, -0.750994), 
                                                new Vector2D(-0.553528, -0.967036)};

            var result = Geometry.ConvexHull(points);
            result.Count.ShouldBe(expected.Count);
        }

        [Test]
        public void ConvexHull_FiveGpsTracks()
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

            var result = Geometry.ConvexHull(points);
            result.Count.ShouldBe(34);


        }
    }
}
