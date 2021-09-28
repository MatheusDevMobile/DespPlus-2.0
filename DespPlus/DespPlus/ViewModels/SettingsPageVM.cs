using DespPlus.Services.Interface;
using DespPlus.ViewModels.Interfaces;
using System.Collections.Generic;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DespPlus.ViewModels
{
    internal class SettingsPageVM : IViewModel
    {
        protected INavigatorService NavigatorService { get; }
        public ICommand GoToCategoryPage { get; }
        public ICommand GoToPaymentMethodPage { get; }
        public SettingsPageVM(INavigatorService navigatorService)
        {
            NavigatorService = navigatorService;

            GoToCategoryPage = new Command(async () => await NavigatorService.NavigateToAsync("CategoryPage"));
            GoToPaymentMethodPage = new Command(async () => await NavigatorService.NavigateToAsync("PaymentMethodPage"));
        }      
        public string Title => "Configurações";
        public string AppVersion { get; set; } = $"versão {VersionTracking.CurrentVersion}.{VersionTracking.CurrentBuild}";

        public async Task ReceiveNavigationParameters(IReadOnlyDictionary<string, object> parameters)
        {
        }
    }
}
