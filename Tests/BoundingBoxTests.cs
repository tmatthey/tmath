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
        private readonly Vector3D _min = Vector3D.PositiveInfinity;
        private readonly Vector3D _max = Vector3D.NegativeInfinity;

        [Test]
        public void Clone()
        {
            var u = new Vector3D(1.1, 3.3, 4.4);
            var v = new Vector3D(2.2, 4.4, 5.5);
            var a = new BoundingBox(u);
            a.Expand(v);
            var b = (BoundingBox) a.Clone();
            ReferenceEquals(a, b).ShouldBe(false);
            ReferenceEquals(a.Min, b.Min).ShouldBe(false);
            ReferenceEquals(a.Max, b.Max).ShouldBe(false);
            a.Equals(b).ShouldBe(true);
            a.Min.Equals(b.Min).ShouldBe(true);
            a.Min.Equals(b.Min).ShouldBe(true);
            a.Max.IsEqual(b.Max).ShouldBe(true);
            a.Max.IsEqual(b.Max).ShouldBe(true);
        }

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
        public void Equals_SameBoundingBox_ReturnsTrue()
        {
            var v = new BoundingBox(new Vector3D(1, 2, 3));
            v.Expand(new Vector3D(4, 5, 6));
            var u = new BoundingBox(v);
            v.Equals(u).ShouldBe(true);
        }

        [Test]
        public void Equals_SameRefBoundingBox_ReturnsTrue()
        {
            var v = new BoundingBox(new Vector3D(1, 2, 3));
            v.Expand(new Vector3D(4, 5, 6));
            var u = v;
            v.Equals(u).ShouldBe(true);
        }

        [Test]
        public void Equals_WihtNullptr_ReturnsFalse()
        {
            var v = new BoundingBox(new Vector3D(1, 2, 3));
            v.Expand(new Vector3D(4, 5, 6));
            BoundingBox u = null;
            v.Equals(u).ShouldBe(false);
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
        public void ExpandLayer_ReturnExpandedBounding()
        {
            var bb = new BoundingBox();
            var u = new Vector3D(1, -2, -0.1);
            var v = new Vector3D(2, -1, 0.1);
            var r = 10.0;
            bb.Expand(u);
            bb.Expand(v);
            var min = new Vector3D(bb.Min);
            var max = new Vector3D(bb.Max);

            bb.ExpandLayer(r);

            bb.Min.ShouldBe(min - Vector3D.One*r);
            bb.Max.ShouldBe(max + Vector3D.One*r);
        }

        [Test]
        public void IsInside_EmptyBox_ReturnsFalse()
        {
            var bb = new BoundingBox();
            bb.IsInside(Vector3D.E1).ShouldBe(false);
        }

        [Test]
        public void IsInside_MidpointOfBox_ReturnsTrue()
        {
            var bb = new BoundingBox();
            var u = new Vector3D(1, -2, -0.1);
            var v = new Vector3D(2, -1, 0.1);
            bb.Expand(u);
            bb.Expand(v);
            bb.IsInside((bb.Min + bb.Max)*0.5).ShouldBe(true);
        }

        [Test]
        public void IsInside_OutSidetOfBox_ReturnsFalse()
        {
            var bb = new BoundingBox();
            var u = new Vector3D(1, -2, -0.1);
            var v = new Vector3D(2, -1, 0.1);
            bb.Expand(u);
            bb.Expand(v);
            bb.IsInside(bb.Min - bb.Max).ShouldBe(false);
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