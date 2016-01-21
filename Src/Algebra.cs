using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math.Algebra
{
    public class Polynomial
    {
        private readonly IList<double> _p;
        private readonly IList<double> _dp;
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

            // Integral
            _P = new List<double>();
            _P.Add(0.0);
            for (var i = 0; i < _p.Count; i++)
            {
                _P.Add(_p[i] / (i+1));
            }
        }

        public IList<double> p() { return _p; }
        public IList<double> dp() { return _dp; }
        public IList<double> P() { return _P; }

        public double p(double x) { return eval(x, _p); }
        public double dp(double x) { return eval(x, _dp); }
        public double P(double x) { return eval(x, _P); }

        private double eval(double x, IList<double> p)
        {
            double y = 0;
            double u = 1;
            foreach(var c in p)
            {
                y += u * c;
                u *= x;
            }
            return y;
        }
    }
}
