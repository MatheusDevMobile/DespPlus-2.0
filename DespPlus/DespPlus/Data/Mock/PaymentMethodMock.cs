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
                    Id = "1",
                    Name = "Dinheiro",
                 },
                 new PaymentMethod
                 {
                    Id = "2",
                    Name = "Cartão de Crédito",
                 },
                 new PaymentMethod
                 {
                    Id = "3",
                    Name = "Cartão de Débito",
                 },
                 new PaymentMethod
                 {
                    Id = "4",
                    Name = "Pix",
                 },
                 new PaymentMethod
                 {
                    Id = "5",
                    Name = "Transferência Bancária Padrão",
                 },
                 new PaymentMethod
                 {
                    Id = "6",
                    Name = "Depósito",
                 },
                 new PaymentMethod
                 {
                    Id = "7",
                    Name = "Boleto",
                 },
                 new PaymentMethod
                 {
                    Id = "8",
                    Name = "Mercado Pago",
                 },
                 new PaymentMethod
                 {
                    Id = "9",
                    Name = "PayPal",
                 },
                 new PaymentMethod
                 {
                    Id = "10",
                    Name = "Criptomoeda",
                 },
                 new PaymentMethod
                 {
                    Id = "11",
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
