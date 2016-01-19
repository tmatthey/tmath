namespace Math
{
    public static class Comparison
    {
        public const double Epsilon = 1e-13;//double.Epsilon;

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
    }
}