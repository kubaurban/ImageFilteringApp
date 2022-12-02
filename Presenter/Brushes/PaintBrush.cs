using Model;

namespace Presenter.Brushes
{
    internal class PaintBrush : IBrush
    {
        public Point? Center { get; set; }
        public int Radius { get; set; }

        public PaintBrush(int radius, Point? center = null)
        {
            Center = center;
            Radius = radius;
        }

        public IEnumerable<Pixel> GetBrushPixels(RawImage image)
        {
            var center = Center!.Value;
            for (int y = center.Y - Radius; y < center.Y + Radius + 1; y++)
            {
                var x_start = (int)Math.Round(Math.Sqrt(Math.Pow(Radius, 2) - Math.Pow(y - center.Y, 2)) + center.X);
                var x_end = 2 * center.X - x_start;
                for (int x = x_start; x < x_end + 1; x++)
                {
                    if (x < 0 || y < 0 || x > image.Bitmap.Width - 1 || y > image.Bitmap.Height - 1)
                        continue;

                    yield return image[x, y];
                }
            }
        }
    }
}
