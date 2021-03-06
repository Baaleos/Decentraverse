﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Decentraverse.Effects;
using Decentraverse.Models;
using Decentraverse.SolidityMethods;
using Decentraverse.ViewModels;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Decentraverse.Views
{
    public partial class CardView : ContentPage
    {
        CancellationToken animationThreadToken;
        CancellationTokenSource animationThread;
        Card Card;
        SendPopup popup;

        public CardView()
        {
            InitializeComponent();
            BindingContextChanged += (object sender, EventArgs e) => BindingChanged();
        }

        public void BindingChanged()
        {
            CardViewModel cardModel = BindingContext as CardViewModel;
            if (cardModel == null)
                return;

            Card = cardModel.Card;
            CardImage.Source = cardModel.Card.GetImage();

            switch (cardModel.Card.CardRarity)
            {
                case Card.Rarity.UNIVERSAL:
                    Universal();
                    break;
                case Card.Rarity.COMMON:
                    Common();
                    break;
                case Card.Rarity.HEAVENLY:
                    Heavenly();
                    break;
                case Card.Rarity.SINGULAR:
                    Singular();
                    break;
            }

            Background.Opacity = 0;
        }

        private void Universal()
        {
            Background.Source = ImageSource.FromResource("Decentraverse.Images.Universal.png", typeof(CardView).Assembly);
            Name.TextColor = Color.Gray;
            OverallFrameExt.BorderColor = Color.Gray;
            OverallFrameInt.BorderColor = Color.Gray;
        }

        private void Common()
        {
            Background.Source = ImageSource.FromResource("Decentraverse.Images.Common.png", typeof(CardView).Assembly);
            Name.TextColor = Color.Black;
            OverallFrameExt.BorderColor = Color.Red;
            OverallFrameInt.BorderColor = Color.Red;
        }

        private void Heavenly()
        {
            Background.Source = ImageSource.FromResource("Decentraverse.Images.Heavenly.png", typeof(CardView).Assembly);
            Name.TextColor = Color.Coral;
            Name.Text = Name.Text.ToUpper();
            OverallFrameExt.BorderColor = Color.Blue;
            OverallFrameInt.BorderColor = Color.Blue;
        }

        private void Singular()
        {
            Background.Source = ImageSource.FromResource("Decentraverse.Images.Singular.png", typeof(CardView).Assembly);
            Name.TextColor = Color.Gold;
            Name.Text = Name.Text.ToUpper();
            OverallFrameExt.BorderColor = Color.Purple;
            OverallFrameInt.BorderColor = Color.Purple;
        }

        private async void OnTradeButton(object sender, EventArgs args)
        {
            if (Card == null)
                return;
            popup = new SendPopup();
            await PopupNavigation.Instance.PushAsync(popup);
            SendPopup.AddressEvent += HandleSend;
        }

        private async void HandleSend(object sender, string address)
        {
            SendPopup.AddressEvent -= HandleSend;
            if (Solidity.MyAddress.Equals(address, StringComparison.CurrentCultureIgnoreCase)) {
                await DisplayAlert("Bad address", "You can't send to yourself!", "OK");
                await PopupNavigation.Instance.RemovePageAsync(popup);
                return;
            }
            await Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    popup.SendButton.IsVisible = false;
                });
                Solidity.ApproveForMe(Card.Token);
                Solidity.SafeTransferFromMe(address, Card.Token);
            });

            await PopupNavigation.Instance.RemovePageAsync(popup);
        }

        private async void OnPurchaseButton(object sender, EventArgs args)
        {
            await Task.Run(() =>
            {
                Solidity.PurchaseCard(Solidity.MyAddress, Solidity.MyPrivateKey);
            });

            await DisplayAlert("Gratz", "Bought! Refresh to see.", "Yay!");
        }
    }
}
