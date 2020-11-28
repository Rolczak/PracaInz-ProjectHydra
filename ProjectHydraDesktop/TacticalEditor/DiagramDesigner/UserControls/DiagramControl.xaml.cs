using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ProjectHydraDesktop.TacticalEditor.DiagramDesigner
{
    /// <summary>
    /// Interaction logic for DiagramControl.xaml
    /// </summary>
    public partial class DiagramControl : UserControl
    {
        public DiagramControl()
        {
            InitializeComponent();
            Mediator.Instance.Register(this);
        }

        private void DesignerCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            DesignerCanvas myDesignerCanvas = sender as DesignerCanvas;
            zoomBox.DesignerCanvas = myDesignerCanvas;
        }
        [MediatorMessageSink("ClearCanvas")]
        public void ClearCanvas(bool dummy)
        {
            Dispatcher.Invoke(() => {
                DrawingCanvasIC.Strokes.Clear();
                DrawingCanvasIC.Background = new SolidColorBrush(Colors.White);
            });
        }
        [MediatorMessageSink("UndoDrawing")]
        public void UndoDrawing(bool dummy)
        {
            Dispatcher.Invoke(() =>
            {
                if (DrawingCanvasIC.Strokes.Count > 0)
                {
                    DrawingCanvasIC.Strokes.RemoveAt(DrawingCanvasIC.Strokes.Count - 1);
                }   
            });
        }

        
        [MediatorMessageSink("SetInkCanvasBackground")]
        public void SetInkCanvasBG(BitmapImage bg)
        {
            Dispatcher.Invoke(() =>
            {
                DrawingCanvasIC.Background = new ImageBrush(bg);
            });
        }

    }
}
