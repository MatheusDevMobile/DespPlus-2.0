using DespPlus.Models;
using DespPlus.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DespPlus.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryPage : ContentPage
    {
        public CategoryPage()
        {
            InitializeComponent();

            BindingContext = DependencyManager.Instance.GetInstance<CategoryPageVM>();
        }

        private async void EditCategoryEvent(object sender, EventArgs e)
        {
            var button = sender as Button;
            var category = (Category)button.CommandParameter;
            var vm = BindingContext as CategoryPageVM;
            await vm.EditCategory(category);
        }
        private async void DeleteCategoryEvent(object sender, EventArgs e)
        {
            var isDelete = await DisplayAlert("Atenção", "Deseja realmente excluir essa categoria ?", "sim", "não");

            if (isDelete)
            {
                var button = sender as Button;
                var category = (Category)button.CommandParameter;
                var vm = BindingContext as CategoryPageVM;
                await vm.DeleteCategory(category.CategoryId);
            }
        }
    }
}