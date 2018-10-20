using System.Collections.ObjectModel;
using Caliburn.Micro;
using Decentraverse.Contracts;
using Decentraverse.Models;

namespace Decentraverse.ViewModels
{
    public class CardViewModel : Screen
    {
        public ObservableCollection<Card> Cards { get; set; }

        public CardViewModel(ICardRepository cardRepo)
        {
            Cards = new ObservableCollection<Card>(cardRepo.GetMyCards());
        }
    }
}
