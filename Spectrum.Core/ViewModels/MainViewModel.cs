using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Spectrum.Services;
using System.Linq;

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

            IsEmpty = !Users.Any();

            base.Prepare();
        }

        public bool IsEmpty { get; set; }

        public IMvxAsyncCommand CreateUserCommand { get; set; }
        
        public string Title { get; set; }

        public ObservableCollection<UserViewModel> Users { get; set; }

        public async Task NavigateAsync()
        {
            var user = await _navigationService
                .Navigate<CreateUserViewModel, NavigationParameters, UserViewModel>(new NavigationParameters());

            if (user != null)
            {
                await _repository.AddUserAsync(user);
                Users.Add(user);
            }

            IsEmpty = !Users.Any();
        }
    }
}