using Caliburn.Micro;
using Decentraverse.Models;

namespace Decentraverse.ViewModels
{
    public class CardViewModel : Screen
    {
        public Card Card { get; set; }

        public CardViewModel(Card card)
        {
            Card = card;
        }
    }
}
