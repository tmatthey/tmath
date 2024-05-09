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
    public class ToolsTests
    {
        [Test]
        public void DistanceVelocityAcceleration_EmptyList_ReturnsEmpty()
        {
            var track = new List<GpsPoint>();
            Tools.DistanceVelocityAcceleration(track, out var d, out var v, out var a);
            d.Count.ShouldBe(0);
            v.Count.ShouldBe(0);
            a.Count.ShouldBe(0);
        }

        [Test]
        public void DistanceVelocityAcceleration_OnePoint_ReturnsZero()
        {
            var track = new List<GpsPoint> {new GpsPoint(0, 0)};
            Tools.DistanceVelocityAcceleration(track, out var d, out var v, out var a);
            d.Count.ShouldBe(1);
            d[0].ShouldBe(0.0);
            v.Count.ShouldBe(1);
            v[0].ShouldBe(0.0);
            a.Count.ShouldBe(1);
            a[0].ShouldBe(0.0);
        }

        [Test]
        public void DistanceVelocityAcceleration_ThreePoints_ReturnsExpected()
        {
            var track = new List<GpsPoint> {new GpsPoint(0, 0), new GpsPoint(1, 0), new GpsPoint(2, 0)};
            Tools.DistanceVelocityAcceleration(track, out var d, out var v, out var a);
            d.Count.ShouldBe(3);
            d[0].ShouldBe(0.0);
            d[1].ShouldBe(Geodesy.DistanceOneDeg);
            d[2].ShouldBe(Geodesy.DistanceOneDeg * 2.0);
            v.Count.ShouldBe(3);
            v[0].ShouldBe(0.0);
            v[1].ShouldBe(Geodesy.DistanceOneDeg);
            v[2].ShouldBe(0.0);
            a.Count.ShouldBe(3);
            a[0].ShouldBe(0.0);
            a[1].ShouldBe(0.0);
            a[2].ShouldBe(0.0);
        }

        [Test]
        public void DistanceVelocityAcceleration_TwoPoints_ReturnsZeroAndDistance()
        {
            var track = new List<GpsPoint> {new GpsPoint(0, 0), new GpsPoint(1, 0)};
            Tools.DistanceVelocityAcceleration(track, out var d, out var v, out var a);
            d.Count.ShouldBe(2);
            d[0].ShouldBe(0.0);
            d[1].ShouldBe(Geodesy.DistanceOneDeg);
            v.Count.ShouldBe(2);
            v[0].ShouldBe(0.0);
            v[1].ShouldBe(0.0);
            a.Count.ShouldBe(2);
            a[0].ShouldBe(0.0);
            a[1].ShouldBe(0.0);
        }

        [Test]
        public void DistanceVelocityAcceleration_TwoSamePoints_ReturnsZero()
        {
            var track = new List<GpsPoint> {new GpsPoint(0, 0), new GpsPoint(0, 0)};
            Tools.DistanceVelocityAcceleration(track, out var d, out var v, out var a);
            d.Count.ShouldBe(2);
            d[0].ShouldBe(0.0);
            d[1].ShouldBe(0.0);
            v.Count.ShouldBe(2);
            v[0].ShouldBe(0.0);
            v[1].ShouldBe(0.0);
            a.Count.ShouldBe(2);
            a[0].ShouldBe(0.0);
            a[1].ShouldBe(0.0);
        }
    }
}