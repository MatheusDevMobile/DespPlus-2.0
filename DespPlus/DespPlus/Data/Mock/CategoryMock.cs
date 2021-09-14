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
                    Name = "Gás de Cozinha",
                    Icon = "house.png"
                 },
                 new Category
                 {
                    Id = "2",
                    Name = "Alimentação",
                    Icon = "restaurant.png"
                 },
                 new Category
                 {
                    Id = "3",
                    Name = "Combustível",
                    Icon = "car.png"
                 },
                 new Category
                 {
                    Id = "4",
                    Name = "Conta de Água",
                    Icon = "house.png"
                 },
                 new Category
                 {
                    Id = "5",
                    Name = "Conta de Luz",
                    Icon = "house.png"
                 },
                 new Category
                 {
                    Id = "6",
                    Name = "Conta de Telefone/Internet/TV",
                    Icon = "house.png"
                 },
                 new Category
                 {
                    Id = "7",
                    Name = "Fatura Cartão",
                    Icon = "house.png"
                 },
                 new Category
                 {
                    Id = "8",
                    Name = "Outros",
                    Icon = "house.png"
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
