using DespPlus.Helpers;
using DespPlus.Models;
using DespPlus.Services.Interface;
using DespPlus.Validators;
using DespPlus.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DespPlus.ViewModels
{
    public class PaymentMethodPopupVM : IViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected IPaymentMethodService PaymentMethodService { get; }
        protected INavigatorService NavigatorService { get; }
        protected PaymentMethodPopupValidator Validator { get; }
        public ICommand GoBackCommand { get; }
        public ICommand RegisterCommand { get; }
        public string Title { get; }
        public PaymentMethodPopupVM(PaymentMethodPopupValidator validator, INavigatorService navigatorService, IPaymentMethodService paymentMethodService)
        {
            Validator = validator;
            NavigatorService = navigatorService;
            PaymentMethodService = paymentMethodService;

            GoBackCommand = new Command(async () => { await NavigatorService.BackToAsync(); });
            RegisterCommand = new Command(async () => { await SavePaymentMethod(); });
        }
        public PaymentMethod PaymentMethod { get; set; } = new PaymentMethod();
        public string Name { get; set; }
        public string ErrorMessage { get; set; }
        public bool HasErrorField { get; set; }
        public bool InProcess { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsEditing { get; set; }
        public async Task ReceiveNavigationParameters(IReadOnlyDictionary<string, object> parameters)
        {
            if (parameters != null && parameters.TryGetValue(ParametersName.EditPaymentMethods, out var paymentMethods))
            {
                PaymentMethod = (PaymentMethod)paymentMethods;

                IsEditing = true;

                Name = PaymentMethod.Name;
            }
        }

        private async Task SavePaymentMethod()
        {
            try
            {
                bool isSuccess;
                HasErrorField = false;
                ErrorMessage = "";

                if (!IsEditing)
                    PaymentMethod.Id = Guid.NewGuid().ToString();

                PaymentMethod.Name = this.Name;

                var resultValidation = await Validator.ValidateAsync(PaymentMethod);
                if (!resultValidation.IsValid)
                {
                    foreach (var error in resultValidation.Errors)
                    {
                        ErrorMessage += $"* {error.ErrorMessage}";
                    }
                    HasErrorField = true;

                    return;
                }
                InProcess = true;

                if (!IsEditing)
                    isSuccess = await PaymentMethodService.CreatePaymentMethod(PaymentMethod);
                else
                    isSuccess = await PaymentMethodService.UpdatePaymentMethod(PaymentMethod.Id, PaymentMethod);

                if (isSuccess)
                {
                    InProcess = false;
                    IsSuccess = isSuccess;

                    await Task.Delay(1500);

                    var parameters = ConstructorParameters.Init(ParametersName.ReloadPage, true).GenerateParameters();

                    await NavigatorService.BackToAsync(parameters);
                }
            }
            catch (Exception)
            {
                InProcess = false;
            }


        }
    }
}
