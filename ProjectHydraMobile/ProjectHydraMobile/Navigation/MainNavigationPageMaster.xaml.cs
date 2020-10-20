using Microsoft.Extensions.DependencyInjection;
using ProjectHydraMobile.Pages;
using ProjectHydraRestLibary;
using ProjectHydraRestLibary.Models;
using ProjectHydraRestLibary.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static ProjectHydraRestLibary.Models.User;

namespace ProjectHydraMobile.Navigation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainNavigationPageMaster : ContentPage
    {
        public ListView ListView;

        public MainNavigationPageMaster()
        {
            InitializeComponent();

            BindingContext = new MainNavigationPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class MainNavigationPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MainNavigationPageMasterMenuItem> MenuItems { get; set; }
            private string _username;
            public string Username
            {
                get { return _username; }
                private set
                {
                    _username = value;
                    OnPropertyChanged("Username");
                }
            }
            private readonly IUserService _userService;
            private readonly IAuthModel _authModel;
            public MainNavigationPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<MainNavigationPageMasterMenuItem>(new[]
                {
                    new MainNavigationPageMasterMenuItem { Id = 0, Title = "Strona Domowa", TargetType = typeof(HomePage) },
                    new MainNavigationPageMasterMenuItem { Id = 1, Title = "Page 2" },
                    new MainNavigationPageMasterMenuItem { Id = 2, Title = "Page 3" },
                    new MainNavigationPageMasterMenuItem { Id = 3, Title = "Page 4" },
                    new MainNavigationPageMasterMenuItem { Id = 4, Title = "Page 5" },
                });
                _authModel = ServiceProviderFactory.ServiceProvider.GetRequiredService<IAuthModel>();
                _userService = ServiceProviderFactory.ServiceProvider.GetRequiredService<IUserService>();
                _ = LoadUserDataAsync();
            }

            private async Task LoadUserDataAsync()
            {
                UserDetailsModel user = await _userService.getUserDetails(_authModel.UserId);
                Username = user.Username;
            }


            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}