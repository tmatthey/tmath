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
using Math.Gps;

namespace Math.Clustering
{
    public static class GpsSegmentClustering
    {
        public static List<List<int>> FindTrackClusters(List<List<GpsPoint>> gpsTracks)
        {
            return PolylineNeighbours.Cluster(gpsTracks);
        }

        public static List<SegmentResult> FindLocalCommonSegments(List<List<GpsPoint>> list, int n = 5,
            double eps = 20.0,
            double minL = 20.0, int cost = 5, double epsTrack = 25.0)
        {
            return FindLocalCommonSegments(new FlatTrackCluster(list), n, eps, minL, cost, epsTrack);
        }

        public static List<SegmentResult> FindLocalCommonSegments(FlatTrackCluster cluster, int n = 5, double eps = 20.0,
            double minL = 20.0, int cost = 5, double epsTrack = 25.0)
        {
            var db = TraClus.Cluster(cluster.Tracks, n, eps, true, minL, cost);
            var segments = new List<SegmentResult>();
            var trackIds = new List<int>();
            foreach (var segment in db)
            {
                trackIds.AddRange(segment.SegmentIndices.Indices());
                var normalizedSegments = new List<Vector2D> {segment.Segment[0]};
                var normalizedGpsSegments = new List<GpsPoint> {segment.Segment[0].ToGpsPoint(cluster.Center)};
                for (var i = 0; i + 1 < segment.Segment.Count; i++)
                {
                    var v0 = segment.Segment[i];
                    var v1 = segment.Segment[i + 1];
                    var g0 = v0.ToGpsPoint(cluster.Center);
                    var g1 = v1.ToGpsPoint(cluster.Center);
                    var d = g0.HaversineDistance(g1);
                    if (Comparison.IsLess(epsTrack, d/2.0))
                    {
                        var v01 = v1 - v0;

                        var m = System.Math.Floor(d/2.0/epsTrack);
                        for (var j = 1; j < m; j++)
                        {
                            normalizedSegments.Add(v0 + v01*j/m);
                            normalizedGpsSegments.Add(g0.Interpolate(g1, j/m));
                        }
                    }
                    normalizedSegments.Add(v1);
                    normalizedGpsSegments.Add(g1);
                }
                segments.Add(new SegmentResult(new List<TrackSegment>(), normalizedSegments, normalizedGpsSegments));
            }
            trackIds = trackIds.Distinct().OrderBy(num => num).ToList();
            foreach (var i in trackIds)
            {
                var flatTrack = cluster.FlatTracks[i];
                var analyzer = new NeighbourDistanceCalculator(flatTrack, eps);
                for (var k = 0; k < db.Count; k++)
                {
                    var segment = db[k];
                    if (segment.SegmentIndices.Indices().Contains(i))
                    {
                        var current = analyzer.Analyze(segment.Segment, epsTrack);
                        var neighbours = current.Neighbours;

                        if (neighbours.Count > 1)
                        {
                            double a, b;
                            Regression.Linear(
                                Enumerable.Range(0, neighbours.Count).Select(dummy => (double) dummy).ToList(),
                                neighbours.Select(
                                        neighbour =>
                                            neighbour[0].Reference +
                                            (Comparison.IsEqual(neighbour[0].Fraction, 1.0) ? 1 : 0))
                                    .Select(dummy => (double) dummy)
                                    .ToList(), out a, out b);

                            var indices =
                                (from neighbour in neighbours
                                        from pt in neighbour
                                        select pt.Reference + (Comparison.IsEqual(pt.Fraction, 1.0) ? 1 : 0)).Distinct()
                                    .OrderBy(num => num)
                                    .ToList();
                            var length = 0.0;
                            var totalLength = 0.0;
                            for (var l = 0; l + 1 < indices.Count; l++)
                            {
                                var i0 = indices[l];
                                var i1 = indices[l + 1];
                                totalLength += flatTrack.Displacement[i1];
                                if (i0 + 1 == i1)
                                {
                                    length += flatTrack.Displacement[i1];
                                }
                            }
                            var segLength = 0.0;
                            for (var l = 0; l + 1 < neighbours.Count; l++)
                            {
                                var i0 = neighbours[l][0].Current;
                                var i1 = neighbours[l + 1][0].Current;
                                if (i0 + 1 == i1)
                                {
                                    segLength += segment.Segment[i0].EuclideanNorm(segment.Segment[i1]);
                                }
                            }
                            if (totalLength > 0.0)
                            {
                                var first = neighbours.First().First();
                                var last = neighbours.Last().First();
                                segments[k].TrackSegments.Add(new TrackSegment(i, indices, first.Reference,
                                    last.Reference + (Comparison.IsEqual(last.Fraction, 1.0) ? 1 : 0),
                                    first.Current, last.Current,
                                    length, length/totalLength, segLength/segments[k].Length, a));
                            }
                        }
                    }
                }
            }
            return segments.OrderByDescending(seg => seg.Length).ToList();
        }


        public static List<List<SegmentResult>> FindGlobalCommonSegments(List<List<GpsPoint>> list, int n = 5,
            double eps = 20.0,
            double minL = 20.0, int cost = 5, double epsTrack = 25.0)
        {
            var clusters = FindTrackClusters(list);
            var result = new List<List<SegmentResult>>();

            foreach (var trackIndices in clusters)
            {
                var cluster = new FlatTrackCluster(trackIndices.Select(i => list[i]).ToList());
                var segments = FindLocalCommonSegments(cluster, n, eps, minL, cost, epsTrack);
                foreach (var segmentResult in segments)
                {
                    foreach (var trackSegment in segmentResult.TrackSegments)
                    {
                        trackSegment.Id = trackIndices[trackSegment.Id];
                    }
                }
                result.Add(segments);
            }
            return result;
        }


        public class SegmentResult
        {
            public SegmentResult(List<TrackSegment> trackSegments, List<Vector2D> segment, List<GpsPoint> gpsSegment)
            {
                TrackSegments = trackSegments;
                RepresentativeTrack = segment;
                Length = Geometry.EuclideanNorm(segment);
                RepresentativeGpsTrack = gpsSegment;
            }

            public List<TrackSegment> TrackSegments { get; }
            public List<Vector2D> RepresentativeTrack { get; private set; }
            public List<GpsPoint> RepresentativeGpsTrack { get; private set; }
            public double Length { get; }
        }

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
            public int Id { get; set; }
            public double Length { get; private set; }
            public double Coverage { get; private set; }
            public double Common { get; private set; }
            public double Direction { get; private set; }
        }
    }
}