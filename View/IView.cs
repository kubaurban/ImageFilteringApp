using View.Enums;

namespace View
{
    public interface IView
    {
        event EventHandler BrushShapeChanged;
        event EventHandler FilterMethodChanged;
        event EventHandler<string> LoadedFilenameChanged;

        BrushShape BrushShape { get; }
        FilterMethod FilterMethod { get; }

        void SetPixel(int x, int y, Color color);
        void DrawVertex(PointF center, Color? color = null);
        void DrawLine(PointF start, PointF end, Color? color = null);
        void DrawCircle(PointF center, int radius, Color? color = null);
        void ClearArea();
        void RefreshArea();
    }
}
