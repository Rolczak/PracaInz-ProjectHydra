using Microsoft.Win32;
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
            vm = new TacticalEditorViewModel();
            DataContext = vm;
            InitializeComponent();
        }
        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {
            // Extract the color of the button that was clicked.
            Button clickedColor = (Button)sender;
            var rectangle = (Rectangle)clickedColor.Content;
            var color = ((SolidColorBrush)rectangle.Fill).Color;

            CurrentColor.Fill = new SolidColorBrush(color);
            vm.PenBrushColor = color;
            myColorButton.Flyout.Hide();
        }

        private void Slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            vm.PenBrushHeight = e.NewValue;
        }
        private void Slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            vm.PenBrushWidth = e.NewValue;
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            dlg.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == true)
            {
                string selectedFileName = dlg.FileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
                vm.SetInkCanvasBackgroundImage(bitmap);
            }
        }
    }
}
