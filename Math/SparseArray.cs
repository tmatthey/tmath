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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Math

{
    public class SparseArray<T> : IList<T>
    {
        private readonly Dictionary<int, T> _table = new Dictionary<int, T>();

        public IEnumerator<T> GetEnumerator()
        {
            return _table.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _table.Count;

        public bool IsReadOnly => false;

        public void Add(T value)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int index)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(T value)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, T value)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T value)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            _table.Remove(index);
        }

        public bool Contains(T value)
        {
            return _table.ContainsValue(value);
        }

        public void Clear()
        {
            _table.Clear();
        }

        public T this[int index]
        {
            get => ContainsKey(index) ? _table[index] : default(T);
            set
            {
                if (ContainsKey(index))
                {
                    _table[index] = value;
                }
                else
                {
                    _table.Add(index, value);
                }
            }
        }

        public IList<int> Indices()
        {
            return _table.Keys.OrderBy(num => num).ToList();
        }

        public bool ContainsKey(int index)
        {
            return _table.ContainsKey(index);
        }
    }
}