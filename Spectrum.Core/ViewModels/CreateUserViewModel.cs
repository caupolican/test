using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Spectrum.Services;

namespace Spectrum.ViewModels
{
    public class CreateUserViewModel: MvxViewModel<NavigationParameters, UserViewModel>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IPasswordValidationService _passwordValidationService;

        public CreateUserViewModel(
            IMvxNavigationService navigationService,
            IPasswordValidationService passwordValidationService)
        {
            _navigationService = navigationService;
            _passwordValidationService = passwordValidationService;
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
                var (Result, Message) = _passwordValidationService.Validate(Password ?? string.Empty);
                if (Result)
                {
                    await _navigationService.Close(this, new UserViewModel
                    {
                        FirstName = FirstName ?? string.Empty,
                        LastName = LastName ?? string.Empty,
                        Name = $"{LastName}, {FirstName}",
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
