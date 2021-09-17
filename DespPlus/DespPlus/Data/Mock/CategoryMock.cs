using DespPlus.Models;
using DespPlus.Services.Interface;
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
                    Id = "1",
                    Name = "Gás de Cozinha"
                 },
                 new Category
                 {
                    Id = "2",
                    Name = "Alimentação"
                 },
                 new Category
                 {
                    Id = "3",
                    Name = "Combustível"
                 },
                 new Category
                 {
                    Id = "4",
                    Name = "Conta de Água"
                 },
                 new Category
                 {
                    Id = "5",
                    Name = "Conta de Luz"
                 },
                 new Category
                 {
                    Id = "6",
                    Name = "Combo telefone/Internet/TV"
                 },
                 new Category
                 {
                    Id = "7",
                    Name = "Fatura Cartão"
                 },
                 new Category
                 {
                    Id = "8",
                    Name = "Outros"
                 },
            };
        }

        public async Task<ObservableCollection<Category>> GetCategories()
        {
            return await Task.Run(() =>
            {
                return _categories;
            });
        }
    }
}
