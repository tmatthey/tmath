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

namespace Math
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

        public double Entropy(double eps, out int n)
        {
            n = int.MaxValue;
            if (_tree == null)
                _tree = TreeBuilder.Build<T, S>(_list);
            if (_data == null)
            {
                _data = new List<Point>();
                foreach (var t in _list)
                    _data.Add(new Point(t));
            }
            var list = new List<int>();
            var sum = 0.0;
            foreach (var p in _data)
            {
                var neighbors = EpsNeighborhood(p, eps).Count();
                list.Add(neighbors);
                sum += neighbors;
            }
            var entropy = 0.0;
            foreach (var a in list)
            {
                entropy -= a/sum*System.Math.Ceiling(System.Math.Log(a/sum, 2));
            }
            if (_data.Count() > 0)
                n = (int) System.Math.Ceiling(sum/_data.Count());

            return entropy;
        }

        public void Estimate(out double eps, out int n)
        {
            eps = double.PositiveInfinity;
            n = int.MaxValue;
            var minEntropy = double.PositiveInfinity;
            for (var e = 10.0; e < 40.0; e += 1.0)
            {
                int n0;
                var entropy = Entropy(e, out n0);
                if (entropy < minEntropy)
                {
                    minEntropy = entropy;
                    eps = e;
                    n = n0;
                    ;
                }
            }
        }

        public IEnumerable<IList<int>> Cluster(double eps = 25.0, int n = 5)
        {
            // Corner cases
            if (_list.Count < n)
                return new List<List<int>>();

            // Intialize
            if (_tree == null)
                _tree = TreeBuilder.Build<T, S>(_list);
            if (_data == null)
            {
                _data = new List<Point>();
                foreach (var t in _list)
                    _data.Add(new Point(t));
            }
            else
            {
                foreach (var t in _data)
                {
                    t.Visited = false;
                    t.ClusterId = Classification.Unclassified;
                }
            }

            // Search
            var clusterId = 0;
            foreach (var p in _data)
            {
                if (p.Visited)
                    continue;

                p.Visited = true;
                var allNeighbors = EpsNeighborhood(p, eps).ToList();
                if (allNeighbors.Count < n)
                {
                    p.ClusterId = Classification.Noise;
                    continue;
                }

                p.ClusterId = clusterId;
                for (var j = 0; j < allNeighbors.Count; j++)
                {
                    var pn = _data[allNeighbors[j]];
                    if (!pn.Visited)
                    {
                        pn.Visited = true;
                        var neighbors = EpsNeighborhood(pn, eps).ToList();
                        if (neighbors.Count() >= n)
                        {
                            allNeighbors = allNeighbors.Union(neighbors).ToList();
                        }
                    }
                    if (pn.ClusterId == Classification.Unclassified)
                        pn.ClusterId = clusterId;
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

        private IEnumerable<int> EpsNeighborhood(Point seed, double eps)
        {
            var bounding = seed.Value.Bounding();
            bounding.ExpandLayer(eps);
            var inside = _tree.Search(bounding.Min, bounding.Max).Distinct();


            return inside.Where(
                index => Comparison.IsLessEqual(seed.Value.ModifiedNorm(_data[index].Value), eps));
        }

        private class Point
        {
            public readonly S Value;
            public int ClusterId;
            public bool Visited;

            public Point(S value)
            {
                Value = value;
                Visited = false;
                ClusterId = Classification.Unclassified;
            }
        }

        private static class Classification
        {
            public const int Unclassified = -2;
            public const int Noise = -1;
        };
    }
}