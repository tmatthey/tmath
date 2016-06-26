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
using Math.Gfx;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests
{
    [TestFixture]
    public class DBScanTests
    {
        public class Segment2DExt : Segment2D
        {
            public Segment2DExt(Vector2D a, Vector2D b, int i0, int i1, int j, int k)
            {
                A = a;
                B = b;
                IA = i0;
                IB = i1;
                J = j;
                K = k;
                U = a;
                V = b;
            }

            public int IA { get; set; }
            public int IB { get; set; }
            public int J { get; set; }
            public int K { get; set; }
            public Vector2D U { get; set; }
            public Vector2D V { get; set; }
        }

        public class Vector2DExt : Vector2D
        {
            public Vector2DExt(Vector2D v, int i, int j, int k)
                : base(v)
            {
                I = i;
                J = j;
                K = k;
            }

            public int I { get; set; }
            public int J { get; set; }
            public int K { get; set; }
        }

        [Test]
        public void DBScan_EmptyList_ReturnsEmpty()
        {
            var list = new List<Segment2D> {new Segment2D(Vector2D.E1, Vector2D.E2)};
            var dbs = new DBScan<Vector2D, Segment2D>(list);
            var res = dbs.Cluster().ToList();
            res.Count.ShouldBe(0);
        }

        [Test]
        public void DBScan_TraClusAlgo()
        {
            const int n = 8;
            const double eps = 29.0;
            const double minL = 50.0;
            var tracks = TrajectoryExamples.deer_1995();
            tracks.Count.ShouldBe(32);

            var segments = new List<Segment2D>();
            var k = 0;
            foreach (var track in tracks)
            {
                var significantPoints = Geometry.SignificantPoints(track);
                var segs =
                    Geometry.PolylineToSegmentPointList(significantPoints.Select(i => track[i]).ToList(), minL).ToList();
                for (var j = 0; j < segs.Count; j += 2)
                {
                    var i0 = significantPoints[segs[j]];
                    var i1 = significantPoints[segs[j + 1]];
                    var s = new Segment2DExt(track[i0], track[i1], i0, i1, j/2, k);
                    segments.Add(s);
                }
                k++;
            }

            var dbs = new DBScan<Vector2D, Segment2D>(segments);
            TestUtils.StartTimer();
            var clusterList = dbs.Cluster(eps, n).ToList();
            TestUtils.StopTimer();
            var clusterPointList = new List<List<Vector2D>>();
            var clusters =
                clusterList.Select(cluster => cluster.Select(s => (Segment2DExt) segments[s]).ToList()).ToList();
            foreach (var cluster in clusters)
            {
                var trajectories = new HashSet<int>();
                foreach (var s in cluster)
                    trajectories.Add(s.K);
                if (trajectories.Count < n)
                    continue;

                var avg = new Vector2D();
                avg = cluster.Aggregate(avg, (c, s) => c + s.Vector())/cluster.Count;
                var angle = -Vector2D.E1.Angle(avg);
                var points = new List<Vector2DExt>();
                var j = 0;
                foreach (var s in cluster)
                {
                    s.U = s.A.Rotate(angle);
                    s.V = s.B.Rotate(angle);
                    points.Add(new Vector2DExt(s.U, s.IA, j, s.K));
                    points.Add(new Vector2DExt(s.V, s.IB, j, s.K));
                    j++;
                }
                points = points.OrderBy(p => p.X).ToList();

                var lineSegments = new HashSet<int>();
                var prevValue = points.First().X;
                var clusterPoints = new List<Vector2D>();

                for (var i = 0; i < points.Count;)
                {
                    Vector2DExt next;
                    Vector2DExt current;
                    var del = new HashSet<int>();
                    var insert = new HashSet<int>();
                    do
                    {
                        current = points[i];
                        i++;
                        if (!lineSegments.Contains(current.J))
                        {
                            insert.Add(current.J);
                            lineSegments.Add(current.J);
                        }
                        else
                        {
                            del.Add(current.J);
                        }

                        if (!(i + 1 < points.Count))
                            break;
                        next = points[i];
                    } while (Comparison.IsEqual(current.X, next.X));

                    foreach (var i0 in insert)
                    {
                        foreach (var i1 in del)
                        {
                            if (i0 == i1)
                            {
                                lineSegments.Remove(i1);
                                del.Remove(i1);
                                break;
                            }
                        }
                        foreach (var i1 in del)
                        {
                            if (cluster[i1].K == cluster[i0].K)
                            {
                                lineSegments.Remove(i1);
                                del.Remove(i1);
                                break;
                            }
                        }
                    }

                    if (lineSegments.Count >= n &&
                        Comparison.IsLessEqual(minL/1.414, System.Math.Abs(current.X - prevValue)))
                    {
                        var sum = new Vector2D();

                        foreach (var seg in lineSegments)
                        {
                            var s = cluster[seg];
                            var c = (current.X - s.U.X)/(s.V.X - s.U.X);
                            var y = s.U.Y + c*(s.V.Y - s.U.Y);
                            sum += new Vector2D(current.X, y);
                        }
                        prevValue = current.X;
                        clusterPoints.Add((sum/lineSegments.Count).Rotate(-angle));
                    }

                    foreach (var i1 in del)
                        lineSegments.Remove(i1);
                }
                if (clusterPoints.Any())
                    clusterPointList.Add(clusterPoints);
            }

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


            BitmapFileWriter.PNG("cluster.png", bitmap.Pixels);

            segments.Count.ShouldBe(1876);
            clusterList.Count.ShouldBe(22);
            clusterPointList.Count.ShouldBe(4);
        }
    }
}