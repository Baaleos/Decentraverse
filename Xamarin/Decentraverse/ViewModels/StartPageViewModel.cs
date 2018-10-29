using System;
using Decentraverse.Views;
using Prism.Commands;
using Prism.Navigation;

namespace Decentraverse.ViewModels
{
    public class StartPageViewModel
    {
        private DelegateCommand Start { get; set; }
        private INavigationService navigationService;

        public StartPageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            Start = new DelegateCommand(StartCommand);
        }

        private async void StartCommand()
        {
            await navigationService.NavigateAsync(CardCarousel.PATH);
        }
    }
}
