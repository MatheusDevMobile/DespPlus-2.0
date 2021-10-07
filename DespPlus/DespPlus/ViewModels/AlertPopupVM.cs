using DespPlus.Helpers;
using DespPlus.Services.Interface;
using DespPlus.ViewModels.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace DespPlus.ViewModels
{
    public class AlertPopupVM : IViewModel, INotifyPropertyChanged
    {
        public string Title { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected INavigatorService NavigatorService { get; }
        public AlertPopupVM(INavigatorService navigatorService)
        {
            NavigatorService = navigatorService;
        }

        public string Animation { get; set; }
        public async Task ReceiveNavigationParameters(IReadOnlyDictionary<string, object> parameters)
        {
            if (parameters != null && parameters.TryGetValue(ParametersName.Success, out var isSuccess))
            {
                if (!(bool)isSuccess)
                {
                    Animation = "error.json";
                    await Task.Delay(1500);

                    await NavigatorService.BackToAsync();
                    return;
                }

                Animation = "success.json";
                await Task.Delay(1500);

                await NavigatorService.BackToAsync();
            }
        }
    }
}
