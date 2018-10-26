using Decentraverse.Contracts;
using Decentraverse.Services;
using Decentraverse.ViewModels;
using Decentraverse.Views;
using Prism;
using Prism.Autofac;
using Prism.Ioc;

namespace Decentraverse
{
    public partial class Decentraverse : PrismApplication
    {
        public Decentraverse() : base() { }

        public Decentraverse(IPlatformInitializer init) : base(init) { }

        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.NavigateAsync(Starfield.PATH);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var keyResolver = new PrivateKeyResolver
            {
                Key = "719736dabed88eb45ee9ceceb31cd671ec2ed5a163985cd2049cda438fc077ec"
            };
            containerRegistry.RegisterInstance(keyResolver);

            var ethereumResolver = new EthereumServiceResolver
            {
                URL = "https://ropsten.infura.io"
            };
            containerRegistry.RegisterInstance(ethereumResolver);
            containerRegistry.Register<IDecentralisedFilesystemService, IPFSFileSystem>();
            containerRegistry.Register<ISolidityService, SolidityService>();
            containerRegistry.Register<ICardRepository, EthereumCardRepository>();

            containerRegistry.RegisterForNavigation<CardCarousel, CardCarouselViewModel>(CardCarousel.PATH);
            containerRegistry.RegisterForNavigation<CardView, CardViewModel>();
            containerRegistry.RegisterForNavigation<StartPage, StartPageViewModel>(StartPage.PATH);
            containerRegistry.RegisterForNavigation<Starfield>(Starfield.PATH);

        }
    }
}
