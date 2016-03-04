/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016 Thierry Matthey
 *
 * Permission is hereby granted, free of charge, to any person
 * obtaining x0 copy of this software and associated documentation
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
    static public class Draw
    {
        //
        // Xiaolin Wu's line algorithm is an algorithm for line antialiasing, which was presented 
        // in the article An Efficient Antialiasing Technique in the July 1991 issue of Computer 
        // Graphics, as well as in the article Fast Antialiasing in the June 1992 issue of 
        // Dr. Dobb's Journal.
        //
        // https://en.wikipedia.org/wiki/Xiaolin_Wu's_line_algorithm
        //
        static public void XiaolinWu(Vector2D a, Vector2D b, IBitmap p)
        {
            if (Comparison.IsZero(a.Distance(b)))
                return;
            a = p.Convert(a);
            b = p.Convert(b);
            var steep = System.Math.Abs(b.Y - a.Y) > System.Math.Abs(b.X - a.X);

            if (steep)
            {
                var t = a.X; a.X = a.Y; a.Y = t;
                t = b.X; b.X = b.Y; b.Y = t;
            }
            if (a.X > b.X)
            {
                Utils.Swap(ref a, ref b);
            }

            var dx = b.X - a.X;
            var dy = b.Y - a.Y;
            var gradient = dy / dx;

            // handle first endpoint
            var xend = round(a.X);
            var yend = a.Y + gradient * (xend - a.X);
            var xgap = rfpart(a.X + 0.5);
            var xpxl1 = xend; // this will be used in the main loop
            var ypxl1 = ipart(yend);
            if (steep)
            {
                p.Plot(ypxl1, xpxl1, rfpart(yend) * xgap);
                p.Plot(ypxl1 + 1, xpxl1, fpart(yend) * xgap);
            }
            else
            {
                p.Plot(xpxl1, ypxl1, rfpart(yend) * xgap);
                p.Plot(xpxl1, ypxl1 + 1, fpart(yend) * xgap);
            }
            var intery = yend + gradient; // first y-intersection for the main loop

            // handle second endpoint
            xend = round(b.X);
            yend = b.Y + gradient * (xend - b.X);
            xgap = fpart(b.X + 0.5);
            var xpxl2 = xend; //this will be used in the main loop
            var ypxl2 = ipart(yend);
            if (steep)
            {
                p.Plot(ypxl2, xpxl2, rfpart(yend) * xgap);
                p.Plot(ypxl2 + 1, xpxl2, fpart(yend) * xgap);
            }
            else
            {
                p.Plot(xpxl2, ypxl2, rfpart(yend) * xgap);
                p.Plot(xpxl2, ypxl2 + 1, fpart(yend) * xgap);
            }

            // main loop
            for (var x = xpxl1 + 1; x < xpxl2; x++)
            {
                if (steep)
                {
                    p.Plot(ipart(intery), x, rfpart(intery));
                    p.Plot(ipart(intery) + 1, x, fpart(intery));
                }
                else
                {
                    p.Plot(x, ipart(intery), rfpart(intery));
                    p.Plot(x, ipart(intery) + 1, fpart(intery));
                }
                intery = intery + gradient;
            }

        }

        private static double fpart(double x)
        {
            var f = x - System.Math.Floor(x);
            return (x < 0.0 ? 1.0 - f : f);
        }

        private static int ipart(double x)
        {
            return (int)x;
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
