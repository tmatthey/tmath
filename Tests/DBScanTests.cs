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
using Math.Tests.Gps;
using NUnit.Framework;
using Shouldly;

namespace Math.Tests
{
    [TestFixture]
    public class DBScanTests
    {
        private readonly GpsTrackExamples _gpsTrackExamples = new GpsTrackExamples();

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
        public void DBScan_OneElementList_ReturnsZero()
        {
            var tracks = TrajectoryExamples.hurricane1950_2006();
            //tracks.Count.ShouldBe(32);
            var box = new BoundingRect();
            foreach (var p in tracks.SelectMany(track => track))
                box.Expand(p);
            /* var gpsTracks = new List<GpsTrack>
            {
                new GpsTrack(_gpsTrackExamples.TrackOne()),
                new GpsTrack(_gpsTrackExamples.TrackTwo()),
                new GpsTrack(_gpsTrackExamples.TrackThree()),
                new GpsTrack(_gpsTrackExamples.TrackFour()),
                new GpsTrack(_gpsTrackExamples.TrackFive())
            };
            var center = HeatMap.CalculateCenter(gpsTracks, null);
            var tracks = new List<List<Vector2D>>();
            var box = new BoundingRect();
            foreach (var track in gpsTracks)
            {
                var transformed = track.CreateTransformedTrack(center);
                tracks.Add(transformed.Track);
                box.Expand(transformed.Size);
            }
            */

            var n = 8;
            var eps = 29.0;
            var minL = 50.0;

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
            //segments.Count.ShouldBe(1879); // 1876

            var dbs = new DBScan<Vector2D, Segment2D>(segments);
            var clusterList = dbs.Cluster(eps, n).ToList();
            //clusterList.Count.ShouldBe(24);//26

            var clusterPointList = new List<List<Vector2D>>();
            var clusters =
                clusterList.Select(cluster => cluster.Select(s => (Segment2DExt) (segments[s])).ToList()).ToList();
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


            var bitmap = new Bitmap(box.Min - Vector2D.One, box.Max + Vector2D.One, 1.0);
            foreach (var list in tracks)
            {
                for (var i = 0; i + 1 < list.Count; i++)
                    Draw.XiaolinWu(list[i], list[i + 1], bitmap.Set, 0.5);
            }
            foreach (var list in clusterPointList)
            {
                for (var i = 0; i + 1 < list.Count; i++)
                    Draw.XiaolinWu(list[i], list[i + 1], bitmap.Set, 1.0);
            }
            /*
            var expected = new List<List<Vector2D>>
            {
                new List<Vector2D>
                {
                    new Vector2D(741.7335304473816,553.6392875383685),new Vector2D(778.6809113991914,525.9013878803172),new Vector2D(792.130279938787,492.62454143104276),new Vector2D(823.5624687349294,463.7162220169296),new Vector2D(848.2324411235637,433.2273399201515),new Vector2D(876.2866279100767,403.4879532275861),new Vector2D(891.6929282788197,370.6938253164625),new Vector2D(903.556475157445,336.2549736382944),new Vector2D(909.5168639297692,298.2590068004327),new Vector2D(908.3496102841481,260.9703090455546),new Vector2D(923.5550683518953,227.66167719103996),new Vector2D(926.460293233958,191.64667686968772),new Vector2D(939.169268092385,157.60433634290118),new Vector2D(943.5073712028221,120.6780171783783),new Vector2D(947.9322810032348,84.89330964985822)

                },
                new List<Vector2D>
                {
new Vector2D(646.3951090899124,552.9171740081795),new Vector2D(611.6314813402975,545.0655515902688),new Vector2D(570.1712154318952,525.7911927799032),new Vector2D(533.3343139326898,523.5559852610412),new Vector2D(497.5247278543948,507.1428290339447),new Vector2D(460.40043800467265,505.6846940978163),new Vector2D(424.205796495269,502.6643342621895),new Vector2D(385.6739382330603,504.80428735229486),new Vector2D(338.42118795093535,537.2987080833357),new Vector2D(293.68473581699266,562.752863242183),new Vector2D(256.7786226786295,562.2692770183576),new Vector2D(228.5078719084648,530.3691829911905),new Vector2D(195.5149260922782,508.2001907668798),new Vector2D(164.3241086090557,483.9665542245886),new Vector2D(127.47278518076712,478.0140199054605)
                }

            };
            foreach (var list in expected)
            {
                for (var i = 0; i + 1 < list.Count; i++)
                    Draw.XiaolinWu(list[i], list[i + 1], bitmap.Set, 0.75);
            }*/


            BitmapFileWriter.PNG("cluster.png", bitmap.Pixels);
        }

        [Test]
        public void Estimate_Deers1995()
        {
            var tracks = TrajectoryExamples.deer_1995();
            var minL = 50.0;

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
            for (var eps = 1.0; eps < 50; eps++)
            {
                int n;
                var entropy = dbs.Entropy(eps, out n);
            }
        }
    }
}