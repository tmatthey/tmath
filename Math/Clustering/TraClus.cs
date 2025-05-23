﻿/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2025 Thierry Matthey
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

namespace Math.Clustering
{
    /// <summary>
    /// Implementation of "Trajectory Clustering: A Partition-and-Group Framework", by Jae-Gil Lee, Jiawei Han and Kyu-Young Whang.
    /// </summary>
    public static class TraClus
    {
        /// <summary>
        /// Clustering of 2D trajectories. 
        /// </summary>
        /// <param name="tracks">List of tracks</param>
        /// <param name="n">Minimum number of common segments required to be recognized as a cluster.</param>
        /// <param name="eps">Epsilon of neighborhood between to segments using trajectory Hausdorff distance.</param>
        /// <param name="direction">Boolean defining if the direction between two segments shall include in the trajectory Hausdorff distance.</param>
        /// <param name="minL">Minimum length of a segment</param>
        /// <param name="mdlCostAdvantage">Maximum cost allowed for minimum description length (MDL).</param>
        /// <returns></returns>
        public static List<Result<Vector2D>> Cluster(IList<List<Vector2D>> tracks, int n, double eps,
            bool direction = false, double minL = 50.0, int mdlCostAdvantage = 25)
        {
            var segments = Partitioning<Vector2D, Segment2D, Segment2DExt>(tracks, minL, mdlCostAdvantage);
            var clusters = DBScan<Vector2D, Segment2D, Segment2DExt>(n, eps, direction, segments);
            return ClusterPoint<Vector2D, Segment2D, Vector2DExt, Segment2DExt>(n, minL, clusters, new Rotation2D(),
                new CreateVector2DExt());
        }

        /// <summary>
        /// Clustering of 3D trajectories. 
        /// </summary>
        /// <param name="tracks">List of tracks</param>
        /// <param name="n">Minimum number of common segments required to be recognized as a cluster.</param>
        /// <param name="eps">Epsilon of neighborhood between to segments using trajectory Hausdorff distance.</param>
        /// <param name="direction">Boolean defining if the direction between two segments shall include in the trajectory Hausdorff distance.</param>
        /// <param name="minL">Minimum length of a segment</param>
        /// <param name="mdlCostAdvantage">Maximum cost allowed for minimum description length (MDL).</param>
        /// <returns></returns>
        public static List<Result<Vector3D>> Cluster(IList<List<Vector3D>> tracks, int n, double eps,
            bool direction = false, double minL = 50.0, int mdlCostAdvantage = 25)
        {
            var segments = Partitioning<Vector3D, Segment3D, Segment3DExt>(tracks, minL, mdlCostAdvantage);
            var clusters = DBScan<Vector3D, Segment3D, Segment3DExt>(n, eps, direction, segments);
            return ClusterPoint<Vector3D, Segment3D, Vector3DExt, Segment3DExt>(n, minL, clusters, new Rotation3D(),
                new CreateVector3DExt());
        }

        private static List<Result<T>> ClusterPoint<T, S, TE, SE>(int n, double minL,
            List<List<SE>> clusters, IRotation<T> rotation, ICreateVectorExt<T, TE> factoryVectorExt)
            where T : IVector<T>, new()
            where S : ISegment<T, S>
            where TE : IVectorExt, IVector<T>
            where SE : ISegmentExt<T>, ISegment<T, S>
        {
            var result = new List<Result<T>>();
            foreach (var cluster in clusters)
            {
                var trajectories = new HashSet<int>();
                foreach (var s in cluster)
                    trajectories.Add(s.K);
                if (trajectories.Count < n)
                    continue;

                rotation.Set(cluster.Aggregate(new T(), (c, s) => c.Add(s.Vector())).Div(cluster.Count));
                var points = new List<TE>();
                for (var j = 0; j < cluster.Count; j++)
                {
                    var s = cluster[j];
                    s.U = rotation.ToE1(s.A);
                    s.V = rotation.ToE1(s.B);
                    points.Add(factoryVectorExt.Create(s.U, s.IA, j, s.K));
                    points.Add(factoryVectorExt.Create(s.V, s.IB, j, s.K));
                }

                points = points.OrderBy(p => p.X).ToList();

                var lineSegments = new HashSet<int>();
                var prevValue = 0.0;
                var r = new Result<T>();

                for (var i = 0; i < points.Count;)
                {
                    TE next;
                    TE current;
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
                        (r.Segment.Count < 1 ||
                         Comparison.IsLessEqual(minL / 1.414, System.Math.Abs(current.X - prevValue))))
                    {
                        var sum = new T();
                        foreach (var seg in lineSegments)
                        {
                            var s = cluster[seg];
                            var c = (current.X - s.U.X) / (s.V.X - s.U.X);
                            var y = s.U.Add(s.V.Sub(s.U).Mul(c));
                            y.X = current.X;
                            sum = sum.Add(y);
                            if (!r.SegmentIndices.ContainsKey(s.K))
                            {
                                r.SegmentIndices[s.K] = new List<int>();
                            }

                            r.SegmentIndices[s.K].Add(s.IA);
                            r.SegmentIndices[s.K].Add(s.IB);
                        }

                        prevValue = current.X;
                        r.Segment.Add(rotation.FromE1(sum.Div(lineSegments.Count)));
                    }

                    foreach (var i1 in del)
                        lineSegments.Remove(i1);
                }

                if (r.Segment.Count > 1)
                {
                    result.Add(r);
                }
            }

            return result;
        }

        private static List<List<SE>> DBScan<T, S, SE>(int n, double eps, bool direction, List<S> segments)
            where T : IVector<T>
            where S : ISegment<T, S>, INorm<S>
            where SE : S, ISegmentExt<T>
        {
            var dbs = new DBScan<T, S>(segments);
            var clusterList = dbs.Cluster(eps, n, direction).ToList();
            return clusterList.Select(cluster => cluster.Select(s => (SE) segments[s]).ToList()).ToList();
        }

        private static List<S> Partitioning<T, S, SE>(IList<List<T>> tracks, double minL, int mdlCostAdvantage)
            where T : IVector<T>
            where S : ISegment<T, S>
            where SE : S, ISegmentExt<T>, new()
        {
            var segments = new List<S>();
            var k = 0;
            foreach (var track in tracks)
            {
                var significantPoints = Geometry.SignificantPoints(track, true, mdlCostAdvantage);
                var segs =
                    Geometry.PolylineToSegmentPointList(significantPoints.Select(i => track[i]).ToList(), minL);
                for (var j = 0; j < segs.Count; j += 2)
                {
                    var i0 = significantPoints[segs[j]];
                    var i1 = significantPoints[segs[j + 1]];
                    var s = new SE
                    {
                        A = track[i0],
                        B = track[i1],
                        IA = i0,
                        IB = i1,
                        //J = j / 2,
                        K = k
                    };
                    segments.Add(s);
                }

                k++;
            }

            return segments;
        }

        /// <summary>
        /// Representative common segment.
        /// </summary>
        /// <typeparam name="T">Vector of dimension n, e.g., Vector2D or Vector3D.</typeparam>
        public class Result<T>
        {
            private SparseArray<IList<int>> _pointIndices;

            /// <summary>
            /// Representative common segment.
            /// </summary>
            public Result()
            {
                Segment = new List<T>();
                _pointIndices = null;
                SegmentIndices = new SparseArray<IList<int>>();
            }

            /// <summary>
            /// Returns a list of tracks of point indices as sorted list, which were used during clustering.
            /// </summary>
            public SparseArray<IList<int>> PointIndices
            {
                get
                {
                    if (_pointIndices == null)
                    {
                        _pointIndices = new SparseArray<IList<int>>();
                        foreach (var i in SegmentIndices.Indices())
                        {
                            _pointIndices[i] = SegmentIndices[i].Distinct().OrderBy(num => num).ToList();
                        }
                    }

                    return _pointIndices;
                }
            }

            /// <summary>
            /// Returns the representative common segment as a polyline, list of points.
            /// </summary>
            public IList<T> Segment { get; set; }

            /// <summary>
            /// Returns a list of tracks of list of segments (pair point index), which were used during clustering.
            /// </summary>
            public SparseArray<IList<int>> SegmentIndices { get; set; }
        }

        internal interface ISegmentExt<T>
            where T : IVector<T>
        {
            int IA { get; set; }

            int IB { get; set; }

            //int J { get; set; }
            int K { get; set; }
            T U { get; set; }
            T V { get; set; }
        }

        internal class Segment2DExt : Segment2D, ISegmentExt<Vector2D>
        {
            public int IA { get; set; }

            public int IB { get; set; }

            //public int J { get; set; }
            public int K { get; set; }
            public Vector2D U { get; set; }
            public Vector2D V { get; set; }
        }

        internal class Segment3DExt : Segment3D, ISegmentExt<Vector3D>
        {
            public int IA { get; set; }

            public int IB { get; set; }

            //public int J { get; set; }
            public int K { get; set; }
            public Vector3D U { get; set; }
            public Vector3D V { get; set; }
        }

        internal interface IVectorExt
        {
            //int I { get; set; }
            int J { get; set; }
            //int K { get; set; }
        }

        internal class Vector2DExt : Vector2D, IVectorExt
        {
            public Vector2DExt(Vector2D v, int i, int j, int k)
                : base(v)
            {
                //I = i;
                J = j;
                //K = k;
            }

            //public int I { get; set; }
            public int J { get; set; }
            // int K { get; set; }
        }

        internal class Vector3DExt : Vector3D, IVectorExt
        {
            public Vector3DExt(Vector3D v, int i, int j, int k)
                : base(v)
            {
                //I = i;
                J = j;
                //K = k;
            }

            //public int I { get; set; }
            public int J { get; set; }
            //public int K { get; set; }
        }

        internal interface ICreateVectorExt<in T, out S>
        {
            S Create(T v, int i, int j, int k);
        }

        internal class CreateVector2DExt : ICreateVectorExt<Vector2D, Vector2DExt>
        {
            public Vector2DExt Create(Vector2D v, int i, int j, int k)
            {
                return new Vector2DExt(v, i, j, k);
            }
        }

        internal class CreateVector3DExt : ICreateVectorExt<Vector3D, Vector3DExt>
        {
            public Vector3DExt Create(Vector3D v, int i, int j, int k)
            {
                return new Vector3DExt(v, i, j, k);
            }
        }

        internal interface IRotation<T>
        {
            void Set(T v);
            T ToE1(T v);
            T FromE1(T v);
        }

        internal class Rotation2D : IRotation<Vector2D>
        {
            private double _angle;


            public void Set(Vector2D v)
            {
                _angle = Vector2D.E1.Angle(v);
            }

            public Vector2D ToE1(Vector2D v)
            {
                return v.Rotate(-_angle);
            }

            public Vector2D FromE1(Vector2D v)
            {
                return v.Rotate(_angle);
            }
        }

        internal class Rotation3D : IRotation<Vector3D>
        {
            private double _angle;
            private Vector3D _axis;


            public void Set(Vector3D v)
            {
                _axis = (Vector3D.E1 ^ v).Normalized();
                _angle = Vector3D.E1.Angle(v);
            }

            public Vector3D ToE1(Vector3D v)
            {
                return v.Rotate(_axis, -_angle);
            }

            public Vector3D FromE1(Vector3D v)
            {
                return v.Rotate(_axis, _angle);
            }
        }
    }
}