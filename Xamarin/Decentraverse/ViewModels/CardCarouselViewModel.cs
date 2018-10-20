using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Caliburn.Micro;
using Decentraverse.Contracts;
using Decentraverse.Views;
using Xamarin.Forms;

namespace Decentraverse.ViewModels
{
    public class CardCarouselViewModel : Screen
    {
        public ObservableCollection<CardView> CardViews = new ObservableCollection<CardView>();

        public CardCarouselViewModel(ICardRepository cardRepository)
        {
            foreach(var card in cardRepository.GetMyCards())
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
            foreach (CardView cardView in CardViews)
            {
                carouselView.GridScroll.Children.Add(cardView);
            }
        }
    }
}
