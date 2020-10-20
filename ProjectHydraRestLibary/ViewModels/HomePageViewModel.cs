using Microsoft.Extensions.DependencyInjection;
using ProjectHydraRestLibary.Models;
using ProjectHydraRestLibary.Services;
using System.ComponentModel;
using System.Threading.Tasks;
using static ProjectHydraRestLibary.Models.User;

namespace ProjectHydraRestLibary.ViewModels
{
    public partial class HomePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IUserService _userService;
        private readonly IAuthModel _authModel;

        public HomePageViewModel()
        {
            _userService = ServiceProviderFactory.ServiceProvider.GetRequiredService<IUserService>();
            _authModel = ServiceProviderFactory.ServiceProvider.GetRequiredService<IAuthModel>();
            _ = LoadUserDataAsync();
        }

        private async Task LoadUserDataAsync()
        {
            UserDetails = await _userService.getUserDetails(_authModel.UserId);
        }


        private UserDetailsModel _userDetails;
        public UserDetailsModel UserDetails
        {
            get { return _userDetails; }
            set
            {
                _userDetails = value;
                OnPropertyChanged("UserDetails");
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
