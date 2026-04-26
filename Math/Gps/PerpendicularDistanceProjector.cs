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
    /// Pipeline stage 2 of <see cref="ANeighbourDistanceCalculator.Analyze(FlatTrack, double)"/>:
    /// for each candidate (reference, current) point pairing produced by the grid lookup, snap
    /// the current point onto the closer of the two adjacent reference segments and recompute the
    /// distance metric and reference-track arc-length parameterisation. Pure, stateless helper.
    /// </summary>
    internal static class PerpendicularDistanceProjector
    {
        public static IList<List<NeighbourDistancePoint>> Project(
            IEnumerable<List<NeighbourDistancePoint>> neighboursCur,
            FlatTrack trackRef,
            FlatTrack trackCur)
        {
            var adjustedNeighboursCur = new List<List<NeighbourDistancePoint>>();
            foreach (var points in neighboursCur)
            {
                var newPoints = new List<NeighbourDistancePoint>();
                foreach (var d in points)
                {
                    var ir = d.Reference;
                    var ic = d.Current;
                    var refDp = trackRef.Track[ir];
                    var curDp = trackCur.Track[ic];
                    var list = new List<NeighbourDistancePoint>();
                    if (ir > 0)
                    {
                        var f = Geometry.PerpendicularSegmentParameter(trackRef.Track[ir - 1], refDp, curDp);
                        list.Add(new NeighbourDistancePoint(ir - 1, ic,
                            Geometry.PerpendicularSegmentDistance(trackRef.Track[ir - 1], refDp, curDp),
                            f, (1.0 - f) * trackRef.Distance[ir - 1] + f * trackRef.Distance[ir]));
                    }

                    if (ir + 1 < trackRef.Track.Count)
                    {
                        var f = Geometry.PerpendicularSegmentParameter(refDp, trackRef.Track[ir + 1], curDp);
                        list.Add(new NeighbourDistancePoint(ir, ic,
                            Geometry.PerpendicularSegmentDistance(refDp, trackRef.Track[ir + 1], curDp),
                            f, (1.0 - f) * trackRef.Distance[ir] + f * trackRef.Distance[ir + 1]));
                    }

                    if (list.Any())
                    {
                        var q = list.OrderBy(a => a.MinDistance).First();
                        if (Comparison.IsEqual(q.Fraction, 1.0) && q.Reference + 2 < trackRef.Track.Count)
                        {
                            q = new NeighbourDistancePoint(q.Reference + 1, q.Current, q.MinDistance, 0.0,
                                q.RefDistance);
                        }

                        if (newPoints.All(w => w.Reference != q.Reference))
                        {
                            newPoints.Add(q);
                        }
                    }
                }

                newPoints.Sort((p0, p1) => p0.MinDistance.CompareTo(p1.MinDistance));
                adjustedNeighboursCur.Add(newPoints);
            }

            return adjustedNeighboursCur;
        }
    }
}
