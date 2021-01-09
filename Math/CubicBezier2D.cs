/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2021 Thierry Matthey
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
using Math.Interfaces;

namespace Math
{
    public class CubicBezier2D : ICubicBezier<Vector2D, CubicBezier2D>, ICloneable<CubicBezier2D>
    {
        public CubicBezier2D()
        {
            P0 = new Vector2D();
            P1 = new Vector2D();
            P2 = new Vector2D();
            P3 = new Vector2D();
        }

        public CubicBezier2D(Vector2D p0, Vector2D p1, Vector2D p2, Vector2D p3)
        {
            P0 = p0;
            P1 = p1;
            P2 = p2;
            P3 = p3;
        }

        public Vector2D P0 { get; set; }
        public Vector2D P1 { get; set; }
        public Vector2D P2 { get; set; }
        public Vector2D P3 { get; set; }

        public int Dimensions => 2;

        public CubicBezier2D Clone()
        {
            return new CubicBezier2D
            {
                P0 = P0.Clone(),
                P1 = P1.Clone(),
                P2 = P2.Clone(),
                P3 = P3.Clone(),
            };
        }

        public IBounding<Vector2D> Bounding()
        {
            var a = 3.0 * (-P0 + 3.0 * P1 - 3.0 * P2 + P3);
            var b = 6.0 * (P0 - 2 * P1 + P2);
            var c = 3.0 * (P1 - P0);

            var roots = new List<double> {0, 1};
            roots.AddRange(Solver.QuadraticEq(a.X, b.X, c.X));
            roots.AddRange(Solver.QuadraticEq(a.Y, b.Y, c.Y));
            var bb = new BoundingRect();
            foreach (var t in roots)
            {
                if (0.0 <= t && t <= 1.0)
                    bb.Expand(Evaluate(t));
            }

            return bb;
        }

        public bool IsEqual(CubicBezier2D a)
        {
            return IsEqual(a, Comparison.Epsilon);
        }

        public bool IsEqual(CubicBezier2D a, double epsilon)
        {
            return P0.IsEqual(a.P0) && P1.IsEqual(a.P1) && P2.IsEqual(a.P2) && P3.IsEqual(a.P3);
        }

        public double Length(double accuracy = 1e-5)
        {
            var a = (P3 - P0).Norm();
            var b = (P0 - P1).Norm() + (P1 - P2).Norm() + (P2 - P1).Norm();
            var l = 0.5 * (a + b);
            if (a * (1.0 + accuracy) < b)
            {
                var (b0, b1) = Split(0.5);
                if (b1 != null)
                    l = b0.Length(accuracy) + b1.Length(accuracy);
            }

            return l;
        }

        public Vector2D Evaluate(double t)
        {
            return (1.0 - t) * (1.0 - t) * (1.0 - t) * P0 +
                   3.0 * t * (1.0 - t) * (1.0 - t) * P1 +
                   3.0 * t * t * (1.0 - t) * P2 +
                   t * t * t * P3;
        }

        public Vector2D dEvaluate(double t)
        {
            return -3.0 * (1.0 - t) * (1.0 - t) * P0 +
                   (3.0 * (1.0 - t) * (1.0 - t) - 6.0 * (1.0 - t) * t) * P1 +
                   (6.0 * (1.0 - t) * t - 3.0 * t * t) * P2 +
                   3.0 * t * t * P3;
        }

        public Vector2D d2Evaluate(double t)
        {
            return 6.0 * (1.0 - t) * P0 +
                   6.0 * (3.0 * t - 2.0) * P1 +
                   6.0 * (1.0 - 3.0 * t) * P2 +
                   6.0 * t * P3;
        }

        public double Kappa(double t)
        {
            var d = dEvaluate(t);
            var d2 = d2Evaluate(t);
            return (d.X * d2.Y + d2.X * d.Y) / System.Math.Pow(d.Norm2(), 1.5);
        }

        public Vector2D Tangent(double t)
        {
            return dEvaluate(t).Normalized();
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = P0.GetHashCode();
                hashCode = (hashCode * 397) ^ P1.GetHashCode();
                hashCode = (hashCode * 397) ^ P2.GetHashCode();
                hashCode = (hashCode * 397) ^ P3.GetHashCode();
                return hashCode;
            }
        }

        public (CubicBezier2D, CubicBezier2D) Split(double t)
        {
            if (Comparison.IsLessEqual(t, 0) || Comparison.IsLessEqual(1.0, t))
                return (Clone(), null);

            var p01 = P0.Interpolate(P1, t);
            var p12 = P1.Interpolate(P2, t);
            var p23 = P2.Interpolate(P3, t);
            var p012 = p01.Interpolate(p12, t);
            var p123 = p12.Interpolate(p23, t);
            var p0123 = p012.Interpolate(p123, t);

            return (new CubicBezier2D(P0.Clone(), p01, p012, p0123),
                new CubicBezier2D(p0123.Clone(), p123, p23, P3.Clone()));
        }
    }
}