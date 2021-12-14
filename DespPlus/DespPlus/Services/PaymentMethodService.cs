using DespPlus.Data.Repository.Interface;
using DespPlus.Models;
using DespPlus.Services.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DespPlus.Services
{
    public class PaymentMethodService : IPaymentMethodService
    {
        protected IRepository<PaymentMethod> Repository { get; }
        public PaymentMethodService(IRepository<PaymentMethod> repository)
        {
            Repository = repository;
        }

        public async Task<List<PaymentMethod>> GetPaymentMethods()
        {
            return await Repository.GetAll();
        }

        public async Task<bool> CreatePaymentMethod(PaymentMethod paymentMethod)
        {
            await Repository.Create(paymentMethod);
            return true;
        }

        public async Task<bool> UpdatePaymentMethod(string id, PaymentMethod paymentMethod)
        {
            if (id != paymentMethod.Id)
                return false;

            await Repository.Update(paymentMethod);
            return true;
        }

        public async Task<bool> DeletePaymentMethod(string id)
        {
            await Repository.Delete(id);
            return true;
        }
    }
}
