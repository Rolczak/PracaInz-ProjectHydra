using ProjectHydraRestLibary.Services;
using System;
using System.ComponentModel;
using System.Windows;

namespace WpfApp3.Views
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
            LoginStr = "admin@admin.pl";
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
                ErrorMessage = ex.Message;
            }

        }
    }
}
