using ProjectHydraRestLibary.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectHydraDesktop.Pages
{
    /// <summary>
    /// Interaction logic for MyLessonsPage.xaml
    /// </summary>
    public partial class MyLessonsPage
    {
        public MyLessonsPage()
        {
            var viewModel = new MyLessonsViewModel();
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
