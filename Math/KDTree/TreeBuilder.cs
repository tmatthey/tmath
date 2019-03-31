/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2019 Thierry Matthey
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

namespace Math.KDTree
{
    public class TreeBuilder
    {
        public const int MaxNLeaf = 3;

        public static ITree<Vector2D> Build(IList<Vector2D> list, int maxLeaf = MaxNLeaf)
        {
            return Build<Vector2D, Vector2D>(list, maxLeaf);
        }

        public static ITree<Vector2D> Build(IList<Segment2D> list, int maxLeaf = MaxNLeaf)
        {
            return Build<Vector2D, Segment2D>(list, maxLeaf);
        }

        public static ITree<Vector3D> Build(IList<Vector3D> list, int maxLeaf = MaxNLeaf)
        {
            return Build<Vector3D, Vector3D>(list, maxLeaf);
        }

        public static ITree<Vector3D> Build(IList<Segment3D> list, int maxLeaf = MaxNLeaf)
        {
            return Build<Vector3D, Segment3D>(list, maxLeaf);
        }


        public static ITree<T> Build<T, S>(IEnumerable<S> list, int maxLeaf = MaxNLeaf)
            where T : IArray, IDimension
            where S : IArray, IDimension, IBoundingFacade<T>
        {
            return Build<T, S>(list.Select((t, i) => new KeyValuePair<S, int>(t, i)).ToList(), 0, maxLeaf);
        }

        private static ITree<T> Build<T, S>(IEnumerable<KeyValuePair<S, int>> data, int depth, int maxLeaf)
            where T : IArray, IDimension
            where S : IArray, IDimension, IBoundingFacade<T>
        {
            var list = data as IList<KeyValuePair<S, int>> ?? data.ToList();
            if (list.Any() == false)
                return new EmptyTree<T>();

            if (list.Count <= maxLeaf)
            {
                return new Tree<T, S>(depth, list.Select(item => item.Key).ToList(), double.NaN,
                    list.Select(item => item.Value).ToList(), new EmptyTree<T>(), new EmptyTree<T>());
            }

            var k = list.First().Key.Dimensions;
            var l = list.First().Key.Array.Length;
            var dim0 = depth % k;
            if (k == l)
            {
                var sorted = list.OrderBy(p => p.Key[dim0]);

                var index = sorted.Count() / 2;
                var median = sorted.ElementAt(index);

                var leftTree = Build<T, S>(sorted.Take(index), depth + 1, maxLeaf);
                var rightTree = Build<T, S>(sorted.Skip(index + 1), depth + 1, maxLeaf);
                return new Tree<T, S>(depth, new List<S> {median.Key}, median.Key[dim0], new List<int> {median.Value},
                    leftTree, rightTree);
            }
            else
            {
                var dim1 = (dim0 + k) % l;
                var sorted = list.OrderBy(p => p.Key[dim0] + p.Key[dim1]);

                var index = sorted.Count() / 2;
                var median = sorted.ElementAt(index);
                var left = new List<KeyValuePair<S, int>>();
                var right = new List<KeyValuePair<S, int>>();
                var medianValue = 0.5 * (median.Key[dim0] + median.Key[dim1]);
                var medians = new List<KeyValuePair<S, int>> {median};
                foreach (var p in list)
                {
                    if (median.Value == p.Value)
                        continue;
                    var min = System.Math.Min(p.Key[dim0], p.Key[dim1]);
                    var max = System.Math.Max(p.Key[dim0], p.Key[dim1]);
                    if (min > medianValue)
                    {
                        right.Add(p);
                    }
                    else if (max < medianValue)
                    {
                        left.Add(p);
                    }
                    else
                    {
                        medians.Add(p);
                    }
                }

                if (right.Count < maxLeaf && left.Count < maxLeaf)
                    return new Tree<T, S>(depth, list.Select(item => item.Key).ToList(), double.NaN,
                        list.Select(item => item.Value).ToList(), new EmptyTree<T>(), new EmptyTree<T>());

                var leftTree = Build<T, S>(left, depth + 1, maxLeaf);
                var rightTree = Build<T, S>(right, depth + 1, maxLeaf);
                return new Tree<T, S>(depth, medians.Select(item => item.Key).ToList(), medianValue,
                    medians.Select(item => item.Value).ToList(), leftTree, rightTree);
            }
        }
    }
}