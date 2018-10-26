using System;
using System.Collections.ObjectModel;
using Caliburn.Micro;
using Decentraverse.Contracts;
using Decentraverse.Models;
using Decentraverse.Views;
using Xamarin.Forms;
using System.Collections.Generic;
namespace Decentraverse.ViewModels
{
    public class CardCarouselViewModel : Screen
    {
        CardCarouselView view;

        public ObservableCollection<CardView> CardViews = new ObservableCollection<CardView>();
        private ICardRepository repo;

        public CardCarouselViewModel(ICardRepository cardRepository)
        {
            repo = cardRepository;
        }

        protected override void OnViewAttached(object view, object context)
        {
            this.view = view as CardCarouselView;
            RefreshCards(null, null);
        }


        private void DoRefreshOnBackgroundThread()
        {
            var cards = repo.GetMyCards();

            Device.BeginInvokeOnMainThread(()=>{
                OnMainThread(cards);
            });
        }

        private void OnMainThread(IEnumerable<Card> cards){
            foreach (var card in repo.GetMyCards())
            {
                CardView cardView = new CardView
                {
                    BindingContext = new CardViewModel(card)
                };

                CardViews.Add(cardView);
            }

            if (CardViews.Count == 0)
                return;

            foreach (var child in CardViews)
            {
                view.Children.Add(child);
            }

            var button = new Button
            {
                Text = "Refresh",
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            button.Clicked += RefreshCards;

            view.Children.Add(new ContentPage
            {
                Content = new AbsoluteLayout
                {
                    Children = {
                        button
                    }
                }
            });
           
        }

        private void RefreshCards(object sender, EventArgs args)
        {
            view.Children.Clear();
            CardViews.Clear();

            System.Threading.Thread newThread = new System.Threading.Thread(DoRefreshOnBackgroundThread);
            newThread.IsBackground = true;
            newThread.Start();


        }
    }
}
