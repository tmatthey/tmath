/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2019 Thierry Matthey
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

using Math.Gfx;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests.Gfx
{
    [TestFixture]
    public class BlueMappingTests
    {
        [TestCase(-0.1, 255)]
        [TestCase(0.0, 255)]
        [TestCase(0.0001, 229)]
        [TestCase(0.1, 209)]
        [TestCase(0.2, 188)]
        [TestCase(0.3, 168)]
        [TestCase(0.4, 147)]
        [TestCase(0.5, 127)]
        [TestCase(0.6, 107)]
        [TestCase(0.7, 86)]
        [TestCase(0.8, 66)]
        [TestCase(0.9, 45)]
        [TestCase(0.999, 25)]
        [TestCase(1.0, 25)]
        [TestCase(1.1, 25)]
        public void GreyRedGreenBlue_ReturnsExpected(double c, byte expected)
        {
            var map = new BlueMapping(0.1, 0.9);
            map.Grey(c).ShouldBe(expected);
            map.Color(c).Red.ShouldBe(expected);
            map.Color(c).Green.ShouldBe(expected);
            map.Color(c).Blue.ShouldBe((byte) 255);
        }

        [TestCase(0.0)]
        [TestCase(0.1)]
        [TestCase(0.5)]
        [TestCase(0.9)]
        [TestCase(1.0)]
        public void DefaultValues(double c)
        {
            var m0 = new BlueMapping();
            var m1 = new BlueMapping(0.001);
            var m2 = new BlueMapping(0.001, 1.0);
            m0.Grey(c).ShouldBe(m1.Grey(c));
            m1.Grey(c).ShouldBe(m2.Grey(c));
            m2.Grey(c).ShouldBe(BlueMapping.Default.Grey(c));
        }
    }
}