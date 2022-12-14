using Model;
using Presenter.Brushes;
using Presenter.Filters;
using View;

namespace Presenter.Extensions
{
    internal static class ViewExtension
    {
        public static void ModifyImage(this IView view, LoadedImage image, IBrush brush, IFilter filter)
        {
            image.Lock();
            view.LockDrawArea();
            foreach (var pixel in brush.GetBrushPixels(image))
            {
                if (!image.Touched[pixel.X, pixel.Y])
                {
                    var filteredColor = filter.Filter(pixel.Color);

                    image.SetPixel(pixel.X, pixel.Y, filteredColor);
                    view.SetPixel(pixel.X, pixel.Y, filteredColor);
                }
            }
            view.UnlockDrawArea();
            image.Unlock();
        }
    }
}
