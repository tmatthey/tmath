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

namespace Math.KDTree
{
    internal class Tree<T, S> : ITree<T> where T : IArray where S : IArray, IDimension, IBoundingFacade<T>
    {
        // Order of fields affetcs runtime +/- 10%
        private readonly double _cut;
        private readonly int _dim;
        private readonly bool _rightNotEmpty;
        private readonly bool _leftNotEmpty;
        private readonly bool _skipBoundingTest;
        private readonly double[] _minMin;
        private readonly double[] _maxMax;
        private readonly IList<double[]> _min;
        private readonly IList<double[]> _max;
        private readonly IList<int> _indices;
        private readonly ITree<T> _left;
        private readonly ITree<T> _right;

        internal Tree(int depth, IList<S> keys, double cut, IList<int> indices, ITree<T> left, ITree<T> right)
        {
            _dim = depth % keys.First().Dimensions;
            _cut = cut;
            _min = keys.Select(key => key.Bounding().Min.Array).ToList();
            _max = keys.Select(key => key.Bounding().Max.Array).ToList();
            _indices = indices;
            _left = left;
            _right = right;
            _skipBoundingTest = left.Depth() + right.Depth() == 0 || _indices.Count < 3;
            if (!_skipBoundingTest)
            {
                var b = keys.First().Bounding();
                b.Reset();
                foreach (var key in keys)
                {
                    b.Expand(key.Bounding());
                }

                _minMin = b.Min.Array;
                _maxMax = b.Max.Array;
            }
            else
            {
                _minMin = null;
                _maxMax = null;
            }

            _leftNotEmpty = left.Depth() > 0;
            _rightNotEmpty = right.Depth() > 0;
        }

        public IEnumerable<int> Search(T min, T max)
        {
            if (_leftNotEmpty && min[_dim] <= _cut)
            {
                foreach (var index in _left.Search(min, max))
                    yield return index;
            }

            if (_skipBoundingTest || _minMin.Select((coord, i) => new {i}).All(x =>
                min[x.i] <= _maxMax[x.i] &&
                _minMin[x.i] <= max[x.i]))
            {
                for (var j = 0; j < _indices.Count; j++)
                    if (_min[j].Select((coord, i) => new {i}).All(x =>
                        min[x.i] <= _max[j][x.i] &&
                        _min[j][x.i] <= max[x.i]))
                        yield return _indices[j];
            }

            if (_rightNotEmpty && max[_dim] >= _cut)
            {
                foreach (var index in _right.Search(min, max))
                    yield return index;
            }
        }

        public int Depth()
        {
            return 1 + System.Math.Max(_right.Depth(), _left.Depth());
        }
    }
}