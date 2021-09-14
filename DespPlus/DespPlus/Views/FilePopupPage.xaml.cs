using DespPlus.ViewModels;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace DespPlus.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilePopupPage : PopupPage
    {
        public FilePopupPage()
        {
            InitializeComponent();
            BindingContext = DependencyManager.Instance.GetInstance<FilePopupPageVM>();
        }
    }
}