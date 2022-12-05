using FastBitmapLib;
using System.Windows.Forms.DataVisualization.Charting;
using View.Enums;

namespace View
{
    public partial class GUI : Form, IView
    {
        private const string BEZIER_CURVE_SERIES = "BezierCurve";
        private const string BEZIER_POINTS_SERIES = "BezierPoints";
        private const string BEZIER_POINTS_CURVE_SERIES = "BezierPointsCurve";

        private Chart BezierChart { get; set; }
        private Chart RChart { get; set; }
        private Chart GChart { get; set; }
        private Chart BChart { get; set; }

        public event EventHandler BrushShapeChanged;
        public event EventHandler RemovePolygonBrushClicked;
        public event EventHandler NoneFilterChecked;
        public event EventHandler NegativeFilterChecked;
        public event EventHandler BrightnessFilterChecked;
        public event EventHandler GammaCorrectionFilterChecked;
        public event EventHandler ContrastFilterChecked;
        public event EventHandler BezierFilterChecked;
        public event EventHandler ApplyPolygonFilter;
        public event EventHandler<decimal> ContrastValueChanged;
        public event EventHandler<decimal> BrightnessValueChanged;
        public event EventHandler<decimal> GammaCorrectionValueChanged;
        public event EventHandler<(int, Point)> BezierPointMoved;
        public event MouseEventHandler CanvasClicked;
        public event MouseEventHandler CanvasMouseMoved;
        public event MouseEventHandler CanvasClickedMouseUp;
        public event EventHandler<string> LoadedFilenameChanged;

        private BrushShape _currentBrushShape;
        private FilterMethod _currentFilterMethod;
        private readonly Bitmap _drawArea;
        private readonly FastBitmap _fastDrawArea;
        private int _vertexSize;
        private int _canvasMargin;
        private Color _defaultColor;

        private Graphics Graphics => Graphics.FromImage(_drawArea);
        private int MovedBezierPointIdx { get; set; }

        public BrushShape BrushShape => _currentBrushShape;
        public FilterMethod FilterMethod => _currentFilterMethod;

        public Size CanvasSize => new(Canvas.Width - 2 * _canvasMargin, Canvas.Height - 2 * _canvasMargin);
        public bool IsCanvasClicked { get; set; }

        public GUI()
        {
            InitializeComponent();
            InitializeCharts();

            _drawArea = new Bitmap(Canvas.Width, Canvas.Height);
            _fastDrawArea = new FastBitmap(_drawArea);
            Canvas.Image = _drawArea;

            InitDefaultState();
        }

        private void InitDefaultState()
        {
            _canvasMargin = 10;
            _vertexSize = 8;
            _defaultColor = Color.Black;
            NoneButton.Checked = true;
            _currentFilterMethod = FilterMethod.None;
            RemovePolygonButton.Enabled = false;
            ApplyButton.Enabled = false;
            RChart.Enabled = false;
            GChart.Enabled = false;
            BChart.Enabled = false;
            BrushShapeLabel.Text = "Brush type: Paintbrush";
            _currentBrushShape = BrushShape.Paintbrush;
            MovedBezierPointIdx = -1;
        }

        public void ToggleApplyButton(bool? enable)
        {
            ApplyButton.Enabled = enable ?? !ApplyButton.Enabled;
        }

        #region Charts
        private void InitializeCharts()
        {
            // BezierChart
            BezierChart = CreateChart("BezierChart", "Bezier curve");
            tableLayoutPanel1.Controls.Add(BezierChart, 0, 3);
            tableLayoutPanel1.SetRowSpan(tableLayoutPanel1.Controls["BezierChart"], 3);
            BezierChart.Dock = DockStyle.Fill;
            BezierChart.Series.Add(new Series()
            {
                Name = BEZIER_CURVE_SERIES,
                ChartType = SeriesChartType.Spline,
                Color = Color.Blue,
            });
            BezierChart.Series.Add(new Series()
            {
                Name = BEZIER_POINTS_CURVE_SERIES,
                ChartType = SeriesChartType.Line,
                Color = Color.DarkGray,
            });
            BezierChart.Series.Add(new Series()
            {
                Name = BEZIER_POINTS_SERIES,
                MarkerSize = 10,
                ChartType = SeriesChartType.Point,
                Color = Color.DarkGray,
            });
            BezierChart.ChartAreas[0].AxisX.Interval = 15;
            BezierChart.ChartAreas[0].AxisX.Minimum = -15;
            BezierChart.ChartAreas[0].AxisX.Maximum = 270;

            BezierChart.ChartAreas[0].AxisY.Interval = 17;
            BezierChart.ChartAreas[0].AxisY.Minimum = -170;
            BezierChart.ChartAreas[0].AxisY.Maximum = 425;

            int startOffset = -2;
            int endOffset = 2;
            for (int i = 0; i < 256; i += (int)BezierChart.ChartAreas[0].AxisX.Interval)
            {
                var label = new CustomLabel(startOffset, endOffset, i.ToString(), 0, LabelMarkStyle.None);
                BezierChart.ChartAreas[0].AxisX.CustomLabels.Add(label);
                startOffset += (int)BezierChart.ChartAreas[0].AxisX.Interval;
                endOffset += (int)BezierChart.ChartAreas[0].AxisX.Interval;
            }
            startOffset = -2;
            endOffset = 2;
            for (int i = 0; i < 256; i += (int)BezierChart.ChartAreas[0].AxisY.Interval)
            {
                var label = new CustomLabel(startOffset, endOffset, i.ToString(), 0, LabelMarkStyle.None);
                BezierChart.ChartAreas[0].AxisY.CustomLabels.Add(label);
                startOffset += (int)BezierChart.ChartAreas[0].AxisY.Interval;
                endOffset += (int)BezierChart.ChartAreas[0].AxisY.Interval;
            }

            BezierChart.MouseDown += OnBezierChartMouseDown;
            BezierChart.MouseUp += OnBezierChartMouseUp;
            BezierChart.MouseMove += OnBezierChartMouseMove;

            // RChart
            RChart = CreateChart("RChart", "R color component");
            tableLayoutPanel1.Controls.Add(RChart, 3, 0);
            tableLayoutPanel1.SetRowSpan(tableLayoutPanel1.Controls["RChart"], 3);
            RChart.Dock = DockStyle.Fill;
            RChart.Series.Add(new Series()
            {
                ChartType = SeriesChartType.SplineArea,
                Color = Color.Red,
            });
            RChart.ChartAreas[0].AxisX.Minimum = 0;
            RChart.ChartAreas[0].AxisX.Maximum = 255;

            // GChart
            GChart = CreateChart("GChart", "G color component");
            tableLayoutPanel1.Controls.Add(GChart, 3, 3);
            tableLayoutPanel1.SetRowSpan(tableLayoutPanel1.Controls["GChart"], 2);
            GChart.Dock = DockStyle.Fill;
            GChart.Series.Add(new Series()
            {
                ChartType = SeriesChartType.SplineArea,
                Color = Color.Green,
            });
            GChart.ChartAreas[0].AxisX.Minimum = 0;
            GChart.ChartAreas[0].AxisX.Maximum = 255;

            // BChart
            BChart = CreateChart("BChart", "B color component");
            tableLayoutPanel1.Controls.Add(BChart, 3, 5);
            BChart.Dock = DockStyle.Fill;
            BChart.Series.Add(new Series()
            {
                ChartType = SeriesChartType.SplineArea,
                Color = Color.Blue,
            });
            BChart.ChartAreas[0].AxisX.Minimum = 0;
            BChart.ChartAreas[0].AxisX.Maximum = 255;
        }

        private static Chart CreateChart(string chartName, string chartTitle)
        {
            var chart = new Chart()
            {
                Name = chartName
            };

            chart.ChartAreas.Add(new ChartArea());
            chart.Titles.Add(chartTitle);
            chart.Series.Clear();

            return chart;
        }

        public void SetBezierCurve(List<int> args, List<int> values)
        {
            if (args.Count != values.Count)
                throw new InvalidDataException();

            BezierChart.Series[BEZIER_CURVE_SERIES].Points.Clear();
            for (int i = 0; i < args.Count; ++i)
            {
                BezierChart.Series[BEZIER_CURVE_SERIES].Points.AddXY(args[i], values[i]);
            }
        }

        public void SetBezierPoints(List<int> args, List<int> values)
        {
            if (args.Count != values.Count)
                throw new InvalidDataException();

            for (int i = 0; i < args.Count; ++i)
            {
                BezierChart.Series[BEZIER_POINTS_CURVE_SERIES].Points.AddXY(args[i], values[i]);
                BezierChart.Series[BEZIER_POINTS_SERIES].Points.AddXY(args[i], values[i]);
            }
        }

        public void SetRChart(List<int> args, List<int> values) => SetColorChart(RChart, args, values);

        public void SetGChart(List<int> args, List<int> values) => SetColorChart(GChart, args, values);

        public void SetBChart(List<int> args, List<int> values) => SetColorChart(BChart, args, values);

        private static void SetColorChart(Chart chart, List<int> args, List<int> values)
        {
            if (args.Count != values.Count)
                throw new InvalidDataException();

            chart.Series[0].Points.Clear();
            for (int i = 0; i < args.Count; ++i)
            {
                chart.Series[0].Points.AddXY(args[i], values[i]);
            }
        }
        #endregion Charts

        #region Canvas drawing
        public void SetPixel(int x, int y, Color color) => _fastDrawArea.SetPixel(x + _canvasMargin, y + _canvasMargin, color);

        public void DrawVertex(PointF center, Color? color = null)
        {
            var p = Offset(center);

            using var g = Graphics;
            g.DrawRectangle(new(color ?? _defaultColor), p.X - _vertexSize / 2, p.Y - _vertexSize / 2, _vertexSize, _vertexSize);
        }

        public void DrawLine(PointF start, PointF end, Color? color = null)
        {
            var s = Offset(start);
            var e = Offset(end);

            using var g = Graphics;
            g.DrawLine(new(color ?? _defaultColor), s.X, s.Y, e.X, e.Y);
        }

        public void DrawCircle(PointF center, int radius, Color? color = null)
        {
            var p = Offset(center);

            using var g = Graphics;
            g.DrawEllipse(new(color ?? _defaultColor), p.X - radius, p.Y - radius, radius * 2, radius * 2);
        }

        public void ClearArea()
        {
            using var g = Graphics;
            g.Clear(Color.White);
        }

        public void RefreshArea() => Canvas.Refresh();

        public void LockDrawArea() => _ = _fastDrawArea.Lock();

        public void UnlockDrawArea() => _fastDrawArea.Unlock();

        private PointF Offset(PointF p) => new PointF(p.X + _canvasMargin, p.Y + _canvasMargin);
        #endregion Canvas drawing

        #region Events
        private void OnPaintBrushButtonClick(object sender, EventArgs e)
        {
            _currentBrushShape = BrushShape.Paintbrush;
            BrushShapeChanged?.Invoke(sender, e);
            RemovePolygonButton.Enabled = false;
            BrushShapeLabel.Text = "Brush type: Paintbrush";
        }

        private void OnAddPolygonButtonClick(object sender, EventArgs e)
        {
            _currentBrushShape = BrushShape.Polygon;
            BrushShapeChanged?.Invoke(sender, e);
            RemovePolygonButton.Enabled = true;
            BrushShapeLabel.Text = "Brush type: Polygon";
        }

        private void OnRemovePolygonButtonClick(object sender, EventArgs e)
        {
            RemovePolygonBrushClicked?.Invoke(sender, e);
        }

        private void OnNoneFilterCheckedChanged(object sender, EventArgs e)
        {
            if (NoneButton.Checked)
            {
                _currentFilterMethod = FilterMethod.None;
                NoneFilterChecked?.Invoke(sender, e);
            }
        }

        private void OnNegativeFilterCheckedChanged(object sender, EventArgs e)
        {
            if (NegativeButton.Checked)
            {
                _currentFilterMethod = FilterMethod.Negative;
                NegativeFilterChecked?.Invoke(sender, e);
            }
        }

        private void OnBrightnessFilterCheckedChanged(object sender, EventArgs e)
        {
            if (BrightnessButton.Checked)
            {
                _currentFilterMethod = FilterMethod.Brightness;
                BrightnessFilterChecked?.Invoke(sender, e);
            }
        }
        private void OnGammaCorrectionFilterCheckedChanged(object sender, EventArgs e)
        {
            if (GammaButton.Checked)
            {
                _currentFilterMethod = FilterMethod.GammaCorrection;
                GammaCorrectionFilterChecked?.Invoke(sender, e);
            }
        }
        private void OnContrastFilterCheckedChanged(object sender, EventArgs e)
        {
            if (ContrastButton.Checked)
            {
                _currentFilterMethod = FilterMethod.Contrast;
                ContrastFilterChecked?.Invoke(sender, e);
            }
        }
        private void OnBezierFilterCheckedChanged(object sender, EventArgs e)
        {
            if (BezierButton.Checked)
            {
                _currentFilterMethod = FilterMethod.BezierCurve;
                BezierFilterChecked?.Invoke(sender, e);
            }
        }

        private void OnLoadImageButtonClick(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog()
            {
                Title = "Please select an mage",
                Filter = "Image Files | *.jpg;*.jpeg;*.png;"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadedFilenameChanged?.Invoke(sender, @openFileDialog.FileName);
            }
        }

        private void OnApplyButtonClick(object sender, EventArgs e)
        {
            ApplyPolygonFilter?.Invoke(sender, e);
        }

        private void OnContrastNumericUpDownValueChanged(object sender, EventArgs e) => ContrastValueChanged?.Invoke(sender, ContrastNumericUpDown.Value);

        private void OnBrightnessNumericUpDownValueChanged(object sender, EventArgs e) => BrightnessValueChanged?.Invoke(sender, BrightnessNumericUpDown.Value);

        private void OnGammaNumericUpDownValueChanged(object sender, EventArgs e) => GammaCorrectionValueChanged?.Invoke(sender, GammaNumericUpDown.Value);

        private void OnCanvasClick(object sender, MouseEventArgs e)
        {
            CanvasClicked?.Invoke(sender, new MouseEventArgs(e.Button, e.Clicks, e.X - _canvasMargin, e.Y - _canvasMargin, e.Delta));
            IsCanvasClicked = true;
        }

        private void OnCanvasClickedMouseUp(object sender, MouseEventArgs e)
        {
            CanvasClickedMouseUp?.Invoke(sender, new MouseEventArgs(e.Button, e.Clicks, e.X - _canvasMargin, e.Y - _canvasMargin, e.Delta));
            IsCanvasClicked = false;
        }

        private void OnCanvasClickedMouseMove(object sender, MouseEventArgs e)
        {
            CanvasMouseMoved?.Invoke(sender, new MouseEventArgs(e.Button, e.Clicks, e.X - _canvasMargin, e.Y - _canvasMargin, e.Delta));
        }

        private void OnBezierChartMouseMove(object sender, MouseEventArgs e)
        {
            // https://stackoverflow.com/questions/36690301/how-to-drag-a-datapoint-and-move-it-in-a-chart-control
            if (FilterMethod == FilterMethod.BezierCurve && MovedBezierPointIdx > -1)
            {
                var ca = BezierChart.ChartAreas[0];
                var ax = ca.AxisX;
                var ay = ca.AxisY;

                try
                {
                    var newX = (int)ax.PixelPositionToValue(e.X);

                    if (MovedBezierPointIdx == 0)
                        newX = 0;
                    else if (MovedBezierPointIdx == 3)
                        newX = 255;
                    else if (newX > 255)
                        newX = 255;
                    else if (newX < 0)
                        newX = 0;

                    var newY = (int)ay.PixelPositionToValue(e.Y);

                    if (newY < 0 && (MovedBezierPointIdx == 0 || MovedBezierPointIdx == 3))
                        newY = 0;
                    else if (newY > 255 && (MovedBezierPointIdx == 0 || MovedBezierPointIdx == 3))
                        newY = 255;

                    if (newY > BezierChart.ChartAreas[0].AxisY.Maximum)
                        newY = (int)BezierChart.ChartAreas[0].AxisY.Maximum;
                    if (newY < BezierChart.ChartAreas[0].AxisY.Minimum)
                        newY = (int)BezierChart.ChartAreas[0].AxisY.Minimum;

                    BezierChart.Series[BEZIER_POINTS_CURVE_SERIES].Points.InsertXY(MovedBezierPointIdx, newX, newY);
                    BezierChart.Series[BEZIER_POINTS_CURVE_SERIES].Points.RemoveAt(MovedBezierPointIdx + 1);
                    BezierChart.Series[BEZIER_POINTS_SERIES].Points.InsertXY(MovedBezierPointIdx, newX, newY);
                    BezierChart.Series[BEZIER_POINTS_SERIES].Points.RemoveAt(MovedBezierPointIdx + 1);

                    BezierPointMoved?.Invoke(sender, (MovedBezierPointIdx, new Point(newX, newY)));
                }
                catch (ArgumentException) { }
            }
        }

        private void OnBezierChartMouseUp(object sender, MouseEventArgs e) => MovedBezierPointIdx = -1;

        private void OnBezierChartMouseDown(object sender, MouseEventArgs e)
        {
            if (FilterMethod == FilterMethod.BezierCurve)
            {
                // https://stackoverflow.com/questions/36690301/how-to-drag-a-datapoint-and-move-it-in-a-chart-control
                var hit = BezierChart.HitTest(e.X, e.Y);
                if (hit?.Series?.Name == BEZIER_POINTS_SERIES)
                {
                    MovedBezierPointIdx = hit.PointIndex;
                }
            }
        }
        #endregion Events
    }
}