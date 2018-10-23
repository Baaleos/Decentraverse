using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace Decentraverse.Views
{
    public partial class CardCarouselContainerView : ContentPage
    {
        public CardCarouselContainerView()
        {
            InitializeComponent();
            var assembly = typeof(CardCarouselContainerView).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("Decentraverse.Effects.starfield.html");

            using (var reader = new StreamReader(stream))
                Starfield.Source = new HtmlWebViewSource
                {
                    Html = reader.ReadToEnd(),
                };
        }
    }
}
