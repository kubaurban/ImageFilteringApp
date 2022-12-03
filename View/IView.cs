﻿using View.Enums;

namespace View
{
    public interface IView
    {
        event EventHandler BrushShapeChanged;
        event EventHandler NoneFilterChecked;
        event EventHandler NegativeFilterChecked;
        event EventHandler BrightnessFilterChecked;
        event EventHandler GammaCorrectionFilterChecked;
        event EventHandler ContrastFilterChecked;
        event EventHandler BezierFilterChecked;
        event EventHandler ApplyPolygonFilter;
        event EventHandler<(int, Point)> BezierPointMoved; 
        event EventHandler<string> LoadedFilenameChanged;
        event MouseEventHandler CanvasClicked;
        event MouseEventHandler CanvasClickedMouseMoved;
        event MouseEventHandler CanvasClickedMouseUp;

        BrushShape BrushShape { get; }
        FilterMethod FilterMethod { get; }

        Size CanvasSize { get; }

        void SetPixel(int x, int y, Color color);
        void DrawVertex(PointF center, Color? color = null);
        void DrawLine(PointF start, PointF end, Color? color = null);
        void DrawCircle(PointF center, int radius, Color? color = null);
        void ClearArea();
        void RefreshArea();
        void LockDrawArea();
        void UnlockDrawArea();

        void SetBezierCurve(List<int> args, List<int> values);
        void SetBezierPoints(List<int> args, List<int> values);
        void SetRChart(List<int> args, List<int> values);
        void SetGChart(List<int> args, List<int> values);
        void SetBChart(List<int> args, List<int> values);
    }
}
