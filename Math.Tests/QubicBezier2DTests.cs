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

using NUnit.Framework;
using Shouldly;

namespace Math.Tests
{
    [TestFixture]
    public class CubicBezier2DTests
    {
        [Test]
        public void Example()
        {
            var P0 = new Vector2D(160, 120);
            var P1 = new Vector2D(200, 35);
            var P2 = new Vector2D(260, 220);
            var P3 = new Vector2D(40, 220);

            var bezier = new CubicBezier2D(P0, P1, P2, P3);

            // Point
            (bezier.Evaluate(0) - P0).Norm().ShouldBeLessThan(1e-9);
            (bezier.Evaluate(1) - P3).Norm().ShouldBeLessThan(1e-9);

            // Bounding rect
            var bb = bezier.Bounding() as BoundingRect;
            bb.Min.X.ShouldBe(P3.X);
            bb.Max.Y.ShouldBe(P3.Y);

            // Tangent
            var t0 = bezier.Tangent(0);
            t0.Norm().ShouldBe(1, 1e-9);
            var t1 = bezier.Tangent(1);
            t1.X.ShouldBe(-1);
            t1.Y.ShouldBe(0);

            // Derivatives
            var d = bezier.dEvaluate(1);
            (d - (P3 - P2) * 3).Norm().ShouldBeLessThan(1e-9);
            var d2 = bezier.d2Evaluate(1);
            d2.X.ShouldNotBe(0.0);
            d2.Y.ShouldNotBe(0.0);

            // Kappa
            var k = bezier.Kappa(0);
            k.ShouldBeGreaterThan(0.0);

            bezier.Dimensions.ShouldBe(2);

            var l = bezier.Length();
            var approximated = ((P3 - P0).Norm() + (P0 - P1).Norm() + (P1 - P2).Norm() + (P2 - P1).Norm()) * 0.5;
            l.ShouldNotBe(approximated);
        }

        [Test]
        public void CloneAndIsEqual()
        {
            var bezier = new CubicBezier2D
            {
                P0 = new Vector2D(160, 120),
                P1 = new Vector2D(200, 35),
                P2 = new Vector2D(260, 220),
                P3 = new Vector2D(40, 220)
            };

            var clone = bezier.Clone();
            clone.IsEqual(bezier).ShouldBeTrue();
            bezier.GetHashCode().ShouldBe(clone.GetHashCode());

            clone.P3.Y += 0.1;

            clone.IsEqual(bezier).ShouldBeFalse();
            bezier.GetHashCode().ShouldNotBe(clone.GetHashCode());
        }

        [Test]
        public void Length_StraightLine()
        {
            var bezier = new CubicBezier2D
            {
                P0 = new Vector2D(0, 0),
                P1 = new Vector2D(50, 0),
                P2 = new Vector2D(100, 0),
                P3 = new Vector2D(150, 0)
            };

            bezier.Length().ShouldBe(150.0, 1e-5);
        }

        [TestCase(0.5, 0.3)]
        [TestCase(0.5, 0.5)]
        [TestCase(0.3, 0.5)]
        [TestCase(-0.1, 0.5)]
        [TestCase(0, 0.5)]
        [TestCase(1, 0.5)]
        [TestCase(1.1, 0.5)]
        public void Split(double split, double t)
        {
            var bezier = new CubicBezier2D
            {
                P0 = new Vector2D(160, 120),
                P1 = new Vector2D(200, 35),
                P2 = new Vector2D(260, 220),
                P3 = new Vector2D(40, 220)
            };
            var p = bezier.Evaluate(t);

            var (b0, b1) = bezier.Split(split);
            if (Comparison.IsLessEqual(split, 0) || Comparison.IsLessEqual(1.0, split))
            {
                b0.IsEqual(bezier).ShouldBeTrue();
                b1.ShouldBeNull();
            }
            else if (Comparison.IsEqual(split, t))
            {
                (b0.Evaluate(1) - p).Norm().ShouldBeLessThan(1e-9);
                (b1.Evaluate(0) - p).Norm().ShouldBeLessThan(1e-9);
            }
            else if (t < split)
            {
                (b0.Evaluate(t / split) - p).Norm().ShouldBeLessThan(1e-9);
            }
            else
            {
                (b1.Evaluate((t - split) / (1.0 - split)) - p).Norm().ShouldBeLessThan(1e-9);
            }
        }
    }
}