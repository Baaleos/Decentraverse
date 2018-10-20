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
            container.PerRequest<CardViewModel>();
            container.Instance<ICardRepository>(new InMemoryCardRepository());

            DisplayRootView<CardView>();
        }

        protected override void PrepareViewFirst(NavigationPage navigationPage)
        {
            container.Instance<INavigationService>(new NavigationPageAdapter(navigationPage));
        }
    }
}
