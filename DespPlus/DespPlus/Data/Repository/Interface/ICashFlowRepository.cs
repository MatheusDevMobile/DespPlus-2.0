using DespPlus.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DespPlus.Data.Repository.Interface
{
    public interface ICashFlowRepository
    {
        Task<bool> Save(CashFlow cashFlow);
        Task<bool> Update(string id, CashFlow cashFlow);
        Task<bool> Delete(string id);
        Task<List<CashFlow>> GetAll();
    }
}
