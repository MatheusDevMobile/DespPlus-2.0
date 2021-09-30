using DespPlus.Models;
using DespPlus.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DespPlus.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentMethodPage : ContentPage
    {
        public PaymentMethodPage()
        {
            InitializeComponent();

            BindingContext = DependencyManager.Instance.GetInstance<PaymentMethodPageVM>();
        }

        private async void EditPaymentMethodEvent(object sender, EventArgs e)
        {
            var vm = BindingContext as PaymentMethodPageVM;
            var button = sender as Button;
            var paymentMethod = (PaymentMethod)button.CommandParameter;
            await vm.EditPaymentMethod(paymentMethod);
        }
        private async void DeletePaymentMethodEvent(object sender, EventArgs e)
        {
            var isDelete = await DisplayAlert("Atenção", "Deseja realmente excluir essa forma de pagamento ?", "sim", "não");

            if (isDelete)
            {
                var vm = BindingContext as PaymentMethodPageVM;
                var button = sender as Button;
                var paymentMethod = (PaymentMethod)button.CommandParameter;
                await vm.DeletePaymentMethod(paymentMethod.PaymentMethodId);
            }
        }
    }
}