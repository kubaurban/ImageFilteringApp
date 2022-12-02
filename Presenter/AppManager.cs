using Model;
using View;

namespace Presenter
{
    public class AppManager : IAppManager
    {
        private IView View { get; }
        private LoadedImage? LoadedImage { get; set; }

        public Form? GetForm() => View as Form;

        public AppManager(IView view)
        {
            View = view;

            View.LoadedFilenameChanged += HandleLoadedFilenameChanged;
        }

        private void HandleLoadedFilenameChanged(object? sender, string filename)
        {
            View.ClearArea();
            LoadImage(filename);

            DrawLoadedImage();
            View.RefreshArea();
        }

        private void LoadImage(string path)
        {
            var bitmap = new Bitmap(path);
            var scale = Math.Max((double)bitmap.Width / View.CanvasSize.Width, bitmap.Height / (double)View.CanvasSize.Height);
            LoadedImage = new LoadedImage(new Bitmap(bitmap, new((int)(bitmap.Width / scale), (int)(bitmap.Height / scale))));
        }

        private void DrawLoadedImage()
        {
            if (LoadedImage is not null)
            {
                View.LockDrawArea();
                foreach (var pixel in LoadedImage.Pixels())
                {
                    View.SetPixel(pixel.X, pixel.Y, pixel.Color);
                }
                View.UnlockDrawArea(); 
            }
        }
    }
}
