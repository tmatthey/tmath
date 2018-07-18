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
using NUnit.Framework;
using Shouldly;

namespace Math.Tests
{
    [TestFixture]
    public class SparseArrayTests
    {
        [Test]
        public void Clear_WithNonEmptyArray_EmptyArray()
        {
            var a = new SparseArray<int>();
            a[1] = 17;
            a.Count.ShouldBeGreaterThan(0);
            a.Clear();
            a.Count.ShouldBe(0);
        }

        [Test]
        public void Constructor()
        {
            var a = new SparseArray<int>();
            a.Count.ShouldBe(0);
        }

        [Test]
        public void Containes_ValueContained_ReturnsTrue()
        {
            var expected = 23;
            var index = 17;
            var a = new SparseArray<int>();
            a[index] = expected;
            a.Contains(expected).ShouldBe(true);
        }

        [Test]
        public void Containes_ValueNotContained_ReturnsFalse()
        {
            var expected = 23;
            var index = 17;
            var a = new SparseArray<int>();
            a[index] = expected;
            a.Contains(expected + 1).ShouldBe(false);
        }

        [Test]
        public void Enumerator_Iterates()
        {
            var expected1 = 23;
            var index1 = 19;
            var expected2 = 31;
            var index2 = 9;
            var a = new SparseArray<int>();
            a[index2] = expected2;
            a[index1] = expected1;
            var list = new List<int>();
            foreach (var i in a)
            {
                list.Add(i);
            }


            list.Count.ShouldBe(2);
        }

        [Test]
        public void Indices_ReturnsList()
        {
            var expected1 = 23;
            var index1 = 19;
            var expected2 = 31;
            var index2 = 9;
            var a = new SparseArray<int>();
            a[index2] = expected2;
            a[index1] = expected1;
            var list = a.Indices();
            list.Count.ShouldBe(2);
            list[0].ShouldBe(index2);
            list[1].ShouldBe(index1);
        }

        [Test]
        public void NotImplementedMethods_ThrowNotImplementedException()
        {
            var a = new SparseArray<int>();
            Should.Throw<NotImplementedException>(() => a.Add(1));
            Should.Throw<NotImplementedException>(() => a.CopyTo(new[] {1, 2, 3}, 0));
            Should.Throw<NotImplementedException>(() => a.IndexOf(1));
            Should.Throw<NotImplementedException>(() => a.Insert(1, 3));
            Should.Throw<NotImplementedException>(() => a.Remove(1));
        }

        [Test]
        public void OverrideDefinitions()
        {
            var a = new SparseArray<int>();
            a.IsReadOnly.ShouldBe(false);
            ((IEnumerable) a).GetEnumerator().ShouldNotBe(null);
        }

        [Test]
        public void RemoveAt()
        {
            var expected1 = 23;
            var index1 = 19;
            var expected2 = 31;
            var index2 = 17;
            var a = new SparseArray<int>();
            a[index1] = expected1;
            a[index2] = expected2;

            a.Count.ShouldBe(2);
            a.RemoveAt(index1);
            a.Count.ShouldBe(1);
            a.RemoveAt(index1);
            a.Count.ShouldBe(1);
            a.RemoveAt(index2);
            a.Count.ShouldBe(0);
        }

        [Test]
        public void SetGetValue_ValueAtExpectedIndex()
        {
            var expected = 23;
            var index = 17;
            var a = new SparseArray<int>();
            a[index] = expected;
            a.Count.ShouldBe(1);
            a[index].ShouldBe(expected);
        }

        [Test]
        public void SetTwiceGetValue_ValueAtExpectedIndex()
        {
            var expected = 23;
            var index = 17;
            var a = new SparseArray<int>();
            a[index] = expected - 1;
            a[index] = expected;
            a.Count.ShouldBe(1);
            a[index].ShouldBe(expected);
        }
    }
}