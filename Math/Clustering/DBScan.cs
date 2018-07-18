/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2018 Thierry Matthey
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
using Math.KDTree;

namespace Math.Clustering
{
    /// <summary>
    /// Density-based spatial clustering of applications with noise (DBSCAN) is a data clustering algorithm proposed by Martin Ester, Hans-Peter Kriegel, Jörg Sander and Xiaowei Xu in 1996. https://en.wikipedia.org/wiki/DBSCAN
    /// </summary>
    /// <typeparam name="T">Point type of dimension n, e.g., Vector3D</typeparam>
    /// <typeparam name="S">A geometric object of dimension n with a norm based on point type T, e.g., Segment3D</typeparam>
    public class DBScan<T, S>
        where T : IArray, IDimension
        where S : IArray, IDimension, INorm<S>, IBoundingFacade<T>
    {
        private readonly IList<S> _list;
        private IList<Point> _data;
        private ITree<T> _tree;

        /// <summary>
        /// Defining a DBScan with a list of the geometric objects to be clustered.
        /// </summary>
        /// <param name="list">List of object of dimension n with a norm based on point type T, e.g., Segment3D.</param>
        public DBScan(IList<S> list)
        {
            _list = list;
            _tree = null;
            _data = null;
        }

        /// <summary>
        /// Clustering the list of objects with a given epsilon and with a threshold.   
        /// </summary>
        /// <param name="eps">Epsilon of neighborhood between to objects using objects (modified) norm.</param>
        /// <param name="n">Minimum number of objects required to be recognized as a cluster.</param>
        /// <param name="direction">Boolean defining if the direction for directional objects shall be considered when evaluating the norm between two objects, e.g., Trajectory Hausdorff distance.</param>
        /// <returns>A list of cluster as list of object indices.</returns>
        public IList<IList<int>> Cluster(double eps = 25.0, int n = 5, bool direction = false)
        {
            // Corner cases
            if (_list.Count < n)
                return new List<IList<int>>();

            // Initialize
            if (_tree == null)
                _tree = TreeBuilder.Build<T, S>(_list); // new NoTree<T,S>(_list);
            if (_data == null)
            {
                _data = new List<Point>();
                foreach (var t in _list)
                    _data.Add(new Point(t));
            }

            foreach (var t in _data)
                t.ClusterId = Classification.UnVisited;

            // Search and collect
            var clusterId = Classification.Classified;
            var clusters = new List<IList<int>>();
            foreach (var p in _data)
            {
                if (p.ClusterId != Classification.UnVisited)
                    continue;
                p.ClusterId = Classification.Noise;

                var seeds = EpsNeighborhood(p, eps, direction);
                if (seeds.Count < n)
                    continue;

                var region = new List<int>();
                for (var i = 0; i < seeds.Count; i++)
                {
                    var j = seeds[i];
                    if (_data[j].ClusterId == Classification.UnVisited)
                    {
                        _data[j].ClusterId = Classification.Noise;
                        var newSeeds = EpsNeighborhood(_data[j], eps, direction);
                        if (newSeeds.Count >= n)
                            seeds = seeds.Union(newSeeds).ToList();
                    }

                    if (_data[j].ClusterId < Classification.Classified)
                    {
                        _data[j].ClusterId = clusterId;
                        region.Add(j);
                    }
                }

                clusters.Add(region);
                clusterId++;
            }

            return clusters;
        }

        private IList<int> EpsNeighborhood(Point seed, double eps, bool direction)
        {
            var bounding = seed.Value.Bounding();
            bounding.ExpandLayer(eps);
            var inside = _tree.Search(bounding.Min, bounding.Max).Distinct();

            return inside.Where(
                    index => Comparison.IsLessEqual(seed.Value.ModifiedNorm(_data[index].Value, direction), eps))
                .OrderBy(num => num)
                .ToList();
        }

        private class Point
        {
            public readonly S Value;
            public int ClusterId;

            public Point(S value)
            {
                Value = value;
                ClusterId = Classification.UnVisited;
            }
        }

        private static class Classification
        {
            public const int UnVisited = -2;
            public const int Noise = -1;
            public const int Classified = 0;
        }
    }
}