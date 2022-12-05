using FastBitmapLib;
using System.Drawing;

namespace Model
{
    public class LoadedImage
    {
        private readonly Bitmap _bitmap;
        private readonly FastBitmap _fastBitmap;
        private readonly Lazy<bool[,]> _touched;

        public int Width => _bitmap.Width;
        public int Height => _bitmap.Height;
        public bool[,] Touched => _touched.Value;

        public LoadedImage(Bitmap bitmap)
        {
            _bitmap = bitmap;
            _fastBitmap = new FastBitmap(bitmap);

            _touched = new Lazy<bool[,]>(new bool[Width, Height]);
        }

        public Pixel this[int x, int y] => new(x, y, _fastBitmap.GetPixel(x, y));

        public void SetPixel(int x, int y, Color color)
        {
            _fastBitmap.SetPixel(x, y, color);
            Touched[x, y] = true;
        }

        public void Lock() => _fastBitmap.Lock();
        public void Unlock() => _fastBitmap.Unlock();

        public IEnumerable<Pixel> Pixels()
        {
            for (int i = 0; i < Width; ++i)
            {
                for (int j = 0; j < Height; ++j)
                {
                    yield return this[i, j];
                }
            }
        }

        public void Untouch()
        {
            for (int i = 0; i < Width; ++i)
            {
                for (int j = 0; j < Height; ++j)
                {
                    Touched[i, j] = false;
                }
            }
        }
    }
}
