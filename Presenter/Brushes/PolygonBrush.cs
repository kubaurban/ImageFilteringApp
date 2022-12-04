using Model;

namespace Presenter.Brushes
{
    internal class PolygonBrush : ShapedBrush
    {
        private IEnumerable<(Point lower, Point upper)> Edges
        {
            get
            {
                var prev = BrushPoints.Last();
                foreach (var p in BrushPoints)
                {
                    if (p.Y < prev.Y)
                        yield return (p, prev);
                    else
                        yield return (prev, p);
                    prev = p;
                }
            }
        }

        public override IEnumerable<Pixel> GetBrushPixels(LoadedImage image)
        {
            if (!CanBrush)
                throw new InvalidOperationException();

            foreach (var p in GetPolygonPoints())
                yield return image[p.X, p.Y];
        }

        private class Node
        {
            public float Ymax { get; set; }
            public float X { get; set; }
            public float Coeff { get; set; }
        }

        private IEnumerable<Point> GetPolygonPoints()
        {
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

                AET.RemoveAll(x => y_cur == (int)Math.Round(x.Ymax));

                for (int i = 0; i < AET.Count / 2; i++)
                {
                    for (int j = (int)AET[2 * i].X; j < AET[2 * i + 1].X; j++)
                    {
                        yield return new Point(j, y_cur);
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
                    Coeff = Math.Abs(lower.X - upper.X) > 1e-10 ? (lower.X - upper.X) / (lower.Y - upper.Y) : 0
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
