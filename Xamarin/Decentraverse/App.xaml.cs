using Decentraverse.Contracts;
using Decentraverse.Services;
using Decentraverse.ViewModels;
using Prism;
using Prism.Autofac;
using Prism.Ioc;

namespace Decentraverse.Views
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer platformInitializer = null) : base(platformInitializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();
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
            containerRegistry.Register<ISolidityService, SolidityService>();
            containerRegistry.Register<ICardRepository, EthereumCardRepository>();

            containerRegistry.RegisterForNavigation<CardCarouselView, CardCarouselViewModel>();
            containerRegistry.RegisterForNavigation<CardView, CardViewModel>();
        }
    }
}
