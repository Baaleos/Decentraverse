using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace Decentraverse.Views
{
    public partial class CardCarouselView : ContentPage
    {
        public Grid GridScroll => Scroller;
        public CardCarouselView()
        {
            InitializeComponent();
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(CardCarouselContainerView)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("Decentraverse.Effects.starfield.html");

            using (var reader = new StreamReader(stream))
                Starfield.Source = new HtmlWebViewSource
                {
                    Html = reader.ReadToEnd(),
                };

            //Starfield.Source = "http://flashcanvas.net/examples/www.chiptune.com/starfield/starfield.html";
        }
    }
}
