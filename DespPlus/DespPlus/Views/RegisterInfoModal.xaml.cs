using DespPlus.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DespPlus.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterInfoModal : ContentPage
    {
        public RegisterInfoModal()
        {
            InitializeComponent();

            BindingContext = DependencyManager.Instance.GetInstance<RegisterInfoModalVM>();
        }
    }
}