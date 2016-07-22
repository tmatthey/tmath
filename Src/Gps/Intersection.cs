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

using System.Collections;

namespace Math.Gps
{
    public static class Intersection
    {
        public enum Result
        {
            Undefined, // A GPS track list is zero or empty, or calculation of center failed
            NotIntersecting, //The GPS tracks do neither intersect nor overlap, but may be close to each other

            Intersecting, // The GPS tracks can overlap or intersect, but must not

            // MinCricle and Rect:
            Same, // Same bounding rectangle or min circle
            Inside, // Track one contained in bounding area of track two
            Outside //Track two contained in bounding area of track one
        }

        // Intersection by minimal circles
        // Note: Unrestricted where the circles reside on the sphere
        public static Result MinCircle(GpsTrack one, GpsTrack two)
        {
            if (double.IsNaN(one.MinCircleAngle) || double.IsNaN(two.MinCircleAngle))
                return Result.Undefined;

            var r1 = one.MinCircleAngle*2.0*Geodesy.EarthRadius;
            var r2 = two.MinCircleAngle*2.0*Geodesy.EarthRadius;
            if (one.Center.EuclideanNorm(two.Center) < 1.0 && Comparison.IsEqual(r1, r2, 1.0))
                return Result.Same;

            var r12 = System.Math.Abs(one.MinCircleCenter.Angle(two.MinCircleCenter))*2.0*Geodesy.EarthRadius;
            if (Comparison.IsLessEqual(r12 + r1, r2, 1.0))
                return Result.Outside;

            if (Comparison.IsLessEqual(r12 + r2, r1, 1.0))
                return Result.Inside;

            if (Comparison.IsLessEqual(r12, r1 + r2, 1.0))
                return Result.Intersecting;

            return Result.NotIntersecting;
        }

        // Intersection by bounding rectangle
        // Note: Both rectangles must reside on a half sphere
        public static Result Rect(GpsTrack one, GpsTrack two)
        {
            if (double.IsNaN(one.RotationAngle) || double.IsNaN(two.RotationAngle))
                return Result.Undefined;

            var t1 = one.CreateTransformedTrack();
            var t2 = two.CreateTransformedTrack();
            var r1 = t1.Size.Min.EuclideanNorm(t1.Size.Max);
            var r2 = t2.Size.Min.EuclideanNorm(t2.Size.Max);
            var r12 = System.Math.Abs(one.Center.Angle(two.Center))*2.0*Geodesy.EarthRadius;
            if (!Comparison.IsLessEqual(r12, r1 + r2, 1.0))
                return Result.NotIntersecting;

            t2 = two.CreateTransformedTrack(one.Center);
            if (t1.Size.Min.EuclideanNorm(t2.Size.Min) < 1.0 && t1.Size.Max.EuclideanNorm(t2.Size.Max) < 1.0)
                return Result.Same;

            var insideMin = t1.Size.IsInside(t2.Size.Min, 1e-10);
            var insideMax = t1.Size.IsInside(t2.Size.Max, 1e-10);
            var outsideMin = t2.Size.IsInside(t1.Size.Min, 1e-10);
            var outsideMax = t2.Size.IsInside(t1.Size.Max, 1e-10);

            if (insideMax && insideMin)
            {
                return Result.Inside;
            }
            if (outsideMin && outsideMax)
            {
                return Result.Outside;
            }

            if (insideMax || insideMin || outsideMin || outsideMax)
                return Result.Intersecting;

            return Result.NotIntersecting;
        }

        // Intersection based on latitude and longitude grid
        // Note: Unrestricted where the GPS points reside on the sphere
        public static Result Grid(GpsTrack one, GpsTrack two, int resolution = 1000)
        {
            if (one.Track.Count == 0 || two.Track.Count == 0)
                return Result.Undefined;
            var grid = new Hashtable();

            var size1 = one.Track.Count;
            var size2 = two.Track.Count;
            var size = System.Math.Max(size1, size2);
            for (var i = 0; i < size; i++)
            {
                var i0 = i > 0 ? i - 1 : 0;
                if (i < size1)
                {
                    if (IsGridPointOccupied(ref grid, resolution, 1, one.Track[i0], one.Track[i]))
                    {
                        return Result.Intersecting;
                    }
                }
                if (i < size2)
                {
                    if (IsGridPointOccupied(ref grid, resolution, 2, two.Track[i0], two.Track[i]))
                    {
                        return Result.Intersecting;
                    }
                }
            }
            return Result.NotIntersecting;
        }

        private static bool IsGridPointOccupied(ref Hashtable grid, int resolution, int index, GpsPoint pt0,
            GpsPoint pt1)
        {
            int i, j;
            pt0.GridIndex(resolution, out i, out j);
            var m = j*resolution + i;

            int k, l;
            pt1.GridIndex(resolution, out k, out l);
            var n = l*resolution + k;

            // Find grid distance (di, dj)
            var di = 0;
            var dj = 0;
            if (m != n)
            {
                // If a point is a pole use same longitude as 
                // other point for grid distance
                var jj = j;
                var ll = l;
                if (i == 0 || i == resolution - 1)
                {
                    jj = ll;
                }
                else if (k == 0 || k == resolution - 1)
                {
                    ll = jj;
                }

                var a = System.Math.Abs((ll + resolution*2 - jj)%(resolution*2));
                var b = System.Math.Abs((jj + resolution*2 - ll)%(resolution*2));
                dj = System.Math.Min(a, b);
                di = System.Math.Abs(i - k);
            }

            // Same grid box / common edge or occupied true
            var isOccupied = UpdateGrid(ref grid, index, n);
            if (di + dj < 2 || isOccupied)
            {
                return isOccupied;
            }

            // Touching in one vertex and check of other two quadrants
            if (di == 1 && dj == 1)
            {
                if (UpdateGrid(ref grid, index, j*resolution + k) ||
                    UpdateGrid(ref grid, index, l*resolution + i))
                    return true;
            }

            // Divide and conquer for corner cases
            var v0 = ((Vector3D) pt0).Normalized();
            var v1 = ((Vector3D) pt1).Normalized();
            var axis = (v0 ^ v1).Normalized();
            var angle = v0.Angle(v1);
            GpsPoint pt = v0.Rotate(axis, angle*0.5);
            pt.Elevation = 0.0;
            if (IsGridPointOccupied(ref grid, resolution, index, pt0, pt))
                return true;
            if (IsGridPointOccupied(ref grid, resolution, index, pt, pt1))
                return true;
            return false;
        }

        private static bool UpdateGrid(ref Hashtable grid, int index, int n)
        {
            if (grid.ContainsKey(n))
            {
                return (int) grid[n] != index;
            }
            grid.Add(n, index);
            return false;
        }
    }
}