using DespPlus.Services.Interface;
using DespPlus.ViewModels.Interfaces;
using DespPlus.Views;
using DespPlus.Views.Tabbed;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
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
                        case nameof(RegisterInfoModal):
                            page = new RegisterInfoModal();
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
                    case nameof(RegisterInfoModal):
                        page = new RegisterInfoModal();
                        break;
                    case nameof(FilePopupPage):
                        page = new FilePopupPage();
                        break;

                }
                if (page is PopupPage popup)
                {
                    await CurrentPage.Navigation.PushPopupAsync(popup);
                    if ((IViewModel)page?.BindingContext != null)
                        await ((IViewModel)page.BindingContext).ReceiveNavigationParameters(parameters);
                    return;
                }
                if (url.Equals("RegisterInfoModal"))
                {
                    await CurrentPage.Navigation.PushModalAsync(new NavigationPage(page), true);
                    await ((IViewModel)page.BindingContext)?.ReceiveNavigationParameters(parameters);
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

            if (currentPage.Navigation.NavigationStack.Count > 0)
                await currentPage.Navigation.PopAsync(true);

            page = currentPage.Navigation.NavigationStack.LastOrDefault() ?? Application.Current.MainPage;

            if (parameters != null)
                await ((IViewModel)page.BindingContext).ReceiveNavigationParameters(parameters);
        }
    }
}
