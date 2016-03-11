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

using NUnit.Framework;
using Shouldly;

namespace Math.Tests
{
    [TestFixture]
    public class BoundingBoxTests
    {
        private readonly Vector3D _min = new Vector3D(double.PositiveInfinity);
        private readonly Vector3D _max = new Vector3D(double.NegativeInfinity);

        [Test]
        public void Constructor_Empty_True()
        {
            var bb = new BoundingBox();
            bb.IsEmpty().ShouldBe(true);
        }

        [Test]
        public void Constructor_Max_returnsNegInfitity()
        {
            var bb = new BoundingBox();
            bb.Max.ShouldBe(_max);
        }

        [Test]
        public void Constructor_Min_returnsPosInfitity()
        {
            var bb = new BoundingBox();
            bb.Min.ShouldBe(_min);
        }

        [Test]
        public void Expand_EmptyWithEmptyBoundingBox_IsEmptyTrue()
        {
            var bb = new BoundingBox();
            bb.Expand(new BoundingBox());
            bb.Min.ShouldBe(_min);
            bb.Max.ShouldBe(_max);
            bb.IsEmpty().ShouldBe(true);
        }

        [Test]
        public void Expand_EmptyWithNonEmptyBoundingBox_returnsVector3D()
        {
            var bb = new BoundingBox();
            var v = new Vector3D(1, -2, 3);
            var bb2 = new BoundingBox();
            bb2.Expand(v);
            bb.Expand(bb2);
            bb.Min.ShouldBe(v);
            bb.Max.ShouldBe(v);
            bb.IsEmpty().ShouldBe(false);
        }

        [Test]
        public void Expand_NonEmptyWithEmptyBoundingBox_returnsVector3D()
        {
            var bb = new BoundingBox();
            var v = new Vector3D(1, -2, 3);
            var bb2 = new BoundingBox();
            bb.Expand(v);
            bb.Expand(bb2);
            bb.Min.ShouldBe(v);
            bb.Max.ShouldBe(v);
            bb.IsEmpty().ShouldBe(false);
        }

        [Test]
        public void Expand_WithTwoVector3D_returnsExpectedMinMax()
        {
            var bb = new BoundingBox();
            var u = new Vector3D(1, -2, 3);
            var v = new Vector3D(2, -1, 4);
            bb.Expand(u);
            bb.Expand(v);
            bb.Min.ShouldBe(u);
            bb.Max.ShouldBe(v);
        }

        [Test]
        public void Expand_WithVector3D_returnsMinMaxVector3D()
        {
            var bb = new BoundingBox();
            var v = new Vector3D(1, -2, 3);
            bb.Expand(v);
            bb.Min.ShouldBe(v);
            bb.Max.ShouldBe(v);
        }

        [Test]
        public void Reset_MinMax_returnsInfitity()
        {
            var bb = new BoundingBox();
            bb.Expand(Vector3D.E1);
            bb.Reset();

            bb.Min.ShouldBe(_min);
            bb.Max.ShouldBe(_max);
        }
    }
}