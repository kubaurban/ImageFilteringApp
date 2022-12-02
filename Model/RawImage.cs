using System.Drawing;

namespace Model
{
    public class RawImage : LoadedImage
    {
        public bool[,] Touched { get; }

        public RawImage(Bitmap bitmap) : base(bitmap) => Touched = new bool[Bitmap.Width, Bitmap.Height];

        public RawImage(LoadedImage image) : base(image.Bitmap) => Touched = new bool[Bitmap.Width, Bitmap.Height];
    }
}