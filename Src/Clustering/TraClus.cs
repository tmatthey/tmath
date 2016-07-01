using System.Collections.Generic;
using System.Linq;

namespace Math.Clustering
{
    public static class TraClus
    {
        public static List<List<Vector2D>> Cluster(IList<List<Vector2D>> tracks, int n, double eps,
            bool direction = false, double minL = 50.0, int mdlCostAdwantage = 25)
        {
            var segments = new List<Segment2D>();
            var k = 0;
            foreach (var track in tracks)
            {
                var significantPoints = Geometry.SignificantPoints(track, true, mdlCostAdwantage);
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
            var clusterList = dbs.Cluster(eps, n, direction).ToList();
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
                        var removeList = del.Where(i1 => cluster[i1].K == cluster[i0].K).ToList();
                        foreach (var i1 in removeList)
                        {
                            lineSegments.Remove(i1);
                            del.Remove(i1);
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
            return clusterPointList;
        }

        internal class Segment2DExt : Segment2D
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

        internal class Vector2DExt : Vector2D
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
    }
}