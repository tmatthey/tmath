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

namespace Math.Gps
{
    /// <summary>
    /// Pipeline stage 4 of <see cref="ANeighbourDistanceCalculator.Analyze(FlatTrack, double)"/>:
    /// remove faulty neighbour reference points - detours, cross-overs, opposite direction matches,
    /// start/end mix-ups - by grouping points into arc-length-connected segments and keeping the
    /// segment whose centroid is closest to the rolling reference index. Pure, stateless helper.
    /// </summary>
    internal static class DisconnectedPointPruner
    {
        public static IList<List<NeighbourDistancePoint>> Prune(double radius,
            IEnumerable<List<NeighbourDistancePoint>> neighboursCur,
            FlatTrack trackRef)
        {
            var index = 0; // Current rolling average of ref. point index
            var reducedNeighboursCur = new List<List<NeighbourDistancePoint>>();
            foreach (var points in neighboursCur)
            {
                var refList = points.Select(p => p.Reference).ToList();
                refList.Sort();

                // Group ref. points into segments such that consecutive arc-length distance < radius
                var segments = new List<List<int>>();
                for (var i = 1; i < refList.Count;)
                {
                    if (Comparison.IsLessEqual(radius,
                        trackRef.Distance[refList[i]] - trackRef.Distance[refList[i - 1]]))
                    {
                        segments.Add(refList.GetRange(0, i));
                        refList.RemoveRange(0, i);
                        i = 1;
                    }
                    else
                    {
                        i++;
                    }
                }

                if (refList.Count > 0)
                {
                    segments.Add(refList);
                }

                // Empty input - nothing to add for this point cloud.
                if (segments.Count == 0)
                {
                    continue;
                }

                var segmentAvg =
                    (from s in segments let sum = s.Aggregate(0.0, (current1, t) => current1 + t) select sum / s.Count)
                    .ToList();
                var segmentDiff = segments
                    .Select(s => System.Math.Abs(
                        (int) (s.Aggregate(0.0, (current1, d) => current1 + d) / s.Count - index)))
                    .ToList();
                var minSegmentIndex = segmentDiff.IndexOf(segmentDiff.Min());
                var newPoint = segments[minSegmentIndex].Select(s => points.First(p => p.Reference == s)).ToList();
                newPoint.Sort((p0, p1) => p0.MinDistance.CompareTo(p1.MinDistance));
                reducedNeighboursCur.Add(newPoint);
                index = (int) segmentAvg[minSegmentIndex];
            }

            return reducedNeighboursCur;
        }
    }
}
