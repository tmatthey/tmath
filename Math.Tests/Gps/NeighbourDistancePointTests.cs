/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2021 Thierry Matthey
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

using Math.Gps;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests.Gps
{
    [TestFixture]
    public class NeighbourDistancePointTests
    {
        [Test]
        public void Constructor_Long()
        {
            var d = new NeighbourDistancePoint(1, 2, 13.17, 1.1, 2.2);
            d.Reference.ShouldBe(1);
            d.Current.ShouldBe(2);
            d.MinDistance.ShouldBe(13.17);
            d.Fraction.ShouldBe(1.1);
            d.RefDistance.ShouldBe(2.2);
        }

        [Test]
        public void Constructor_Short()
        {
            var d = new NeighbourDistancePoint(1, 2, 13.17);
            d.Reference.ShouldBe(1);
            d.Current.ShouldBe(2);
            d.MinDistance.ShouldBe(13.17);
            d.Fraction.ShouldBe(double.NaN);
            d.RefDistance.ShouldBe(double.NaN);
        }

        [Test]
        public void Constructor_WithObject()
        {
            var d = new NeighbourDistancePoint(new NeighbourDistancePoint(1, 2, 13.17, 1.1, 2.2));
            d.Reference.ShouldBe(1);
            d.Current.ShouldBe(2);
            d.MinDistance.ShouldBe(13.17);
            d.Fraction.ShouldBe(1.1);
            d.RefDistance.ShouldBe(2.2);
        }

        [Test]
        public void Equals_Nullptr_ReturnFalse()
        {
            var d = new NeighbourDistancePoint(1, 2, 13.17);
            d.Equals(null).ShouldBe(false);
        }

        [Test]
        public void Equals_SameRef_ReturnTrue()
        {
            var d = new NeighbourDistancePoint(1, 2, 13.17);
            d.Equals(d).ShouldBe(true);
        }

        [Test]
        public void IsEqual_Nullptr_ReturnFalse()
        {
            var d = new NeighbourDistancePoint(1, 2, 13.17);
            d.IsEqual(null).ShouldBe(false);
        }
    }
}