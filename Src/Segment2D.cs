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

using Math.Interfaces;

namespace Math
{
    public class Segment2D : ISegment<Segment2D, Vector2D>
    {
        public Segment2D()
        {
            A = new Vector2D();
            B = new Vector2D();
        }

        public Segment2D(Segment2D d)
        {
            A = d.A;
            B = d.B;
        }

        public Segment2D(Vector2D a, Vector2D b)
        {
            A = a;
            B = b;
        }

        public Vector2D A { get; set; }
        public Vector2D B { get; set; }

        public double Length()
        {
            return A.Distance(B);
        }

        public double Distance(Segment2D d)
        {
            double per, par, angular;
            Geometry.TrajectoryHausdorffDistances(A, B, d.A, d.B, out per, out par, out angular);
            return per;
        }
    }
}