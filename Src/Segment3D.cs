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

using System;
using Math.Interfaces;

namespace Math
{
    public class Segment3D : ISegment<Segment3D, Vector3D>
    {
        public Segment3D()
        {
            A = new Vector3D();
            B = new Vector3D();
        }

        public Segment3D(Segment3D d)
        {
            A = d.A;
            B = d.B;
        }

        public Segment3D(Vector3D a, Vector3D b)
        {
            A = a;
            B = b;
        }

        public Vector3D A { get; set; }
        public Vector3D B { get; set; }

        public double Length()
        {
            return A.Distance(B);
        }

        public int Dimensions
        {
            get { return A.Dimensions; }
        }

        public double[] Array
        {
            get { return new[] {A.X, A.Y, A.Z, B.X, B.Y, B.Z}; }
        }

        public double this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return A.X;
                    case 1:
                        return A.Y;
                    case 2:
                        return A.Z;
                    case 3:
                        return B.X;
                    case 4:
                        return B.Y;
                    case 5:
                        return B.Z;
                }
                throw new IndexOutOfRangeException();
            }
        }

        public double Distance(Segment3D d)
        {
            double per, par, angular;
            Geometry.TrajectoryHausdorffDistances(A, B, d.A, d.B, out per, out par, out angular);
            return per;
        }
    }
}