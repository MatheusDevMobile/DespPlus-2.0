using DespPlus.ViewModels;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace DespPlus.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentMethodPopup : PopupPage
    {
        public PaymentMethodPopup()
        {
            InitializeComponent();

            BindingContext = DependencyManager.Instance.GetInstance<PaymentMethodPopupVM>();
        }
    }
}