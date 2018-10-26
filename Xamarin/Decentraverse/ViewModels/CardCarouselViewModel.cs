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

        public ObservableCollection<Card> Cards {
            get => _cards;
            private set => SetProperty(ref _cards, value);
        }

        private ObservableCollection<Card> _cards = new ObservableCollection<Card>();
        private ICardRepository repo;

        public CardCarouselViewModel(ICardRepository cardRepository)
        {
            repo = cardRepository;
            Cards.Add(new Card("Jupiter", "ASD", "Banter", Card.Rarity.HEAVENLY, ImageSource.FromUri(new Uri("https://upload.wikimedia.org/wikipedia/commons/thumb/2/2b/Jupiter_and_its_shrunken_Great_Red_Spot.jpg/330px-Jupiter_and_its_shrunken_Great_Red_Spot.jpg"))));
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
