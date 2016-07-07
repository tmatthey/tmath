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
using Math.KDTree;

namespace Math.Clustering
{
    public class DBScan<T, S>
        where T : IArray, IDimension
        where S : IArray, IDimension, INorm<S>, IBoundingFacade<T>
    {
        private readonly IList<S> _list;
        private IList<Point> _data;
        private ITree<T> _tree;

        public DBScan(IList<S> list)
        {
            _list = list;
            _tree = null;
            _data = null;
        }

        public IEnumerable<IList<int>> Cluster(double eps = 25.0, int n = 5, bool direction = false)
        {
            // Corner cases
            if (_list.Count < n)
                return new List<List<int>>();

            // Intialize
            if (_tree == null)
                _tree = TreeBuilder.Build<T, S>(_list); // new NoTree<T,S>(_list);
            if (_data == null)
            {
                _data = new List<Point>();
                foreach (var t in _list)
                    _data.Add(new Point(t));
            }
            foreach (var t in _data)
            {
                t.ClusterId = Classification.Unclassified;
            }

            // Search
            var clusterId = Classification.Classified;
            foreach (var p in _data)
            {
                if (p.ClusterId != Classification.Unclassified)
                    continue;

                var seeds = EpsNeighborhood(p, eps, direction).ToList();
                if (seeds.Count < n)
                {
                    p.ClusterId = Classification.Noise;
                    continue;
                }
                foreach (var j in seeds)
                {
                    _data[j].ClusterId = clusterId;
                }
                seeds.Remove(_data.IndexOf(p));

                while (seeds.Count > 0)
                {
                    var newSeeds = EpsNeighborhood(_data[seeds[0]], eps, direction).ToList();
                    if (newSeeds.Count >= n)
                    {
                        foreach (var j in newSeeds)
                        {
                            var pn = _data[j];
                            if (pn.ClusterId < Classification.Classified)
                            {
                                if (pn.ClusterId == Classification.Unclassified && !seeds.Contains(j))
                                    seeds.Add(j);
                                pn.ClusterId = clusterId;
                            }
                        }
                    }
                    seeds.RemoveAt(0);
                }
                clusterId++;
            }

            // Aggregate result
            var clusters = new List<List<int>>();
            for (var i = 0; i < clusterId; i++)
                clusters.Add(new List<int>());

            for (var i = 0; i < _data.Count; i++)
            {
                var j = _data[i].ClusterId;
                if (j >= 0)
                    clusters[j].Add(i);
            }

            return clusters;
        }

        private IEnumerable<int> EpsNeighborhood(Point seed, double eps, bool direction)
        {
            var bounding = seed.Value.Bounding();
            bounding.ExpandLayer(eps);
            var inside = _tree.Search(bounding.Min, bounding.Max).Distinct();

            return inside.Where(
                index => Comparison.IsLessEqual(seed.Value.ModifiedNorm(_data[index].Value, direction), eps))
                .OrderBy(num => num);
        }

        private class Point
        {
            public readonly S Value;
            public int ClusterId;

            public Point(S value)
            {
                Value = value;
                ClusterId = Classification.Unclassified;
            }
        }

        private static class Classification
        {
            public const int Unclassified = -2;
            public const int Noise = -1;
            public const int Classified = 0;
        }
    }
}