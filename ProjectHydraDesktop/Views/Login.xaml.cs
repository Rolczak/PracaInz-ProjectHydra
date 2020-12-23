using ModernWpf;
using ProjectHydraRestLibary.Services;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ProjectHydraDesktop.Views
{
    /// <summary>
    /// Logika interakcji dla klasy Login.xaml
    /// </summary>
    public partial class Login : Window, INotifyPropertyChanged
    {

        private IApiHelper _apiHelper;

        public event PropertyChangedEventHandler PropertyChanged;

        public Login(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
            DataContext = this;
            InitializeComponent();

            if (ThemeManager.Current.ActualApplicationTheme == ApplicationTheme.Dark)
            {
                LogoImage.Source = new BitmapImage(new Uri(@"../Images/logowhite.png", UriKind.Relative));
            }
            else
            {
                LogoImage.Source = new BitmapImage(new Uri(@"../Images/logo.png", UriKind.Relative));
            }
        }
        private string _loginStr;
        public string LoginStr
        {
            get { return _loginStr; }
            set
            {
                _loginStr = value;
                if (null != this.PropertyChanged)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LoginStr"));
                }
            }
        }

        public bool IsErrorVisible
        {
            get
            {
                bool output = false;
                if (ErrorMessage?.Length > 0)
                {
                    output = true;
                }
                return output;
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ErrorMessage"));
                PropertyChanged(this, new PropertyChangedEventArgs("IsErrorVisible"));
            }
        }

        private async void LoginBtn(object sender, RoutedEventArgs e)
        {
            try
            {
                ErrorMessage = "";
                await _apiHelper.Authenticate(LoginStr, PasswordStrBox.Password);
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
            catch (Exception ex)
            {
                if (ex.Message == "0")
                {
                    ErrorMessage = "API nie odpowiada";
                }
                else
                {
                    ErrorMessage = "Dane nieprawidłowe";
                }
            }

        }
    }
}
