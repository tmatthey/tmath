/*
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

namespace Math.KDTree
{
    /// <summary>
    /// Builds k-d trees for any payload type that exposes an axis-aligned bounding box via
    /// <see cref="IBoundingFacade{T}"/>. The OCP entry point is the generic
    /// <see cref="Build{T,S}(IEnumerable{S}, int)"/>; the four typed overloads below are pure
    /// ergonomic shortcuts so callers do not have to spell out the type parameters for the
    /// first-party Vector/Segment combinations.
    /// </summary>
    public static class TreeBuilder
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

        /// <summary>
        /// Generic builder accepting any payload <typeparamref name="S"/> that can produce a
        /// <typeparamref name="T"/>-valued bounding box. Adding a new geometric primitive to the
        /// k-d tree only requires implementing <see cref="IBoundingFacade{T}"/> on it - the
        /// builder itself does not need to change (Open/Closed Principle).
        /// </summary>
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
            if (!list.Any())
                return new EmptyTree<T>();

            if (list.Count <= maxLeaf)
            {
                return new Tree<T, S>(depth, list.Select(item => item.Key).ToList(), double.NaN,
                    list.Select(item => item.Value).ToList(), new EmptyTree<T>(), new EmptyTree<T>());
            }

            var k = list.First().Key.Dimensions;
            var l = list.First().Key.ToArray().Length;
            var dim0 = depth % k;
            if (k == l)
            {
                // Materialize the sorted sequence ONCE; previously OrderBy + Count + ElementAt
                // + Take + Skip walked and sorted the IOrderedEnumerable up to four times.
                var sorted = list.OrderBy(p => p.Key[dim0]).ToList();

                var index = sorted.Count / 2;
                var median = sorted[index];

                var leftTree = Build<T, S>(sorted.GetRange(0, index), depth + 1, maxLeaf);
                var rightTree = Build<T, S>(sorted.GetRange(index + 1, sorted.Count - index - 1), depth + 1, maxLeaf);
                return new Tree<T, S>(depth, new List<S> {median.Key}, median.Key[dim0], new List<int> {median.Value},
                    leftTree, rightTree);
            }
            else
            {
                var dim1 = (dim0 + k) % l;
                var sorted = list.OrderBy(p => p.Key[dim0] + p.Key[dim1]).ToList();

                var index = sorted.Count / 2;
                var median = sorted[index];
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