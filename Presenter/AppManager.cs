﻿using Model;
using Presenter.Brushes;
using Presenter.Extensions;
using Presenter.Filters;
using View;

namespace Presenter
{
    public class AppManager : IAppManager
    {
        private readonly List<KeyValuePair<int, int>> _emptyQuantity;

        #region Filters
        private Lazy<NoneFilter> _noneFilter;
        private Lazy<NegativeFilter> _negativeFilter;
        private Lazy<BrightnessFilter> _brightnessFilter;
        private Lazy<GammaCorrectionFilter> _gammaCorrectionFilter;
        private Lazy<ContrastFilter> _contrastFilter;
        private Lazy<BezierFilter> _bezierFilter;
        #endregion Filters

        private IView View { get; }
        private LoadedImage? LoadedImage { get; set; }
        private IFilter Filter { get; set; }

        public Form? GetForm() => View as Form;

        public AppManager(IView view)
        {
            _emptyQuantity = new List<KeyValuePair<int, int>>(256);
            for (int i = 0; i < 256; ++i)
                _emptyQuantity.Add(new KeyValuePair<int, int>(i, 0));

            _noneFilter = new Lazy<NoneFilter>(() => new NoneFilter());
            _negativeFilter = new Lazy<NegativeFilter>(() => new NegativeFilter());
            _brightnessFilter = new Lazy<BrightnessFilter>(() => new BrightnessFilter(50));
            _gammaCorrectionFilter = new Lazy<GammaCorrectionFilter>(() => new GammaCorrectionFilter(0.5));
            _contrastFilter = new Lazy<ContrastFilter>(() => new ContrastFilter(100));
            _bezierFilter = new Lazy<BezierFilter>(() => new BezierFilter());

            View = view;
            Filter = new NoneFilter();

            View.LoadedFilenameChanged += HandleLoadedFilenameChanged;
            View.CanvasClicked += HandleCanvasClicked;
            View.CanvasClickedMouseMoved += HandleCanvasClickedMouseMoved;
            View.CanvasClickedMouseUp += HandleCanvasClickedMouseUp;
            View.NoneFilterChecked += HandleNoneFilterChecked;
            View.NegativeFilterChecked += HandleNegativeFilterChecked;
            View.BrightnessFilterChecked += HandleBrightnessFilterChecked;
            View.GammaCorrectionFilterChecked += HandleGammaCorrectionFilterChecked;
            View.ContrastFilterChecked += HandlenContrastFilterChecked;
            View.BezierFilterChecked += HandleBezierFilterChecked;
        }

        private void HandleCanvasClickedMouseUp(object? sender, MouseEventArgs e) => LoadedImage?.Untouch();

        private void HandleCanvasClickedMouseMoved(object? sender, MouseEventArgs e)
        {
            if (LoadedImage is not null)
            {
                DrawFilteredImage(e.Location);
                View.RefreshArea();

                ComputeHistograms();
            }
        }

        private void HandleCanvasClicked(object? sender, MouseEventArgs e)
        {
            if (LoadedImage is not null)
            {
                DrawFilteredImage(e.Location);
                View.RefreshArea();

                ComputeHistograms();
            }
        }

        private void HandleLoadedFilenameChanged(object? sender, string filename)
        {
            View.ClearArea();
            LoadImage(filename);

            DrawLoadedImage();
            View.RefreshArea();

            ComputeHistograms();
        }

        private void HandleNoneFilterChecked(object? sender, EventArgs e) => Filter = _noneFilter.Value;

        private void HandleNegativeFilterChecked(object? sender, EventArgs e) => Filter = _negativeFilter.Value;

        private void HandleBrightnessFilterChecked(object? sender, EventArgs e) => Filter = _brightnessFilter.Value;

        private void HandleGammaCorrectionFilterChecked(object? sender, EventArgs e) => Filter = _gammaCorrectionFilter.Value;

        private void HandlenContrastFilterChecked(object? sender, EventArgs e) => Filter = _contrastFilter.Value;

        private void HandleBezierFilterChecked(object? sender, EventArgs e)
        {
            var bezierFilter = _bezierFilter.Value;
            Filter = bezierFilter;
            View.SetBezierChart(bezierFilter.BezierArgs, bezierFilter.BezierValues, bezierFilter.BezierPointsArgs, bezierFilter.BezierPointsArgs);
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

        private void DrawFilteredImage(Point click)
        {
            View.ModifyImage(LoadedImage!, new PaintBrush(100, click), Filter);
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
