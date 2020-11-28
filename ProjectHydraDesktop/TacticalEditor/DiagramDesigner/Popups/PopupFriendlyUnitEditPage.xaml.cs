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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectHydraDesktop.TacticalEditor
{
    /// <summary>
    /// Interaction logic for PopupFriendlyUnitEditPage.xaml
    /// </summary>
    public partial class PopupFriendlyUnitEditPage : Page
    {
        public FriendlyUnitModel FriendlyUnit;
        bool? dialogResult = null;

        public PopupFriendlyUnitEditPage(ref FriendlyUnitModel unitModel)
        {
            InitializeComponent();
            FriendlyUnit = unitModel;
            DataContext = this;
            ProbablyCbx.IsChecked = unitModel.Probably;
            KnownPositionCbx.IsChecked = unitModel.ConfirmedPosition;
            TypeCmbx.SelectedItem = unitModel.Type;
            SizeCmbx.SelectedItem = unitModel.Size;
        }
        public bool? ShowDialog()
        {
            DisableMainPage();

            while (dialogPopUp.IsOpen)
            {
                // HACK: Stop the thread if the application is about to close
                if (this.Dispatcher.HasShutdownStarted ||
                    this.Dispatcher.HasShutdownFinished)
                {
                    break;
                }
            }

            return dialogResult;
        }

        private void DisableMainPage()
        {
            Application.Current.MainWindow.IsEnabled = false;
            this.Background = Brushes.LightGray;
            this.dialogPopUp.PlacementTarget = Application.Current.MainWindow;
            this.dialogPopUp.IsOpen = true;
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            FriendlyUnit.Probably = ProbablyCbx.IsChecked.Value;
            FriendlyUnit.ConfirmedPosition = KnownPositionCbx.IsChecked.Value;
            FriendlyUnit.Type = (UnitType)TypeCmbx.SelectedItem;
            FriendlyUnit.Size = (UnitSize)SizeCmbx.SelectedItem;
            dialogResult = true;
            EnableMainPage();
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            dialogResult = false;
            EnableMainPage();
        }
        private void EnableMainPage()
        {
            Application.Current.MainWindow.IsEnabled = true;
            this.Background = null;
            this.dialogPopUp.PlacementTarget = Application.Current.MainWindow;
            this.dialogPopUp.IsOpen = false;
        }
    }
}
