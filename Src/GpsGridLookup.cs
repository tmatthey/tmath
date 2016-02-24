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

namespace Math
{
    public class GpsGridLookup
    {
        private readonly Vector2D _gridOffset;

        public class GpsDistance
        {
            public GpsDistance(int reference, int target, double distance)
            {
                Reference = reference;
                Traget = target;
                Distance = distance;
            }

            public int Reference { get; private set; }
            public int Traget { get; private set; }
            public double Distance { get; private set; }
        }

        public GpsGridLookup(GpsTransformer transformed, double gridSize)
        {
            Size = gridSize;
            Offset = transformed.Min;
            Track = transformed.Track;
            _gridOffset = new Vector2D(gridSize, gridSize);

            var d = (transformed.Max - transformed.Min);
            int nx, ny;
            Index(d + Offset, out nx, out ny);
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
        public Vector2D Offset { get; private set; }
        public double Size { get; private set; }
        public List<Vector2D> Track { get; private set; }

        public IList<GpsDistance> Find(Vector2D point, double radius)
        {
            return Find(point, radius, -1);
        }

        public IList<List<GpsDistance>> Find(IList<Vector2D> track, double radius)
        {
            var list = new List<List<GpsDistance>>();
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

        private List<GpsDistance> Find(Vector2D point, double radius, int referenceIndex)
        {
            int minI, minJ;
            Index(point - _gridOffset, out minI, out minJ);
            int maxI, maxJ;
            Index(point + _gridOffset, out maxI, out maxJ);
            var list = new List<GpsDistance>();
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
                        double r = point.Distance(pt);
                        if (Comparison.IsLessEqual(r, radius))
                        {
                            list.Add(new GpsDistance(k, referenceIndex, r));
                        }
                    }
                }
            }
            list.Sort((x, y) => x.Distance.CompareTo(y.Distance));
            return list;
        }

        private void Index(Vector2D u, out int i, out int j)
        {
            var v = u - Offset;
            i = (int)System.Math.Floor(v.X / Size);
            j = (int)System.Math.Floor(v.Y / Size);
        }
    }
}