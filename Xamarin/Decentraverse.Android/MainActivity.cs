﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using Decentraverse.Contracts;
using Decentraverse.Droid.Services;
using Prism;
using Prism.Ioc;

namespace Decentraverse.Droid
{
    [Activity(Label = "Decentraverse", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity {
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            //Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new Decentraverse(new AndroidInitializer()));
        }

        //public override void OnBackPressed()
        //{
        //    base.OnBackPressed();
        //    if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
        //    {
        //        // Do something if there are some pages in the `PopupStack`
        //    }
        //    else
        //    {
        //        // Do something if there are not any pages in the `PopupStack`
        //    }
        //}
    }

    public class AndroidInitializer : IPlatformInitializer {
        public void RegisterTypes(IContainerRegistry containerRegistry) {
            containerRegistry.Register<IToastService, AndroidToastService>();
        }
    }
}