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

namespace Tools.Gps
{
    public class TrackSegment
    {
        public TrackSegment(int id, List<int> indices, int first, int last, int segFirst, int segLast, double length,
            double coverageFactor,
            double commonFactor, double direction)
        {
            Indices = indices;
            First = first;
            Last = last;
            SegmentFirst = segFirst;
            SegmentLast = segLast;
            Id = id;
            Length = length;
            Coverage = coverageFactor;
            Common = commonFactor;
            Direction = direction;
        }

        public List<int> Indices { get; private set; }
        public int First { get; private set; }
        public int Last { get; private set; }
        public int SegmentFirst { get; private set; }
        public int SegmentLast { get; private set; }
        public int Id { get; private set; }
        public double Length { get; private set; }
        public double Coverage { get; private set; }
        public double Common { get; private set; }
        public double Direction { get; private set; }
    }
}