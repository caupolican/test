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
        }

        public override Task Initialize()
        {
            //TODO: Add starting logic here

            return base.Initialize();
        }

        public override void Prepare(NavigationParameters parameter)
        {

        }
    }
}
