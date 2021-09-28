using DespPlus.Data.Repository.Interface;
using DespPlus.Models;
using DespPlus.Services.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DespPlus.Services
{
    public class PaymentMethodService : IPaymentMethodService
    {
        protected IRegisterRepository<PaymentMethod> Repository { get; }
        public PaymentMethodService(IRegisterRepository<PaymentMethod> repository)
        {
            Repository = repository;
        }

        public async Task<List<PaymentMethod>> GetPaymentMethods()
        {
            return await Repository.GetAll();
        }

        public async Task<bool> CreatePaymentMethod(PaymentMethod paymentMethod)
        {
            return await Repository.Save(paymentMethod);
        }

        public async Task<bool> UpdatePaymentMethod(string id, PaymentMethod paymentMethod)
        {
            return await Repository.Update(id, paymentMethod);
        }

        public async Task<bool> DeletePaymentMethod(string id)
        {
            return await Repository.Delete(id);
        }
    }
}
