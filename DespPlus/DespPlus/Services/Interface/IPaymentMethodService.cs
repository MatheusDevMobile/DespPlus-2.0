using DespPlus.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DespPlus.Services.Interface
{
    public interface IPaymentMethodService
    {
        Task<List<PaymentMethod>> GetPaymentMethods();
        Task<bool> CreatePaymentMethod(PaymentMethod paymentMethod);
        Task<bool> UpdatePaymentMethod(string id, PaymentMethod paymentMethod);
        Task<bool> DeletePaymentMethod(string id);
    }
}
