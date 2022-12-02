﻿using Model;
using View;

namespace Presenter
{
    public class AppManager : IAppManager
    {
        private IView View { get; }
        private LoadedImage? LoadedImage { get; set; }
        private List<KeyValuePair<int, int>> _emptyQuantity;

        public Form? GetForm() => View as Form;

        public AppManager(IView view)
        {
            View = view;

            _emptyQuantity = new List<KeyValuePair<int, int>>(255);
            for (int i = 1; i < 256; i++)
                _emptyQuantity.Add(new KeyValuePair<int, int>(i, 0));

            View.LoadedFilenameChanged += HandleLoadedFilenameChanged;
        }

        private void HandleLoadedFilenameChanged(object? sender, string filename)
        {
            View.ClearArea();
            LoadImage(filename);

            DrawLoadedImage();
            View.RefreshArea();

            ComputeHistograms();
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

        private void ComputeHistograms()
        {
            var quantityR = new Dictionary<int, int>(_emptyQuantity);
            var quantityG = new Dictionary<int, int>(_emptyQuantity);
            var quantityB = new Dictionary<int, int>(_emptyQuantity);

            foreach (var pixel in LoadedImage!.Pixels())
            {
                ++quantityR[pixel.R];
                ++quantityG[pixel.G];
                ++quantityB[pixel.B];
            }

            View.SetRChart(quantityR.Keys.ToList(), quantityR.Values.ToList());
            View.SetGChart(quantityG.Keys.ToList(), quantityG.Values.ToList());
            View.SetBChart(quantityB.Keys.ToList(), quantityB.Values.ToList());
        }
    }
}
