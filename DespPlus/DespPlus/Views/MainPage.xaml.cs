﻿using DespPlus.Models;
using DespPlus.Services.Interface;
using DespPlus.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DespPlus.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = DependencyManager.Instance.GetInstance<MainPageVM>();

            if (VersionTracking.IsFirstLaunchEver)
            {
                var vm = BindingContext as MainPageVM;
                Task.Run(async () => { await vm.OpenOnboardingScreen(); });
            }
        }

        protected override void OnAppearing()
        {
            var vm = BindingContext as MainPageVM;
            Task.Run(async () => { await vm.ExecuteRefreshCommand(); });
        }

        private async void OpenModalRegisterInfoEvent(object sender, EventArgs e)
        {
            var param = ((TappedEventArgs)e).Parameter;
            var vm = BindingContext as MainPageVM;
            await vm.OpenRegisterInfo((CashFlow)param);
        }
    }
}