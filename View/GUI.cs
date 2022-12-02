using System.Windows.Forms.DataVisualization.Charting;
using View.Enums;

namespace View
{
    public partial class GUI : Form, IView
    {
        private Chart BrezierChart { get; set; }
        private Chart RChart { get; set; }
        private Chart GChart { get; set; }
        private Chart BChart { get; set; }

        public event EventHandler BrushShapeChanged;
        public event EventHandler FilterMethodChanged;
        public event EventHandler<string> LoadedFilenameChanged;

        private BrushShape _currentBrushShape;
        private FilterMethod _currentFilterMethod;

        public BrushShape BrushShape => _currentBrushShape;
        public FilterMethod FilterMethod => _currentFilterMethod;

        public GUI()
        {
            InitializeComponent();
            InitializeCharts();

            InitDefaultState();
        }

        private void InitializeCharts()
        {
            // BrezierChart
            BrezierChart = CreateChart("BrezierChart", "Brezier curve");
            tableLayoutPanel1.Controls.Add(BrezierChart, 0, 4);
            tableLayoutPanel1.SetRowSpan(tableLayoutPanel1.Controls["BrezierChart"], 2);
            BrezierChart.Dock = DockStyle.Fill;
            BrezierChart.Series.Add(new Series()
            {
                ChartType = SeriesChartType.Spline
            });

            // Color component charts
            var colorSeries = new Series()
            {
                ChartType = SeriesChartType.Spline
            };
            //// RChart
            RChart = CreateChart("RChart", "R color component");
            tableLayoutPanel1.Controls.Add(RChart, 3, 0);
            tableLayoutPanel1.SetRowSpan(tableLayoutPanel1.Controls["RChart"], 3);
            RChart.Dock = DockStyle.Fill;
            RChart.Series.Add(colorSeries);

            //// GChart
            GChart = CreateChart("GChart", "G color component");
            tableLayoutPanel1.Controls.Add(GChart, 3, 3);
            tableLayoutPanel1.SetRowSpan(tableLayoutPanel1.Controls["GChart"], 2);
            GChart.Dock = DockStyle.Fill;
            GChart.Series.Add(colorSeries);

            //// BChart
            BChart = CreateChart("BChart", "B color component");
            tableLayoutPanel1.Controls.Add(BChart, 3, 5);
            BChart.Dock = DockStyle.Fill;
            BChart.Series.Add(colorSeries);
        }

        private void InitDefaultState()
        {
            NegativeButton.Checked = true;
            _currentFilterMethod = FilterMethod.Negative;
            RemovePolygonButton.Enabled = false;
            BrezierChart.Enabled = false;
            RChart.Enabled = false;
            GChart.Enabled = false;
            BChart.Enabled = false;
            BrushShapeLabel.Text = "Brush type: Paintbrush";
            _currentBrushShape = BrushShape.Paintbrush;
        }

        private Chart CreateChart(string chartName, string chartTitle)
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
    }
}