using DespPlus.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DespPlus.Services.Interface
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategories();
        Task<bool> CreateCategory(Category category);
        Task<bool> UpdateCategory(string id, Category category);
        Task<bool> DeleteCategory(string id);
    }
}
