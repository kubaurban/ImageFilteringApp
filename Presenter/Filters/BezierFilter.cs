using System.Numerics;

namespace Presenter.Filters
{
    internal class BezierFilter : IFilter
    {
        private readonly List<KeyValuePair<int, int>> _defaultBezier;
        private readonly List<Point> _bezierPoints;

        private Dictionary<int, int> BezierCurve { get; }
        private bool BezierComputed { get; set; }

        public List<int> BezierPointsArgs => _bezierPoints.Select(p => p.X).ToList();
        public List<int> BezierPointsValues => _bezierPoints.Select(p => p.Y).ToList();
        public List<int> BezierArgs
        {
            get
            {
                if (!BezierComputed)
                    ComputeBezierCurve();
                return BezierCurve.Keys.ToList();
            }
        }
        public List<int> BezierValues
        {
            get
            {
                if (!BezierComputed)
                    ComputeBezierCurve();
                return BezierCurve.Values.ToList();
            }
        }

        public BezierFilter()
        {
            _defaultBezier = new List<KeyValuePair<int, int>>(256);
            for (int i = 0; i < 256; ++i)
                _defaultBezier.Add(new KeyValuePair<int, int>(i, i));
            _bezierPoints = new List<Point>() { new(0, 0), new(85, 85), new(170, 170), new(255, 255) };

            BezierCurve = new Dictionary<int, int>(_defaultBezier);
            BezierComputed = true;
        }

        public Color Filter(Color color)
        {
            if (!BezierComputed)
                ComputeBezierCurve();

            return Color.FromArgb(BezierCurve[color.R], BezierCurve[color.G], BezierCurve[color.B]);
        }

        public void MoveBezierPoint(int idx, Vector2 move)
        {
            if (idx < 0 || idx > 3)
                throw new InvalidDataException();

            if (idx == 0 || idx == 3)
                move.X = 0;

            var oldPoint = _bezierPoints[idx];
            _bezierPoints[idx] = new Point(oldPoint.X + (int)move.X, oldPoint.Y + (int)move.Y);

            BezierComputed = false;
        }

        private void ComputeBezierCurve()
        {
            (var p0, var p1, var p2, var p3) = (_bezierPoints[0], _bezierPoints[1], _bezierPoints[2], _bezierPoints[3]);
            var v0 = new Vector2(p0.X, p0.Y);
            var v1 = new Vector2(p1.X, p1.Y);
            var v2 = new Vector2(p2.X, p2.Y);
            var v3 = new Vector2(p3.X, p3.Y);

            var it = 1001;
            var d = (float)1 / (it - 1);

            var A0 = v0;
            var A1 = 3 * (v1 - v0);
            var A2 = 3 * (v2 - 2 * v1 + v0);
            var A3 = v3 - 3 * v2 + 3 * v1 - v0;

            for (int i = 0; i < it; ++i)
            {
                var t = d * i;
                var Px = (int)Math.Round(Horner(new float[] { A3.X, A2.X, A1.X, A0.X }, t));
                var Py = (int)Math.Round(Horner(new float[] { A3.Y, A2.Y, A1.Y, A0.Y }, t));
                BezierCurve[Px] = Py;
            }
            BezierComputed = true;
        }

        private static float Horner(float[] poly, float x)
        {
            var result = poly[0];

            for (int i = 1; i < poly.Length; i++)
                result = result * x + poly[i];

            return result;
        }
    }
}
