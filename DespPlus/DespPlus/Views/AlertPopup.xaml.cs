using DespPlus.ViewModels;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace DespPlus.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlertPopup : PopupPage
    {
        public AlertPopup()
        {
            InitializeComponent();

            BindingContext = DependencyManager.Instance.GetInstance<AlertPopupVM>();
        }
    }
}