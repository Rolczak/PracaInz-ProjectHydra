using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace ProjectHydraDesktop.TacticalEditor.DiagramDesigner.Models
{
    public class TextModel
    {

        public string Text { get; set; }
        public SolidColorBrush TextColor { get; set; }
        public double TextSize { get; set; }

        public TextModel(string text = "A", double textSize = 16, SolidColorBrush textColor = null)
        {
            Text = text;
            TextColor = textColor ?? Brushes.Black;
            TextSize = textSize;
        }
    }
}
