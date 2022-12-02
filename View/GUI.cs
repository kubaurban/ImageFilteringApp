using FastBitmapLib;
using System.Windows.Forms.DataVisualization.Charting;
using View.Enums;

namespace View
{
    public partial class GUI : Form, IView
    {
        private Chart BezierChart { get; set; }
        private Chart RChart { get; set; }
        private Chart GChart { get; set; }
        private Chart BChart { get; set; }

        public event EventHandler BrushShapeChanged;
        public event EventHandler FilterMethodChanged;
        public event EventHandler ApplyPolygonFilter;
        public event MouseEventHandler CanvasClicked;
        public event EventHandler<string> LoadedFilenameChanged;

        private BrushShape _currentBrushShape;
        private FilterMethod _currentFilterMethod;
        private readonly Bitmap _drawArea;
        private readonly FastBitmap _fastDrawArea;
        private int _vertexSize;
        private int _canvasMargin;
        private Color _defaultColor;

        private Graphics Graphics => Graphics.FromImage(_drawArea);

        public BrushShape BrushShape => _currentBrushShape;
        public FilterMethod FilterMethod => _currentFilterMethod;

        public Size CanvasSize => new(Canvas.Width - 2 * _canvasMargin, Canvas.Height - 2 * _canvasMargin);

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
            _vertexSize = 5;
            _defaultColor = Color.Black;
            NoneButton.Checked = true;
            _currentFilterMethod = FilterMethod.None;
            RemovePolygonButton.Enabled = false;
            ApplyButton.Enabled = false;
            BezierChart.Enabled = false;
            RChart.Enabled = false;
            GChart.Enabled = false;
            BChart.Enabled = false;
            BrushShapeLabel.Text = "Brush type: Paintbrush";
            _currentBrushShape = BrushShape.Paintbrush;
        }

        #region Charts
        private void InitializeCharts()
        {
            // BezierChart
            BezierChart = CreateChart("BezierChart", "Bezier curve");
            tableLayoutPanel1.Controls.Add(BezierChart, 0, 4);
            tableLayoutPanel1.SetRowSpan(tableLayoutPanel1.Controls["BezierChart"], 2);
            BezierChart.Dock = DockStyle.Fill;
            BezierChart.Series.Add(new Series()
            {
                ChartType = SeriesChartType.Spline,
                Color = Color.Blue,
            });
            BezierChart.Series.Add(new Series()
            {
                ChartType = SeriesChartType.Point,
                Color = Color.DarkGray,
            });
            BezierChart.Series.Add(new Series()
            {
                ChartType = SeriesChartType.Line,
                Color = Color.DarkGray,
            });
            BezierChart.ChartAreas[0].AxisX.Minimum = 0;
            BezierChart.ChartAreas[0].AxisX.Maximum = 255;

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

        public void SetBezierChart(List<int> args, List<int> values, List<int> bezierPointArgs, List<int> bezierPointValues)
        {
            if (args.Count != values.Count || bezierPointArgs.Count != bezierPointValues.Count)
                throw new InvalidDataException();

            BezierChart.Series[0].Points.Clear();
            BezierChart.Series[1].Points.Clear();
            BezierChart.Series[2].Points.Clear();
            for (int i = 0; i < args.Count; i++)
            {
                BezierChart.Series[0].Points.AddXY(args[i], values[i]);
            }

            for (int i = 0; i < bezierPointArgs.Count; i++)
            {
                BezierChart.Series[1].Points.AddXY(bezierPointArgs[i], bezierPointValues[i]);
                BezierChart.Series[2].Points.AddXY(bezierPointArgs[i], bezierPointValues[i]);
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
            for (int i = 0; i < args.Count; i++)
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
            g.DrawRectangle(new(color ?? _defaultColor), p.X - _vertexSize, p.Y - _vertexSize, _vertexSize, _vertexSize);
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

        #region Handlers
        private void OnBrushButtonClick(object sender, EventArgs e)
        {
            BrushShapeChanged?.Invoke(sender, e);
            BrushShapeLabel.Text = "Brush type: Paintbrush";
        }

        private void OnAddPolygonButtonClick(object sender, EventArgs e)
        {
            BrushShapeChanged?.Invoke(sender, e);
            BrushShapeLabel.Text = "Brush type: Polygon";
        }

        private void OnFilterCheckedChanged(object sender, EventArgs e)
        {
            FilterMethodChanged?.Invoke(sender, e);
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

        private void OnCanvasClick(object sender, MouseEventArgs e)
        {
            CanvasClicked?.Invoke(sender, new MouseEventArgs(e.Button, e.Clicks, e.X - _canvasMargin, e.Y - _canvasMargin, e.Delta));
        }
        #endregion Handlers
    }
}