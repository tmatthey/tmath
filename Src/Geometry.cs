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
using Math.Interfaces;

namespace Math
{
    public static class Geometry
    {
        public static IList<Vector2D> ConvexHullJarvismarch(IList<Vector2D> allPoints)
        {
            // Make unique, O(N) (?)
            var points = allPoints.Distinct().ToList();

            // Reduce point set to O(sqrt(N))
            points = Eliminate(points);

            // Sort, O(N*log(N))
            points.Sort((a, b) => a == b ? 0 : (a.Y < b.Y || Comparison.IsEqual(a.Y, b.Y) && a.X < b.X ? -1 : 1));

            if (points.Count < 3)
                return points;

            var hull = new List<Vector2D>();

            var m = 0;
            var n = -1;

            var tmp = new List<Vector2D>(points) {points.First()};
            var l = tmp.Count;
            var lastAngle = 0.0;
            do
            {
                n++;
                hull.Add(tmp[m]);
                if (n != m)
                {
                    var v = new Vector2D(tmp[m]);
                    tmp[m] = tmp[n];
                    tmp[n] = v;
                }
                var newAngle = System.Math.PI*2.0;
                var newDistance = 0.0;
                m = l - 1;
                for (var i = n + 1; i < l; i++)
                {
                    var v = tmp[i] - tmp[n];
                    var distance = v.Norm2();
                    var a = System.Math.Atan2(v.Y, v.X);
                    if (a < 0.0)
                    {
                        a += System.Math.PI*2.0;
                    }
                    if (Comparison.IsEqual(a, lastAngle))
                    {
                        if (distance > newDistance)
                        {
                            newAngle = a;
                            m = i;
                            newDistance = distance;
                        }
                    }
                    else if (a > lastAngle && Comparison.IsLessEqual(a, newAngle))
                    {
                        if (a < newAngle || (Comparison.IsEqual(a, newAngle) && distance > newDistance))
                        {
                            newAngle = a;
                            m = i;
                            newDistance = distance;
                        }
                    }
                }

                lastAngle = newAngle;
            } while (m != l - 1);
            return hull;
        }

        public static IList<Vector2D> ConvexHullMonotoneChain(IList<Vector2D> allPoints)
        {
            // https://en.wikibooks.org/wiki/Algorithm_Implementation/Geometry/Convex_hull/Monotone_chain
            // Make unique, O(N) (?)
            var points = allPoints.Distinct().ToList();

            // Reduce point set to O(sqrt(N))
            points = Eliminate(points);

            // Sort, O(N*log(N))
            points.Sort((a, b) => a == b ? 0 : (a.X < b.X || Comparison.IsEqual(a.X, b.X) && a.Y < b.Y ? -1 : 1));

            var k = 0;
            var hull = new Vector2D[points.Count*2];

            // O(N)
            // Build lower hull
            for (var i = 0; i < points.Count; ++i)
            {
                while (k >= 2 && Cross(hull[k - 2], hull[k - 1], points[i]) <= 0) k--;
                hull[k++] = points[i];
            }

            // Build upper hull
            for (int i = points.Count - 2, t = k + 1; i >= 0; i--)
            {
                while (k >= t && Cross(hull[k - 2], hull[k - 1], points[i]) <= 0) k--;
                hull[k++] = points[i];
            }

            if (k > 1)
                k--;
            return hull.Take(k).ToList();
        }

        public static bool CircleLineIntersect(Circle3D c, Vector3D a, Vector3D b)
        {
            var u = b - a;
            var area = c.Normal*u;
            if (Comparison.IsZero(area))
                return false;
            var w = a - c.Center;
            var t = -(c.Normal*w)/area;
            var v = u*t + a;
            return Comparison.IsLessEqual(c.Center.EuclideanNorm(v), c.Radius);
        }

        // Minimal boundary circle based on Welzl's algorithm
        public static Circle2D MinCircle(IList<Vector2D> allPoints)
        {
            // Make unique, O(N) (?)
            var points = allPoints.Distinct().ToList();

            // Reduce point set to O(sqrt(N))
            points = Eliminate(points);

            // Trival cases
            if (points.Count == 0)
                return new Circle2D(new Vector2D(double.NaN), double.NaN);
            if (points.Count == 1)
                return Circle2D.Create(points[0]);
            if (points.Count == 2)
                return Circle2D.Create(points[0], points[1]);
            if (points.Count == 3)
                return Circle2D.Create(points[0], points[1], points[2]);

            var array = new Vector2D[3];

            return DoMinCricle(points, points.Count, array, 0);
        }

        // Minimal boundary circle based on Welzl's algorithm
        public static Circle3D MinCircleOnSphere(IList<Vector3D> allPoints)
        {
            // Make unique, O(N) (?)
            var points = allPoints.Distinct().ToList();

            // Trival cases
            if (points.Count == 0)
                return new Circle3D(new Vector3D(double.NaN), double.NaN);
            if (points.Count == 1)
                return Circle3D.Create(points[0]);
            if (points.Count == 2)
                return Circle3D.Create(points[0], points[1]);
            if (points.Count == 3)
                return Circle3D.Create(points[0], points[1], points[2]);

            var array = new Vector3D[3];

            return DoMinCricleOnSphere(points, points.Count, array, 0);
        }

        private static Circle2D DoMinCricle(IList<Vector2D> points, int n, Vector2D[] array, int k)
        {
            if (k == 3)
                return Circle2D.Create(array[0], array[1], array[2]);
            if (n == 1 && k == 0)
                return Circle2D.Create(points[0]);
            if (n == 0 && k == 2)
                return Circle2D.Create(array[0], array[1]);
            if (n == 1 && k == 1)
                return Circle2D.Create(array[0], points[0]);

            var c = DoMinCricle(points, n - 1, array, k);
            if (c.IsInside(points[n - 1]))
                return c;
            array[k++] = points[n - 1];
            c = DoMinCricle(points, n - 1, array, k);
            return c;
        }

        private static Circle3D DoMinCricleOnSphere(IList<Vector3D> points, int n, Vector3D[] array, int k)
        {
            if (k == 3)
                return Circle3D.Create(array[0], array[1], array[2]);
            if (n == 1 && k == 0)
                return Circle3D.Create(points[0]);
            if (n == 0 && k == 2)
                return Circle3D.Create(array[0], array[1]);
            if (n == 1 && k == 1)
                return Circle3D.Create(array[0], points[0]);

            var c = DoMinCricleOnSphere(points, n - 1, array, k);
            if (CircleLineIntersect(c, Vector3D.Zero, points[n - 1]))
                return c;
            array[k++] = points[n - 1];
            c = DoMinCricleOnSphere(points, n - 1, array, k);
            return c;
        }

        private static double Cross(Vector2D o, Vector2D a, Vector2D b)
        {
            return Vector2D.Cross(a - o, b - o);
        }

        //  Analysis of a Simple Yet Efficient Convex Hull Algorithm; Golin, Sedgewick, ACM 1988
        private static List<Vector2D> Eliminate(List<Vector2D> points)
        {
            if (points.Count < 2)
                return points;
            var array = points.ToArray();
            int i2, i3, i4;
            double a3, a4;

            var i1 = i2 = i3 = i4 = 1;
            var a1 = a4 = array[0].X + array[0].Y;
            var a2 = a3 = array[0].X - array[0].Y;

            for (var j = 1; j < array.Length; j++)
            {
                if (array[j].X + array[j].Y < a1)
                {
                    i1 = j;
                    a1 = array[j].X + array[j].Y;
                }
                else if (array[j].X + array[j].Y > a4)
                {
                    i4 = j;
                    a4 = array[j].X + array[j].Y;
                }

                if (array[j].X - array[j].Y < a2)
                {
                    i2 = j;
                    a2 = array[j].X - array[j].Y;
                }
                else if (array[j].X - array[j].Y > a3)
                {
                    i3 = j;
                    a3 = array[j].X - array[j].Y;
                }
            }

            var lowX = System.Math.Max(array[i1].X, array[i2].X);
            var highX = System.Math.Min(array[i3].X, array[i4].X);
            var lowY = System.Math.Max(array[i1].Y, array[i3].Y);
            var highY = System.Math.Min(array[i2].Y, array[i4].Y);
            var n = 0;
            var i = array.Length - 1;

            while (i > n)
            {
                if ((lowX < array[i].X) && (array[i].X < highX) &&
                    (lowY < array[i].Y) && (array[i].Y < highY))
                {
                    i--;
                }
                else
                {
                    n++;
                    Utils.Swap(ref array[n], ref array[i]);
                }
            }
            return array.Take(n + 1).ToList();
        }

        public static double PerpendicularDistance<T>(T x0, T x1, T p) where T : IVector<T>
        {
            var l = x0.EuclideanNorm(x1);
            if (Comparison.IsZero(l))
            {
                return x0.EuclideanNorm(p);
            }
            return x1.Sub(x0).CrossNorm(p.Sub(x0))/l;
        }

        public static double PerpendicularSegmentDistance<T>(T x0, T x1, T p) where T : IVector<T>
        {
            var dist = PerpendicularDistance(x0, x1, p);
            if (x0.IsEqual(x1))
                return dist;

            if (x1.Sub(x0).Dot(p.Sub(x1)) >= 0.0)
                return x1.EuclideanNorm(p);
            if (x0.Sub(x1).Dot(p.Sub(x0)) >= 0.0)
                return x0.EuclideanNorm(p);

            return dist;
        }

        public static double PerpendicularParameter<T>(T x0, T x1, T p) where T : IVector<T>
        {
            var d = x1.Sub(x0).Norm2();
            if (Comparison.IsZero(d))
                return 0.0;
            var a = p.Sub(x0);
            var b = x1.Sub(x0);
            return a.Dot(b)/d;
        }

        public static double PerpendicularSegmentParameter<T>(T x0, T x1, T p) where T : IVector<T>
        {
            return System.Math.Max(System.Math.Min(PerpendicularParameter(x0, x1, p), 1.0), 0.0);
        }

        // Trajectory clustering: a partition-and-group framework
        // Jae-Gil Lee, Jiawei Han, Kyu-Young Whang
        // SIGMOD '07 Proceedings of the 2007 ACM SIGMOD international conference on Management of data 
        public static double TrajectoryHausdorffDistance<TV>(ISegment<TV> a, ISegment<TV> b, bool direction = true,
            double wPerpendicular = 1.0, double wParallel = 1.0, double wAngular = 1.0) where TV : IVector<TV>
        {
            return TrajectoryHausdorffDistance(a.A, a.B, b.A, b.B, direction, wPerpendicular, wParallel, wAngular);
        }

        public static double TrajectoryHausdorffDistance<T>(T a0, T a1, T b0, T b1, bool direction = true,
            double wPerpendicular = 1.0, double wParallel = 1.0, double wAngular = 1.0) where T : IVector<T>
        {
            double perpendicular, parallel, angular;
            TrajectoryHausdorffDistances(a0, a1, b0, b1, direction, out perpendicular, out parallel, out angular);
            return wPerpendicular*perpendicular + wParallel*parallel + wAngular*angular;
        }

        public static void TrajectoryHausdorffDistances<T>(T a0, T a1, T b0, T b1, bool direction,
            out double perpendicular, out double parallel, out double angular) where T : IVector<T>
        {
            var l1 = a0.EuclideanNorm(a1);
            var l2 = b0.EuclideanNorm(b1);
            if (l1 > l2)
            {
                Utils.Swap(ref a0, ref b0);
                Utils.Swap(ref a1, ref b1);
                Utils.Swap(ref l1, ref l2);
            }

            var dPerpA = PerpendicularDistance(b0, b1, a0);
            var dPerpB = PerpendicularDistance(b0, b1, a1);
            perpendicular = 0.0;
            if (Comparison.IsPositive(dPerpA + dPerpB))
            {
                perpendicular = (dPerpA*dPerpA + dPerpB*dPerpB)/(dPerpA + dPerpB);
            }

            var t0 = PerpendicularParameter(b0, b1, a0);
            var p0 = b0.Mul(1.0 - t0).Add(b1.Mul(t0));
            var d0 = t0 < 0.5 ? b0.EuclideanNorm(p0) : b1.EuclideanNorm(p0);

            var t1 = PerpendicularParameter(b0, b1, a1);
            var p1 = b0.Mul(1.0 - t1).Add(b1.Mul(t1));
            var d1 = t1 < 0.5 ? b0.EuclideanNorm(p1) : b1.EuclideanNorm(p1);

            parallel = System.Math.Min(d0, d1);

            var angle = a1.Sub(a0).AngleAbs(b1.Sub(b0));
            angular = l1*(direction && Comparison.IsLessEqual(System.Math.PI/2.0, angle) ? 1.0 : System.Math.Sin(angle));
        }

        public static IList<int> SignificantPoints<T>(IList<T> track, int mdlCostAdwantage = 25) where T : IVector<T>
        {
            if (track == null || track.Count < 1)
                return new List<int>();
            if (track.Count == 1)
                return new List<int> {0};

            var i = 0;
            int j;
            var points = new List<int> {0};
            do
            {
                var globalCost = 0;

                for (j = 1; i + j < track.Count; j++)
                {
                    globalCost += ModelCost(track[i + j - 1], track[i + j]);
                    var localCost = ModelCost(track[i], track[i + j]) + EncodingCost(track, i, i + j);

                    if (globalCost + mdlCostAdwantage < localCost)
                    {
                        points.Add(i + j - 1);
                        i = i + j - 1;
                        j = 0;
                        break;
                    }
                }
            } while (i + j < track.Count);
            if (points.Last() != track.Count - 1 && !track[points.Last()].IsEqual(track.Last()))
            {
                points.Add(track.Count - 1);
            }


            return points;
        }

        private static int EncodingCost<T>(IList<T> track, int i0, int i1) where T : IVector<T>, INorm<T>
        {
            var cost = 0;
            for (var i = i0; i < i1; i++)
            {
                double perpendicular, parallel, angular;
                TrajectoryHausdorffDistances(track[i], track[i + 1], track[i0], track[i1], true, out perpendicular,
                    out parallel, out angular);
                perpendicular = System.Math.Max(perpendicular, 1.0);
                angular = System.Math.Max(angular, 1.0);

                cost += (int) System.Math.Ceiling(System.Math.Log(perpendicular, 2))
                        + (int) System.Math.Ceiling(System.Math.Log(angular, 2));
            }
            return cost;
        }

        private static int ModelCost<T>(T p0, T p1) where T : IVector<T>
        {
            var d = System.Math.Max(p0.EuclideanNorm(p1), 1.0);
            return (int) System.Math.Ceiling(System.Math.Log(d, 2));
        }

        public static IList<int> PolylineToSegmentPointList<T>(IList<T> polyline, double minDistance = 0.0)
            where T : IVector<T>
        {
            var list = new List<int>();
            if (polyline == null || polyline.Count <= 1)
            {
                return list;
            }

            minDistance = System.Math.Max(minDistance, Comparison.Epsilon);
            for (var i = 0; i + 1 < polyline.Count; i++)
            {
                if (Comparison.IsLessEqual(minDistance, polyline[i].EuclideanNorm(polyline[i + 1])))
                {
                    list.Add(i);
                    list.Add(i + 1);
                }
            }
            return list;
        }

        public static IList<Segment2D> PolylineToSegments(IList<Vector2D> polyline, double minDistance = 0.0)
        {
            var list = PolylineToSegmentPointList(polyline, minDistance);
            var segments = new List<Segment2D>();
            for (var i = 0; i < list.Count; i += 2)
                segments.Add(new Segment2D(polyline[list[i]], polyline[list[i + 1]]));
            return segments;
        }

        public static IList<Segment3D> PolylineToSegments(IList<Vector3D> polyline, double minDistance = 0.0)
        {
            var list = PolylineToSegmentPointList(polyline, minDistance);
            var segments = new List<Segment3D>();
            for (var i = 0; i < list.Count; i += 2)
                segments.Add(new Segment3D(polyline[list[i]], polyline[list[i + 1]]));
            return segments;
        }
    }
}