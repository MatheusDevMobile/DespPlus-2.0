using DespPlus.Data.EFCoreSqlite;
using DespPlus.Models;

namespace DespPlus.Data.Repository
{
    internal class EFCoreCategoryRepository : EFCoreRepository<Category, DespPlusContext>
    {
        public EFCoreCategoryRepository(DespPlusContext context) : base(context)
        {
        }
    }
}
