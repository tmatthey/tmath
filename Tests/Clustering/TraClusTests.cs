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
using Math.Clustering;
using Math.Gfx;
using NUnit.Framework;
using Shouldly;

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
                    new Vector2D(144.29446403837088, 575.9234849575189),
                    new Vector2D(184.1767709560477, 570.8177843943499),
                    new Vector2D(237.97414909603404, 547.4314106216683),
                    new Vector2D(275.80949479027413, 549.1026784196047),
                    new Vector2D(319.6448149271142, 539.1002664981366),
                    new Vector2D(371.6740517552487, 497.76750680884066),
                    new Vector2D(407.7052521153526, 502.47409826772287),
                    new Vector2D(445.1378165514049, 501.8861442381799),
                    new Vector2D(479.7987949762706, 510.0682289908822),
                    new Vector2D(515.5372768286757, 514.6005816023113),
                    new Vector2D(548.3582901660582, 530.0420576125782),
                    new Vector2D(583.5048754127748, 541.8654191952315),
                    new Vector2D(635.7317800552779, 556.5531066540136)
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
                for (var i = 0; i + 1 < list.Count; i++)
                    Draw.XiaolinWu(list[i], list[i + 1], bitmap.Set);
            }

            BitmapFileWriter.PNG(TestUtils.OutputPath() + "cluster2D.png", bitmap.Pixels);

            clusterPointList.Count.ShouldBe(4);
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
                clusterPointList3D[i].Count.ShouldBe(clusterPointList2D[i].Count);
                for (var j = 0; j < clusterPointList3D[i].Count; j++)
                {
                    var v = clusterPointList3D[i][j].Rotate(axis, -angle);
                    v.X.ShouldBe(clusterPointList2D[i][j].X, 1e-9);
                    v.Y.ShouldBe(clusterPointList2D[i][j].Y, 1e-9);
                    v.Z.ShouldBe(0, 1e-9);
                }
            }

            var box = new BoundingRect();
            foreach (var p in tracks2D.SelectMany(track => track))
                box.Expand(p);
            var bitmap = new Bitmap(box.Min - Vector2D.One, box.Max + Vector2D.One, 1.0);
            foreach (var list in clusterPointList2D)
            {
                for (var i = 0; i + 1 < list.Count; i++)
                    Draw.XiaolinWu(list[i], list[i + 1], bitmap.Set, 0.5);
            }
            foreach (var list in clusterPointList3D)
            {
                for (var i = 0; i + 1 < list.Count; i++)
                {
                    var a = list[i].Rotate(axis, -angle);
                    var u = new Vector2D(a.X, a.Y);
                    var b = list[i + 1].Rotate(axis, -angle);
                    var v = new Vector2D(b.X, b.Y);
                    Draw.XiaolinWu(u, v, bitmap.Set);
                }
            }
            BitmapFileWriter.PNG(TestUtils.OutputPath() + "cluster3D.png", bitmap.Pixels);
        }
    }
}