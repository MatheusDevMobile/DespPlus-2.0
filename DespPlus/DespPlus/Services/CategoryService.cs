using DespPlus.Data.Repository.Interface;
using DespPlus.Models;
using DespPlus.Services.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DespPlus.Services
{
    public class CategoryService : ICategoryService
    {
        protected IRegisterRepository<Category> Repository { get; }
        public CategoryService(IRegisterRepository<Category> repository)
        {
            Repository = repository;
        }

        public async Task<bool> CreateCategory(Category category)
        {
            return await Repository.Save(category);
        }

        public async Task<bool> DeleteCategory(string id)
        {
            return await Repository.Delete(id);
        }

        public async Task<List<Category>> GetCategories()
        {
            return await Repository.GetAll();
        }

        public async Task<bool> UpdateCategory(string id, Category category)
        {
            return await Repository.Update(id, category);
        }
    }
}
