using DespPlus.Data.EFCoreSqlite;
using DespPlus.Models;

namespace DespPlus.Data.Repository
{
    public class EFCorePaymentMethodRepository : EFCoreRepository<PaymentMethod, DespPlusContext>
    {
        public EFCorePaymentMethodRepository(DespPlusContext context) : base(context)
        {

        }
    }
}
