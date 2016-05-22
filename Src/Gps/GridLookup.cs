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
using Math.Gfx;

namespace Math.Gps
{
    public class GridLookup
    {
        public GridLookup(Transformer transformed, double gridSize)
        {
            Min = transformed.Size.Min;
            Max = transformed.Size.Max;
            Size = gridSize;
            Track = transformed.Track;
            int nx, ny;
            Index(Max, out nx, out ny);
            NX = nx + 1;
            NY = ny + 1;
            Grid = new List<int>[NX, NY];
            for (var i = 0; i < NX; ++i)
                for (var j = 0; j < NY; j++)
                    Grid[i, j] = new List<int>();
            var i0 = -1;
            var j0 = -1;
            for (var k = 0; k < transformed.Track.Count; k++)
            {
                int i, j;
                Index(transformed.Track[k], out i, out j);
                if (k > 0 && System.Math.Abs(i - i0) + System.Math.Abs(j - j0) > 1)
                {
                    // To handle corner case when two consecutive points do not share an edge or reside 
                    // in the same grid point. Normally the grid size is superior to the distance between 
                    // to consecutive points.
                    var l = k;
                    var i1 = i0;
                    var j1 = j0;
                    Draw.Bresenham(new Vector2D(i0, j0), new Vector2D(i, j), delegate(int x, int y, double c)
                    {
                        if (c > Comparison.Epsilon)
                        {
                            if (x == i1 && y == j1)
                            {
                                Grid[x, y].Add(l - 1);
                            }
                            else if (x == i && y == j)
                            {
                                Grid[x, y].Add(l);
                            }
                            else
                            {
                                Grid[x, y].Add(l);
                                Grid[x, y].Add(l - 1);
                            }
                            Grid[x, y] = Grid[x, y].Distinct().ToList();
                        }
                    });
                }
                else
                {
                    Grid[i, j].Add(k);
                    Grid[i, j] = Grid[i, j].Distinct().ToList();
                }
                i0 = i;
                j0 = j;
            }
        }

        public List<int>[,] Grid { get; private set; }
        public int NX { get; private set; }
        public int NY { get; private set; }
        public Vector2D Min { get; private set; }
        public Vector2D Max { get; private set; }
        public double Size { get; private set; }
        public List<Vector2D> Track { get; private set; }

        public IList<NeighbourDistancePoint> Find(Vector2D point, double radius)
        {
            return Find(point, radius, -1);
        }

        public IList<List<NeighbourDistancePoint>> Find(IList<Vector2D> track, IList<double> displacement, double radius)
        {
            var list = new List<List<NeighbourDistancePoint>>();
            for (var i = 0; i < track.Count; i++)
            {
                var d0 = displacement[i];
                var d1 = i + 1 < track.Count ? displacement[i + 1] : 0.0;
                var d = System.Math.Max(d0, d1)/2.0;
                var a = Find(track[i], System.Math.Max(radius, d), i);
                if (a.Count > 0)
                {
                    list.Add(a);
                }
            }

            return list;
        }

        public static IList<List<NeighbourDistancePoint>> ReferenceOrdering(IList<List<NeighbourDistancePoint>> current)
        {
            var map = new Dictionary<int, List<NeighbourDistancePoint>>();
            foreach (var point in current)
            {
                foreach (var distance in point)
                {
                    if (!map.ContainsKey(distance.Reference))
                    {
                        map[distance.Reference] = new List<NeighbourDistancePoint>();
                    }
                    map[distance.Reference].Add(distance);
                }
            }

            foreach (var point in map)
            {
                point.Value.Sort((x, y) => x.MinDistance.CompareTo(y.MinDistance));
            }

            return map.OrderBy(i => i.Key).Select(point => point.Value).ToList();
        }

        private List<NeighbourDistancePoint> Find(Vector2D point, double radius, int referenceIndex)
        {
            int minI, minJ;
            var dp = new Vector2D(radius + Size);
            Index(point - dp, out minI, out minJ);
            int maxI, maxJ;
            Index(point + dp, out maxI, out maxJ);
            var list = new List<NeighbourDistancePoint>();
            if (maxI < 0 || maxJ < 0 || NX <= minI || NY <= minJ)
            {
                return list;
            }
            minI = System.Math.Min(System.Math.Max(minI, 0), NX - 1);
            maxI = System.Math.Min(System.Math.Max(maxI, 0), NX - 1);
            minJ = System.Math.Min(System.Math.Max(minJ, 0), NY - 1);
            maxJ = System.Math.Min(System.Math.Max(maxJ, 0), NY - 1);
            for (var i = minI; i <= maxI; i++)
            {
                for (var j = minJ; j <= maxJ; j++)
                {
                    foreach (var k in Grid[i, j])
                    {
                        list.Add(new NeighbourDistancePoint(k, referenceIndex, point.Distance(Track[k])));
                    }
                }
            }
            list = list.Distinct().ToList();
            list.Sort((x, y) => x.CompareTo(y));
            return list;
        }

        private void Index(Vector2D u, out int i, out int j)
        {
            var v = u - Min;
            i = (int) System.Math.Floor(v.X/Size);
            j = (int) System.Math.Floor(v.Y/Size);
        }
    }
}