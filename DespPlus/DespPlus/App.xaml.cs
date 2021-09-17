using DespPlus.Services.Interface;
using Xamarin.Forms;

namespace DespPlus
{
    public partial class App : Application
    {
        public App()
        {
            MainPage = new ContentPage();
            InitializeComponent();

            var navigator = DependencyManager.Instance.GetInstance<INavigatorService>();
            navigator.BeginNavigation("MainPage");
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
