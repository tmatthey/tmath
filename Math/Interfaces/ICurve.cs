namespace Math.Interfaces
{
    /// <summary>
    /// Interface of a curve
    /// </summary>
    /// <typeparam name="T">Vector type</typeparam>
    /// <typeparam name="S">Curve type</typeparam>
    public interface ICurve<T, S> : IBoundingFacade<T>, IDimension, IIsEqual<S>
    {
        /// <summary>
        /// Length of the curve
        /// </summary>
        /// <param name="accuracy">Relative accuracy</param>
        /// <returns></returns>
        double Length(double accuracy = 1e-5);

        /// <summary>
        /// Point at t
        /// </summary>
        /// <param name="t">parameter t, [0,1]</param>
        /// <returns></returns>
        T Evaluate(double t);

        /// <summary>
        /// Derivative at t
        /// </summary>
        /// <param name="t">parameter t, [0,1]</param>
        /// <returns>Derivative</returns>
        T dEvaluate(double t);

        /// <summary>
        /// 2nd derivative at t
        /// </summary>
        /// <param name="t">parameter t, [0,1]</param>
        /// <returns>2nd derivative</returns>
        T d2Evaluate(double t);

        /// <summary>
        /// Curvature of the curve at t
        /// </summary>
        /// <param name="t">parameter t, [0,1]</param>
        /// <returns></returns>
        double Kappa(double t);

        /// <summary>
        /// Tangent at t
        /// </summary>
        /// <param name="t">parameter t, [0,1]</param>
        /// <returns></returns>
        T Tangent(double t);

        /// <summary>
        /// Split curve into two parts at given split point
        /// </summary>
        /// <param name="t">split at t, [0,1]</param>
        /// <returns></returns>
        (S, S) Split(double t);
    }
}