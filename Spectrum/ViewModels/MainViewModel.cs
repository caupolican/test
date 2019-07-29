using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;

namespace Spectrum.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public MainViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            CreateUserCommand = new MvxAsyncCommand(NavigateAsync);
        }
        
        public override Task Initialize()
        {
            //TODO: Add starting logic here

            return base.Initialize();
        }

        public override void Prepare()
        {
            Title = "Spectrum Users";

            base.Prepare();
        }

        public IMvxAsyncCommand CreateUserCommand { get; set; }
        
        public string Title { get; set; }

        public ObservableCollection<UserViewModel> Users { get; set; }

        public async Task NavigateAsync()
        {
            var user = await _navigationService
                .Navigate<CreateUserViewModel, NavigationParameters, UserViewModel>(new NavigationParameters());

            Users.Add(user);
        }
    }
}