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
    }
}
