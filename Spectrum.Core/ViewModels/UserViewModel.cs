using MvvmCross.ViewModels;

namespace Spectrum.ViewModels
{
    public class UserViewModel: MvxNotifyPropertyChanged
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
