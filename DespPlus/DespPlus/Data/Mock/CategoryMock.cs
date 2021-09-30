﻿using DespPlus.Models;
using DespPlus.Services.Interface;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DespPlus.Data.Mock
{
    public class CategoryMock : ICategoryService
    {
        private ObservableCollection<Category> _categories;
        public CategoryMock()
        {
            _categories = new ObservableCollection<Category>
            {
                 new Category
                 {
                    CategoryId = "1",
                    Name = "Prestação do imóvel/Aluguel",
                    IsIncome = false,
                    IsExpense = true
                 },
                 new Category
                 {
                    CategoryId = "2",
                    Name = "Alimentação",
                    IsIncome = false,
                    IsExpense = true
                 },
                 new Category
                 {
                    CategoryId = "3",
                    Name = "Combustível",
                    IsIncome = false,
                    IsExpense = true
                 },
                 new Category
                 {
                    CategoryId = "4",
                    Name = "Conta de Água",
                    IsIncome = false,
                    IsExpense = true
                 },
                 new Category
                 {
                    CategoryId = "5",
                    Name = "Conta de Luz",
                    IsIncome = false,
                    IsExpense = true
                 },
                 new Category
                 {
                    CategoryId = "6",
                    Name = "Combo telefone/Internet/TV",
                    IsIncome = false,
                    IsExpense = true
                 },
                 new Category
                 {
                    CategoryId = "7",
                    Name = "Fatura Cartão",
                    IsIncome = false,
                    IsExpense = true
                 },
                 new Category
                 {
                    CategoryId = "8",
                    Name = "Salário",
                    IsIncome = true,
                    IsExpense = false
                 },
                 new Category
                 {
                    CategoryId = "9",
                    Name = "Aluguel",
                    IsIncome = true,
                    IsExpense = false
                 },
                 new Category
                 {
                    CategoryId = "10",
                    Name = "Pensão",
                    IsIncome = true,
                    IsExpense = false
                 },
                 new Category
                 {
                    CategoryId = "11",
                    Name = "Outros",
                    IsIncome = true,
                    IsExpense = true
                 },
            };
        }

        public Task<bool> CreateCategory(Category category)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteCategory(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ObservableCollection<Category>> GetCategories()
        {
            return await Task.Run(() =>
            {
                return _categories;
            });
        }

        public Task<bool> UpdateCategory(string id, Category category)
        {
            throw new System.NotImplementedException();
        }

        Task<List<Category>> ICategoryService.GetCategories()
        {
            throw new System.NotImplementedException();
        }
    }
}
