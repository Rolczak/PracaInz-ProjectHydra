using ModernWpf;
using ModernWpf.Controls;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ProjectHydraDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Application.Current.MainWindow = this;
            MainNavigation.SelectedItem = MainNavigation.MenuItems.OfType<NavigationViewItem>().First();

            if (ThemeManager.Current.ActualApplicationTheme == ApplicationTheme.Dark)
            {
                logoImage.Source = new BitmapImage(new Uri(@"Images/logowhite.png", UriKind.Relative));
            }
            else
            {
                logoImage.Source = new BitmapImage(new Uri(@"Images/logo.png", UriKind.Relative));
            }
        }

        private void MainNavigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            NavigationViewItem selectedItem;
            try
            {
               selectedItem = (NavigationViewItem)args.SelectedItem;
            }
            catch
            {
                return;
            }

            if (selectedItem != null)
            {
                string pageName = "ProjectHydraDesktop.Pages." + (string)selectedItem.Tag;
                Type pageType = Assembly.GetExecutingAssembly().GetType(pageName);
                ContentFrame.Navigate(pageType);

            }
        }
    }
}
