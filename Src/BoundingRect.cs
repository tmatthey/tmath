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

namespace Math
{
    public class BoundingRect
    {
        public BoundingRect()
        {
            Min = new Vector2D(double.PositiveInfinity);
            Max = new Vector2D(double.NegativeInfinity);
        }

        public Vector2D Min { get; private set; }
        public Vector2D Max { get; private set; }

        public bool IsEmpty()
        {
            return Min.X > Max.X && Min.Y > Max.Y;
        }

        public void Reset()
        {
            Min.X = double.PositiveInfinity;
            Min.Y = double.PositiveInfinity;
            Max.X = double.NegativeInfinity;
            Max.Y = double.NegativeInfinity;
        }

        public void Expand(Vector2D v)
        {
            ExpandX(v.X);
            ExpandY(v.Y);
        }

        public void Expand(BoundingRect b)
        {
            Expand(b.Min);
            Expand(b.Max);
        }

        public void ExpandX(double x)
        {
            Min.X = ExpandMin(Min.X, x);
            Max.X = ExpandMax(Max.X, x);
        }

        public void ExpandY(double y)
        {
            Min.Y = ExpandMin(Min.Y, y);
            Max.Y = ExpandMax(Max.Y, y);
        }

        private double ExpandMin(double a, double b)
        {
            if (Comparison.IsNumber(b))
            {
                return Comparison.IsNumber(a) ? System.Math.Min(a, b) : b;
            }
            return a;
        }

        private double ExpandMax(double a, double b)
        {
            if (Comparison.IsNumber(b))
            {
                return Comparison.IsNumber(a) ? System.Math.Max(a, b) : b;
            }
            return a;
        }


    }
}
