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
    public class GeodesyTests
    {
        [Test]
        public void Haversine_2ElementList_ReturnsExpectedDistance()
        {
            var a = new GpsPoint(10, 30);
            var b = new GpsPoint(20, 40);
            var list = new List<GpsPoint> {a, b};
            Geodesy.Distance.Haversine(list).Sum().ShouldBe(Geodesy.Distance.Haversine(a, b));
        }

        [Test]
        public void Haversine_LatMove45DegMainCircle_ReturnsPiEarthRadiusDiv4()
        {
            Geodesy.Distance.Haversine(87, -10, 87 + 45, -10).ShouldBe(System.Math.PI*6367000.0/4.0);
        }

        [Test]
        public void Haversine_LongMove45DegMainCircle_ReturnsPiEarthRadiusDiv4()
        {
            Geodesy.Distance.Haversine(0, 10, 0, 10 + 45).ShouldBe(System.Math.PI*6367000.0/4.0);
        }

        [Test]
        public void Haversine_StartEndSameLocation_ReturnsZeroDistance()
        {
            Geodesy.Distance.Haversine(17, 19, 17, 19).ShouldBe(0);
        }

        [Test]
        public void HaversineAccumulated_4Points_ReturnsAccumulated()
        {
            var a = new GpsPoint(10, 30);
            var b = new GpsPoint(20, 40);
            var c = new GpsPoint(30, 50);
            var d = new GpsPoint(40, 60);
            var ab = a.HaversineDistance(b);
            var bc = b.HaversineDistance(c);
            var cd = c.HaversineDistance(d);
            var list = new List<GpsPoint> {a, b, c, d};
            var res = Geodesy.Distance.HaversineAccumulated(list);
            res.Count.ShouldBe(list.Count);
            res[0].ShouldBe(0.0);
            res[1].ShouldBe(ab);
            res[2].ShouldBe(ab + bc);
            res[3].ShouldBe(ab + bc + cd);
        }

        [Test]
        public void HaversineAccumulated_Empty_ReturnsEmpty()
        {
            var list = new List<GpsPoint>();
            Geodesy.Distance.HaversineAccumulated(list).Count.ShouldBe(0);
        }

        [Test]
        public void HaversineAccumulated_OnePoint_ReturnsZero()
        {
            var a = new GpsPoint(10, 30);
            var list = new List<GpsPoint> {a};
            var res = Geodesy.Distance.HaversineAccumulated(list);
            res.Count.ShouldBe(1);
            res[0].ShouldBe(0.0);
        }

        [Test]
        public void HaversineTotal_2ElementList_ReturnsExpectedDistance()
        {
            var a = new GpsPoint(10, 30);
            var b = new GpsPoint(20, 40);
            var list = new List<GpsPoint> {a, b};
            Geodesy.Distance.HaversineTotal(list).ShouldBe(Geodesy.Distance.Haversine(a, b));
        }
    }
}