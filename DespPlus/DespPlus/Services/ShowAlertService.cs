using DespPlus.Helpers;
using DespPlus.Services.Interface;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DespPlus.Services
{
    public class ShowAlertService : IShowAlertService
    {
        protected Page CurrentPage => Application.Current.MainPage;
        public async Task CallAlertAsync(string title, string message, AlertAction confirmButton)
        {
            await CurrentPage.DisplayAlert(title, message, confirmButton?.TextButton ?? "OK");
            confirmButton?.Action?.Invoke();
        }

        public async Task CallAlertAsync(string title, string message, AlertAction confirmButton, AlertAction cancelButton)
        {
            var r = await CurrentPage.DisplayAlert(title, message, confirmButton?.TextButton ?? "OK", cancelButton?.TextButton ?? "Cancelar");
            if (r)
                confirmButton?.Action?.Invoke();
            else
                cancelButton?.Action?.Invoke();
        }
    }
}
