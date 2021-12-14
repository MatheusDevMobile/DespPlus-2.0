using DespPlus.Data.Repository.Interface;
using DespPlus.Models;
using DespPlus.Services.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DespPlus.Services
{
    public class CategoryService : ICategoryService
    {
        protected IRepository<Category> Repository { get; }
        public CategoryService(IRepository<Category> repository)
        {
            Repository = repository;
        }

        public async Task<bool> CreateCategory(Category category)
        {
            await Repository.Create(category);
            return true;
        }

        public async Task<bool> DeleteCategory(string id)
        {
            await Repository.Delete(id);
            return true;
        }

        public async Task<List<Category>> GetCategories()
        {
            return await Repository.GetAll();
        }

        public async Task<bool> UpdateCategory(string id, Category category)
        {
            await Repository.Update(category);
            return true;
        }
    }
}
