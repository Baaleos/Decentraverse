using System.Threading.Tasks;
using Decentraverse.Contracts;
using Decentraverse.Models;
using Prism.Mvvm;

namespace Decentraverse.ViewModels
{
    public class CardViewModel : BindableBase
    {
        private ISolidityService solidityService;

        public CardViewModel(ISolidityService solidityService)
        {
            this.solidityService = solidityService;
        }

        public async Task OnTradeButton(Card card)
        {

        }
    }
}
