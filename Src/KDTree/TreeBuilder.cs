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

namespace Math.KDTree
{
    public class TreeBuilder
    {
        public static ITree<Vector2D> Build(IList<Vector2D> list)
        {
            return Build<Vector2D, Vector2D>(list);
        }

        public static ITree<Vector2D> Build(IList<Segment2D> list)
        {
            return Build<Vector2D, Segment2D>(list);
        }

        public static ITree<Vector3D> Build(IList<Vector3D> list)
        {
            return Build<Vector3D, Vector3D>(list);
        }

        public static ITree<Vector3D> Build(IList<Segment3D> list)
        {
            return Build<Vector3D, Segment3D>(list);
        }

        private static ITree<T> Build<T, S>(IEnumerable<S> list)
            where T : IArray
            where S : IArray, IDimension
        {
            return Build<T, S>(list.Select((t, i) => new KeyValuePair<S, int>(t, i)).ToList(), 0);
        }

        private static ITree<T> Build<T, S>(IEnumerable<KeyValuePair<S, int>> data, int depth)
            where T : IArray
            where S : IArray, IDimension
        {
            if (data.Any() == false)
                return new EmptyTree<T>();

            var k = data.First().Key.Dimensions;
            var l = data.First().Key.Array.Length;
            var dim0 = depth%k;
            if (k == l)
            {
                var sorted = data.OrderBy(p => p.Key[dim0]);

                var index = sorted.Count()/2;
                var median = sorted.ElementAt(index);

                var leftTree = Build<T, S>(sorted.Take(index), depth + 1);
                var rightTree = Build<T, S>(sorted.Skip(index + 1), depth + 1);
                return new Tree<T, S>(depth, median.Key, median.Key[dim0], median.Value, leftTree, rightTree);
            }
            else
            {
                var dim1 = (dim0 + k)%l;
                var sorted = data.OrderBy(p => System.Math.Min(p.Key[dim0], p.Key[dim1]));

                var median = sorted.ElementAt(sorted.Count()/2);
                var left = new List<KeyValuePair<S, int>>();
                var rigth = new List<KeyValuePair<S, int>>();
                var medianValue = System.Math.Min(median.Key[dim0], median.Key[dim1]);

                foreach (var p in data)
                {
                    if (median.Value == p.Value)
                        continue;
                    var min = System.Math.Min(p.Key[dim0], p.Key[dim1]);
                    var max = System.Math.Max(p.Key[dim0], p.Key[dim1]);
                    if (max > medianValue)
                    {
                        rigth.Add(p);
                    }
                    if (min <= medianValue)
                    {
                        left.Add(p);
                    }
                }
                var leftTree = Build<T, S>(left, depth + 1);
                var rightTree = Build<T, S>(rigth, depth + 1);
                return new Tree<T, S>(depth, median.Key, medianValue, median.Value, leftTree, rightTree);
            }
        }
    }
}