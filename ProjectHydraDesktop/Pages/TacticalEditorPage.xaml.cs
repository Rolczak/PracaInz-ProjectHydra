using ProjectHydraDesktop.TacticalEditor;
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
    /// Interaction logic for TacticalEditorPage.xaml
    /// </summary>
    public partial class TacticalEditorPage : Page
    {
        private TacticalEditorViewModel vm;
        public TacticalEditorPage()
        {
            InitializeComponent();
            vm = new TacticalEditorViewModel();
            DataContext = vm;

        }
    }
}
