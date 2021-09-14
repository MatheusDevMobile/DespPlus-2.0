using DespPlus.Helpers;
using DespPlus.Models;
using DespPlus.Services.Interface;
using DespPlus.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DespPlus.ViewModels
{
    public class RegisterVM : IViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected ICashFlowService CashFlowService { get; }
        protected IPickPhotoService PickPhotoService { get; }
        protected INavigatorService NavigatorService { get; }
        protected ICategoryService CashFlowTypeService { get; }
        protected IPaymentMethodService PaymentMethodService { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteImageCommand { get; }
        public ICommand OpenCameraCommand { get; }
        public ICommand OpenPhotoLibraryCommand { get; }
        public ICommand OpenFileCommand { get; }
        public RegisterVM(ICashFlowService cashFlowService, IPickPhotoService pickPhotoService, INavigatorService navigatorService, ICategoryService cashFlowTypeService, IPaymentMethodService paymentMethodService)
        {
            CashFlowService = cashFlowService;
            PickPhotoService = pickPhotoService;
            NavigatorService = navigatorService;
            CashFlowTypeService = cashFlowTypeService;
            PaymentMethodService = paymentMethodService;

            SaveCommand = new Command(() => { SaveRegister(); });
            OpenFileCommand = new Command(async () => { await OpenImage(); });
            OpenCameraCommand = new Command(() => { OpenCamera(); });
            DeleteImageCommand = new Command(() => { DeleteImage(); });
            OpenPhotoLibraryCommand = new Command(() => { OpenPhotoLibrary(); });
        }

        public bool IsIncome { get; set; }
        public bool IsExpense { get; set; } = true;
        public bool HasImage { get; set; }
        public bool AddImageSwitch { get; set; }
        public bool AddImageContainer => AddImageSwitch;
        public string SwitchTitleLabel => "Adicionar Comprovante?";
        public string ValueLabel { get; set; }
        public string CashFlowType { get; set; }
        public ImageSource ReceipPhoto { get; set; }
        public string FileName { get; set; }
        public string Base64 { get; set; }
        public string CheckingCopyPhotoName { get; set; }
        public Category CategoryItem { get; set; }
        public string OtherCategoryDescription { get; set; }
        public PaymentMethod PaymentMethodItem { get; set; }
        public string OtherPaymentMethodDescription { get; set; }
        public string CommentDescription { get; set; }
        public DateTime DateLabel { get; set; } = DateTime.Today;
        public TimeSpan TimeLabel { get; set; } = DateTime.Now.TimeOfDay;
        public ObservableCollection<Category> CategoryList { get; set; }
        public ObservableCollection<PaymentMethod> PaymentMethodList { get; set; }
        public string Title { get; set; } = "REGISTRO";

        public async Task ReceiveNavigationParameters(IReadOnlyDictionary<string, object> parameters)
        {
            CategoryList = await CashFlowTypeService.GetCategories();
            PaymentMethodList = await PaymentMethodService.GetPaymentMethods();
        }

        private async Task OpenImage()
        {
            var parameters = ConstructorParameters.Init(ParametersName.FilePopup, ReceipPhoto).GenerateParameters();
            await NavigatorService.NavigateToAsync("FilePopupPage", parameters);
        }
        private async void OpenPhotoLibrary()
        {
            var imageSource = await PickPhotoService.PickPhotoFromLibraryAsync();
            if (imageSource.Item1 != null)
            {
                ReceipPhoto = imageSource.Item1;
                FileName = imageSource.Item2;
                Base64 = imageSource.Item3;
                AddImageSwitch = false;
                HasImage = true;
            }
        }

        private async void OpenCamera()
        {
            var imageSource = await PickPhotoService.TakePhotoAsync();
            if (imageSource.Item1 != null)
            {
                ReceipPhoto = imageSource.Item1;
                FileName = imageSource.Item2;
                Base64 = imageSource.Item3;
                AddImageSwitch = false;
                HasImage = true;
            }
        }

        private async void DeleteImage()
        {
            var removeAlert = await Application.Current.MainPage.DisplayAlert("Atenção", "Deseja remover esse comprovante?", "sim", "não");
            if (removeAlert)
            {
                ReceipPhoto = null;
                AddImageSwitch = false;
                HasImage = false;
            }
        }

        private async void SaveRegister()
        {
            var cashFlowLabel = "";
            var colorLabel = "";
            if (IsIncome)
            {
                cashFlowLabel = "Rendimento";
                colorLabel = "#65BCBF";
            }
            if (IsExpense)
            {
                cashFlowLabel = "Despesa";
                colorLabel = "#F8777D";
            }

            var toStringValue = ValueLabel.Replace("R$ ", "").Replace(",", "").Replace(".", "");
            var prepareToStringValue = toStringValue.Insert(toStringValue.Length - 2, ",");

            var register = new CashFlow
            {
                Id = Guid.NewGuid().ToString(),
                Date = DateLabel,
                Time = TimeLabel,
                RegisterType = cashFlowLabel,
                Value = double.Parse(prepareToStringValue, CultureInfo.GetCultureInfo("pt-br")),
                ValueLabel = ValueLabel,
                PaymentDescription = PaymentMethodItem.Name,
                OtherPaymentDescription = OtherPaymentMethodDescription,
                CategoryDescription = CategoryItem.Name,
                OtherCategoryDescription = OtherCategoryDescription,
                ImageString = Base64,
                Comment = CommentDescription,
                LabelColor = colorLabel,
                Icon = CategoryItem.Name == "Salário" ? "money.png" :
                       CategoryItem.Name == "Alimentação" ? "restaurant.png" :
                       CategoryItem.Name == "Combustível" ? "car.png" :
                       CategoryItem.Name == "Outros" ? "house.png" :
                       CategoryItem.Name == "" ? "" : ""
            };


            if (await CashFlowService.CreateRegister(register))
            {
                await Application.Current.MainPage.DisplayAlert("Ok", "Salvo", "ok");

                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }
    }
}
