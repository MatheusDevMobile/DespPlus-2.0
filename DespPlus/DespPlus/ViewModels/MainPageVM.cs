using DespPlus.Helpers;
using DespPlus.Models;
using DespPlus.Services.Interface;
using DespPlus.ViewModels.Interfaces;
using ProgressRingControl.Forms.Plugin;
using Xamarin.Essentials;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace DespPlus.ViewModels
{
    public class MainPageVM : IViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected INavigatorService NavigatorService { get; }
        protected ICashFlowService CashFlowService { get; }
        protected ICategoryService CategoryService { get; }
        protected IPaymentMethodService PaymentMethodService { get; }
        public ICommand OpenInfoRegisterCommand { get; }
        public ICommand GoToSettingsCommand { get; }
        public ICommand GoToRegisterCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand DeleteCommand { get; }
        public MainPageVM(ICashFlowService cashFlowServices, INavigatorService navigatorService, ICategoryService categoryService, IPaymentMethodService paymentMethodService)
        {
            CashFlowService = cashFlowServices;
            NavigatorService = navigatorService;

            RefreshCommand = new Command(async () => { await ExecuteRefreshCommand(); });
            GoToSettingsCommand = new Command(async () => await NavigatorService.NavigateToAsync("SettingsPage"));
            GoToRegisterCommand = new Command(async () => await NavigatorService.NavigateToAsync("RegisterPage"));
            OpenInfoRegisterCommand = new Command(async (param) => await OpenRegisterInfo((CashFlow)param));
            CategoryService = categoryService;
            PaymentMethodService = paymentMethodService;
        }
        public bool IsRefreshing { get; set; }
        public double TotalIncomesValue { get; set; }
        public double TotalExpensesValue { get; set; }
        public double TotalPercentage { get; set; }
        public double AnimationProgressPercentage { get; set; }
        public string Title => $"Olá {DeviceInfo.Name}!";
        public Animation Animation { get; set; }
        public ObservableCollection<CashFlow> CashFlows { get; set; } = new ObservableCollection<CashFlow>();
        public async Task ReceiveNavigationParameters(IReadOnlyDictionary<string, object> parameters)
        {
            if (parameters != null && parameters.TryGetValue(ParametersName.ReloadPage, out var reloadPage))
            {
                if ((bool)reloadPage)
                {
                    await ExecuteRefreshCommand();
                }
            }
        }
       
        private void GetTotalPercentage()
        {
            var percentage = 0d;
            TotalIncomesValue = CashFlows.Where(x => x.IsIncome).Sum(l => l.Value);
            TotalExpensesValue = CashFlows.Where(x => x.IsIncome == false).Sum(l => l.Value);

            if (TotalIncomesValue > 0 && TotalExpensesValue < 1)
            {
                percentage = 100d;
            }
            if (TotalIncomesValue > 0 && TotalExpensesValue > 0)
            {
                TotalPercentage = 100 - ((TotalExpensesValue * 100) / TotalIncomesValue);
                AnimationProgressPercentage = TotalPercentage / 100;
                return;
            }

            TotalPercentage = percentage;
            AnimationProgressPercentage = TotalPercentage / 100;
        }

        private void ShowPercentageValuesAnimation()
        {
            var startValue = 0;

            var progressBar = new ProgressRing();

            if (Animation != null)
            {
                progressBar.AbortAnimation("Percentage");
            }

            Animation = new Animation(v =>
            {
                if (v == 0)
                {
                    AnimationProgressPercentage = 0;
                    return;
                }
                AnimationProgressPercentage = v / 100;
                TotalPercentage = v;
            }, startValue, TotalPercentage, Easing.SinInOut);

            Animation.Commit(new ProgressRing(), "TotalPercentage", length: 1500,
                finished: (l, c) => { Animation = null; });

        }

        public async Task ExecuteRefreshCommand()
        {
            CashFlows.Clear();
            var cashFlowList = await CashFlowService.GetAllCashFlow();
            CashFlows = new ObservableCollection<CashFlow>(cashFlowList.OrderByDescending(c => c.Date).OrderByDescending(c => c.Time));

            GetTotalPercentage();
            ShowPercentageValuesAnimation();

            IsRefreshing = false;
        }

        internal async Task OpenRegisterInfo(CashFlow cashFlow)
        {
            var parametros = ConstructorParameters.Init(ParametersName.CashFlowDetail, cashFlow).GenerateParameters();
            await NavigatorService.NavigateToAsync("DetailRegister", parametros);
        }

        public async Task<bool> DeleteRegister(string id)
        {
            if (await CashFlowService.DeleteRegister(id))
            {
                await ExecuteRefreshCommand();
                return true;
            }
            return false;
        }
    }
}
