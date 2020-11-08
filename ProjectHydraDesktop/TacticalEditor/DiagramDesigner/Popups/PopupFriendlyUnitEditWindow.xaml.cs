using ProjectHydraDesktop.TacticalEditor.DiagramDesigner.Models;
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
    /// Interaction logic for PopupFriendlyUnitEditWindow.xaml
    /// </summary>
    public partial class PopupFriendlyUnitEditWindow : Window
    {
        public FriendlyUnitModel FriendlyUnit;
        public PopupFriendlyUnitEditWindow(ref FriendlyUnitModel unitModel)
        {
            InitializeComponent();
            FriendlyUnit = unitModel;
            DataContext = this;
            ProbablyCbx.IsChecked = unitModel.Probably;
            KnownPositionCbx.IsChecked = unitModel.ConfirmedPosition;
            TypeCmbx.SelectedItem = unitModel.Type;
            SizeCmbx.SelectedItem = unitModel.Size;
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            FriendlyUnit.Probably = ProbablyCbx.IsChecked.Value;
            FriendlyUnit.ConfirmedPosition = KnownPositionCbx.IsChecked.Value;
            FriendlyUnit.Type = (UnitType)TypeCmbx.SelectedItem;
            FriendlyUnit.Size = (UnitSize)SizeCmbx.SelectedItem;
            this.DialogResult = true;
            this.Close();
        }
    }
}
