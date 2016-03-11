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
    public class GridLookup
    {
        private readonly Vector2D _gridOffset;

        public GridLookup(Transformer transformed, double gridSize)
            : this(transformed, gridSize, transformed.Min, transformed.Max)
        {
        }

        public GridLookup(Transformer transformed, double gridSize, Vector2D min, Vector2D max)
        {
            min.X = System.Math.Min(min.X, transformed.Min.X);
            min.Y = System.Math.Min(min.Y, transformed.Min.Y);
            max.X = System.Math.Max(max.X, transformed.Max.X);
            max.Y = System.Math.Max(max.Y, transformed.Max.Y);
            Min = min;
            Max = min;
            Size = gridSize;
            Track = transformed.Track;
            _gridOffset = new Vector2D(gridSize, gridSize);
            int nx, ny;
            Index(max, out nx, out ny);
            NX = nx + 1;
            NY = ny + 1;
            Grid = new List<int>[NX, NY];
            for (var i = 0; i < NX; ++i)
                for (var j = 0; j < NY; j++)
                    Grid[i, j] = new List<int>();
            for (var k = 0; k < transformed.Track.Count; k++)
            {
                int i, j;
                Index(transformed.Track[k], out i, out j);
                Grid[i, j].Add(k);
            }
        }

        public List<int>[,] Grid { get; private set; }
        public int NX { get; private set; }
        public int NY { get; private set; }
        public Vector2D Min { get; private set; }
        public Vector2D Max { get; private set; }
        public double Size { get; private set; }
        public List<Vector2D> Track { get; private set; }

        public IList<Distance> Find(Vector2D point, double radius)
        {
            return Find(point, radius, -1);
        }

        public IList<List<Distance>> Find(IList<Vector2D> track, double radius)
        {
            var list = new List<List<Distance>>();
            for (var i = 0; i < track.Count; i++)
            {
                var a = Find(track[i], radius, i);
                if (a.Count > 0)
                {
                    list.Add(a);
                }
            }

            return list;
        }

        public static IList<List<Distance>> ReferenceOrdering(IList<List<Distance>> current)
        {
            var map = new Dictionary<int, List<Distance>>();
            foreach (var point in current)
            {
                foreach (var distance in point)
                {
                    if (!map.ContainsKey(distance.Reference))
                    {
                        map[distance.Reference] = new List<Distance>();
                    }
                    map[distance.Reference].Add(distance);
                }
            }

            foreach (var point in map)
            {
                point.Value.Sort((x, y) => x.Dist.CompareTo(y.Dist));
            }

            return map.OrderBy(i => i.Key).Select(point => point.Value).ToList();
        }

        private List<Distance> Find(Vector2D point, double radius, int referenceIndex)
        {
            int minI, minJ;
            Index(point - _gridOffset, out minI, out minJ);
            int maxI, maxJ;
            Index(point + _gridOffset, out maxI, out maxJ);
            var list = new List<Distance>();
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
                        var pt = Track[k];
                        var r = point.Distance(pt);
                        if (Comparison.IsLessEqual(r, radius))
                        {
                            list.Add(new Distance(k, referenceIndex, r));
                        }
                    }
                }
            }
            list.Sort((x, y) => x.Dist.CompareTo(y.Dist));
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