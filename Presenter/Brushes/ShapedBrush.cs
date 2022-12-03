using Model;

namespace Presenter.Brushes
{
    internal abstract class ShapedBrush : IBrush
    {
        private readonly List<Point> _brushPoints;
        protected List<Point> BrushPoints => _brushPoints.ToList();

        protected ShapedBrush() => _brushPoints = new List<Point>();

        public void AddBrushPoint(Point p) => _brushPoints.Add(p);
        public void ClearBrushPoints() => _brushPoints.Clear();

        public abstract IEnumerable<Pixel> GetBrushPixels(LoadedImage image);
    }
}
