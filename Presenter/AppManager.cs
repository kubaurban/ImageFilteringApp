using Model;
using View;

namespace Presenter
{
    public class AppManager : IAppManager
    {
        private IView View { get; }
        private LoadedImage? LoadedImage { get; set; }

        public Form? GetForm() => View as Form;

        public AppManager(IView view)
        {
            View = view;
        }
    }
}
