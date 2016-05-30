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
    internal class Tree<T, S> : ITree<T> where T : IArray where S : IArray, IDimension
    {
        private readonly double _cut;
        private readonly int _dim;
        private readonly int _index;
        private readonly ITree<T> _left;
        private readonly double[] _max;
        private readonly double[] _min;
        private readonly ITree<T> _right;

        internal Tree(int depth, S key, double cut, int index, ITree<T> left, ITree<T> right)
        {
            _dim = depth%key.Dimensions;
            _cut = cut;
            if (key.Dimensions == key.Array.Length)
            {
                _min = key.Array;
                _max = key.Array;
            }
            else
            {
                _min = new double[key.Dimensions];
                _max = new double[key.Dimensions];
                for (var i = 0; i < key.Dimensions; i++)
                {
                    _min[i] = System.Math.Min(key[i], key[i + key.Dimensions]);
                    _max[i] = System.Math.Max(key[i], key[i + key.Dimensions]);
                }
            }
            _index = index;
            _left = left;
            _right = right;
        }

        public IEnumerable<int> Search(T min, T max)
        {
            if (Compare(min[_dim], _cut) <= 0)
            {
                foreach (var index in _left.Search(min, max))
                    yield return index;
            }

            if (_min.Select((coord, i) => new {coord, i}).All(x =>
                Compare(min[x.i], _max[x.i]) <= 0 &&
                Compare(_min[x.i], max[x.i]) <= 0))
                yield return _index;

            if (Compare(max[_dim], _cut) >= 0)
            {
                foreach (var index in _right.Search(min, max))
                    yield return index;
            }
        }

        private static int Compare(double p1, double p2)
        {
            return p1.CompareTo(p2);
        }
    }
}