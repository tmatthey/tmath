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
using Math.KDTree;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests.KDTree
{
    [TestFixture]
    public class KDTreeTests
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(17)]
        [TestCase(19)]
        public void NoTree_WitnNElements_ReturnsList0ToNMinusOne(int n)
        {
            var list = Enumerable.Range(100, n).ToList();
            var tree = new NoTree<int, int>(list);
            var res = tree.Search(1, 2).ToList();
            res.Count.ShouldBe(n);
            for (var i = 0; i < res.Count; i++)
                res[i].ShouldBe(i);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void Builder_WithListSegment3DOnE1AndSegmentRange_ReturnsTheSegment(int n)
        {
            var list = new List<Segment3D>
            {
                new Segment3D(Vector3D.E1*0.1, Vector3D.E1*1.0),
                new Segment3D(Vector3D.E1*1.1, Vector3D.E1*2.0),
                new Segment3D(Vector3D.E1*2.1, Vector3D.E1*3.0),
                new Segment3D(Vector3D.E1*3.1, Vector3D.E1*4.0),
                new Segment3D(Vector3D.E1*4.1, Vector3D.E1*5.0),
                new Segment3D(Vector3D.E1*5.1, Vector3D.E1*6.0)
            };
            var tree = TreeBuilder.Build(list);
            var res = tree.Search(list[n].A, list[n].B).ToList().Distinct().ToList();
            res.Count.ShouldBe(1);
            res[0].ShouldBe(n);
        }

        [Test]
        public void Builder_WithEmptyListVector2DAndInfRange_ReturnsEmpty()
        {
            var list = new List<Vector2D>();
            var tree = TreeBuilder.Build(list);
            tree.Search(new Vector2D(double.NegativeInfinity, double.NegativeInfinity),
                new Vector2D(double.PositiveInfinity, double.PositiveInfinity)).Count().ShouldBe(0);
        }

        [Test]
        public void Builder_WithEmptyListVector3DAndInfRange_ReturnsEmpty()
        {
            var list = new List<Vector3D>();
            var tree = TreeBuilder.Build(list);
            tree.Search(new Vector3D(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity),
                new Vector3D(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity))
                .Count()
                .ShouldBe(0);
        }

        [Test]
        public void Builder_WithListOneOutSideSegment2DAndUnitCubeRange_ReturnsListMinusOne()
        {
            var list = new List<Segment2D>
            {
                new Segment2D(Vector2D.E1*0.5, Vector2D.E2*1.5),
                new Segment2D(Vector2D.E1*1.5, Vector2D.E1*1.5 + Vector2D.E2*1.5)
            };
            var tree = TreeBuilder.Build(list);
            var res = tree.Search(Vector2D.Zero, Vector2D.One).ToList().Distinct();
            res.Count().ShouldBe(list.Count - 1);
            res.Contains(1).ShouldBe(false);
        }

        [Test]
        public void Builder_WithListOneOutSideSegment3DAndUnitCubeRange_ReturnsListMinusOne()
        {
            var list = new List<Segment3D>
            {
                new Segment3D(Vector3D.E1*0.5, Vector3D.E2*1.5),
                new Segment3D(Vector3D.E1*5.0, Vector3D.E2*5.0),
                new Segment3D(Vector3D.E1*1.5, Vector3D.E1*1.5 + Vector3D.E2*1.5),
                new Segment3D(Vector3D.E2*0.5, Vector3D.E3*1.5),
                new Segment3D(Vector3D.E3*0.5, Vector3D.E1*1.5),
                new Segment3D(Vector3D.E1*0.5, Vector3D.E2*1.5),
                new Segment3D(Vector3D.E1*5.0, Vector3D.E2*5.0)
            };
            var tree = TreeBuilder.Build(list);
            var res = tree.Search(Vector3D.Zero, Vector3D.One).ToList().Distinct();
            res.Count().ShouldBe(list.Count - 1);
            res.Contains(2).ShouldBe(false);
        }

        [Test]
        public void Builder_WithListSegment3DAndUnitCubeRange_ReturnsList()
        {
            var list = new List<Segment3D>
            {
                new Segment3D(Vector3D.E1*0.5, Vector3D.E2*1.5),
                new Segment3D(Vector3D.E2*0.5, Vector3D.E3*1.5),
                new Segment3D(Vector3D.E3*0.5, Vector3D.E1*1.5)
            };
            var tree = TreeBuilder.Build(list);
            var res = tree.Search(Vector3D.Zero, Vector3D.One).ToList().Distinct();
            res.Count().ShouldBe(list.Count);
        }

        [Test]
        public void Builder_WithListVector2DAndInfRange_ReturnsList()
        {
            var list = new List<Vector2D> {Vector2D.E1, Vector2D.E2, Vector2D.Zero, Vector2D.One};
            var tree = TreeBuilder.Build(list);
            var res = tree.Search(new Vector2D(double.NegativeInfinity, double.NegativeInfinity),
                new Vector2D(double.PositiveInfinity, double.PositiveInfinity)).ToList().Distinct();
            res.Count().ShouldBe(list.Count);
        }

        [Test]
        public void Builder_WithListVector2DAndUnitCubeRange_ReturnsList()
        {
            var list = new List<Vector2D> {Vector2D.E1, Vector2D.E2, Vector2D.Zero, Vector2D.One};
            var tree = TreeBuilder.Build(list);
            var res = tree.Search(Vector2D.Zero, Vector2D.One).ToList().Distinct();
            res.Count().ShouldBe(list.Count);
        }

        [Test]
        public void Builder_WithListVector3DAndInfRange_ReturnsList()
        {
            var list = new List<Vector3D>
            {
                Vector3D.E1,
                Vector3D.E2,
                Vector3D.E3,
                Vector3D.Zero,
                Vector3D.One,
                Vector3D.E2,
                Vector3D.E3
            };
            var tree = TreeBuilder.Build(list);
            var res =
                tree.Search(new Vector3D(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity),
                    new Vector3D(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity))
                    .ToList().Distinct();
            res.Count().ShouldBe(list.Count);
        }

        [Test]
        public void Builder_WithListVector3DAndUnitCubeRange_ReturnsList()
        {
            var list = new List<Vector3D> {Vector3D.E1, Vector3D.E2, Vector3D.E3, Vector3D.Zero, Vector3D.One};
            var tree = TreeBuilder.Build(list);
            var res = tree.Search(Vector3D.Zero, Vector3D.One).ToList().Distinct();
            res.Count().ShouldBe(list.Count);
        }
    }
}