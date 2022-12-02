using System.Drawing;

namespace Model
{
    public class LoadedImage
    {
        public Bitmap Bitmap { get; }

        public LoadedImage(Bitmap bitmap) => Bitmap = bitmap;

        public Color this[int x, int y] => Bitmap.GetPixel(x, y);
    }
}
