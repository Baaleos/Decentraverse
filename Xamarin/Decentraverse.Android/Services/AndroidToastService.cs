using Android.App;
using Android.Widget;
using Decentraverse.Contracts;
using Google.Protobuf;

namespace Decentraverse.Droid.Services {
    public class AndroidToastService : IToastService {
        public void LongAlert(string message) {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message) {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}