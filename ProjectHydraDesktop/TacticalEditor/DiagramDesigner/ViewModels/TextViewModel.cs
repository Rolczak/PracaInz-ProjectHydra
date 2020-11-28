using ProjectHydraDesktop.TacticalEditor.DiagramDesigner.Models;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ProjectHydraDesktop.TacticalEditor.DiagramDesigner
{
    class TextViewModel : DesignerItemViewModelBase
    {
        private IUIVisualizerService visualiserService;

        TextModel refTextModel;

        private TextModel _textModel;
        public TextModel TextModel
        {
            get { return _textModel; }
            set
            {
                _textModel = value;
                NotifyChanged("TextModel");
            }
        
        }


        public TextViewModel(int id, DiagramViewModel parent, double left, double top)
          : base(id, parent, left, top)
        {
            init();
        }
        public TextViewModel(int id, DiagramViewModel parent, double left, double top, double itemWidth, double itemHeight)
    : base(id, parent, left, top, itemWidth, itemHeight)
        {
            init();
        }
        public TextViewModel()
         : base()
        {
            init();
        }

        public ICommand ShowDataChangeWindowCommand { get; private set; }

        public void ExecuteShowDataChangeWindowCommand(object parameter)
        {
            refTextModel = TextModel;
            if (visualiserService.ShowTextEditDialog(ref refTextModel) == true)
            {
                TextModel = refTextModel;
                ItemHeight = refTextModel.TextSize + 50;
                ItemWidth = MeasureString(TextModel.Text).Width + 50;
            }
        }
        private void init()
        {
            TextModel = new TextModel();
            visualiserService = ApplicationServicesProvider.Instance.Provider.VisualizerService;
            ShowDataChangeWindowCommand = new SimpleCommand(ExecuteShowDataChangeWindowCommand);
            this.ShowConnectors = false;
        }
        private Size MeasureString(string candidate)
        {
            var formattedText = new FormattedText(
                candidate,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(new FontFamily("Verdana"), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal),
                TextModel.TextSize,
                Brushes.Black,
                new NumberSubstitution(),
                1);

            return new Size(formattedText.Width, formattedText.Height);
        }
    }
}
