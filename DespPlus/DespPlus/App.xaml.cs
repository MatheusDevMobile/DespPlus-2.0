using DespPlus.Services.Interface;
using DespPlus.Views;
using DespPlus.Views.Tabbed;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
