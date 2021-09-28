using DespPlus.Helpers;
using DespPlus.Models;
using DespPlus.Services.Interface;
using DespPlus.ViewModels.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DespPlus.ViewModels
{
    internal class CategoryPageVM : IViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected INavigatorService NavigatorService { get; }
        protected ICategoryService CategoryService { get; }
        public ICommand OpenCategoryPopupCommand { get; }
        public CategoryPageVM(INavigatorService navigatorService, ICategoryService categoryService)
        {
            NavigatorService = navigatorService;
            CategoryService = categoryService;
            OpenCategoryPopupCommand = new Command(async () => await NavigatorService.NavigateToAsync("CategoryPopup"));
        }

        public string Title => "Categorias";
        public ObservableCollection<Category> Categories { get; set; }

        public async Task ReceiveNavigationParameters(IReadOnlyDictionary<string, object> parameters)
        {
            var categoryList = await CategoryService.GetCategories();
            if(categoryList.Count > 0)
                Categories = new ObservableCollection<Category>(categoryList.OrderBy(c => c.Name));
        }

        public async Task DeleteCategory(string id)
        {
            var isDeleted = await CategoryService.DeleteCategory(id);
            if (isDeleted)
            {
                var categoryList = await CategoryService.GetCategories();
                if (categoryList.Count > 0)
                    Categories = new ObservableCollection<Category>(categoryList.OrderBy(c => c.Name));
                else
                    Categories = null;
            }
        }

        public  async Task EditCategory(Category category)
        {
            var parameters = ConstructorParameters.Init(ParametersName.EditCategory, category).GenerateParameters();

            await NavigatorService.NavigateToAsync("CategoryPopup", parameters);
        }
    }
}
