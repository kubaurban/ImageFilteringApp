using System.Drawing;

namespace Model
{
    public class LoadedImage
    {
        public Bitmap Bitmap { get; }

        public LoadedImage(Bitmap bitmap) => Bitmap = bitmap;

        public Pixel this[int x, int y] => new Pixel(x, y, Bitmap.GetPixel(x, y));

        public IEnumerable<Pixel> Pixels()
        {
            for (int i = 0; i < Bitmap.Width; i++)
            {
                for (int j = 0; j < Bitmap.Height; j++)
                {
                    yield return this[i, j];
                }
            }
        }
    }
}
