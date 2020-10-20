using Microsoft.Extensions.DependencyInjection;
using ProjectHydraRestLibary;
using ProjectHydraRestLibary.Models;
using ProjectHydraRestLibary.Services;
using System;
using Xamarin.Forms;

namespace ProjectHydraMobile
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            InitializeComponent();
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
            ServiceProviderFactory.ServiceProvider = ServiceProvider;

            MainPage = new MainPage();
        }


        protected override void OnStart()
        {

        }
        private void ConfigureServices(IServiceCollection services)
        {
            ApiHelper.BaseApiUrl = "https://10.0.2.2:44305";
            services.AddSingleton<IApiHelper, ApiHelper>();
            services.AddSingleton<IAuthModel, AuthModel>();
            services.AddScoped<IUserService, UserService>();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
