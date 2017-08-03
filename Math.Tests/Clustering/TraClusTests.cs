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
using Math.Clustering;
using Math.Gfx;
using NUnit.Framework;
using Shouldly;
using BitmapFileWriter = Math.Gfx.BitmapFileWriter;

namespace Math.Tests.Clustering
{
    [TestFixture]
    public class TraClusTests
    {
        [Test]
        public void TraClus_2D()
        {
            const int n = 8;
            const double eps = 29.0;
            var tracks = TrajectoryExamples.deer_1995();
            tracks.Count.ShouldBe(32);

            var clusterPointList = TraClus.Cluster(tracks, n, eps);

            var box = new BoundingRect();
            foreach (var p in tracks.SelectMany(track => track))
                box.Expand(p);
            var bitmap = new Bitmap(box.Min - Vector2D.One, box.Max + Vector2D.One, 1.0);
            foreach (var list in tracks)
            {
                for (var i = 0; i + 1 < list.Count; i++)
                    Draw.XiaolinWu(list[i], list[i + 1], bitmap.Set, 0.5);
            }

            var expected = new List<List<Vector2D>>
            {
                new List<Vector2D>
                {
                    new Vector2D(720.5433602090745, 563.7818785282013),
                    new Vector2D(742.6654964014616, 487.5016920170826),
                    new Vector2D(779.4034006787246, 481.24923594289896),
                    new Vector2D(810.5557658939762, 455.68962534466823),
                    new Vector2D(842.759614956762, 434.22880255526826),
                    new Vector2D(874.3644125064528, 405.37987429352154),
                    new Vector2D(903.7556786268015, 370.8092360113303),
                    new Vector2D(930.1907853337177, 319.33848441807004),
                    new Vector2D(955.7332883197433, 233.3893395826258)
                },
                new List<Vector2D>
                {
                    new Vector2D(141.1824428563939, 575.4081885137225),
                    new Vector2D(181.24595752626806, 580.5792385425581),
                    new Vector2D(221.97255070987745, 562.7310336223853),
                    new Vector2D(260.04471675549627, 554.8926525768136),
                    new Vector2D(303.8238385934801, 546.0750109905307),
                    new Vector2D(344.65008843260273, 522.8448313161166),
                    new Vector2D(386.8469941833413, 494.5680676309053),
                    new Vector2D(421.637258552711, 502.1516100075995),
                    new Vector2D(459.17997682626856, 502.94918708644525),
                    new Vector2D(495.45792335891457, 508.1576089302648),
                    new Vector2D(531.223731698255, 514.2302103985002),
                    new Vector2D(566.4711678767858, 527.8864633306744),
                    new Vector2D(601.2863950620653, 544.3522724007256),
                    new Vector2D(644.5026819667878, 551.1598646353114)
                },
                new List<Vector2D>
                {
                    new Vector2D(350.5027874855198, 591.856585200759),
                    new Vector2D(294.6862989686186, 588.4845201573708),
                    new Vector2D(265.3201104886492, 565.2966163415649),
                    new Vector2D(243.31064727205595, 537.0406347378847),
                    new Vector2D(220.9501909469821, 507.1571064815744),
                    new Vector2D(190.89282483735636, 484.07862309931096),
                    new Vector2D(152.92962106394532, 465.48807933311514)
                },
                new List<Vector2D>
                {
                    new Vector2D(322.574533439217, 537.4372623794318),
                    new Vector2D(306.11861708799046, 573.1699497489491),
                    new Vector2D(290.3301129543721, 609.678880532123)
                }
            };
            foreach (var list in expected)
            {
                for (var i = 0; i + 1 < list.Count; i++)
                    Draw.XiaolinWu(list[i], list[i + 1], bitmap.Set, 0.75);
            }

            foreach (var list in clusterPointList)
            {
                for (var i = 0; i + 1 < list.Segment.Count; i++)
                    Draw.XiaolinWu(list.Segment[i], list.Segment[i + 1], bitmap.Set);
            }

            BitmapFileWriter.PNG(TestUtils.OutputPath() + "cluster2D.png", bitmap.Pixels);

            var bitmapPoints = new Bitmap(box.Min - Vector2D.One, box.Max + Vector2D.One, 1.0);
            foreach (var list in clusterPointList)
            {
                foreach (var track in list.PointIndices.Indices())
                {
                    foreach (var point in list.PointIndices[track])
                    {
                        Draw.Plot(tracks[track][point], 1.0, bitmapPoints.Set);
                    }
                }
            }
            BitmapFileWriter.PNG(TestUtils.OutputPath() + "cluster2D.points.png", bitmapPoints.Pixels);

            var bitmapSegs = new Bitmap(box.Min - Vector2D.One, box.Max + Vector2D.One, 1.0);
            foreach (var list in clusterPointList)
            {
                foreach (var track in list.SegmentIndices.Indices())
                {
                    for (var i = 0; i < list.SegmentIndices[track].Count; i += 2)
                    {
                        Draw.XiaolinWu(tracks[track][list.SegmentIndices[track][i]],
                            tracks[track][list.SegmentIndices[track][i + 1]], bitmapSegs.Set);
                    }
                }
            }
            BitmapFileWriter.PNG(TestUtils.OutputPath() + "cluster2D.segments.png", bitmapSegs.Pixels);

            clusterPointList.Count.ShouldBe(4);
            for (var i = 0; i < clusterPointList.Count; i++)
            {
                clusterPointList[i].Segment.Count.ShouldBe(expected[i].Count);
                for (var j = 0; j < clusterPointList[i].Segment.Count; j++)
                {
                    clusterPointList[i].Segment[j].EuclideanNorm(expected[i][j]).ShouldBeLessThan(1e-7);
                }
            }
        }

        [Test]
        public void TraClus_3D()
        {
            var axis = new Vector3D(1, 2, 3);
            const double angle = 0.1;
            const int n = 8;
            const double eps = 29.0;
            var tracks2D = TrajectoryExamples.deer_1995();
            var tracks =
                tracks2D.Select(track2D => track2D.Select(p => new Vector3D(p.X, p.Y, 0.0).Rotate(axis, angle)).ToList())
                    .ToList();
            tracks.Count.ShouldBe(32);

            var clusterPointList3D = TraClus.Cluster(tracks, n, eps);

            var clusterPointList2D = TraClus.Cluster(tracks2D, n, eps);

            clusterPointList3D.Count.ShouldBe(clusterPointList2D.Count);
            for (var i = 0; i < clusterPointList3D.Count; i++)
            {
                clusterPointList3D[i].Segment.Count.ShouldBe(clusterPointList2D[i].Segment.Count);
                for (var j = 0; j < clusterPointList3D[i].Segment.Count; j++)
                {
                    var v = clusterPointList3D[i].Segment[j].Rotate(axis, -angle);
                    v.X.ShouldBe(clusterPointList2D[i].Segment[j].X, 1e-9);
                    v.Y.ShouldBe(clusterPointList2D[i].Segment[j].Y, 1e-9);
                    v.Z.ShouldBe(0, 1e-9);
                }
            }

            var box = new BoundingRect();
            foreach (var p in tracks2D.SelectMany(track => track))
                box.Expand(p);
            var bitmap = new Bitmap(box.Min - Vector2D.One, box.Max + Vector2D.One, 1.0);
            foreach (var list in clusterPointList2D)
            {
                for (var i = 0; i + 1 < list.Segment.Count; i++)
                    Draw.XiaolinWu(list.Segment[i], list.Segment[i + 1], bitmap.Set, 0.5);
            }
            foreach (var list in clusterPointList3D)
            {
                for (var i = 0; i + 1 < list.Segment.Count; i++)
                {
                    var a = list.Segment[i].Rotate(axis, -angle);
                    var u = new Vector2D(a.X, a.Y);
                    var b = list.Segment[i + 1].Rotate(axis, -angle);
                    var v = new Vector2D(b.X, b.Y);
                    Draw.XiaolinWu(u, v, bitmap.Set);
                }
            }
            BitmapFileWriter.PNG(TestUtils.OutputPath() + "cluster3D.png", bitmap.Pixels);
        }
    }
}