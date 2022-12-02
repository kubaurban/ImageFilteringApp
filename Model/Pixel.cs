using System.Drawing;

namespace Model
{
    public class Pixel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Color Color { get; set; }
        public int R => Color.R;
        public int G => Color.G;
        public int B => Color.B;

        public Pixel(int x, int y, Color color)
        {
            X = x;
            Y = y;
            Color = color;
        }
    }
}
