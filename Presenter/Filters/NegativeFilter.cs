namespace Presenter.Filters
{
    internal class NegativeFilter : IFilter
    {
        public Color Filter(Color color) => Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B);
    }
}
