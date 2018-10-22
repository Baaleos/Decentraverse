using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Decentraverse.Contracts;
using Decentraverse.Models;
using Decentraverse.Views;
using Prism.Mvvm;
using Xamarin.Forms;

namespace Decentraverse.ViewModels
{
    public class CardCarouselViewModel : BindableBase
    {
        CardCarousel view;

        public ObservableCollection<Models.Card> Cards = new ObservableCollection<Models.Card>();
        private ICardRepository repo;

        public CardCarouselViewModel(ICardRepository cardRepository)
        {
            repo = cardRepository;
        }

        private async Task RefreshCards()
        {
            Cards.Clear();

            foreach (var card in await repo.GetMyCards())
                Cards.Add(card);

            //view.Children.Clear();
            //CardViews.Clear();

            //foreach (var card in await repo.GetMyCards())
            //{
            //    CardView cardView = new CardView
            //    {
            //        BindingContext = new CardViewModel(card)
            //    };

            //    CardViews.Add(cardView);
            //}

            //if (CardViews.Count == 0)
            //    return;

            //foreach (var child in CardViews)
            //{
            //    view.Children.Add(child);
            //}

            //var button = new Button
            //{
            //    Text = "Refresh",
            //    HorizontalOptions = LayoutOptions.CenterAndExpand
            //};

            //button.Clicked += async (object sender, EventArgs e) => await RefreshCards();

            //view.Children.Add(new ContentPage
            //{
            //    Content = new AbsoluteLayout
            //    {
            //        Children = {
            //            button
            //        }
            //    }
            //});
        }
    }
}
