namespace Presenter.Filters
{
    internal class NoneFilter : IFilter
    {
        public Color Filter(Color color) => color;
    }
}
