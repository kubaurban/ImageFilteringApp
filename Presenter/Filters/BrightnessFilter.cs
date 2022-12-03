using Presenter.Extensions;

namespace Presenter.Filters
{
    internal class BrightnessFilter : IFilter
    {
        public int Modifier { get; set; }

        public BrightnessFilter(int modifier) => Modifier = modifier;

        public Color Filter(Color color) => Color.FromArgb(color.R + Modifier, color.G + Modifier, color.B + Modifier).Truncate();
    }
}
