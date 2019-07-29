using System;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;

namespace Spectrum.ViewModels
{
    public class CreateUserViewModel: MvxViewModel<NavigationParameters, UserViewModel>
    {
        private readonly IMvxNavigationService _navigationService;

        public CreateUserViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            CreateUserCommand = new MvxAsyncCommand(CloseAsync);
        }

        public override Task Initialize()
        {
            //TODO: Add starting logic here

            return base.Initialize();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public IMvxAsyncCommand CreateUserCommand { get; set; }


        public override void Prepare(NavigationParameters parameter)
        {
            //
        }

        public async Task CloseAsync()
        {
            await _navigationService.Close(this, new UserViewModel {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = Password
            });
        }


    }
}
