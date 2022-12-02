using Model;
using Presenter.Brushes;
using Presenter.Filters;
using View;

namespace Presenter
{
    internal static class ViewExtension
    {
        public static void ModifyImage(this IView view, LoadedImage image, IBrush brush, IFilter filter)
        {
            image.Lock();
            view.LockDrawArea();
            foreach (var pixel in brush.GetBrushPixels(image))
            {
                var filteredColor = filter.Filter(pixel.Color);

                image.SetPixel(pixel.X, pixel.Y, filteredColor);
                view.SetPixel(pixel.X, pixel.Y, filteredColor);
            }
            view.UnlockDrawArea();
            image.Unlock();
        }
    }
}
