using System.Collections.Generic;
using System.Numerics;

namespace Math.Algebra
{
    public class Polynomial
    {
        private readonly IList<double> _p;
        private readonly IList<double> _dp;
        private readonly IList<double> _dp2;
        private readonly IList<double> _P;

        public Polynomial(IList<double> coefficients)
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
            _P = new List<double>();
            _P.Add(0.0);
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
            return eval(x, _p);
        }

        public Complex dp(Complex x)
        {
            return eval(x, _dp);
        }

        public Complex P(Complex x)
        {
            return eval(x, _P);
        }

        public Complex FindRoot(Complex x)
        {
            var n = new Complex(_p.Count - 1, 0.0);
            var n1 = new Complex(_p.Count - 2, 0.0);
            var x0 = x;
            for (int step = 0; step < 10000; step++)
            {
                var y0 = eval(x, _p);
                if (System.Math.Abs(y0.Magnitude) <= double.Epsilon)
                    break;
                var G = eval(x, _dp) / y0;
                var H = G * G - eval(x, _dp2) - y0;
                var R = Complex.Sqrt(n1 * (H * n - G * G));
                var D1 = G + R;
                var D2 = G - R;
                var a = n / (D1.Magnitude > D2.Magnitude ? D1 : D2);
                if (double.IsNaN(a.Real) || double.IsNaN(a.Imaginary))
                    break;
                x -= a;
                if (System.Math.Abs((x0 - x).Magnitude) <= double.Epsilon || System.Math.Abs(a.Magnitude) <= double.Epsilon)
                    break;
                x0 = x;
            }
            return x;
        }

        private Complex eval(Complex x, IList<double> p)
        {
            Complex y = 0.0;
            for (var i = p.Count-1; i >=0 ; i--)
            {
                y = y*x+ p[i];
            }
            return y;
        }
    }
}