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
using Math.Interfaces;

namespace Math.Clustering
{
    public static class PolylineNeighbours
    {
        public static List<List<int>> Cluster(List<List<GpsPoint>> gpsTracks, double minDistance = 500.0)
        {
            return Cluster(gpsTracks.Select(l => l.Select(pt => (Vector3D) pt).ToList()).ToList(), minDistance);
        }

        public static List<List<int>> Cluster<T>(List<List<T>> polylines, double minDistance = 500.0)
            where T : IVector<T>
        {
            if (polylines.Count == 0)
                return new List<List<int>>();
            if (polylines.Count == 1)
                return new List<List<int>> {new List<int> {0}};

            var lineId = new List<int>();
            var points = new List<T>();
            for (var i = 0; i < polylines.Count; i++)
            {
                var polyline = polylines[i];
                if (polyline.Count > 0)
                {
                    var a = polyline[0];
                    points.Add(a);
                    lineId.Add(i);
                    for (var j = 1; j < polyline.Count; j++)
                    {
                        var b = polyline[j];
                        if (Comparison.IsLessEqual(minDistance*0.5, a.EuclideanNorm(b)))
                        {
                            points.Add(b);
                            lineId.Add(i);
                            a = b;
                        }
                    }
                }
            }

            var dbscan = new DBScan<T, T>(points);
            var clusters = dbscan.Cluster(minDistance, 1);

            var polyClusters = new List<HashSet<int>>();
            foreach (var cluster in clusters)
            {
                var set = new HashSet<int>();
                foreach (var k in cluster)
                {
                    set.Add(lineId[k]);
                }
                polyClusters.Insert(0, set);
                for (var m = 0; m + 1 < polyClusters.Count; m++)
                {
                    var l = m + 1;
                    while (l < polyClusters.Count)
                    {
                        if (polyClusters[m].Overlaps(polyClusters[l]))
                        {
                            polyClusters[m].UnionWith(polyClusters[l]);
                            polyClusters.RemoveAt(l);
                        }
                        else
                        {
                            l++;
                        }
                    }
                }
            }

            return
                polyClusters.Select(polyCluster => polyCluster.ToList())
                    .Select(c => c.OrderBy(num => num).ToList())
                    .OrderBy(n => n.First())
                    .ToList();
        }
    }
}