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
            // Make unique, O(N) (?)
            var points = allPoints.Distinct().ToList();

            // Reduce point set to O(sqrt(N))
            points = Eliminate(points);

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

        // Minimal boundary circle based on Welzl's algorithm
        public static Circle2D MinCircle(IList<Vector2D> allPoints)
        {
            // Make unique, O(N) (?)
            var points = allPoints.Distinct().ToList();

            // Reduce point set to O(sqrt(N))
            points = Eliminate(points);

            // Trival cases
            if (points.Count == 0)
                return new Circle2D(new Vector2D(double.NaN), double.NaN);
            if (points.Count == 1)
                return Circle2D.Create(points[0]);
            if (points.Count == 2)
                return Circle2D.Create(points[0], points[1]);
            if (points.Count == 3)
                return Circle2D.Create(points[0], points[1], points[2]);

            var array = new Vector2D[3];

            return DoMinCricle(points, points.Count, array, 0);
        }

        // Minimal boundary circle based on Welzl's algorithm
        public static Circle3D MinCircleOnSphere(IList<Vector3D> allPoints)
        {
            // Make unique, O(N) (?)
            var points = allPoints.Distinct().ToList();

            // Reduce point set to O(sqrt(N))
            points = Eliminate(points);

            // Trival cases
            if (points.Count == 0)
                return new Circle3D(new Vector3D(double.NaN), double.NaN);
            if (points.Count == 1)
                return Circle3D.Create(points[0]);
            if (points.Count == 2)
                return Circle3D.Create(points[0], points[1]);
            if (points.Count == 3)
                return Circle3D.Create(points[0], points[1], points[2]);

            var array = new Vector3D[3];

            return DoMinCricleOnSphere(points, points.Count, array, 0);
        }

        private static Circle2D DoMinCricle(IList<Vector2D> points, int n, Vector2D[] array, int k)
        {
            if (k == 3)
                return Circle2D.Create(array[0], array[1], array[2]);
            if (n == 1 && k == 0)
                return Circle2D.Create(points[0]);
            if (n == 0 && k == 2)
                return Circle2D.Create(array[0], array[1]);
            if (n == 1 && k == 1)
                return Circle2D.Create(array[0], points[0]);

            var c = DoMinCricle(points, n - 1, array, k);
            if (c.IsInside(points[n - 1]))
                return c;
            array[k++] = points[n - 1];
            c = DoMinCricle(points, n - 1, array, k);
            return c;
        }

        private static Circle3D DoMinCricleOnSphere(IList<Vector3D> points, int n, Vector3D[] array, int k)
        {
            if (k == 3)
                return Circle3D.Create(array[0], array[1], array[2]);
            if (n == 1 && k == 0)
                return Circle3D.Create(points[0]);
            if (n == 0 && k == 2)
                return Circle3D.Create(array[0], array[1]);
            if (n == 1 && k == 1)
                return Circle3D.Create(array[0], points[0]);

            var c = DoMinCricleOnSphere(points, n - 1, array, k);
            if (CircleOriginLineIntersect(c, points[n - 1]))
                return c;
            array[k++] = points[n - 1];
            c = DoMinCricleOnSphere(points, n - 1, array, k);
            return c;
        }

        public static bool CircleOriginLineIntersect(Circle3D c, Vector3D p)
        {
            var u = p;
            var dot = c.Normal*u;
            if (Comparison.IsZero(dot))
                return false;
            var w = p - c.Center;
            var fac = -(c.Normal*w)/dot;
            var v = u*fac + p;
            return Comparison.IsLessEqual(c.Center.Distance(v), c.Radius);

        }

        private static double Cross(Vector2D o, Vector2D a, Vector2D b)
        {
            return Vector2D.Cross(a - o, b - o);
        }

        //  Analysis of a Simple Yet Efficient Convex Hull Algorithm; Golin, Sedgewick, ACM 1988
        private static List<Vector2D> Eliminate(List<Vector2D> points)
        {
            if (points.Count < 2)
                return points;
            var array = points.ToArray();
            int i2, i3, i4;
            double a3, a4;

            var i1 = i2 = i3 = i4 = 1;
            var a1 = a4 = array[0].X + array[0].Y;
            var a2 = a3 = array[0].X - array[0].Y;

            for (var j = 1; j < array.Length; j++)
            {
                if ((array[j].X + array[j].Y) < a1)
                {
                    i1 = j;
                    a1 = array[j].X + array[j].Y;
                }
                else if ((array[j].X + array[j].Y) > a4)
                {
                    i4 = j;
                    a4 = array[j].X + array[j].Y;
                }

                if ((array[j].X - array[j].Y) < a2)
                {
                    i2 = j;
                    a2 = array[j].X - array[j].Y;
                }
                else if ((array[j].X - array[j].Y) > a3)
                {
                    i3 = j;
                    a3 = array[j].X - array[j].Y;
                }
            }

            var lowX = System.Math.Max(array[i1].X, array[i2].X);
            var highX = System.Math.Min(array[i3].X, array[i4].X);
            var lowY = System.Math.Max(array[i1].Y, array[i3].Y);
            var highY = System.Math.Min(array[i2].Y, array[i4].Y);
            var n = 0;
            var i = array.Length - 1;

            while (i > n)
            {
                if ((lowX < array[i].X) && (array[i].X < highX) &&
                    (lowY < array[i].Y) && (array[i].Y < highY))
                {
                    i--;
                }
                else
                {
                    n++;
                    Utils.Swap(ref array[n], ref array[i]);
                }
            }
            return array.Take(n + 1).ToList();
        }

        private static List<Vector3D> Eliminate(List<Vector3D> points)
        {
            if (points.Count < 2)
                return points;
            var array = points.ToArray();
            int i2, i3, i4, i5, i6, i7, i8;
            double a5, a6, a7, a8;

            var i1 = i2 = i3 = i4 = i5 = i6 = i7 = i8 = 1;
            var a1 = a8 = array[0].X + array[0].Y + array[0].Z;
            var a2 = a7 = array[0].X - array[0].Y + array[0].Z;
            var a3 = a6 = array[0].X + array[0].Y - array[0].Z;
            var a4 = a5 = array[0].X - array[0].Y - array[0].Z;

            for (var j = 1; j < array.Length; j++)
            {
                var d = array[j].X + array[j].Y + array[j].Z;
                if (d < a1)
                {
                    i1 = j;
                    a1 = d;
                }
                else if (d > a8)
                {
                    i8 = j;
                    a8 = d;
                }

                d = array[j].X - array[j].Y + array[j].Z;
                if (d < a2)
                {
                    i2 = j;
                    a2 = d;
                }
                else if (d > a7)
                {
                    i7 = j;
                    a7 = d;
                }

                d = array[j].X + array[j].Y - array[j].Z;
                if (d < a3)
                {
                    i3 = j;
                    a3 = d;
                }
                else if (d > a6)
                {
                    i6 = j;
                    a6 = d;
                }

                d = array[j].X - array[j].Y - array[j].Z;
                if (d < a4)
                {
                    i4 = j;
                    a4 = d;
                }
                else if (d > a5)
                {
                    i5 = j;
                    a5 = d;
                }
            }

            var lowX  = new List<double> { array[i1].X, array[i2].X, array[i3].X, array[i4].X }.Max();
            var highX = new List<double> { array[i5].X, array[i6].X, array[i7].X, array[i8].X }.Min();
            var lowY  = new List<double> { array[i1].Y, array[i3].Y, array[i5].Y, array[i7].Y }.Max();
            var highY = new List<double> { array[i2].Y, array[i4].Y, array[i6].Y, array[i8].Y }.Min();
            var lowZ  = new List<double> { array[i1].Z, array[i2].Z, array[i5].Z, array[i6].Z }.Max();
            var highZ = new List<double> { array[i3].Z, array[i4].Z, array[i7].Z, array[i8].Z }.Min();
            var n = 0;
            var i = array.Length - 1;

            while (i > n)
            {
                if ((lowX < array[i].X) && (array[i].X < highX) &&
                    (lowY < array[i].Y) && (array[i].Y < highY) &&
                    (lowZ < array[i].Z) && (array[i].Z < highZ))
                {
                    i--;
                }
                else
                {
                    n++;
                    Utils.Swap(ref array[n], ref array[i]);
                }
            }
            return array.Take(n + 1).ToList();
        }
    }
}