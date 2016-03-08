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

namespace Math
{
    public static class Geometry
    {
        public static IList<Vector2D> ConvexHull(IList<Vector2D> allPoints)
        {
            // Make unique, O(N)
            var hs = new HashSet<Vector2D>();
            allPoints.All(x => hs.Add(x));
            var points = hs.ToList();

            // Sort, O(N*log(N))
            points.Sort((a, b) => (a == b ? 0 : (a.X < b.X || ((Comparison.IsEqual(a.X, b.X) && a.Y < b.Y)) ? -1 : 1)));

            var k = 0;
            var hull = new Vector2D[points.Count * 2];

            // Build lower hull
            for (var i = 0; i < points.Count; ++i)
            {
                while (k >= 2 && Cross(hull[k - 2], hull[k - 1], points[i]) <= 0) k--;
                hull[k++] = points[i];
            }

            // Build upper hull
            for (int i = points.Count - 2, t = k + 1; i >= 0; i--)
            {
                while (k >= t && Cross(hull[k - 2], hull[k - 1], points[i]) <= 0) k--;
                hull[k++] = points[i];
            }

            if (k > 1)
                k--;
            return hull.Take(k).ToList();

        }

        private static double Cross(Vector2D o, Vector2D a, Vector2D b)
        {
            return Vector2D.Cross(a - o, b - o);
        }
    }
}
