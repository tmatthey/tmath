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
using System.Numerics;

namespace Math
{
    public class Polynomial
    {
        private readonly IList<double> _p;
        private readonly IList<double> _dp;
        private readonly IList<double> _dp2;
        private readonly IList<double> _P;

        public Polynomial(IEnumerable<double> coefficients)
        {
            // Polynomial
            _p = new List<double>(coefficients);

            if (_p.Count == 0)
                _p.Add(0.0);

            // Derivative
            _dp = new List<double>();
            for (var i = 1; i < _p.Count; i++)
            {
                _dp.Add(_p[i] * i);
            }

            _dp2 = new List<double>();
            for (var i = 1; i < _dp.Count; i++)
            {
                _dp2.Add(_dp[i] * i);
            }

            // Integral
            _P = new List<double> {0.0};
            for (var i = 0; i < _p.Count; i++)
            {
                _P.Add(_p[i] / (i + 1));
            }
        }

        public IList<double> p()
        {
            return _p;
        }

        public IList<double> dp()
        {
            return _dp;
        }

        public IList<double> P()
        {
            return _P;
        }

        public double p(double x)
        {
            return p(new Complex(x, 0.0)).Real;
        }

        public double dp(double x)
        {
            return dp(new Complex(x, 0.0)).Real;
        }

        public double P(double x)
        {
            return P(new Complex(x, 0.0)).Real;
        }

        public Complex p(Complex x)
        {
            return Eval(x, _p);
        }

        public Complex dp(Complex x)
        {
            return Eval(x, _dp);
        }

        public Complex P(Complex x)
        {
            return Eval(x, _P);
        }

        public Complex FindRoot(Complex x)
        {
            // https://en.wikipedia.org/wiki/Laguerre's_method
            var n = new Complex(_p.Count - 1, 0.0);
            var n1 = new Complex(_p.Count - 2, 0.0);
            var x0 = x;
            for (var step = 0; step < 10000; step++)
            {
                var y0 = Eval(x, _p);
                if (System.Math.Abs(y0.Magnitude) <= double.Epsilon)
                    break;
                var G = Eval(x, _dp) / y0;
                var H = G * G - Eval(x, _dp2) - y0;
                var R = Complex.Sqrt(n1 * (H * n - G * G));
                var D1 = G + R;
                var D2 = G - R;
                var a = n / (D1.Magnitude > D2.Magnitude ? D1 : D2);
                if (double.IsNaN(a.Real) ||
                    double.IsNaN(a.Imaginary) ||
                    System.Math.Abs(a.Magnitude) <= double.Epsilon ||
                    System.Math.Abs((x0 - (x - a)).Magnitude) <= double.Epsilon)
                    break;
                x -= a;
                x0 = x;
            }
            return x;
        }

        public Polynomial DivideByRoot(double c)
        {
            var n = _p.Count;
            var coefficients = new List<double>(new double[n - 1]);
            for (var i = n - 1; i > 0; i--)
            {
                coefficients[i - 1] = _p[i] + (i < n - 1 ? coefficients[i] * c : 0);

            }
            return new Polynomial(coefficients);
        }

        private static Complex Eval(Complex x, IList<double> p)
        {
            Complex y = 0.0;
            for (var i = p.Count - 1; i >= 0; i--)
            {
                y = y * x + p[i];
            }
            return y;
        }

    }
}