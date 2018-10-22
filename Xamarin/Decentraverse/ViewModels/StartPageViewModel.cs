using System;
using Decentraverse.Views;
using Prism.Commands;
using Prism.Navigation;

namespace Decentraverse.ViewModels
{
    public class StartPageViewModel
    {
        public DelegateCommand Start { get; protected set; }
        private INavigationService navigationService;

        public StartPageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            Start = new DelegateCommand(StartCommand);
        }

        public async void StartCommand()
        {
            await navigationService.NavigateAsync(CardCarousel.PATH);
        }
    }
}
