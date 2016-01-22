using System.Collections.Generic;
using System.Linq;

namespace Math
{
    public static class Comparison
    {
        public const double Epsilon = 1e-13;//double.Epsilon;

        public static bool IsEqual(double x, double y)
        {
            return IsEqual(x, y, Epsilon);
        }

        public static bool IsEqual(double x, double y, double eps)
        {
            return (System.Math.Abs(x - y) < eps);
        }

        public static bool IsNumber(double x)
        {
            return !(double.IsNaN(x) || double.IsInfinity(x));
        }

        public static bool IsZero(double x)
        {
            return IsZero(x, Epsilon);
        }

        public static bool IsZero(double x, double eps)
        {
            return (-eps <= x && x <= eps);
        }

        public static bool IsPositive(double x)
        {
            return IsPositive(x, Epsilon);
        }

        public static bool IsPositive(double x, double eps)
        {
            return (eps < x && x < double.PositiveInfinity);
        }

        public static bool IsNegative(double x)
        {
            return IsNegative(x, Epsilon);
        }

        public static bool IsNegative(double x, double eps)
        {
            return (double.NegativeInfinity < x && x < -eps);
        }

        public static IList<double> UniqueSorted(IList<double> v)
        {
            return UniqueSorted(v, Epsilon);
        }

        public static IList<double> UniqueSorted(IList<double> v, double eps)
        {
            var vTmp = new List<double>(v);
            vTmp.Sort();
            var res = new List<double>();
            var tmp = new List<double>();
            for (var i = 0; i < vTmp.Count; )
            {
                if (i + 1 < vTmp.Count && IsEqual(vTmp[i], vTmp[i + 1], eps * 2.0))
                {
                    if (tmp.Count == 0)
                    {
                        tmp.Add(vTmp[i]);
                    }
                    tmp.Add(vTmp[i + 1]);
                    vTmp.RemoveAt(i + 1);
                }
                else if (tmp.Count > 0)
                {
                    res.Add(tmp.Sum()/tmp.Count);
                    tmp.Clear();
                    i++;
                }
                else
                {
                    res.Add(vTmp[i]);
                    i++;
                }
            }

            return res;
        }
    }
}