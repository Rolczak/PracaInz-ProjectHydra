using Microsoft.Extensions.DependencyInjection;
using ProjectHydraMobile.Navigation;
using ProjectHydraRestLibary;
using ProjectHydraRestLibary.Services;
using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectHydraMobile.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand LoginCommand { get; protected set; }
        private IApiHelper _apiHelper;

        public LoginViewModel()
        {
            LoginCommand = new Command(Login);
            _apiHelper = ServiceProviderFactory.ServiceProvider.GetRequiredService<IApiHelper>();
        }

        private async void Login()
        {
            try
            {
                ErrorMessage = "";
                await _apiHelper.Authenticate(LoginStr, PasswordStr);
                App.Current.MainPage = new MainNavigationPage();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
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
        private string _passwordStr;
        public string PasswordStr
        {
            get { return _passwordStr; }
            set
            {
                _passwordStr = value;
                if (null != this.PropertyChanged)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("PasswordStr"));
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
    }
}
