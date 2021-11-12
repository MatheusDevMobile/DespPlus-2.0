using DespPlus.Helpers;
using DespPlus.Models;
using DespPlus.Services.Interface;
using DespPlus.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace DespPlus.ViewModels
{
    public class DetailRegisterVM : IViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected ICashFlowService CashFlowService { get; }
        protected INavigatorService NavigatorService { get; }
        protected ICategoryService CategoryService { get; }
        protected IPaymentMethodService PaymentMethodService { get; }
        public ICommand EditRegisterCommand { get; }
        public ICommand DeleteRegisterCommand { get; }
        public ICommand OpenImageCommand { get; }
        public ICommand ClosePopupCommand { get; }
        public DetailRegisterVM(ICashFlowService service, INavigatorService navigatorService, ICategoryService categoryService, IPaymentMethodService paymentMethodService)
        {
            CashFlowService = service;
            NavigatorService = navigatorService;
            CategoryService = categoryService;
            PaymentMethodService = paymentMethodService;

            OpenImageCommand = new Command(async () => { await OpenImage(); });
            ClosePopupCommand = new Command(async () => { await ClosePopup(); });
            EditRegisterCommand = new Command(async () => { await EditRegister(); });
            DeleteRegisterCommand = new Command(async () => { await DeleteRegister(); });
        }

        public string Title => "Detalhes";
        public string TimeLabel { get; set; }
        public string ImageLabel { get; set; }
        public string CategoryDescription { get; set; }
        public string PaymentMethodDescription { get; set; }
        public bool HasComment { get; set; }
        public bool HasImage { get; set; }
        public LayoutState StateView { get; set; }
        public CashFlow CashFlowRegister { get; set; } = new CashFlow();

        public async Task ReceiveNavigationParameters(IReadOnlyDictionary<string, object> parameters)
        {
            var categories = await CategoryService.GetCategories();
            var paymentMethods = await PaymentMethodService.GetPaymentMethods();
            await Task.Run(() =>
            {
                StateView = LayoutState.Loading;

                if (parameters != null && parameters.TryGetValue(ParametersName.CashFlowDetail, out var cashFlow))
                {
                    CashFlowRegister = (CashFlow)cashFlow;

                    TimeLabel = CashFlowRegister.Time.ToString(@"hh\:mm");
                    HasComment = CashFlowRegister.Comment != null;
                    HasImage = CashFlowRegister.ImageString64 != null;
                    ImageLabel = CashFlowRegister.ImageName;
                    CategoryDescription = categories.Where(r => r.CategoryId == CashFlowRegister.CategoryId).FirstOrDefault().Name;
                    PaymentMethodDescription = paymentMethods.Where(r => r.PaymentMethodId == CashFlowRegister.PaymentMethodId).FirstOrDefault().Name; ;
                }

                StateView = LayoutState.Success;
            });
        }
        private async Task EditRegister()
        {
            var parameters = ConstructorParameters.Init(ParametersName.EditRegister, CashFlowRegister).GenerateParameters();

            await NavigatorService.BackToAsync();

            await NavigatorService.NavigateToAsync("RegisterPage", parameters);
        }


        public async Task DeleteRegister()
        {
            var isDelete = await Application.Current.MainPage.DisplayAlert("Atenção","Deseja realmente excluir este registro ?","sim","não");
            
            if (isDelete)
            {
                var isDeleted = await CashFlowService.DeleteRegister(CashFlowRegister.Id);
                
                var parametersRegisterSuccess = ConstructorParameters.Init(ParametersName.Success, isDeleted).GenerateParameters();

                await NavigatorService.NavigateToAsync("AlertPopup", parametersRegisterSuccess);

                await NavigatorService.BackToAsync(parametersRegisterSuccess);
            }
        }
        private async Task OpenImage()
        {
            var imageSource = ImageSource.FromStream(() =>
            {
                return new MemoryStream(Convert.FromBase64String(CashFlowRegister.ImageString64));
            });

            var parameters = ConstructorParameters.Init(ParametersName.FilePopup, imageSource).GenerateParameters();
            await NavigatorService.NavigateToAsync("FilePopupPage", parameters);
        }

        private async Task ClosePopup()
        {
            var parameters = ConstructorParameters.Init(ParametersName.ReloadPage, true).GenerateParameters();
            await NavigatorService.BackToAsync(parameters);
        }
    }
}
