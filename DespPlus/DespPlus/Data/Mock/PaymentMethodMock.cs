using DespPlus.Models;
using DespPlus.Services.Interface;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DespPlus.Data.Mock
{
    public class PaymentMethodMock : IPaymentMethodService
    {
        private ObservableCollection<PaymentMethod> _paymentMethods;
        public PaymentMethodMock()
        {
            _paymentMethods = new ObservableCollection<PaymentMethod>
            {
                 new PaymentMethod
                 {
                    PaymentMethodId = "1",
                    Name = "Dinheiro",
                 },
                 new PaymentMethod
                 {
                    PaymentMethodId = "2",
                    Name = "Cartão de Crédito",
                 },
                 new PaymentMethod
                 {
                    PaymentMethodId = "3",
                    Name = "Cartão de Débito",
                 },
                 new PaymentMethod
                 {
                    PaymentMethodId = "4",
                    Name = "Pix",
                 },
                 new PaymentMethod
                 {
                    PaymentMethodId = "5",
                    Name = "Transferência Bancária Padrão",
                 },
                 new PaymentMethod
                 {
                    PaymentMethodId = "6",
                    Name = "Depósito",
                 },
                 new PaymentMethod
                 {
                    PaymentMethodId = "7",
                    Name = "Boleto",
                 },
                 new PaymentMethod
                 {
                    PaymentMethodId = "8",
                    Name = "Mercado Pago",
                 },
                 new PaymentMethod
                 {
                    PaymentMethodId = "9",
                    Name = "PayPal",
                 },
                 new PaymentMethod
                 {
                    PaymentMethodId = "10",
                    Name = "Criptomoeda",
                 },
                 new PaymentMethod
                 {
                    PaymentMethodId = "11",
                    Name = "Outros"
                 }
            };
        }

        public Task<bool> CreatePaymentMethod(PaymentMethod paymentMethod)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeletePaymentMethod(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ObservableCollection<PaymentMethod>> GetPaymentMethods()
        {
            return await Task.Run(() =>
            {
                return _paymentMethods;
            });
        }

        public Task<bool> UpdatePaymentMethod(string id, PaymentMethod paymentMethod)
        {
            throw new System.NotImplementedException();
        }

        Task<List<PaymentMethod>> IPaymentMethodService.GetPaymentMethods()
        {
            throw new System.NotImplementedException();
        }
    }
}
