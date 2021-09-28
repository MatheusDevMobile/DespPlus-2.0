using DespPlus.Helpers;
using DespPlus.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DespPlus.ViewModels
{
    public class FilePopupPageVM : IViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public FilePopupPageVM()
        {

        }
        public string Title { get; }
        public bool InProcess { get; set; }
        public ImageSource Image { get; set; }

        public async Task ReceiveNavigationParameters(IReadOnlyDictionary<string, object> parameters)
        {
            InProcess = true;

            //TODO REMOVER POSTERIORMENTE
            await Task.Delay(2000);

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
