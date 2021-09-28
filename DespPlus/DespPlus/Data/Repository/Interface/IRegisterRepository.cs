using DespPlus.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DespPlus.Data.Repository.Interface
{
    public interface IRegisterRepository<T>
    {
        Task<bool> Save(T register);
        Task<bool> Update(string id, T register);
        Task<bool> Delete(string id);
        Task<List<T>> GetAll();
    }
}
