using ModernWpf.Controls;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;

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
            MainNavigation.SelectedItem = MainNavigation.MenuItems.OfType<NavigationViewItem>().First();
        }

        private void MainNavigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var selectedItem = (NavigationViewItem)args.SelectedItem;
            if (selectedItem != null)
            {
                string pageName = "ProjectHydraDesktop.Pages." + (string)selectedItem.Tag;
                Type pageType = Assembly.GetExecutingAssembly().GetType(pageName);
                ContentFrame.Navigate(pageType);

            }
        }
    }
}
