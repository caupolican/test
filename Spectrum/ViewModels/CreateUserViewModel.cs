using System;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using Spectrum.Services;

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
        public string EmailError { get; set; }

        public string Password { get; set; }
        public string PasswordError { get; set; }


        public IMvxAsyncCommand CreateUserCommand { get; set; }


        public override void Prepare(NavigationParameters parameter)
        {
            //
        }

        public async Task CloseAsync()
        {
            // Validate
            if (string.IsNullOrWhiteSpace(Email))
            {
                EmailError = "User Name is required.";
            }
            else
            {
                var (Result, Message) = PasswordValidationService.Validate(Password ?? string.Empty);
                if (Result)
                {
                    await _navigationService.Close(this, new UserViewModel
                    {
                        FirstName = FirstName ?? string.Empty,
                        LastName = LastName ?? string.Empty,
                        Email = Email ?? string.Empty,
                        Password = Password
                    });
                }
                else
                {

                    PasswordError = Message;
                }
            }
        }

    }
}
