using Model;

namespace Presenter.Brushes
{
    internal interface IBrush
    {
        IEnumerable<Pixel> GetBrushPixels(RawImage image);
    }
}
