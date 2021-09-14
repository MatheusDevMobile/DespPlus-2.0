using DespPlus.Models;
using DespPlus.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DespPlus.Views.Tabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = DependencyManager.Instance.GetInstance<MainPageVM>();
        }

        protected override void OnAppearing()
        {
            var vm = BindingContext as MainPageVM;
            Task.Run(() => { vm.ExecuteRefreshCommand(); });
        }

        private void OpenModalRegisterInfoEvent(object sender, EventArgs e)
        {
            var param = ((TappedEventArgs)e).Parameter;
            var vm = BindingContext as MainPageVM;
            vm.OpenRegisterInfo((CashFlow)param);
        }
    }
}