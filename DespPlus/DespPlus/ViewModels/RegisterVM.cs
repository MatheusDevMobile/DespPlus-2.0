using DespPlus.Converters;
using DespPlus.Helpers;
using DespPlus.Models;
using DespPlus.Services.Interface;
using DespPlus.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
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

        public bool IsEdit { get; set; }
        public bool IsIncome { get; set; }
        public bool IsExpense => !IsIncome;
        public bool HasImage { get; set; }
        public bool AddImageSwitch { get; set; }
        public bool AddImageContainer => AddImageSwitch;
        public string SwitchTitleLabel => "Adicionar Comprovante?";
        public string ValueLabel { get; set; }
        public string CashFlowType { get; set; }
        public ImageSource ReceipPhotoSource { get; set; }
        public string FileName { get; set; }
        public string Base64 { get; set; }
        public string CheckingCopyPhotoName { get; set; }
        public CashFlow CashFlowRegister { get; set; }
        public Category CategoryItem { get; set; } = new Category();
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

            if (parameters != null && parameters.TryGetValue(ParametersName.EditRegister, out var cashFlowParam))
            {
                CashFlowRegister = (CashFlow)cashFlowParam;

                var toStringValue = CashFlowRegister.ValueLabel.Replace("R$ ", "").Replace(",", "").Replace(".", "");

                //Verificar trasnfêrencia para mapeamento
                IsEdit = true;

                IsIncome = CashFlowRegister.IsIncome;

                ValueLabel = toStringValue;
                DateLabel = CashFlowRegister.Date;
                TimeLabel = CashFlowRegister.Time;
                CommentDescription = CashFlowRegister.Comment;
                OtherCategoryDescription = CashFlowRegister.OtherCategoryDescription;
                OtherPaymentMethodDescription = CashFlowRegister.OtherPaymentDescription;
                CategoryItem = CategoryList.FirstOrDefault(c => c.Name.Equals(CashFlowRegister.CategoryDescription));
                PaymentMethodItem = PaymentMethodList.FirstOrDefault(c => c.Name.Equals(CashFlowRegister.PaymentMethodDescription));
                Base64 = CashFlowRegister.ImageString64;
                HasImage = CashFlowRegister.ImageString64 != null;
                FileName = CashFlowRegister.ImageName;

                ReceipPhotoSource = ImageSource.FromStream(() =>
                {
                    return new MemoryStream(Convert.FromBase64String(CashFlowRegister.ImageString64));
                });

            }
        }

        private async Task OpenImage()
        {
            var parameters = ConstructorParameters.Init(ParametersName.FilePopup, ReceipPhotoSource).GenerateParameters();
            await NavigatorService.NavigateToAsync("FilePopupPage", parameters);
        }
        private async void OpenPhotoLibrary()
        {
            var imageSource = await PickPhotoService.PickPhotoFromLibraryAsync();
            if (imageSource.Item1 != null)
            {
                ReceipPhotoSource = imageSource.Item1;
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
                ReceipPhotoSource = imageSource.Item1;
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
                ReceipPhotoSource = null;
                AddImageSwitch = false;
                HasImage = false;
            }
        }

        private async void SaveRegister()
        {
            var toStringValue = ValueLabel.Replace("R$ ", "").Replace(",", "").Replace(".", "");
            var prepareToStringValue = toStringValue.Insert(toStringValue.Length - 2, ",");
            var idRegister = Guid.NewGuid().ToString();

            if (IsEdit)
                idRegister = CashFlowRegister.Id;

            CashFlowRegister = new CashFlow
            {
                Id = idRegister,
                Date = DateLabel,
                Time = TimeLabel,
                IsIncome = this.IsIncome,
                Value = double.Parse(prepareToStringValue, CultureInfo.GetCultureInfo("pt-br")),
                ValueLabel = this.ValueLabel,
                PaymentMethodDescription = PaymentMethodItem.Name,
                OtherPaymentDescription = this.OtherPaymentMethodDescription,
                CategoryDescription = CategoryItem.Name,
                OtherCategoryDescription = OtherCategoryDescription,
                ImageName = FileName,
                ImageString64 = Base64,

                Comment = CommentDescription
            };

            if (IsEdit)
            {
                await CashFlowService.UpdateRegister(idRegister, CashFlowRegister);

                await Application.Current.MainPage.DisplayAlert("Sucesso", "Registro atualizado", "ok");

                var parameters = ConstructorParameters.Init(ParametersName.ReloadPage, true).GenerateParameters();
                await NavigatorService.BackToAsync(parameters);
            }

            if (await CashFlowService.CreateRegister(CashFlowRegister))
            {
                await Application.Current.MainPage.DisplayAlert("Ok", "Salvo", "ok");

                var parameters = ConstructorParameters.Init(ParametersName.ReloadPage, true).GenerateParameters();
                await NavigatorService.BackToAsync(parameters);
            }
        }
    }
}
