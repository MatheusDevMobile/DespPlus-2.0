using DespPlus.Helpers;
using DespPlus.Services.Interface;
using DespPlus.ViewModels.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DespPlus.ViewModels
{
    public class FilePopupPageVM : IViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected INavigatorService NavigatorService { get; }
        public ICommand ClosePopupCommand { get; }
        public FilePopupPageVM(INavigatorService navigatorService)
        {
            NavigatorService = navigatorService;
            ClosePopupCommand = new Command(async () => { await NavigatorService.BackToAsync(); });
        }
        public string Title { get; }
        public bool InProcess { get; set; }
        public ImageSource Image { get; set; }

        public async Task ReceiveNavigationParameters(IReadOnlyDictionary<string, object> parameters)
        {
            InProcess = true;

            await Task.Run(() => {
                if (parameters != null && parameters.TryGetValue(ParametersName.FilePopup, out var imageSource))
                {
                    Image = (ImageSource)imageSource;
                }
            });

            InProcess = false;
        }
    }
}
