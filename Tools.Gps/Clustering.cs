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
using Math;
using Math.Clustering;
using Math.Gps;
using Tools.TrackReaders;

namespace Tools.Gps
{
    public static class Clustering
    {
        public static List<List<int>> FindTrackClusters(IList<Track> activities)
        {
            return FindTrackClusters((from activity in activities select activity.GpsPoints().ToList()).ToList());
        }

        public static List<List<int>> FindTrackClusters(List<List<GpsPoint>> gpsTracks)
        {
            return PolylineNeighbours.Cluster(gpsTracks);
        }

        public static List<SegmentResult> FindCommonSegments(ClusterDefinition cluster, int n, double eps, double minL,
            int cost, double epsTrack)
        {
            var db = TraClus.Cluster(cluster.Tracks, n, eps, true, minL, cost);
            var segments = new List<SegmentResult>();
            var trackIds = new List<int>();
            foreach (var segment in db)
            {
                trackIds.AddRange(segment.SegmentIndices.Indices());
                segments.Add(new SegmentResult(new List<TrackSegment>(), segment.Segment.ToList(),cluster.Center));
            }
            trackIds = trackIds.Distinct().OrderBy(num => num).ToList();
            foreach (var i in trackIds)
            {
                var flatTrack = cluster.FlatTracks[i];
                var analyzer = new NeighbourDistanceCalculator(flatTrack, eps);
                for (var k = 0; k < db.Count; k++)
                {
                    var segment = db[k];
                    if (!segment.SegmentIndices.Indices().Contains(i))
                        continue;

                    var current = analyzer.Analyze(segment.Segment, epsTrack);
                    var neighbours = current.Neighbours;

                    if (neighbours.Count < 2)
                        continue;

                    double a, b;
                    Regression.Linear(
                        Enumerable.Range(0, neighbours.Count).Select(dummy => (double) dummy).ToList(),
                        neighbours.Select(neighbour => neighbour[0].Reference)
                            .Select(dummy => (double) dummy)
                            .ToList(), out a, out b);

                    var refIndex =
                        (from neighbour in neighbours from pt in neighbour select pt.Reference).Distinct()
                            .OrderBy(num => num)
                            .ToList();
                    var length = 0.0;
                    var common = 0;
                    for (var l = 0; l + 1 < refIndex.Count; l++)
                    {
                        var i0 = refIndex[l];
                        var i1 = refIndex[l + 1];
                        if (i0 + 1 == i1)
                        {
                            length += flatTrack.Displacement[i1];
                            common++;
                        }
                    }
                    if (common > 0)
                        segments[k].TrackSegments.Add(new TrackSegment(i, refIndex, neighbours.First().First().Reference,
                            neighbours.First().Last().Reference, length, (double) common/(refIndex.Count - 1), a));
                }
            }
            return segments;
        }
    }
}