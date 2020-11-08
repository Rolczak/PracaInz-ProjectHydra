using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectHydraDektop;
using ProjectHydraRestLibary.Models;
using ProjectHydraRestLibary.Services;
using System;
using System.Windows;
using ProjectHydraDesktop.Views;

namespace ProjectHydraDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();
            ServiceProviderFactory.ServiceProvider = ServiceProvider;

            var mainWindow = ServiceProvider.GetRequiredService<Login>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // ...
            ApiHelper.BaseApiUrl = ConfigurationStrings.BaseAPI_URL;
            services.AddSingleton<IApiHelper, ApiHelper>();
            services.AddSingleton<IAuthModel, AuthModel>();
            services.AddSingleton<IUserService, UserService>();
            services.AddTransient(typeof(MainWindow));
            services.AddTransient(typeof(Login));
        }

    }
}
