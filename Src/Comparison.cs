using System.Collections.Generic;

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
            var res = new List<double>(v);
            res.Sort();
            for (int i = 0; i + 1 < res.Count;)
            {
                if (IsEqual(res[i], res[i + 1], eps))
                {
                    res.RemoveAt(i + 1);
                }
                else
                {
                    i++;
                }
            }

            return res;
        }
    }
}