using Model;
using Presenter.Brushes;
using Presenter.Filters;
using View;

namespace Presenter
{
    internal static class ViewExtension
    {
        public static void ModifyImage(this IView view, RawImage image, IBrush brush, IFilter filter)
        {
            view.LockDrawArea();
            foreach (var pixel in brush.GetBrushPixels(image))
            {
                view.SetPixel(pixel.X, pixel.Y, filter.Filter(pixel.Color));
            }
            view.UnlockDrawArea();
        }
    }
}
