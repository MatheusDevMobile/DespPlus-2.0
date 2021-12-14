
using DespPlus.Models;

namespace DespPlus.Data.EFCoreSqlite
{
    public class EFCoreCashFlowRepository : EFCoreRepository<CashFlow, DespPlusContext>
    {
        public EFCoreCashFlowRepository(DespPlusContext context) : base(context)
        {

        }
    }
}
