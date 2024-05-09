/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2024 Thierry Matthey
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

namespace Math.Gps.Filters
{
    public abstract class FilterDuplicates : IFilter
    {
        protected FilterDuplicates()
        {
            Clear();
        }

        public double NewAccelerationVariance { get; private set; }
        public double OldAccelerationVariance { get; private set; }

        public double NewVelocityVariance { get; private set; }
        public double OldVelocityVariance { get; private set; }
        public List<GpsPointExt> List { get; private set; }

        public bool HasDetected()
        {
            return List.Count > 0;
        }

        public abstract int Takes();

        public void Filter(List<GpsPoint> track, List<double> time, IList<int> startIdx, List<int> endIdx)
        {
            Clear();
            if (Takes() <= startIdx.Count &&
                Takes() <= endIdx.Count &&
                0 < startIdx[0] &&
                endIdx[Takes() - 1] + 1 < track.Count)
            {
                List = Interpolate(track, time, startIdx, endIdx);
                if (List.Count > 0)
                {
                    var index = List.First().I;
                    var count = List.Last().I - index + 1;
                    Tools.Variance(List, time.GetRange(index, count), out var vel, out var acc);
                    NewVelocityVariance = vel;
                    NewAccelerationVariance = acc;
                    Tools.Variance(track.GetRange(index, count), time.GetRange(index, count), out vel, out acc);
                    OldVelocityVariance = vel;
                    OldAccelerationVariance = acc;
                }
            }
        }

        protected abstract List<GpsPointExt> Interpolate(IList<GpsPoint> track, IList<double> time, IList<int> startIdx,
            List<int> endIdx);

        private void Clear()
        {
            NewVelocityVariance = double.PositiveInfinity;
            NewAccelerationVariance = double.PositiveInfinity;
            OldVelocityVariance = double.PositiveInfinity;
            OldAccelerationVariance = double.PositiveInfinity;
            List = new List<GpsPointExt>();
        }
    }
}