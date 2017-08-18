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

using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Math.Interfaces;

namespace Math
{
    /// <summary> 
    /// Representation of a polynomial of n-th degree by coefficients. Functions to evaluate the polynomial, and also 1st derivatives and its integral. 
    /// </summary> 
    /// <remarks> 
    /// General root finder based on Laguerre's_method.
    /// </remarks> 
    public class Polynomial : ICloneable
    {
        private readonly IList<double> _dp;
        private readonly IList<double> _dp2;
        private readonly IList<double> _frac = new List<double> {0.0, 0.5, 0.25, 0.13, 0.38, 0.62, 0.88, 1.0};
        private readonly IList<double> _p;
        private readonly IList<double> _P;
        private readonly int MR;
        private readonly int MT = 10;

        /// <summary>
        /// Defining a polynomial by coefficients of n-th degree.
        /// </summary>
        /// <param name="coefficients">Coefficients in decreasing order. E.g., 2x^2 + x + 3 as {2,1,3}.</param>
        public Polynomial(IEnumerable<double> coefficients)
        {
            MR = _frac.Count - 1;

            // Polynomial
            _p = new List<double>(coefficients);
            while (_p.Count > 1 && Comparison.IsZero(_p.Last()))
            {
                _p.RemoveAt(_p.Count - 1);
            }

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

        public Polynomial(Polynomial a) : this(new List<double>(a.p()))
        {
        }

        public object Clone()
        {
            return new Polynomial(this);
        }

        /// <summary>
        /// Returns the polynomial coefficients 
        /// </summary> 
        public IList<double> p()
        {
            return _p;
        }

        /// <summary>
        /// Returns the 1st derivative of the polynomial 
        /// </summary> 
        public IList<double> dp()
        {
            return _dp;
        }

        /// <summary>
        /// Returns the Integral of the polynomial 
        /// </summary> 
        public IList<double> P()
        {
            return _P;
        }

        /// <summary>
        /// Evaluates the polynomial 
        /// </summary> 
        public double p(double x)
        {
            return p(new Complex(x, 0.0)).Real;
        }

        /// <summary>
        /// Evaluates for 1st derivative of polynomial 
        /// </summary> 
        public double dp(double x)
        {
            return dp(new Complex(x, 0.0)).Real;
        }

        /// <summary>
        /// Evaluates for integral of polynomial 
        /// </summary> 
        public double P(double x)
        {
            return P(new Complex(x, 0.0)).Real;
        }

        /// <summary>
        /// Evaluates polynomial for complex numbers
        /// </summary> 
        public Complex p(Complex x)
        {
            return Eval(x, _p);
        }

        /// <summary>
        /// Evaluates 1st derivative of polynomial for complex numbers
        /// </summary> 
        public Complex dp(Complex x)
        {
            return Eval(x, _dp);
        }

        /// <summary>
        /// Evaluates the integral of polynomial for complex numbers
        /// </summary> 
        public Complex P(Complex x)
        {
            return Eval(x, _P);
        }

        /// <summary>
        /// Generic root solver based on Laguerre's_method.
        /// </summary> 
        /// <param name="x">Start point for finding a root.
        /// </param>
        public Complex FindRoot(Complex x)
        {
            // https://en.wikipedia.org/wiki/Laguerre's_method
            // Numerical Recipes
            var n = new Complex(_p.Count - 1, 0.0);
            var n1 = new Complex(_p.Count - 2, 0.0);
            for (var i = 0; i < MT * MR; i++)
            {
                var y0 = Eval(x, _p);
                if (y0.Magnitude <= double.Epsilon)
                    break;
                var G = Eval(x, _dp) / y0;
                var G2 = G * G;
                var H = G2 - Eval(x, _dp2) / y0;
                var R = Complex.Sqrt(n1 * (H * n - G2));
                var GP = G + R;
                var gp = GP.Magnitude;
                var GM = G - R;
                var gm = GM.Magnitude;
                var dx = System.Math.Max(gp, gm) > 0.0
                    ? n / (gp > gm ? GP : GM)
                    : Complex.FromPolarCoordinates(1.0 + x.Magnitude, i + 1);
                var x0 = x - dx;
                if (x == x0)
                {
                    break;
                }
                if ((i + 1) % MT != 0)
                {
                    x -= dx;
                }
                else
                {
                    x -= _frac[(i + 1) / MT] * dx;
                }
            }
            return x;
        }

        /// <summary>
        /// Returns the polynomial divided by a root
        /// </summary> 
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

        /// <summary>
        /// Returns the polynomial divided by a complex root and with its conjugated
        /// </summary> 
        public Polynomial DivideByRootAndConjugate(Complex c)
        {
            var n = _p.Count;
            var c0 = new List<Complex>(new Complex[n - 1]);
            for (var i = n - 1; i > 0; i--)
            {
                c0[i - 1] = _p[i] + (i < n - 1 ? c0[i] * c : 0);
            }
            n--;
            var c1 = new List<double>(new double[n - 1]);
            var d = Complex.Conjugate(c);
            for (var i = n - 1; i > 0; i--)
            {
                c1[i - 1] = (c0[i] + (i < n - 1 ? c1[i] * d : 0)).Real;
            }
            return new Polynomial(c1);
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