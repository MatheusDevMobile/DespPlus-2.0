using DespPlus.Helpers;
using DespPlus.Models;
using DespPlus.Services.Interface;
using DespPlus.ViewModels.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DespPlus.ViewModels
{
    public class PaymentMethodPageVM : IViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Title => "Métodos de Pagamento";
        protected INavigatorService NavigatorService { get; }
        public ICommand OpenPaymentMethodPopupCommand { get; }
        protected IPaymentMethodService PaymentMethodService { get; }
        public PaymentMethodPageVM(INavigatorService navigatorService, IPaymentMethodService paymentMethodService)
        {
            NavigatorService = navigatorService;
            PaymentMethodService = paymentMethodService;
            OpenPaymentMethodPopupCommand = new Command(async () => await NavigatorService.NavigateToAsync("PaymentMethodPopup"));
        }

        public ObservableCollection<PaymentMethod> PaymentMethods { get; set; }

        public async Task ReceiveNavigationParameters(IReadOnlyDictionary<string, object> parameters)
        {
            var paymentMethodList = await PaymentMethodService.GetPaymentMethods();
            if (paymentMethodList.Count > 0)
                PaymentMethods = new ObservableCollection<PaymentMethod>(paymentMethodList.OrderBy(c => c.Name));
        }

        public async Task DeletePaymentMethod(string id)
        {
            var isDeleted = await PaymentMethodService.DeletePaymentMethod(id);
            if (isDeleted)
            {
                var paymentMethodList = await PaymentMethodService.GetPaymentMethods();
                if (paymentMethodList.Count > 0)
                    PaymentMethods = new ObservableCollection<PaymentMethod>(paymentMethodList.OrderBy(c => c.Name));
                else
                    PaymentMethods = null;

            }
        }

        public async Task EditPaymentMethod(PaymentMethod paymentMethod)
        {
            var parameters = ConstructorParameters.Init(ParametersName.EditPaymentMethods, paymentMethod).GenerateParameters();

            await NavigatorService.NavigateToAsync("PaymentMethodPopup", parameters);
        }
    }
}
