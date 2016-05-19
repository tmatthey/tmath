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

namespace Math.Gps
{
    public static class Clustering
    {
        public static IList<int> SignificantPoints(IList<Vector2D> track, int mdlCostAdwantage = 25)
        {
            if (track == null || track.Count < 1)
                return new List<int>();
            if (track.Count == 1)
                return new List<int> {0};

            var i = 0;
            int j;
            var points = new List<int> {0};
            do
            {
                var globalCost = 0;

                for (j = 1; i + j < track.Count; j++)
                {
                    globalCost += ModelCost(track[i + j - 1], track[i + j]);
                    var localCost = ModelCost(track[i], track[i + j]) + EncodingCost(track, i, i + j);

                    if (globalCost + mdlCostAdwantage < localCost)
                    {
                        points.Add(i + j - 1);
                        i = i + j - 1;
                        j = 0;
                        break;
                    }
                }
            } while (i + j < track.Count);
            points.Add(track.Count - 1);

            return points;
        }

        private static int EncodingCost(IList<Vector2D> track, int i0, int i1)
        {
            var cost = 0;
            for (var i = i0; i < i1; i++)
            {
                double perpendicular, parallel, angular;
                Geometry.TrajectoryHausdorffDistances(track[i0], track[i1], track[i], track[i + 1],
                    out perpendicular, out parallel, out angular);

                perpendicular = System.Math.Max(perpendicular, 1.0);
                angular = System.Math.Max(angular, 1.0);

                cost += (int) System.Math.Ceiling(System.Math.Log(perpendicular, 2))
                        + (int) System.Math.Ceiling(System.Math.Log(angular, 2));
            }
            return cost;
        }

        private static int ModelCost(Vector2D p0, Vector2D p1)
        {
            var d = System.Math.Max(p0.Distance(p1), 1.0);
            return (int) System.Math.Ceiling(System.Math.Log(d, 2));
        }
    }
}