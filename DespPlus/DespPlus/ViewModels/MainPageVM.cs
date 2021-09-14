using DespPlus.Helpers;
using DespPlus.Models;
using DespPlus.Services;
using DespPlus.Services.Interface;
using DespPlus.Utils;
using DespPlus.ViewModels.Interfaces;
using DespPlus.Views;
using ProgressRingControl.Forms.Plugin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DespPlus.ViewModels
{
    public class MainPageVM : IViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected INavigatorService NavigatorService { get; }
        protected ICashFlowService CashFlowService { get; }
        public ICommand GoToRegisterCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand DeleteCommand { get; }
        public MainPageVM(ICashFlowService cashFlowServices, INavigatorService navigatorService)
        {
            CashFlowService = cashFlowServices;
            NavigatorService = navigatorService;

            RefreshCommand = new Command(ExecuteRefreshCommand);
            GoToRegisterCommand = new Command(() => GoToRegister());
            //DeleteCommand = new Command<CashFlow>(async (register) => await DeleteRegister(register));
        }
        public double TotalIncomesValue { get; set; }
        public double TotalExpensesValue { get; set; }
        public bool IsRefreshing { get; set; }
        public double TotalPercentage { get; set; }
        public string TitleMainPage => DateTime.Today.ToString("dddd, dd MMMM DE yyyy").ToUpper();
        public ObservableCollection<CashFlow> CashFlows { get; set; } = new ObservableCollection<CashFlow>();
        public Animation Animation { get; set; }
        public double AnimationProgressPercentage { get; set; }
        public string LabelCurrentMonth { get; set; } = "BALANÇO DO MÊS CORRENTE";
        public string Title { get; set; } = "MainPage";
        public async Task ReceiveNavigationParameters(IReadOnlyDictionary<string, object> parameters)
        {
        }
        private async void GetCashFlow()
        {
            CashFlows.Clear();
            CashFlows = new ObservableCollection<CashFlow>(await CashFlowService.GetAllCashFlow());
        }

        private void GetTotalPercentage()
        {
            var percentage = 0d;
            TotalIncomesValue = CashFlows.Where(x => x.RegisterType == "Rendimento").Sum(l => l.Value);
            TotalExpensesValue = CashFlows.Where(x => x.RegisterType == "Despesa").Sum(l => l.Value);

            if ( TotalIncomesValue > 0 && TotalExpensesValue < 1)
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

        public void ExecuteRefreshCommand()
        {
            GetCashFlow();
            GetTotalPercentage();
            ShowPercentageValuesAnimation();

            IsRefreshing = false;
        }

        private async void GoToRegister()
        {
            await NavigatorService.NavigateToAsync("RegisterPage");
        }
        
        internal async void OpenRegisterInfo(CashFlow cashFlow)
        {
            var parametros = ConstructorParameters.Init(ParametersName.CashFlow, cashFlow).GenerateParameters();
            await NavigatorService.NavigateToAsync("RegisterInfoModal", parametros);
        }

        public async Task<bool> DeleteRegister(CashFlow register)
        {
            if (await CashFlowService.DeleteRegister(register))
            {
                ExecuteRefreshCommand();
                return true;
            }
            return false;
        }
    }
}
