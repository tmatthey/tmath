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
    public class NeighbourDistanceCalculator
    {
        public NeighbourDistanceCalculator(IList<GpsPoint> reference, double gridSize)
        {
            Reference = new GpsTrack(reference);
            Reference.SetupLookup(Reference.Center, gridSize);
        }

        public NeighbourDistanceCalculator(IList<GpsPoint> reference)
            : this(reference, 50.0)
        {
        }

        public GpsTrack Reference { get; private set; }

        public NeighbourDistance Analyze(IList<GpsPoint> current, double radius)
        {
            var gpsTrackCur = new GpsTrack(current);
            var trackCur = new Transformer(gpsTrackCur.Track, Reference.Center);

            var neighboursCur = Reference.Lookup.Find(trackCur.Track, trackCur.Displacement, radius);
            var adjustedNeighboursCur = PerpendicularDistance(neighboursCur, Reference.TransformedTrack, trackCur);
            var cutNeighboursCutoff = CutOffDistance(adjustedNeighboursCur, radius);
            var cutNeighboursCur = RemoveDisconnectedPoints(radius, cutNeighboursCutoff, Reference.TransformedTrack);

            return new NeighbourDistance(current, trackCur.Track, cutNeighboursCur, trackCur.Distance,
                trackCur.Displacement);
        }

        private static IEnumerable<List<NeighbourDistancePoint>> CutOffDistance(
            IEnumerable<List<NeighbourDistancePoint>> neighboursCur, double radius)
        {
            return
                neighboursCur.Select(
                    points =>
                        (from d in points where d.MinDistance <= radius select new NeighbourDistancePoint(d)).ToList())
                    .Where(newPoints => newPoints.Count > 0)
                    .ToList();
        }

        private static IEnumerable<List<NeighbourDistancePoint>> PerpendicularDistance(
            IEnumerable<List<NeighbourDistancePoint>> neighboursCur,
            Transformer trackRef,
            Transformer trackCur)
        {
            var adjsuteddNeighboursCur = new List<List<NeighbourDistancePoint>>();
            foreach (var points in neighboursCur)
            {
                var newPoints = new List<NeighbourDistancePoint>();
                foreach (var d in points)
                {
                    var d0 = d;
                    var d1 = d;
                    var ir = d.Reference;
                    var ic = d.Current;
                    var refDp = trackRef.Track[ir];
                    var curDp = trackCur.Track[ic];
                    if (ir > 0)
                    {
                        var f = Geometry.PerpendicularSegmentParameter(trackRef.Track[ir - 1], refDp, curDp);
                        d0 = new NeighbourDistancePoint(ir - 1, ic,
                            Geometry.PerpendicularSegmentDistance(trackRef.Track[ir - 1], refDp, curDp),
                            f, (f - 1.0)*trackRef.Distance[ir - 1] + f*trackRef.Distance[ir]);
                    }
                    if (ir + 1 < trackRef.Track.Count)
                    {
                        var f = Geometry.PerpendicularSegmentParameter(refDp, trackRef.Track[ir + 1], curDp);
                        d1 = new NeighbourDistancePoint(ir, ic,
                            Geometry.PerpendicularSegmentDistance(refDp, trackRef.Track[ir + 1], curDp),
                            f, (f - 1.0)*trackRef.Distance[ir] + f*trackRef.Distance[ir + 1]);
                    }
                    var dNew = (d0.MinDistance < d1.MinDistance ? d0 : d1);

                    if (newPoints.All(q => q.Reference != dNew.Reference))
                    {
                        newPoints.Add(dNew);
                    }
                }
                newPoints.Sort((p0, p1) => p0.MinDistance.CompareTo(p1.MinDistance));
                adjsuteddNeighboursCur.Add(newPoints);
            }
            return adjsuteddNeighboursCur;
        }

        private static IList<List<NeighbourDistancePoint>> RemoveDisconnectedPoints(double radius,
            IEnumerable<List<NeighbourDistancePoint>> neighboursCur,
            Transformer trackRef)
        {
            var index = 0;
            var reducedNeighboursCur = new List<List<NeighbourDistancePoint>>();
            foreach (var points in neighboursCur)
            {
                var refList = points.Select(p => p.Reference).ToList();
                refList.Sort();

                var segments = new List<List<int>>();
                for (var i = 1; i < refList.Count;)
                {
                    if (Comparison.IsLessEqual(radius,
                        trackRef.Distance[refList[i]] -
                        trackRef.Distance[refList[i - 1]]))
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
                var segmentAvg =
                    (from s in segments let sum = s.Aggregate(0.0, (current1, t) => current1 + t) select sum/s.Count)
                        .ToList();
                var segmentDiff =
                    segments.Select(
                        s => (System.Math.Abs(s.Aggregate(0.0, (current1, d) => current1 + d)/s.Count - index)))
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