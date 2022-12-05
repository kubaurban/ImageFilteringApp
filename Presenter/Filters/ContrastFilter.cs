using Presenter.Extensions;

namespace Presenter.Filters
{
    internal class ContrastFilter : IFilter
    {
        public int Contrast { get; set; }
        private double ContrastFactor => 259 * (double)(255 + Contrast) / (255 * (259 - Contrast));

        public ContrastFilter(int contrast) => Contrast = contrast;

        public Color Filter(Color color) => (Filter(color.R), Filter(color.G), Filter(color.B)).Truncate();

        private int Filter(int i) => (int)(ContrastFactor * (i - 128)) + 128;
    }
}
