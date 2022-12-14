using Model;
using Presenter.Brushes;
using Presenter.Extensions;
using Presenter.Filters;
using View;
using View.Enums;

namespace Presenter
{
    public class AppManager : IAppManager
    {
        private readonly List<KeyValuePair<int, int>> _emptyQuantity;
        private readonly int _pointRadius;

        #region Brushes
        private readonly PaintBrush _paintBrush;
        private readonly PolygonBrush _polygonBrush;
        private readonly ImageBrush _imageBrush;
        #endregion Brushes

        #region Filters
        private readonly NoneFilter _noneFilter;
        private readonly NegativeFilter _negativeFilter;
        private readonly BrightnessFilter _brightnessFilter;
        private readonly GammaCorrectionFilter _gammaCorrectionFilter;
        private readonly ContrastFilter _contrastFilter;
        private readonly BezierFilter _bezierFilter;
        #endregion Filters

        private IView View { get; }
        private LoadedImage? LoadedImage { get; set; }
        private ShapedBrush Brush { get; set; }
        private IFilter Filter { get; set; }

        public Form? GetForm() => View as Form;

        public AppManager(IView view)
        {
            _emptyQuantity = new List<KeyValuePair<int, int>>(256);
            for (int i = 0; i < 256; ++i)
                _emptyQuantity.Add(new KeyValuePair<int, int>(i, 0));

            _pointRadius = 10;

            _paintBrush = new PaintBrush(55);
            _polygonBrush = new PolygonBrush();
            _imageBrush = new ImageBrush();

            _noneFilter = new NoneFilter();
            _negativeFilter = new NegativeFilter();
            _brightnessFilter = new BrightnessFilter(20);
            _gammaCorrectionFilter = new GammaCorrectionFilter(0.05);
            _contrastFilter = new ContrastFilter(10);
            _bezierFilter = new BezierFilter();

            View = view;

            Brush = _paintBrush;
            Filter = _noneFilter;
            View.SetBezierPoints(BezierFilter.DefaultBezierPointsArgs, BezierFilter.DefaultBezierPointsValues);

            InitViewHandlers();
        }

        private void InitViewHandlers()
        {
            View.LoadedFilenameChanged += HandleLoadedFilenameChanged;

            View.CanvasClicked += HandleCanvasClicked;
            View.CanvasMouseMoved += HandleCanvasMouseMoved;
            View.CanvasClickedMouseUp += HandleCanvasClickedMouseUp;

            View.PaintBrushValueChanged += HandlePaintBrushValueChanged;
            View.BrushShapeChanged += HandleBrushShapeChanged;
            View.RemovePolygonBrushClicked += HandleRemovePolygonBrushClicked;
            View.ApplyPolygonFilter += HandleApplyFilter;

            View.ContrastValueChanged += HandleContrastValueChanged;
            View.BrightnessValueChanged += HandleBrightnessValueChanged;
            View.GammaCorrectionValueChanged += HandleGammaCorrectionValueChanged;

            View.NoneFilterChecked += HandleNoneFilterChecked;
            View.NegativeFilterChecked += HandleNegativeFilterChecked;
            View.BrightnessFilterChecked += HandleBrightnessFilterChecked;
            View.GammaCorrectionFilterChecked += HandleGammaCorrectionFilterChecked;
            View.ContrastFilterChecked += HandleContrastFilterChecked;
            View.BezierFilterChecked += HandleBezierFilterChecked;

            View.BezierPointMoved += HandleBezierPointMoved;
        }

        #region Handlers
        private void HandleBrushShapeChanged(object? sender, EventArgs e)
        {
            switch (View.BrushShape)
            {
                case BrushShape.WholeImage:
                    View.ToggleApplyButton(true);
                    RedrawImage();
                    View.RefreshArea();
                    Brush = _imageBrush;
                    break;
                case BrushShape.Paintbrush:
                    View.ToggleApplyButton(false);
                    RedrawImage();
                    View.RefreshArea();
                    Brush = _paintBrush;
                    break;
                case BrushShape.Polygon:
                    View.ToggleApplyButton(false);
                    Brush = _polygonBrush;
                    break;
                default:
                    break;
            }
        }

        private void HandlePaintBrushValueChanged(object? sender, int e) => _paintBrush.Radius = e;

        private void HandleRemovePolygonBrushClicked(object? sender, EventArgs e)
        {
            View.ToggleApplyButton(false);
            Brush.ClearBrushPoints();
            Brush.CanBrush = false;
            RedrawImage();
            View.RefreshArea();
        }

        private void HandleApplyFilter(object? sender, EventArgs e)
        {
            if (LoadedImage is not null)
            {
                DrawFilteredImage();
                LoadedImage.Untouch();

                if (View.BrushShape == BrushShape.Polygon)
                {
                    var prev = Brush.BrushPoints.Last();
                    foreach (var p in Brush.BrushPoints)
                    {
                        View.DrawVertex(p);
                        View.DrawLine(prev, p);
                        prev = p;
                    }
                }

                View.RefreshArea(); 
            }
        }

        private void HandleGammaCorrectionValueChanged(object? sender, decimal e) => _gammaCorrectionFilter.Gamma = (double)e;

        private void HandleBrightnessValueChanged(object? sender, decimal e) => _brightnessFilter.Modifier = (int)e;

        private void HandleContrastValueChanged(object? sender, decimal e) => _contrastFilter.Contrast = (int)e;

        private void HandleBezierPointMoved(object? sender, (int idx, Point newPoint) e)
        {
            if (Filter is BezierFilter bezierFilter)
            {
                bezierFilter.SetBezierPoint(e.idx, e.newPoint);
                View.SetBezierCurve(bezierFilter.BezierArgs, bezierFilter.BezierValues);
            }
        }

        private void HandleCanvasClickedMouseUp(object? sender, MouseEventArgs e) => LoadedImage?.Untouch();

        private void HandleCanvasMouseMoved(object? sender, MouseEventArgs e)
        {
            if (LoadedImage is not null && View.BrushShape == BrushShape.Paintbrush && View.IsCanvasClicked)
            {
                Brush.ClearBrushPoints();
                Brush.AddBrushPoint(e.Location);
                DrawFilteredImage();
                View.RefreshArea();
            }
        }

        private void HandleCanvasClicked(object? sender, MouseEventArgs e)
        {
            if (LoadedImage is not null)
            {
                switch (View.BrushShape)
                {
                    case BrushShape.Paintbrush:
                        Brush.ClearBrushPoints();
                        Brush.AddBrushPoint(e.Location);
                        DrawFilteredImage();
                        View.RefreshArea();
                        break;
                    case BrushShape.Polygon:
                        if (!Brush.CanBrush)
                        {
                            if (Brush.BrushPoints.Any())
                            {
                                var p = e.Location;
                                if (Brush.BrushPoints.First().Clicked(e.Location, _pointRadius))
                                {
                                    p = Brush.BrushPoints.First();
                                    View.ToggleApplyButton();
                                    Brush.CanBrush = true;
                                    View.DrawLine(Brush.BrushPoints.Last(), p);
                                }
                                else
                                {
                                    View.DrawLine(Brush.BrushPoints.Last(), p);
                                    Brush.AddBrushPoint(p);
                                    View.DrawVertex(p);
                                }
                            }
                            else
                            {
                                Brush.AddBrushPoint(e.Location);
                                View.DrawVertex(e.Location);
                            }

                            View.RefreshArea();
                        }
                        break;
                    default:
                        break;
                }
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

        private void HandleNoneFilterChecked(object? sender, EventArgs e) => Filter = _noneFilter;

        private void HandleNegativeFilterChecked(object? sender, EventArgs e) => Filter = _negativeFilter;

        private void HandleBrightnessFilterChecked(object? sender, EventArgs e) => Filter = _brightnessFilter;

        private void HandleGammaCorrectionFilterChecked(object? sender, EventArgs e) => Filter = _gammaCorrectionFilter;

        private void HandleContrastFilterChecked(object? sender, EventArgs e) => Filter = _contrastFilter;

        private void HandleBezierFilterChecked(object? sender, EventArgs e)
        {
            var bezierFilter = _bezierFilter;
            Filter = bezierFilter;
            View.SetBezierCurve(bezierFilter.BezierArgs, bezierFilter.BezierValues);
        }
        #endregion Handlers

        private void LoadImage(string path)
        {
            var bitmap = new Bitmap(path);
            var scale = Math.Max((double)bitmap.Width / View.CanvasSize.Width, bitmap.Height / (double)View.CanvasSize.Height);
            LoadedImage = new LoadedImage(new Bitmap(bitmap, new((int)(bitmap.Width / scale), (int)(bitmap.Height / scale))));
        }

        private void DrawLoadedImage()
        {
            LoadedImage!.Lock();
            View.LockDrawArea();
            foreach (var pixel in LoadedImage!.Pixels())
            {
                View.SetPixel(pixel.X, pixel.Y, pixel.Color);
            }
            View.UnlockDrawArea();
            LoadedImage!.Unlock();
        }

        private void RedrawImage()
        {
            if (LoadedImage is not null)
            {
                View.ClearArea();
                DrawLoadedImage();
            }
        }

        private void DrawFilteredImage()
        {
            View.ModifyImage(LoadedImage!, Brush, Filter);

            ComputeHistograms();
        }

        private void ComputeHistograms()
        {
            var quantityR = new Dictionary<int, int>(_emptyQuantity);
            var quantityG = new Dictionary<int, int>(_emptyQuantity);
            var quantityB = new Dictionary<int, int>(_emptyQuantity);

            LoadedImage!.Lock();
            foreach (var pixel in LoadedImage!.Pixels())
            {
                ++quantityR[pixel.R];
                ++quantityG[pixel.G];
                ++quantityB[pixel.B];
            }
            LoadedImage!.Unlock();

            View.SetRChart(quantityR.Keys.ToList(), quantityR.Values.ToList());
            View.SetGChart(quantityG.Keys.ToList(), quantityG.Values.ToList());
            View.SetBChart(quantityB.Keys.ToList(), quantityB.Values.ToList());
        }
    }
}
