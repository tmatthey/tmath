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

namespace Math.Gfx
{
    public static class Draw
    {
        // 
        // Modified Bresenham's line algorithm supporting non-integer start end end point. 
        // Each point is rounded to nearest integer.
        //
        // https://en.wikipedia.org/wiki/Bresenham's_line_algorithm
        //
        public static void Bresenham(Vector2D a, Vector2D b, PlotWrapper w, double magnitude = 1.0)
        {
            a = w.Converter(a);
            b = w.Converter(b);
            Bresenham(a, b, w.Plot, magnitude);
        }

        public static void Bresenham(Vector2D a, Vector2D b, DelegatePlotFunction plotFunction, double magnitude = 1.0)
        {
            if ((int) System.Math.Round(a.X) == (int) System.Math.Round(b.X) &&
                (int) System.Math.Round(a.Y) == (int) System.Math.Round(b.Y))
            {
                plotFunction((int) System.Math.Round(a.X), (int) System.Math.Round(a.Y), magnitude);
                return;
            }

            var steep = System.Math.Abs(b.Y - a.Y) > System.Math.Abs(b.X - a.X);

            if (steep)
            {
                var t = a.X;
                a.X = a.Y;
                a.Y = t;
                t = b.X;
                b.X = b.Y;
                b.Y = t;
            }
            if (a.X > b.X)
            {
                Utils.Swap(ref a, ref b);
            }

            var dx = b.X - a.X;
            var dy = System.Math.Abs(b.Y - a.Y);
            var err = dx/2.0;
            var ystep = a.Y < b.Y ? 1 : -1;
            var y = (int) System.Math.Round(a.Y);

            var ax = (int) System.Math.Round(a.X);
            var bx = (int) System.Math.Round(b.X);

            for (var x = ax; x <= bx; ++x)
            {
                var c = magnitude;
                if (x == ax)
                {
                    c = (0.5 + ax - a.X)*magnitude;
                }
                else if (x == bx)
                {
                    c = (0.5 + b.X - bx)*magnitude;
                }
                if (steep)
                {
                    plotFunction(y, x, c);
                }
                else
                {
                    plotFunction(x, y, c);
                }
                err -= dy;
                if (err < 0)
                {
                    y += ystep;
                    err += dx;
                }
            }
        }

        //
        // Xiaolin Wu's line algorithm is an algorithm for line antialiasing, which was presented 
        // in the article An Efficient Antialiasing Technique in the July 1991 issue of Computer 
        // Graphics, as well as in the article Fast Antialiasing in the June 1992 issue of 
        // Dr. Dobb's Journal.
        //
        // https://en.wikipedia.org/wiki/Xiaolin_Wu's_line_algorithm
        //
        public static void XiaolinWu(Vector2D a, Vector2D b, PlotWrapper w, double magnitude = 1.0)
        {
            a = w.Converter(a);
            b = w.Converter(b);
            XiaolinWu(a, b, w.Plot, magnitude);
        }

        public static void XiaolinWu(Vector2D a, Vector2D b, DelegatePlotFunction plotFunction, double magnitude = 1.0)
        {
            if (Comparison.IsZero(a.EuclideanNorm(b)))
                return;

            var steep = System.Math.Abs(b.Y - a.Y) > System.Math.Abs(b.X - a.X);

            if (steep)
            {
                var t = a.X;
                a.X = a.Y;
                a.Y = t;
                t = b.X;
                b.X = b.Y;
                b.Y = t;
            }
            if (a.X > b.X)
            {
                Utils.Swap(ref a, ref b);
            }

            var dx = b.X - a.X;
            var dy = b.Y - a.Y;
            var gradient = dy/dx;

            // handle first endpoint
            var xend = round(a.X);
            var yend = a.Y + gradient*(xend - a.X);
            var xgap = rfpart(a.X + 0.5);
            var xpxl1 = xend; // this will be used in the main loop
            var ypxl1 = ipart(yend);
            if (steep)
            {
                plotFunction(ypxl1, xpxl1, rfpart(yend)*xgap*magnitude);
                plotFunction(ypxl1 + 1, xpxl1, fpart(yend)*xgap*magnitude);
            }
            else
            {
                plotFunction(xpxl1, ypxl1, rfpart(yend)*xgap*magnitude);
                plotFunction(xpxl1, ypxl1 + 1, fpart(yend)*xgap*magnitude);
            }
            var intery = yend + gradient; // first y-intersection for the main loop

            // handle second endpoint
            xend = round(b.X);
            yend = b.Y + gradient*(xend - b.X);
            xgap = fpart(b.X + 0.5);
            var xpxl2 = xend; //this will be used in the main loop
            var ypxl2 = ipart(yend);
            if (steep)
            {
                plotFunction(ypxl2, xpxl2, rfpart(yend)*xgap*magnitude);
                plotFunction(ypxl2 + 1, xpxl2, fpart(yend)*xgap*magnitude);
            }
            else
            {
                plotFunction(xpxl2, ypxl2, rfpart(yend)*xgap*magnitude);
                plotFunction(xpxl2, ypxl2 + 1, fpart(yend)*xgap*magnitude);
            }

            // main loop
            for (var x = xpxl1 + 1; x < xpxl2; x++)
            {
                if (steep)
                {
                    plotFunction(ipart(intery), x, rfpart(intery)*magnitude);
                    plotFunction(ipart(intery) + 1, x, fpart(intery)*magnitude);
                }
                else
                {
                    plotFunction(x, ipart(intery), rfpart(intery)*magnitude);
                    plotFunction(x, ipart(intery) + 1, fpart(intery)*magnitude);
                }
                intery = intery + gradient;
            }
        }

        public static void Plot(Vector2D a, double magnitude, PlotWrapper w)
        {
            a = w.Converter(a);
            const double eps = 1e-9;
            var x = ipart(a.X);
            var y = ipart(a.Y);
            var dx1 = fpart(a.X);
            var dx0 = 1.0 - dx1;
            var dy1 = fpart(a.Y);
            var dy0 = 1.0 - dy1;
            PlotAreaFraction(x, y, dx0, dy0, eps, magnitude, w.Plot);
            PlotAreaFraction(x + 1, y, dx1, dy0, eps, magnitude, w.Plot);
            PlotAreaFraction(x, y + 1, dx0, dy1, eps, magnitude, w.Plot);
            PlotAreaFraction(x + 1, y + 1, dx1, dy1, eps, magnitude, w.Plot);
        }

        private static void PlotAreaFraction(int x, int y, double dx, double dy, double eps, double magnitude,
            DelegatePlotFunction plotFunction)
        {
            var f = dx*dy;
            if (dx > eps && dy > eps && f > eps)
                plotFunction(x, y, magnitude*dx*dy);
        }

        private static double fpart(double x)
        {
            var f = x - System.Math.Floor(x);
            return x < 0.0 ? 1.0 - f : f;
        }

        private static int ipart(double x)
        {
            return (int) x;
        }

        private static double rfpart(double x)
        {
            return 1.0 - fpart(x);
        }

        private static int round(double x)
        {
            return ipart(x + 0.5);
        }
    }
}