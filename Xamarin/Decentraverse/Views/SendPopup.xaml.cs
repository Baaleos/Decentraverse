using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Decentraverse.SolidityMethods;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Decentraverse.Views
{
    public partial class SendPopup : PopupPage
    {
        public static event EventHandler<string> AddressEvent;
        public Button SendButton => SendAddress;

        public SendPopup()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            PopupNavigation.Instance.PopAllAsync();
            return false;
        }

        protected override bool OnBackgroundClicked()
        {
            PopupNavigation.Instance.PopAllAsync();
            return false;
        }

        private void OnSend(object sender, EventArgs e)
        {
            Activity.IsEnabled = true;
            AddressEvent.Invoke(this, Address.Text);
        }
    }
}
