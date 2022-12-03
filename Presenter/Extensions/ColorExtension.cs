namespace Presenter.Extensions
{
    internal static class ColorExtension
    {
        internal static Color Truncate(this Color c) => Color.FromArgb(Truncate(c.R), Truncate(c.G), Truncate(c.B));

        private static int Truncate(int i)
        {
            if (i < 0)
                i = 0;
            else if (i > 255)
                i = 255;
            return i;
        }
    }
}
