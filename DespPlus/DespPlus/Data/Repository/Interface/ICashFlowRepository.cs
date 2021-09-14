using DespPlus.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DespPlus.Data.Repository.Interface
{
    public interface ICashFlowRepository
    {
        Task<bool> Save(CashFlow cashFlow);
        Task<bool> Delete(CashFlow cashFlow);
        Task<List<CashFlow>> GetAll();
    }
}
