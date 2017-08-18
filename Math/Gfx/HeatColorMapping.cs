/*
 * ***** BEGIN LICENSE BLOCK *****
 * Version: MIT
 *
 * Copyright (c) 2016-2017 Thierry Matthey
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
    public class HeatColorMapping : AColorMapping
    {
        public static readonly IColorMapping Default = new HeatColorMapping();

        public override Color Color(double c)
        {
            const double c0 = 0.0;
            const double c1 = 1.0 / 3.0;
            const double c2 = 2.0 / 3.0;
            const double c3 = 1.0;
            if (Comparison.IsLessEqual(c, c0))
                return Gfx.Color.Default.White;
            if (Comparison.IsEqual(c, c1))
                return Gfx.Color.Default.Green;
            if (Comparison.IsEqual(c, c2))
                return Gfx.Color.Default.Yellow;
            if (Comparison.IsLessEqual(c3, c))
                return Gfx.Color.Default.Red;

            double b;
            Color col0;
            Color col1;
            if (c < c1)
            {
                b = c * 3.0;
                col0 = Gfx.Color.Default.LigthBlue;
                col1 = Gfx.Color.Default.Green;
            }
            else if (c < c2)
            {
                b = (c - c1) * 3.0;
                col0 = Gfx.Color.Default.Green;
                col1 = Gfx.Color.Default.Yellow;
            }
            else
            {
                b = (c - c2) * 3.0;
                col0 = Gfx.Color.Default.Yellow;
                col1 = Gfx.Color.Default.Red;
            }
            var a = 1.0 - b;
            return new Color(
                (byte) (col0.Red * a + col1.Red * b),
                (byte) (col0.Green * a + col1.Green * b),
                (byte) (col0.Blue * a + col1.Blue * b));
        }
    }
}