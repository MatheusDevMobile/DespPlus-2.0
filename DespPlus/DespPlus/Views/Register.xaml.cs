using DespPlus.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DespPlus.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();

            BindingContext = DependencyManager.Instance.GetInstance<RegisterVM>();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Ops!", "Camlma ae zé, logo tem mais", "ok");
        }

        async void OnPickPhotoButtonClicked(object sender, EventArgs e)
        {

            //Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();

            //if (stream != null)
            //{
            //    //mostra link para visualização se a imagem existir
            //    linkShowImage.IsVisible = true;
            //    cupom.IsToggled = false;
            //    iconTrashImage.IsVisible = true;

            //    addChangeCupon.Text = "Alterar Comprovante ?";

            //    //var target = new MemoryStream();
            //    //stream.CopyTo(target);
            //    //var byteFile = target.ToArray();

            //    //Base64 = Convert.ToBase64String(byteFile);
            //    //Bytes = byteFile.Length;
            //    //ContentType = "image/jpg";
            //    //FileName = "Picture.jpg";

            //}

        }

        async void PhotoCamera(object sender, EventArgs args)
        {
            try
            {
                //await CrossMedia.Current.Initialize();

                //if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                //{
                //    await DisplayAlert("Atenção!", "Sem câmera disponível", "OK");
                //    return;
                //}

                var file = "";//await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                //{
                //    PhotoSize = PhotoSize.Custom,
                //    CustomPhotoSize = 30,
                //    Directory = "RDO_IMAGES",
                //    Name = "Picture.jpg",
                //    SaveToAlbum = false
                //});

                if (file != null)
                {
                    //await PopupNavigation.Instance.PushAsync(new IsLoadingPage(LabelInformacao.carregando));

                    //mostra link para visualização se a imagem existir
                    //linkShowImage.IsVisible = true;
                    //cupom.IsToggled = false;
                    //iconTrashImage.IsVisible = true;

                    //addChangeCupon.Text = "Alterar Comprovante ?";

                    //var target = new MemoryStream();
                    //file.GetStream().CopyTo(target);
                    //var byteFile = target.ToArray();

                    //Base64 = Convert.ToBase64String(byteFile);
                    //Bytes = byteFile.Length;
                    //ContentType = "image/jpg";
                    //FileName = "Picture.jpg";

                    //file.Dispose();

                    //await PopupNavigation.Instance.PopAsync();
                }

                if (file == null)
                {
                    return;
                }
            }

            catch (Exception ex)
            {
                await DisplayAlert("Erro!", ex.Message, "ok");

                //await PopupNavigation.Instance.PopAsync();
            }
        }

        private async void DeleteImage(object sender, EventArgs args)
        {
            var confirm = await DisplayAlert("Excluir Comprovante?", "", "sim", "não");

            if (confirm)
            {
                try
                {
                    //this.Base64 = null;
                    //this.Bytes = 0;
                    //this.ContentType = null;
                    //this.FileName = null;

                    //linkShowImage.IsVisible = false;
                    //nTrashImage.IsVisible = false;

                    //addChangeCupon.Text = "Adicionar Comprovante ?";
                }
                catch (Exception ex)
                {

                    await DisplayAlert("Erro ao excluir", ex.Message, "ok");
                }

            }
        }
        async void OpenImage(object sender, EventArgs args)
        {
            //await PopupNavigation.Instance.PushAsync(new ModalImage(this.Base64));
        }

       
    }
}