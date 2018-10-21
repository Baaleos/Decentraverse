using System.Collections.ObjectModel;
using Caliburn.Micro;
using Decentraverse.Contracts;
using Decentraverse.Models;
using Decentraverse.Views;

namespace Decentraverse.ViewModels
{
    public class CardCarouselViewModel : Screen
    {
        public ObservableCollection<CardView> CardViews = new ObservableCollection<CardView>();
        public CardCarouselViewModel(ICardRepository cardRepository)
        {
            foreach (var card in cardRepository.GetMyCards())
            {
                CardView cardView = new CardView
                {
                    BindingContext = new CardViewModel(card)
                };

                CardViews.Add(cardView);
            }
        }

        protected override void OnViewAttached(object view, object context)
        {
            if (CardViews.Count == 0)
                return;

            var carouselView = view as CardCarouselView;
            //carouselView.ScrollLayout.Children.RemoveAt(1);

            foreach(var child in CardViews)
            {
                carouselView.Children.Add(child);
            }
        }
    }
}
