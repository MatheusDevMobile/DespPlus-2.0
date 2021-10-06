using DespPlus.Helpers;
using DespPlus.Models;
using DespPlus.Services.Interface;
using DespPlus.Validators;
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
        private event EventHandler ChangePickerEvent;
        public event PropertyChangedEventHandler PropertyChanged;

        protected RegisterValidator Validator { get; }
        protected ICashFlowService CashFlowService { get; }
        protected IPickPhotoService PickPhotoService { get; }
        protected INavigatorService NavigatorService { get; }
        protected ICategoryService CategoryService { get; }
        protected IPaymentMethodService PaymentMethodService { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteImageCommand { get; }
        public ICommand OpenCameraCommand { get; }
        public ICommand OpenPhotoLibraryCommand { get; }
        public ICommand OpenFileCommand { get; }
        public RegisterVM(ICashFlowService cashFlowService, IPickPhotoService pickPhotoService, INavigatorService navigatorService, ICategoryService categoryService, IPaymentMethodService paymentMethodService, RegisterValidator validator)
        {
            CashFlowService = cashFlowService;
            PickPhotoService = pickPhotoService;
            NavigatorService = navigatorService;
            CategoryService = categoryService;
            PaymentMethodService = paymentMethodService;
            Validator = validator;

            SaveCommand = new Command(() => { SaveRegister(); });
            OpenFileCommand = new Command(async () => { await OpenImage(); });
            OpenCameraCommand = new Command(() => { OpenCamera(); });
            DeleteImageCommand = new Command(() => { DeleteImage(); });
            OpenPhotoLibraryCommand = new Command(() => { OpenPhotoLibrary(); });

            ChangePickerEvent += RegisterVM_ChangePickerEvent;
        }

        private async void RegisterVM_ChangePickerEvent(object sender, EventArgs e)
        {
            var categories = await CategoryService.GetCategories();
            CategoryList = new ObservableCollection<Category>(categories.Where(c => c.IsIncome == this.IsIncome));
        }

        public bool IsEdit { get; set; }
        private bool _isIncome;
        public bool IsIncome
        {
            get { return _isIncome; }
            set
            {
                if (value != _isIncome)
                {
                    _isIncome = value;
                    ChangePickerEvent?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        public bool HasImage { get; set; }
        public bool AddImageSwitch { get; set; }
        public bool AddImageContainer => AddImageSwitch;
        public string ErrorMessage { get; set; }
        public bool HasErrorField { get; set; }
        public string SwitchTitleLabel => "Adicionar Comprovante?";
        public string ValueLabel { get; set; }
        public string CashFlowType { get; set; }
        public string ImageName { get; set; }
        public string Base64 { get; set; }
        public string CheckingCopyPhotoName { get; set; }
        public string OtherCategoryDescription { get; set; }
        public string OtherPaymentMethodDescription { get; set; }
        public string CommentDescription { get; set; }
        public string Title => "REGISTRO";
        public ImageReceip ImageReceip { get; set; }
        public ImageSource ReceipPhotoSource { get; set; }
        public CashFlow CashFlowRegister { get; set; }
        public PaymentMethod PaymentMethodItem { get; set; } = new PaymentMethod();
        public Category CategoryItem { get; set; } = new Category();
        public DateTime DateLabel { get; set; } = DateTime.Today;
        public TimeSpan TimeLabel { get; set; } = DateTime.Now.TimeOfDay;
        public ObservableCollection<Category> CategoryList { get; set; }
        public ObservableCollection<PaymentMethod> PaymentMethodList { get; set; }

        public async Task ReceiveNavigationParameters(IReadOnlyDictionary<string, object> parameters)
        {
            var categories = await CategoryService.GetCategories();
            CategoryList = new ObservableCollection<Category>(categories.Where(c => c.IsIncome == this.IsIncome));

            PaymentMethodList = new ObservableCollection<PaymentMethod>(await PaymentMethodService.GetPaymentMethods());

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
                CategoryItem = CategoryList.Where(c => c.CategoryId == CashFlowRegister.CategoryId).FirstOrDefault();
                PaymentMethodItem = PaymentMethodList.Where(c => c.PaymentMethodId == CashFlowRegister.PaymentMethodId).FirstOrDefault();
                Base64 = CashFlowRegister.ImageString64;
                HasImage = CashFlowRegister.ImageString64 != null;
                ImageName = CashFlowRegister.ImageName;

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
            var imageReceip = await PickPhotoService.PickPhotoFromLibrary();
            if (imageReceip != null)
            {
                ReceipPhotoSource = imageReceip.ImageSource;
                ImageName = imageReceip.ImageName;
                Base64 = imageReceip.Base64;
                AddImageSwitch = false;
                HasImage = true;
            }
        }

        private async void OpenCamera()
        {
            var imageReceip = await PickPhotoService.TakePhoto();
            if (imageReceip != null)
            {
                ReceipPhotoSource = imageReceip.ImageSource;
                ImageName = imageReceip.ImageName;
                Base64 = imageReceip.Base64;
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
            HasErrorField = false;
            ErrorMessage = "";

            if (ValueLabel == null)
                ValueLabel = "0";
            if (CategoryItem == null)
                CategoryItem = new Category { CategoryId = "", Name = "", IsExpense = false, IsIncome = false };
            if (PaymentMethodItem == null)
                PaymentMethodItem = new PaymentMethod { PaymentMethodId = "", Name = "" };

            var toStringValue = ValueLabel.Replace("R$ ", "").Replace(",", "").Replace(".", "");
            var prepareToStringValue = toStringValue.Insert(toStringValue.Length - 2, ",");

            string idRegister;

            if (IsEdit)
                idRegister = CashFlowRegister.Id;
            else
                idRegister = Guid.NewGuid().ToString();

            var cashFlowRegister = new CashFlow
            {
                Id = idRegister,
                Date = DateLabel,
                Time = TimeLabel,
                IsIncome = this.IsIncome,
                Value = double.Parse(prepareToStringValue, CultureInfo.GetCultureInfo("pt-br")),
                ValueLabel = this.ValueLabel,
                PaymentMethodId = PaymentMethodItem.PaymentMethodId,
                CategoryId = CategoryItem.CategoryId,
                ImageName = this.ImageName,
                ImageString64 = Base64,

                Comment = CommentDescription
            };

            var resultValidation = await Validator.ValidateAsync(cashFlowRegister);
            if (!resultValidation.IsValid)
            {
                foreach (var error in resultValidation.Errors)
                {
                    ErrorMessage += $"* {error.ErrorMessage}\n";
                }
                HasErrorField = true;

                return;
            }


            cashFlowRegister = new CashFlow
            {
                Id = idRegister,
                Date = DateLabel,
                Time = TimeLabel,
                IsIncome = this.IsIncome,
                Value = double.Parse(prepareToStringValue, CultureInfo.GetCultureInfo("pt-br")),
                ValueLabel = this.ValueLabel,
                PaymentMethodId = PaymentMethodItem.PaymentMethodId,
                CategoryId = CategoryItem.CategoryId,
                ImageName = this.ImageName,
                ImageString64 = Base64,

                Comment = CommentDescription
            };

            if (IsEdit)
            {
                await CashFlowService.UpdateRegister(idRegister, cashFlowRegister);

                await Application.Current.MainPage.DisplayAlert("Sucesso", "Registro atualizado", "ok");

                await NavigatorService.BackToAsync();
                return;
            }

            if (await CashFlowService.CreateRegister(cashFlowRegister))
            {
                await Application.Current.MainPage.DisplayAlert("Ok", "Salvo", "ok");

                await NavigatorService.BackToAsync();
            }
        }
    }
}
