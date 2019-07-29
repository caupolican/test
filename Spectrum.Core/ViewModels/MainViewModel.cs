using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Spectrum.Services;

namespace Spectrum.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IRepository _repository;

        public MainViewModel(
            IMvxNavigationService navigationService,
            IRepository repository)
        {
            _navigationService = navigationService;
            _repository = repository;

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

            var users = _repository
                .GetAllAsync()
                .GetAwaiter()
                .GetResult();

            Users = new ObservableCollection<UserViewModel>(users);

            base.Prepare();
        }

        public IMvxAsyncCommand CreateUserCommand { get; set; }
        
        public string Title { get; set; }

        public ObservableCollection<UserViewModel> Users { get; set; }

        public async Task NavigateAsync()
        {
            var user = await _navigationService
                .Navigate<CreateUserViewModel, NavigationParameters, UserViewModel>(new NavigationParameters());

            await _repository.AddUserAsync(user);

            Users.Add(user);
        }
    }
}