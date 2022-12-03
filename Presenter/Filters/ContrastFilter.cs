using Presenter.Extensions;

namespace Presenter.Filters
{
    internal class ContrastFilter : IFilter
    {
        public int Contrast { get; set; }
        private int ContrastFactor => 259 * (255 + Contrast) / (255 * (259 - Contrast));

        public ContrastFilter(int contrast) => Contrast = contrast;

        public Color Filter(Color color) => Color.FromArgb(Filter(color.R), Filter(color.G), Filter(color.B)).Truncate();

        private int Filter(int i) => ContrastFactor * (i - 128) + 128;
    }
}
