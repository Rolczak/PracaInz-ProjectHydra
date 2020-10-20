using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectHydraMobile.Navigation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainNavigationPage : MasterDetailPage
    {
        public MainNavigationPage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MainNavigationPageMasterMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}