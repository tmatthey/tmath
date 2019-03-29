namespace Math.Interfaces
{
    public interface ICurve<T,S> : IBoundingFacade<T>, IDimension, ICloneable, IIsEqual<S>
    {
        double Length(double accuracy=1e-5);
        T Evaluate(double t);
        T dEvaluate(double t);
        T d2Evaluate(double t);
        double Kappa(double t);
        T Tangent(double t);
        (S, S) Split(double t);
    }
}
