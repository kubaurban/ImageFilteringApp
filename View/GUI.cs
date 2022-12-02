using System.Windows.Forms.DataVisualization.Charting;

namespace View
{
    public partial class GUI : Form
    {
        private Chart BrezierChart { get; set; } = default!;
        private Chart RChart { get; set; } = default!;
        private Chart GChart { get; set; } = default!;
        private Chart BChart { get; set; } = default!;

        public GUI()
        {
            InitializeComponent();
            InitializeCharts();
        }

        private void InitializeCharts()
        {
            // BrezierChart
            BrezierChart = CreateChart("BrezierChart", "Brezier curve");
            tableLayoutPanel1.Controls.Add(BrezierChart, 0, 3);
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
            tableLayoutPanel1.Controls.Add(RChart, 2, 0);
            tableLayoutPanel1.SetRowSpan(tableLayoutPanel1.Controls["RChart"], 2);
            RChart.Dock = DockStyle.Fill;
            RChart.Series.Add(colorSeries);

            //// GChart
            GChart = CreateChart("GChart", "G color component");
            tableLayoutPanel1.Controls.Add(GChart, 2, 2);
            tableLayoutPanel1.SetRowSpan(tableLayoutPanel1.Controls["GChart"], 2);
            GChart.Dock = DockStyle.Fill;
            GChart.Series.Add(colorSeries);

            //// BChart
            BChart = CreateChart("BChart", "B color component");
            tableLayoutPanel1.Controls.Add(BChart, 2, 4);
            BChart.Dock = DockStyle.Fill;
            BChart.Series.Add(colorSeries);
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
    }
}