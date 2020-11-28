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
    /// Interaction logic for TextEditWindow.xaml
    /// </summary>
    public partial class TextEditWindow : Window
    {
        public TextModel textModel;
        public TextEditWindow(ref TextModel model)
        {

            textModel = model;
            InitializeComponent();
            Slider1.Value = textModel.TextSize;
            CurrentColor.Fill = textModel.TextColor;
            textTB.Text = textModel.Text;

        }

        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedColor = (Button)sender;
            var rectangle = (Rectangle)clickedColor.Content;
            var color = ((SolidColorBrush)rectangle.Fill).Color;

            textModel.TextColor = new SolidColorBrush(color);

            CurrentColor.Fill = new SolidColorBrush(color);
            myColorButton.Flyout.Hide();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            //wczytaj dane
            textModel.TextSize = Slider1.Value;
            this.DialogResult = true;
            this.Close();
        }

        private void textTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            textModel.Text = ((TextBox)sender).Text;
        }
    }
}
