using System;
using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Decentraverse.Contracts;
using Decentraverse.Services;
using Decentraverse.ViewModels;
using Decentraverse.Views;
using Xamarin.Forms;

namespace Decentraverse
{
    public class App : FormsApplication
    {
        private readonly SimpleContainer container;

        public App(SimpleContainer container)
        {
            Initialize();
            this.container = container;
            container.PerRequest<CardCarouselViewModel>();
            var keyResolver = new PrivateKeyResolver
            {
                Key = "719736dabed88eb45ee9ceceb31cd671ec2ed5a163985cd2049cda438fc077ec"
            };
            container.Instance(keyResolver);
            container.Instance<ICardRepository>(new EthereumCardRepository(keyResolver));

            DisplayRootView<CardCarouselView>();
        }

        protected override void PrepareViewFirst(NavigationPage navigationPage)
        {
            container.Instance<INavigationService>(new NavigationPageAdapter(navigationPage));
        }
    }
}
