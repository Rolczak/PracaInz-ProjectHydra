using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectHydraDektop;
using ProjectHydraRestLibary.Models;
using ProjectHydraRestLibary.Services;
using System;
using System.Windows;
using ProjectHydraDesktop.Views;
using ProjectHydraRestLibary;
using System.Windows.Threading;
using System.Diagnostics;
using NLog;

namespace ProjectHydraDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();
            ServiceProviderFactory.ServiceProvider = ServiceProvider;

            var mainWindow = ServiceProvider.GetRequiredService<Login>();

            var config = new NLog.Config.LoggingConfiguration();
            // Targets where to log to: File and Console
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "file.txt" };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            // Rules for mapping loggers to targets            
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);

            // Apply config           
            NLog.LogManager.Configuration = config;

            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // ...
            ApiHelper.BaseApiUrl = ConfigurationStrings.BaseAPI_URL;
            services.AddSingleton<IApiHelper, ApiHelper>();
            services.AddSingleton<IAuthModel, AuthModel>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<ILessonService, LessonService>();
            services.AddSingleton<IGradesService, GradesService>();
            services.AddSingleton<IUnitService, UnitService>();
            services.AddTransient(typeof(MainWindow));
            services.AddTransient(typeof(Login));
        }
        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs args)
        {
            Logger.Fatal(args.Exception, args.Exception.Message);
            MessageBox.Show("An unexpected exception has occurred. Shutting down the application. Please check the log file for more details.");

            // Prevent default unhandled exception processing
            args.Handled = true;

            Environment.Exit(0);
        }
    }
}
