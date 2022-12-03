namespace Presenter.Filters
{
    internal class BrightnessFilter : IFilter
    {
        public int Modifier { get; set; }

        public Color Filter(Color color) => Color.FromArgb(ToBounds(color.R + Modifier), ToBounds(color.G + Modifier), ToBounds(color.B + Modifier));

        private static int ToBounds(int v)
        {
            if (v < 0)
                v = 0;
            else if (v > 255)
                v = 255;
            return v;
        }
    }
}
