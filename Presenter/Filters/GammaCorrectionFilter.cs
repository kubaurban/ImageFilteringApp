namespace Presenter.Filters
{
    internal class GammaCorrectionFilter : IFilter
    {
        public double Gamma { get; set; }
        private double GammaCorrection => 1 / Gamma;

        public GammaCorrectionFilter(double gamma) => Gamma = gamma;

        public Color Filter(Color color) => Color.FromArgb(Filter(color.R), Filter(color.G), Filter(color.B));

        private int Filter(int i) => (int)(Math.Pow((double)i / 255, GammaCorrection) * 255);
    }
}
