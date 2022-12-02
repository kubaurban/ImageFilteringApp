using View.Enums;

namespace View
{
    public interface IView
    {
        event EventHandler BrushShapeChanged;
        event EventHandler FilterMethodChanged;

        BrushShape BrushShape { get; }
        FilterMethod FilterMethod { get; }
    }
}
