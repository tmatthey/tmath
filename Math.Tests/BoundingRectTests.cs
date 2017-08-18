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

using NUnit.Framework;
using Shouldly;

namespace Math.Tests
{
    [TestFixture]
    public class BoundingRectTests
    {
        private readonly Vector2D _min = Vector2D.PositiveInfinity;
        private readonly Vector2D _max = Vector2D.NegativeInfinity;

        [Test]
        public void Clone()
        {
            var u = new Vector2D(1.1, 3.3);
            var v = new Vector2D(2.2, 4.4);
            var a = new BoundingRect(u);
            a.Expand(v);
            var b = (BoundingRect) a.Clone();
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
            var bb = new BoundingRect();
            bb.IsEmpty().ShouldBe(true);
        }

        [Test]
        public void Constructor_Max_returnsNegInfitity()
        {
            var bb = new BoundingRect();
            bb.Max.ShouldBe(_max);
        }

        [Test]
        public void Constructor_Min_returnsPosInfitity()
        {
            var bb = new BoundingRect();
            bb.Min.ShouldBe(_min);
        }

        [Test]
        public void Equals_SameBoundingRect_ReturnsTrue()
        {
            var v = new BoundingRect(new Vector2D(1, 2));
            v.Expand(new Vector2D(4, 5));
            var u = new BoundingRect(v);
            v.Equals(u).ShouldBe(true);
        }

        [Test]
        public void Equals_SameRefBoundingRect_ReturnsTrue()
        {
            var v = new BoundingRect(new Vector2D(1, 2));
            v.Expand(new Vector2D(4, 5));
            var u = v;
            v.Equals(u).ShouldBe(true);
        }

        [Test]
        public void Equals_WihtNullptr_ReturnsFalse()
        {
            var v = new BoundingRect(new Vector2D(1, 2));
            v.Expand(new Vector2D(4, 5));
            BoundingRect u = null;
            v.Equals(u).ShouldBe(false);
        }

        [Test]
        public void Expand_EmptyWithEmptyBoundingRect_IsEmptyTrue()
        {
            var bb = new BoundingRect();
            bb.Expand(new BoundingRect());
            bb.Min.ShouldBe(_min);
            bb.Max.ShouldBe(_max);
            bb.IsEmpty().ShouldBe(true);
        }

        [Test]
        public void Expand_EmptyWithNonEmptyBoundingRect_returnsVector2D()
        {
            var bb = new BoundingRect();
            var v = new Vector2D(1, -2);
            var bb2 = new BoundingRect();
            bb2.Expand(v);
            bb.Expand(bb2);
            bb.Min.ShouldBe(v);
            bb.Max.ShouldBe(v);
            bb.IsEmpty().ShouldBe(false);
        }

        [Test]
        public void Expand_NonEmptyWithEmptyBoundingRect_returnsVector2D()
        {
            var bb = new BoundingRect();
            var v = new Vector2D(1, -2);
            var bb2 = new BoundingRect();
            bb.Expand(v);
            bb.Expand(bb2);
            bb.Min.ShouldBe(v);
            bb.Max.ShouldBe(v);
            bb.IsEmpty().ShouldBe(false);
        }

        [Test]
        public void Expand_WithTwoVector2D_returnsExpectedMinMax()
        {
            var bb = new BoundingRect();
            var u = new Vector2D(1, -2);
            var v = new Vector2D(2, -1);
            bb.Expand(u);
            bb.Expand(v);
            bb.Min.ShouldBe(u);
            bb.Max.ShouldBe(v);
        }

        [Test]
        public void Expand_WithVector2D_returnsMinMaxVector2D()
        {
            var bb = new BoundingRect();
            var v = new Vector2D(1, -2);
            bb.Expand(v);
            bb.Min.ShouldBe(v);
            bb.Max.ShouldBe(v);
        }

        [Test]
        public void ExpandLayer_ReturnExpandedBounding()
        {
            var bb = new BoundingRect();
            var u = new Vector2D(1, -2);
            var v = new Vector2D(2, -1);
            var r = 10.0;
            bb.Expand(u);
            bb.Expand(v);
            var min = new Vector2D(bb.Min);
            var max = new Vector2D(bb.Max);

            bb.ExpandLayer(r);

            bb.Min.ShouldBe(min - Vector2D.One * r);
            bb.Max.ShouldBe(max + Vector2D.One * r);
        }

        [Test]
        public void GetHashCode_DifferentObjects_ReturnsDifferentHashCode()
        {
            var b0 = new BoundingRect(Vector2D.E1);
            var b1 = new BoundingRect(Vector2D.E2);
            b0.GetHashCode().ShouldNotBe(b1.GetHashCode());
        }

        [Test]
        public void IsInside_EmptyBox_ReturnsFalse()
        {
            var bb = new BoundingRect();
            bb.IsInside(Vector2D.E1).ShouldBe(false);
        }

        [Test]
        public void IsInside_MidpointOfBox_ReturnsTrue()
        {
            var bb = new BoundingRect();
            var u = new Vector2D(1, -2);
            var v = new Vector2D(2, -1);
            bb.Expand(u);
            bb.Expand(v);
            bb.IsInside((bb.Min + bb.Max) * 0.5).ShouldBe(true);
        }

        [Test]
        public void IsInside_OutSidetOfBox_ReturnsFalse()
        {
            var bb = new BoundingRect();
            var u = new Vector2D(1, -2);
            var v = new Vector2D(2, -1);
            bb.Expand(u);
            bb.Expand(v);
            bb.IsInside(bb.Min - bb.Max).ShouldBe(false);
        }

        [Test]
        public void Reset_MinMax_returnsInfitity()
        {
            var bb = new BoundingRect();
            bb.Expand(Vector2D.E1);
            bb.Reset();

            bb.Min.ShouldBe(_min);
            bb.Max.ShouldBe(_max);
        }
    }
}