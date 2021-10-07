using DespPlus.Services.Interface;
using DespPlus.ViewModels.Interfaces;
using DespPlus.Views;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DespPlus.Services
{
    public class NavigatorService : INavigatorService
    {
        protected Page CurrentPage => Application.Current.MainPage;
        protected IHandleExeptionService HandleExeption { get; }

        public NavigatorService(IHandleExeptionService handleExeption)
        {
            HandleExeption = handleExeption;
        }

        public void BeginNavigation(string url, IReadOnlyDictionary<string, object> parameters = null, bool navigationBar = true)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                Page page = null;
                try
                {
                    switch (url)
                    {
                        case nameof(MainPage):
                            page = new MainPage();
                            break;
                        case nameof(DetailRegisterPage):
                            page = new DetailRegisterPage();
                            break;
                    }
                    Application.Current.MainPage = navigationBar ? new NavigationPage(page) : page;

                    if ((IViewModel)page?.BindingContext != null)
                        await ((IViewModel)page.BindingContext).ReceiveNavigationParameters(parameters);
                    
                }
                catch (Exception ex)
                {
                    await HandleExeption.ExeptionNotExpected(ex, "Falha na Navegação", $"Ocorreu um erro ao navegar para {url}");
                }
            });
        }

        public async Task NavigateToAsync(string url, IReadOnlyDictionary<string, object> parameters = null)
        {
            try
            {
                Page page = null;
                switch (url)
                {
                    case nameof(RegisterPage):
                        page = new RegisterPage();
                        break;
                    case nameof(DetailRegisterPage):
                        page = new DetailRegisterPage();
                        break;
                    case nameof(FilePopupPage):
                        page = new FilePopupPage();
                        break;
                    case nameof(SettingsPage):
                        page = new SettingsPage();
                        break;
                    case nameof(PaymentMethodPage):
                        page = new PaymentMethodPage();
                        break;
                    case nameof(CategoryPage):
                        page = new CategoryPage();
                        break;
                    case nameof(CategoryPopup):
                        page = new CategoryPopup();
                        break;
                    case nameof(PaymentMethodPopup):
                        page = new PaymentMethodPopup();
                        break;
                    case nameof(OnboardingPage):
                        page = new OnboardingPage();
                        break;
                    case nameof(AlertPopup):
                        page = new AlertPopup();
                        break;


                }
                if (page is PopupPage popup)
                {
                    await CurrentPage.Navigation.PushPopupAsync(popup);
                    if ((IViewModel)page?.BindingContext != null)
                        await ((IViewModel)page.BindingContext).ReceiveNavigationParameters(parameters);
                    return;
                }
                else
                {
                    await CurrentPage.Navigation.PushAsync(page, true);
                    await ((IViewModel)page.BindingContext)?.ReceiveNavigationParameters(parameters);
                }

            }
            catch (Exception ex)
            {
                await HandleExeption.ExeptionNotExpected(ex, "Falha na Navegação", $"Ocorreu um erro ao navegar para {url}");
            }
        }

        public async Task BackToAsync(IReadOnlyDictionary<string, object> parameters = null)
        {
            var currentPage = Application.Current.MainPage;
            Page page;

            try
            {
                if (PopupNavigation.Instance.PopupStack.Count > 0)
                {
                    await currentPage.Navigation.PopPopupAsync();
                    page = PopupNavigation.Instance.PopupStack.LastOrDefault() ?? currentPage.Navigation.ModalStack.LastOrDefault() ?? currentPage.Navigation.NavigationStack.LastOrDefault() ?? Application.Current.MainPage;
                }
                else
                {
                    if (currentPage.Navigation.NavigationStack.Count > 0)
                        await currentPage.Navigation.PopAsync(true);

                    page = currentPage.Navigation.NavigationStack.LastOrDefault() ?? Application.Current.MainPage;
                }

                if (parameters != null)
                    await ((IViewModel)page.BindingContext).ReceiveNavigationParameters(parameters);

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "ok");
            }
        }
    }
}
