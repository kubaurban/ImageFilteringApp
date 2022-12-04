using Model;

namespace Presenter.Brushes
{
    internal class PolygonBrush : ShapedBrush
    {
        public override IEnumerable<Pixel> GetBrushPixels(LoadedImage image)
        {
            if (!CanBrush)
                throw new InvalidOperationException();

            throw new NotImplementedException();
        }
    }
}
