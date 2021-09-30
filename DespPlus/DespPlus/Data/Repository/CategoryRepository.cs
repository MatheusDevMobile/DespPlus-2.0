using DespPlus.Data.Repository.Interface;
using DespPlus.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DespPlus.Data.Repository
{
    internal class CategoryRepository : IRegisterRepository<Category>
    {
        private DespPlusContext _context;
        public CategoryRepository()
        {
            _context = DependencyManager.Instance.GetInstance<DespPlusContext>();
        }
        public async Task<bool> Delete(string id)
        {
            try
            {
                var register = await _context.Categories.FindAsync(id);
                if (register != null)
                {
                    _context.Categories.Remove(register);
                    var row = _context.SaveChanges();
                    return row > 0;
                }

                return false;

            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<List<Category>> GetAll()
        {
            try
            {
                var list = await _context.Categories.ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> Save(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                var row = await _context.SaveChangesAsync();

                return row > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Update(string id, Category category)
        {
            try
            {
                var register = await _context.Categories.FindAsync(id);
                if (register != null)
                {
                    register.Name = category.Name;
                    //register.IsIncome = category.IsIncome;
                    //register.IsExpense = category.IsExpense;

                    _context.Categories.Update(register);
                    var row = _context.SaveChanges();

                    return row > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
