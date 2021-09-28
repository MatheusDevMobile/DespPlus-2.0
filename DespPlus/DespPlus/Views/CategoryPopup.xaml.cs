using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;
using DespPlus.ViewModels;

namespace DespPlus.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryPopup : PopupPage
    {
        public CategoryPopup()
        {
            InitializeComponent();

            BindingContext = DependencyManager.Instance.GetInstance<CategoryPopupVM>();
        }
    }
}