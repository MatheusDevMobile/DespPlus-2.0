using DespPlus.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace DespPlus.ViewModels
{
    public class RegisterInfoModalVM : IViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public RegisterInfoModalVM()
        {

        }

        public string Title { get; set; } = "Informações do registro";
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public string Type { get; set; }


        public async Task ReceiveNavigationParameters(IReadOnlyDictionary<string, object> parameters)
        {
            if (parameters != null)
            {
            }
        }
    }
}
