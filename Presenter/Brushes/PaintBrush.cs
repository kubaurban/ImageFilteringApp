using Model;

namespace Presenter.Brushes
{
    internal class PaintBrush : ShapedBrush
    {
        public int Radius { get; set; }

        public PaintBrush(int radius, Point? center = null)
        {
            if (center is not null)
                AddBrushPoint(center.Value);
            Radius = radius;
        }

        public override IEnumerable<Pixel> GetBrushPixels(LoadedImage image)
        {
            var center = BrushPoints.First();

            for (int y = center.Y - Radius; y < center.Y + Radius + 1; ++y)
            {
                var x0 = (int)Math.Round(Math.Sqrt(Math.Pow(Radius, 2) - Math.Pow(y - center.Y, 2)) + center.X);
                var x1 = 2 * center.X - x0;
                
                if (x1 < x0)
                    (x0, x1) = (x1, x0);

                for (int x = x0; x < x1 + 1; ++x)
                {
                    if (x < 0 || y < 0 || x > image.Width - 1 || y > image.Height - 1)
                        continue;

                    yield return image[x, y];
                }
            }
        }
    }
}
