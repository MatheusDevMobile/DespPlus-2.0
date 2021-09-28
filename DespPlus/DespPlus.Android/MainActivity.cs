using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using DespPlus.ApplicationApp;

namespace DespPlus.Droid
{
    [Activity(Label = "DespPlus", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Window.SetStatusBarColor(Android.Graphics.Color.Rgb(101, 188, 191));
            // Window.SetStatusBarColor(Android.Graphics.Color.Rgb(101, 132, 240));

            Rg.Plugins.Popup.Popup.Init(this);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            EnterpriseAplication.InitializeAplication<Factory>();
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        //public override void OnBackPressed()
        //{
        //    Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed);
        //}

    }
}