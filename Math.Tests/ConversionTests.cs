/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2018 Thierry Matthey
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
using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests
{
    [TestFixture]
    public class ConversionTests
    {
        [TestCase(0, 0)]
        [TestCase(90, 0.5 * System.Math.PI)]
        [TestCase(180, System.Math.PI)]
        [TestCase(360, 2.0 * System.Math.PI)]
        [TestCase(-90, -0.5 * System.Math.PI)]
        [TestCase(-180, -System.Math.PI)]
        [TestCase(-360, -2.0 * System.Math.PI)]
        public void DegToRad(double d, double r)
        {
            Conversion.DegToRad(d).ShouldBe(r);
        }

        [TestCase(0, 0)]
        [TestCase(90, 0.5 * System.Math.PI)]
        [TestCase(180, System.Math.PI)]
        [TestCase(360, 2.0 * System.Math.PI)]
        [TestCase(-90, -0.5 * System.Math.PI)]
        [TestCase(-180, -System.Math.PI)]
        [TestCase(-360, -2.0 * System.Math.PI)]
        public void RadToDeg(double d, double r)
        {
            Conversion.RadToDeg(r).ShouldBe(d);
        }

        [TestCase(0)]
        [TestCase(12)]
        [TestCase(59)]
        public void DateTimeToSeconds(int sec)
        {
            var d0 = new DateTime(2000, 1, 1, 0, 0, 0);
            var d1 = new DateTime(2000, 1, 1, 0, 0, sec);

            var s0 = Conversion.DateTimeToSeconds(d0);
            var s1 = Conversion.DateTimeToSeconds(d1);
            (s1 - s0).ShouldBe(sec);
        }

        [TestCase(1)]
        [TestCase(59)]
        [TestCase(959)]
        public void DateTimeToSeconds_WithMillisecond(int ms)
        {
            var d0 = new DateTime(2000, 1, 1, 0, 0, 0, 0);
            var d1 = new DateTime(2000, 1, 1, 0, 0, 0, ms);

            var s0 = Conversion.DateTimeToSeconds(d0);
            var s1 = Conversion.DateTimeToSeconds(d1);
            (s1 - s0).ShouldBe(ms / 1000.0, 1e-5);
        }

        [Test]
        public void DateTimeToElapsedSeconds_1Element_ReturnsZero()
        {
            var list = new List<DateTime> {new DateTime(2000, 1, 1, 0, 0, 0, 0)};
            var res = Conversion.DateTimeToElapsedSeconds(list);
            res.Count.ShouldBe(1);
            res[0].ShouldBe(0.0);
        }

        [Test]
        public void DateTimeToElapsedSeconds_2Elements_ReturnsExpected()
        {
            var list = new List<DateTime> {new DateTime(2000, 1, 1, 0, 0, 0), new DateTime(2000, 1, 1, 0, 0, 1)};
            var res = Conversion.DateTimeToElapsedSeconds(list);
            res.Count.ShouldBe(2);
            res[0].ShouldBe(0.0);
            res[1].ShouldBe(1.0);
        }

        [Test]
        public void DateTimeToElapsedSeconds_EmptyList_ReturnsEmptyList()
        {
            var list = new List<DateTime>();
            Conversion.DateTimeToElapsedSeconds(list).Count.ShouldBe(0);
        }
    }
}