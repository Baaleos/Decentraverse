using Android.App;
using Android.Content.PM;
using Android.OS;
using Caliburn.Micro;

namespace Decentraverse.Droid
{
    [Activity(Label = "Decentraverse", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(IoC.Get<App>());
        }
    }
}