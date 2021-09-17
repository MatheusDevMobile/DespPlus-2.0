using DespPlus.ViewModels;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace DespPlus.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailRegister : PopupPage
    {
        public DetailRegister()
        {
            InitializeComponent();

            BindingContext = DependencyManager.Instance.GetInstance<DetailRegisterVM>();
        }
    }
}