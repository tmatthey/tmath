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
            var n = 8;
            var eps = 29.0;
            var minL = 50.0;
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
            var clusterList = dbs.Cluster(eps, n).ToList();

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
                var current = points.First();
                var next = points.First();
                var prevValue = points.First().X;
                var clusterPoints = new List<Vector2D>();

                for (var i = 0; i < points.Count;)
                {
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
                    new Vector2D(920.8684892703366, 281.82456167090857),
                    new Vector2D(890.8664034584274, 332.3529280506337),
                    new Vector2D(837.1354739859266, 384.3818866751725),
                    new Vector2D(788.8975280678949, 434.071610952178)
                },
                new List<Vector2D>
                {
                    new Vector2D(447.63800633761247, 496.38669074815334),
                    new Vector2D(514.3432018724736, 472.9094447657483)
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
            clusterList.Count.ShouldBe(26);
            clusterPointList.Count.ShouldBe(2);
        }
    }
}