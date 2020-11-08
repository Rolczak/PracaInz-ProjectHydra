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
using System.Windows.Shapes;

namespace ProjectHydraDesktop.TacticalEditor
{
    /// <summary>
    /// Interaction logic for PopupFriendlyUnitWindow.xaml
    /// </summary>
    public partial class PopupFriendlyUnitWindow : Window
    {
        Integer integer;
        public PopupFriendlyUnitWindow(ref Dictionary<int, string> type, ref Integer selected)
        {
            InitializeComponent();
            DataContext = this;
            cmbType.ItemsSource = type;
            cmbType.SelectedIndex = selected.Value;
            integer = selected;
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            integer.Value = cmbType.SelectedIndex;
            this.DialogResult = true;
            this.Close();
        }
    }
}
