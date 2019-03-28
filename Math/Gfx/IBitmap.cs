namespace Math.Gfx
{
    public interface IBitmap
    {
        PlotWrapper Add { get; }
        PlotWrapper Set { get; }
        PlotWrapper AddMagnitude { get; }
        PlotWrapper SetMagnitude { get; }
        void PixelAdd(int x, int y, double c);
        void PixelSet(int x, int y, double c);
        Vector2D ConvertToBitmap(Vector2D x);
        bool IsInRange(int x, int y);
    }
}