namespace Presenter.Extensions
{
    internal static class PointExtension
    {
        public static bool Clicked(this Point point, Point click, int pointRadius) => Math.Abs(point.X - click.X) <= pointRadius / 2 && Math.Abs(point.Y - click.Y) <= pointRadius / 2;
    }
}
