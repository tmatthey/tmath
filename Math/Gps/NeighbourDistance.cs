﻿/*
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

namespace Math.Gps
{
    public class NeighbourDistance
    {
        public NeighbourDistance(FlatTrack flattendTrack, IList<List<NeighbourDistancePoint>> neighbours)
        {
            Neighbours = neighbours;
            TotalDistance = flattendTrack.Distance.LastOrDefault();
            CommonDistance = 0.0;
            FlattendTrack = flattendTrack;
            for (var i = 1; i < neighbours.Count; i++)
            {
                if (neighbours[i - 1][0].Current + 1 == neighbours[i][0].Current)
                    CommonDistance += flattendTrack.Displacement[i];
            }
        }

        public FlatTrack FlattendTrack { get; }
        public IList<List<NeighbourDistancePoint>> Neighbours { get; }
        public double TotalDistance { get; }
        public double CommonDistance { get; }
    }
}