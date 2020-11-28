using Microsoft.Extensions.DependencyInjection;
using ProjectHydraRestLibary;
using ProjectHydraRestLibary.Models;
using ProjectHydraRestLibary.Services;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using static ProjectHydraRestLibary.Models.User;

namespace ProjectHydraDesktop.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy HomePage.xaml
    /// </summary>
    public partial class HomePage : Page, INotifyPropertyChanged
    {
        //Properties
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IUserService _userService;
        private readonly IAuthModel _authModel;

        private UserDetailsModel _userDetails;
        public UserDetailsModel UserDetails
        {
            get { return _userDetails; }
            private set
            {
                _userDetails = value;
                OnPropertyChanged("UserDetails");
            }
        }

        //Constructors
        public HomePage()
        {
            InitializeComponent();
            DataContext = this;
            _userService = ServiceProviderFactory.ServiceProvider.GetRequiredService<IUserService>();
            _authModel = ServiceProviderFactory.ServiceProvider.GetRequiredService<IAuthModel>();
            _ = LoadUserDataAsync();
        }

        private async Task LoadUserDataAsync()
        {
            UserDetails = await _userService.GetUserDetails(_authModel.UserId);
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
