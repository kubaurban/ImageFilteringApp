using System.Windows.Forms.DataVisualization.Charting;

namespace View
{
    public partial class GUI : Form
    {
        private Chart BrezierChart;

        public GUI()
        {
            InitializeComponent();

            BrezierChart = new Chart();
            tableLayoutPanel1.Controls.Add(BrezierChart, 0, 3);

            BrezierChart.Titles.Add("Brezier");
            BrezierChart.Series.Clear();
            var series = BrezierChart.Series.Add("Brezier");
            //series.ChartType = SeriesChartType.Spline;
            series.Points.AddXY(1, 20);
            series.Points.AddXY(2, 30);
            series.Points.AddXY(3, 20);
            series.Points.AddXY(4, 5);
        }
    }
}