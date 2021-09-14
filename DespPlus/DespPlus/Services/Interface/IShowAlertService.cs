using DespPlus.Helpers;
using System.Threading.Tasks;

namespace DespPlus.Services.Interface
{
    public interface IShowAlertService
    {
        Task CallAlertAsync(string title, string message, AlertAction confirmButton);
        Task CallAlertAsync(string title, string message, AlertAction confirmButton, AlertAction cancelButton);
    }
}
