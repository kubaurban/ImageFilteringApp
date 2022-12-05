using Model;

namespace Presenter.Brushes
{
    internal class ImageBrush : ShapedBrush
    {
        public override IEnumerable<Pixel> GetBrushPixels(LoadedImage image)
        {
            foreach (var pixel in image.Pixels())
            {
                yield return pixel;
            }
        }
    }
}
