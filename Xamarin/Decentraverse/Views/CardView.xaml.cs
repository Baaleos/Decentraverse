using System;
using System.Collections.Generic;
using System.Threading;
using Decentraverse.Effects;
using Decentraverse.Models;
using Decentraverse.ViewModels;
using Xamarin.Forms;

namespace Decentraverse.Views
{
    public partial class CardView : ContentView
    {
        CancellationToken animationThreadToken;
        CancellationTokenSource animationThread;

        public CardView()
        {
            InitializeComponent();
            BindingContextChanged += (object sender, EventArgs e) => BindingChanged();
        }

        public void BindingChanged()
        {
            if (!(BindingContext is CardViewModel cardModel))
                return;
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
        }

        private void Universal()
        {
            Background.Source = ImageSource.FromResource("Decentraverse.Images.Universal.png", typeof(CardView).Assembly);
            Name.TextColor = Color.Gray;
        }

        private void Common()
        {
            Background.Source = ImageSource.FromResource("Decentraverse.Images.Common.png", typeof(CardView).Assembly);
            Name.TextColor = Color.Black;

        }

        private void Heavenly()
        {
            Background.Source = ImageSource.FromResource("Decentraverse.Images.Heavenly.png", typeof(CardView).Assembly);
            Name.TextColor = Color.Coral;
            //Name.Effects.Add(new ShadowEffect
            //{
            //    Color = Color.Blue,
            //    DistanceX = 2,
            //    DistanceY = 2,
            //    Radius = 2
            //});
        }

        private void Singular()
        {
            Background.Source = ImageSource.FromResource("Decentraverse.Images.Singular.png", typeof(CardView).Assembly);
            Name.TextColor = Color.Red;
            //Name.Effects.Add(new ShadowEffect
            //{
            //    Color = Color.Chocolate,
            //    DistanceX = 2,
            //    DistanceY = 2,
            //    Radius = 2
            //});
        }
    }
}
