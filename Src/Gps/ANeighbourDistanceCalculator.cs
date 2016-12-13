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

namespace Math.Gps
{
    //
    // Aggregates all points, which are at least as close (perpendicular distance) 
    // as given radius to the given reference track.
    //
    public abstract class ANeighbourDistanceCalculator
    {
        protected FlatTrack _flatTrack;
        protected GridLookup _gridLookup;

        public FlatTrack ReferenceFlattendTrack
        {
            get { return _flatTrack; }
        }

        public NeighbourDistance Analyze(FlatTrack trackCur, double radius)
        {
            var neighboursCur = _gridLookup.Find(trackCur.Track, trackCur.Displacement, radius);
            var adjustedNeighboursCur = PerpendicularDistance(neighboursCur, _gridLookup.FlattendTrack, trackCur);
            var cutNeighboursCutoff = CutOffDistance(adjustedNeighboursCur, radius);
            var cutNeighboursCur = RemoveDisconnectedPoints(radius, cutNeighboursCutoff, _gridLookup.FlattendTrack);

            return new NeighbourDistance(trackCur, cutNeighboursCur);
        }

        public NeighbourDistance Analyze(IList<Vector2D> current, double radius)
        {
            return Analyze(new FlatTrack(current), radius);
        }

        private static IList<List<NeighbourDistancePoint>> CutOffDistance(
            IEnumerable<List<NeighbourDistancePoint>> neighboursCur, double radius)
        {
            return
                neighboursCur.Select(
                        points =>
                            (from d in points where d.MinDistance <= radius select new NeighbourDistancePoint(d))
                                .ToList())
                    .Where(newPoints => newPoints.Count > 0)
                    .ToList();
        }

        private static IList<List<NeighbourDistancePoint>> PerpendicularDistance(
            IEnumerable<List<NeighbourDistancePoint>> neighboursCur,
            FlatTrack trackRef,
            FlatTrack trackCur)
        {
            var adjsuteddNeighboursCur = new List<List<NeighbourDistancePoint>>();
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
                            f, (1.0 - f)*trackRef.Distance[ir - 1] + f*trackRef.Distance[ir]));
                    }
                    if (ir + 1 < trackRef.Track.Count)
                    {
                        var f = Geometry.PerpendicularSegmentParameter(refDp, trackRef.Track[ir + 1], curDp);
                        list.Add(new NeighbourDistancePoint(ir, ic,
                            Geometry.PerpendicularSegmentDistance(refDp, trackRef.Track[ir + 1], curDp),
                            f, (1.0 - f)*trackRef.Distance[ir] + f*trackRef.Distance[ir + 1]));
                    }

                    if (list.Any())
                    {
                        var q = list.OrderBy(a => a.MinDistance).First();
                        if (Comparison.IsEqual(q.Fraction, 1.0) && q.Reference + 1 < trackRef.Track.Count)
                        {
                            q = new NeighbourDistancePoint(q.Reference+1, q.Current, q.MinDistance, 0.0, q.RefDistance);
                        }
                        if (newPoints.All(w => w.Reference != q.Reference))
                        {
                            newPoints.Add(q);
                        }
                    }
                }
                newPoints.Sort((p0, p1) => p0.MinDistance.CompareTo(p1.MinDistance));
                adjsuteddNeighboursCur.Add(newPoints);
            }
            return adjsuteddNeighboursCur;
        }

        // Removed faulty neighbor ref. points from list, e.g., detours, cross-overs, 
        // opposite direction, mix-up of start and end of track, etc. 
        private static IList<List<NeighbourDistancePoint>> RemoveDisconnectedPoints(double radius,
            IEnumerable<List<NeighbourDistancePoint>> neighboursCur,
            FlatTrack trackRef)
        {
            var index = 0; // Current average of ref. point index
            var reducedNeighboursCur = new List<List<NeighbourDistancePoint>>();
            foreach (var points in neighboursCur)
            {
                var refList = points.Select(p => p.Reference).ToList();
                refList.Sort();

                // Group ref. points into segments such that distance <= radius to next point  
                var segments = new List<List<int>>();
                for (var i = 1; i < refList.Count;)
                {
                    if (Comparison.IsLessEqual(radius,
                        trackRef.Distance[refList[i]] -
                        trackRef.Distance[refList[i - 1]]))
                    {
                        // Disconnected on ref. track, e.g., ref. track takes a detour 
                        segments.Add(refList.GetRange(0, i));
                        refList.RemoveRange(0, i);
                        i = 1;
                    }
                    else
                    {
                        i++;
                    }
                }
                // Find average ref. index of each segment
                if (refList.Count > 0)
                {
                    segments.Add(refList);
                }
                var segmentAvg =
                    (from s in segments let sum = s.Aggregate(0.0, (current1, t) => current1 + t) select sum/s.Count)
                        .ToList();
                var segmentDiff =
                    segments.Select(
                            s =>
                                System.Math.Abs(
                                    (int) (s.Aggregate(0.0, (current1, d) => current1 + d)/s.Count - index)))
                        .ToList();
                // Pick segment closest to previous index average
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