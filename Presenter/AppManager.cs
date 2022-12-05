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
        private ShapedBrush Brush { get; set; }
        private IFilter Filter { get; set; }

        public Form? GetForm() => View as Form;

        public AppManager(IView view)
        {
            _emptyQuantity = new List<KeyValuePair<int, int>>(256);
            for (int i = 0; i < 256; ++i)
                _emptyQuantity.Add(new KeyValuePair<int, int>(i, 0));

            _pointRadius = 10;

            _noneFilter = new Lazy<NoneFilter>(() => new NoneFilter());
            _negativeFilter = new Lazy<NegativeFilter>(() => new NegativeFilter());
            _brightnessFilter = new Lazy<BrightnessFilter>(() => new BrightnessFilter(50));
            _gammaCorrectionFilter = new Lazy<GammaCorrectionFilter>(() => new GammaCorrectionFilter(0.5));
            _contrastFilter = new Lazy<ContrastFilter>(() => new ContrastFilter(100));
            _bezierFilter = new Lazy<BezierFilter>(() => new BezierFilter());

            View = view;

            Brush = new PaintBrush(100);
            Filter = new NoneFilter();
            View.SetBezierPoints(BezierFilter.DefaultBezierPointsArgs, BezierFilter.DefaultBezierPointsValues);

            InitViewHandlers();
        }

        private void InitViewHandlers()
        {
            View.LoadedFilenameChanged += HandleLoadedFilenameChanged;

            View.CanvasClicked += HandleCanvasClicked;
            View.CanvasMouseMoved += HandleCanvasMouseMoved;
            View.CanvasClickedMouseUp += HandleCanvasClickedMouseUp;

            View.BrushShapeChanged += HandleBrushShapeChanged;
            View.RemovePolygonBrushClicked += HandleRemovePolygonBrushClicked;
            View.ApplyPolygonFilter += HandleApplyPolygonFilter;

            View.NoneFilterChecked += HandleNoneFilterChecked;
            View.NegativeFilterChecked += HandleNegativeFilterChecked;
            View.BrightnessFilterChecked += HandleBrightnessFilterChecked;
            View.GammaCorrectionFilterChecked += HandleGammaCorrectionFilterChecked;
            View.ContrastFilterChecked += HandlenContrastFilterChecked;
            View.BezierFilterChecked += HandleBezierFilterChecked;

            View.BezierPointMoved += HandleBezierPointMoved;
        }

        private void HandleApplyPolygonFilter(object? sender, EventArgs e)
        {
            DrawFilteredImage();
            LoadedImage!.Untouch();

            var prev = Brush.BrushPoints.Last();
            foreach (var p in Brush.BrushPoints)
            {
                View.DrawVertex(p);
                View.DrawLine(prev, p);
                prev = p;
            }
            View.RefreshArea();
        }

        #region Handlers
        private void HandleBrushShapeChanged(object? sender, EventArgs e)
        {
            switch (View.BrushShape)
            {
                case BrushShape.Paintbrush:
                    View.ToggleApplyButton(false);
                    RedrawImage();
                    View.RefreshArea();
                    Brush = new PaintBrush(100);
                    break;
                case BrushShape.Polygon:
                    View.ToggleApplyButton(false);
                    Brush = new PolygonBrush();
                    break;
                default:
                    break;
            }
        }

        private void HandleRemovePolygonBrushClicked(object? sender, EventArgs e)
        {
            View.ToggleApplyButton(false);
            Brush.ClearBrushPoints();
            Brush.CanBrush = false;
            RedrawImage();
            View.RefreshArea();
        }

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

        private void HandleNoneFilterChecked(object? sender, EventArgs e) => Filter = _noneFilter.Value;

        private void HandleNegativeFilterChecked(object? sender, EventArgs e) => Filter = _negativeFilter.Value;

        private void HandleBrightnessFilterChecked(object? sender, EventArgs e) => Filter = _brightnessFilter.Value;

        private void HandleGammaCorrectionFilterChecked(object? sender, EventArgs e) => Filter = _gammaCorrectionFilter.Value;

        private void HandlenContrastFilterChecked(object? sender, EventArgs e) => Filter = _contrastFilter.Value;

        private void HandleBezierFilterChecked(object? sender, EventArgs e)
        {
            var bezierFilter = _bezierFilter.Value;
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
            View.LockDrawArea();
            foreach (var pixel in LoadedImage!.Pixels())
            {
                View.SetPixel(pixel.X, pixel.Y, pixel.Color);
            }
            View.UnlockDrawArea();
        }

        private void RedrawImage()
        {
            View.ClearArea();
            DrawLoadedImage();
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
