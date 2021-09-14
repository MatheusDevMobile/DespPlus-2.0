using DespPlus.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DespPlus.Services
{
    public class HandleExeptionService : IHandleExeptionService
    {
        protected IShowAlertService ShowAlert { get; }
        public HandleExeptionService(IShowAlertService showAlert)
        {
            ShowAlert = showAlert;
        }
        public async Task ExeptionNotExpected(Exception ex, string title = "Falha", string message = "Ocorreu um erro não esperado.")
        {
            //Crashes.TrackError(ex);
            await ShowAlert.CallAlertAsync(title, message, null);
        }
    }
}
