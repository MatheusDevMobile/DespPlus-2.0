using DespPlus.Helpers;
using DespPlus.Models;
using DespPlus.Services.Interface;
using DespPlus.Validators;
using DespPlus.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DespPlus.ViewModels
{
    internal class CategoryPopupVM : IViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected ICategoryService CategoryService { get; }
        protected INavigatorService NavigatorService { get; }
        protected CategoryPopupValidator Validator { get; }
        public ICommand GoBackCommand { get; }
        public ICommand RegisterCommand { get; }
        public string Title { get; }
        public CategoryPopupVM(ICategoryService categoryService, INavigatorService navigatorService, CategoryPopupValidator validator)
        {
            CategoryService = categoryService;
            NavigatorService = navigatorService;
            Validator = validator;

            GoBackCommand = new Command(async () => { await NavigatorService.BackToAsync(); });
            RegisterCommand = new Command(async () => { await SaveCategory(); });
        }

        public Category Category { get; set; } = new Category();
        public string Name { get; set; }
        public string ErrorMessage { get; set; }
        public bool HasErrorField { get; set; }
        public bool InProcess { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsExpense => !IsIncome;
        public bool IsIncome { get; set; }
        public bool IsEditing { get; set; }
        public async Task ReceiveNavigationParameters(IReadOnlyDictionary<string, object> parameters)
        {
            if (parameters != null && parameters.TryGetValue(ParametersName.EditCategory, out var category))
            {
                Category = (Category)category;

                IsEditing = true;

                Name = Category.Name;
                IsIncome = Category.IsIncome;
            }
        }
        private async Task SaveCategory()
        {
            try
            {
                bool isSuccess;
                HasErrorField = false;
                ErrorMessage = "";

                if (!IsEditing)
                    Category.Id = Guid.NewGuid().ToString();

                Category.IsExpense = this.IsExpense;
                Category.IsIncome = this.IsIncome;
                Category.Name = this.Name;

                var resultValidation = await Validator.ValidateAsync(Category);
                if (!resultValidation.IsValid)
                {
                    foreach (var error in resultValidation.Errors)
                    {
                        ErrorMessage += $"* {error.ErrorMessage}";
                    }
                    HasErrorField = true;

                    return;
                }
                InProcess = true;

                if(!IsEditing)
                    isSuccess = await CategoryService.CreateCategory(Category);
                else
                    isSuccess = await CategoryService.UpdateCategory(Category.Id, Category);

                if (isSuccess)
                {
                    InProcess = false;
                    IsSuccess = isSuccess;

                    await Task.Delay(1500);

                    var parameters = ConstructorParameters.Init(ParametersName.ReloadPage, true).GenerateParameters();

                    await NavigatorService.BackToAsync(parameters);
                }
            }
            catch (Exception)
            {
                InProcess = false;
            }
        }
    }
}
