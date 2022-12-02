using Microsoft.Practices.Unity;
using View;

namespace Presenter
{
    public static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            ApplicationConfiguration.Initialize();

            var container = new UnityContainer();
            container.RegisterType<IAppManager, AppManager>(new ContainerControlledLifetimeManager());
            container.RegisterType<IView, GUI>(new ContainerControlledLifetimeManager());

            var appManager = container.Resolve<IAppManager>();
            Application.Run(appManager.GetForm());
        }
    }
}