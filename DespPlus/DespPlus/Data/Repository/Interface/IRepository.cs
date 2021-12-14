using System.Collections.Generic;
using System.Threading.Tasks;

namespace DespPlus.Data.Repository.Interface
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<List<T>> GetAll();
        Task<T> Get(string id);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(string id);
    }
}
