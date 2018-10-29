using System.Threading.Tasks;
using System;
using Decentraverse.Contracts;
using Decentraverse.Models;
using Prism.Mvvm;

namespace Decentraverse.ViewModels
{
    public class CardViewModel : BindableBase {
        private ISolidityService solidityService;
        private IToastService toastService;

        public CardViewModel(ISolidityService solidityService, IToastService toastService) {
            this.solidityService = solidityService;
            this.toastService = toastService;
        }

        public async Task OnTradeButton(Card card)
        {

        }

        private async void OnPurchaseButton(object sender, EventArgs args)
        {
            await solidityService.PurchaseCardForMyself();
            toastService.ShortAlert("Your card has been successfully purchased! Refresh to see.");
        }
    }
}