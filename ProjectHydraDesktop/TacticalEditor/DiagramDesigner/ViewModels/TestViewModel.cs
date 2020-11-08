using ProjectHydraDesktop.TacticalEditor.DiagramDesigner;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Windows.Media;

namespace ProjectHydraDesktop.TacticalEditor.DiagramDesigner
{
    class TestViewModel : DesignerItemViewModelBase
    {
        private IUIVisualizerService visualiserService;

        public TestViewModel(int id, DiagramViewModel parent, double left, double top, Brush color, Dictionary<int, string> colorType)
            : base(id, parent, left, top)
        {
            ColorType = colorType;
            BgColor = color;
            init();
        }
        public TestViewModel(int id, DiagramViewModel parent, double left, double top, double itemWidth, double itemHeight, Brush color, Dictionary<int, string> colorType)
    : base(id, parent, left, top, itemWidth, itemHeight)
        {
            ColorType = colorType;
            BgColor = color;
            init();
        }
        public TestViewModel()
            : base()
        {
            ColorType = new Dictionary<int, string>();
            init();
        }
        public Dictionary<int, string> ColorType { get; set; }
        public Brush BgColor { get; set; }
        public int Selected;
        public ICommand ShowDataChangeWindowCommand { get; private set; }

        public void ExecuteShowDataChangeWindowCommand(object parameter)
        {
            //TestData data = new TestData(BgColor);
            Dictionary<int, string> colors = ColorType;
            Integer SelectedInt = new Integer(Selected);
            if (visualiserService.ShowUnitEditDialog(ref colors, ref SelectedInt) == true)
            {
                Selected = SelectedInt.Value;
                switch (Selected)
                {
                    case 0:
                        BgColor = new SolidColorBrush(Color.FromRgb(0, 0, 255));
                        break;
                    case 1:
                        BgColor = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                        break;
                }
                NotifyChanged("BgColor");
            }
        }
        private void init()
        {
            ColorType.Add(1, "Swój");
            ColorType.Add(2, "Nieprzyjaciel");
            Selected = 0;
            visualiserService = ApplicationServicesProvider.Instance.Provider.VisualizerService;
            ShowDataChangeWindowCommand = new SimpleCommand(ExecuteShowDataChangeWindowCommand);
            this.ShowConnectors = false;

        }
    }
}
