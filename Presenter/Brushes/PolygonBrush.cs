using Model;

namespace Presenter.Brushes
{
    internal class PolygonBrush : ShapedBrush
    {
        private IEnumerable<(Point lower, Point upper)> Edges
        {
            get
            {
                // winforms Y axis is reversed
                var scaledPoints = BrushPoints.Select(x => new Point(x.X, -x.Y));

                var prev = scaledPoints.Last();
                foreach (var p in scaledPoints)
                {
                    if (p.Y < prev.Y)
                        yield return (p, prev);
                    else
                        yield return (prev, p);
                    prev = p;
                }
            }
        }

        private class Node
        {
            public int Ymax { get; set; }
            public float X { get; set; }
            public float Coeff { get; set; }
        }

        public override IEnumerable<Pixel> GetBrushPixels(LoadedImage image)
        {
            if (!CanBrush)
                throw new InvalidOperationException();

            FillFacePreprocessing(out var ET);

            var AET = new List<Node>();
            int y_cur = ET.Min(x => x.Key);
            do
            {
                if (ET.TryGetValue(y_cur, out var list))
                {
                    AET.AddRange(list);
                }
                AET = AET.OrderBy(x => x.X).ToList();

                AET.RemoveAll(x => y_cur == x.Ymax);

                for (int i = 0; i < AET.Count / 2; i++)
                {
                    for (int j = (int)AET[2 * i].X; j < AET[2 * i + 1].X; ++j)
                    {
                        if (j < 0 || y_cur > 0 || j > image.Width - 1 || y_cur < 1 - image.Height)
                            continue;

                        yield return image[j, -y_cur];
                    }
                }

                foreach (var node in AET)
                {
                    node.X += node.Coeff;
                }

                ET.Remove(y_cur);
                ++y_cur;
            } while (ET.Any() || AET.Any());
        }

        private void FillFacePreprocessing(out Dictionary<int, List<Node>> ET)
        {
            ET = new Dictionary<int, List<Node>>();

            foreach ((var lower, var upper) in Edges)
            {
                var node = new Node()
                {
                    Ymax = upper.Y,
                    X = lower.X,
                    Coeff = lower.Y != upper.Y ? (float)(lower.X - upper.X) / (lower.Y - upper.Y) : 0
                };

                var yMin = lower.Y;
                if (!ET.TryAdd(yMin, new() { node }))
                {
                    ET[yMin].Add(node);
                }
            }
        }
    }
}
