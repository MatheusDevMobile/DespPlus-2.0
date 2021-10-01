using DespPlus.Services.Interface;
using Xamarin.Forms;

namespace DespPlus
{
    public partial class App : Application
    {
        private ISeedingService Service { get; }
        public App()
        {
            MainPage = new ContentPage();
            InitializeComponent();

            Service = DependencyManager.Instance.GetInstance<ISeedingService>();
            var navigator = DependencyManager.Instance.GetInstance<INavigatorService>();
            navigator.BeginNavigation("MainPage");
        }

        protected override void OnStart()
        {
            Service.Seed();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
