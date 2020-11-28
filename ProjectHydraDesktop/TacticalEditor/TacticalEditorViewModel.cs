using ProjectHydraDesktop.TacticalEditor.DiagramDesigner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using ProjectHydraDesktop.TacticalEditor.DiagramDesigner.Persistance;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ProjectHydraDesktop.TacticalEditor
{
    class TacticalEditorViewModel : INPCBase
    {
        private List<SelectableDesignerItemViewModelBase> itemsToRemove;
        private IMessageBoxService messageBoxService;
        private DiagramViewModel diagramViewModel = new DiagramViewModel();
        private bool isBusy = false; 
        private int itemId;
        public string FilePath { get; set; }
        public string ComponentName { get; set; }

        public TacticalEditorViewModel()
        {
            messageBoxService = ApplicationServicesProvider.Instance.Provider.MessageBoxService;

            ToolBoxViewModel = new ToolBoxViewModel();
            DiagramViewModel = new DiagramViewModel();

            DeleteSelectedItemsCommand = new SimpleCommand(ExecuteDeleteSelectedItemsCommand);
            CreateNewDiagramCommand = new SimpleCommand(ExecuteCreateNewDiagramCommand);
            UndoDrawingCommand = new SimpleCommand(ExecuteUndoDrawingCommand);
            itemId = 0;
            ConnectorViewModel.PathFinder = new OrthogonalPathFinder();

            Mediator.Instance.Register(this);
        }

        public SimpleCommand DeleteSelectedItemsCommand { get; private set; }
        public SimpleCommand CreateNewDiagramCommand { get; private set; }
        public SimpleCommand UndoDrawingCommand { get; private set; }
        public ToolBoxViewModel ToolBoxViewModel { get; private set; }

        private bool _isDrawingChecked;

        public bool IsDrawingChecked
        {
            get { return _isDrawingChecked; }
            set { 
                _isDrawingChecked = value;
                NotifyChanged("IsDrawingChecked");
                if(DiagramViewModel != null)
                    DiagramViewModel.IsDrawingChecked = !value;
            }
        }

        private Color _penBrushColor;

        public Color PenBrushColor
        {
            get { return _penBrushColor; }
            set 
            { 
                _penBrushColor = value;
                diagramViewModel.InkBrush.Color = value;
                NotifyChanged("InkBrush");
            }
        }

        private double _penBrushHeight;

        public double PenBrushHeight
        {
            get { return _penBrushHeight; }
            set 
            {
                _penBrushHeight = value;
                diagramViewModel.InkBrush.Height = value;
                NotifyChanged("InkBrush");
            }
        }

        private double _penBrushWidth;

        public double PenBrushWidth
        {
            get { return _penBrushWidth; }
            set
            {
                _penBrushWidth = value;
                diagramViewModel.InkBrush.Width = value;
                NotifyChanged("InkBrush");
            }
        }


        public DiagramViewModel DiagramViewModel
        {
            get
            {
                return diagramViewModel;
            }
            set
            {
                if (diagramViewModel != value)
                {
                    diagramViewModel = value;
                    NotifyChanged("DiagramViewModel");
                }
            }
        }

        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                if (isBusy != value)
                {
                    isBusy = value;
                    NotifyChanged("IsBusy");
                }
            }
        }

        public void SetInkCanvasBackgroundImage(BitmapImage bitmapImage )
        {
            Mediator.Instance.NotifyColleaguesAsync<BitmapImage>("SetInkCanvasBackground", bitmapImage);
        }
        private void ExecuteDeleteSelectedItemsCommand(object parameter)
        {
            itemsToRemove = DiagramViewModel.SelectedItems;
            List<SelectableDesignerItemViewModelBase> connectionsToAlsoRemove = new List<SelectableDesignerItemViewModelBase>();

            foreach (var connector in DiagramViewModel.Items.OfType<ConnectorViewModel>())
            {
                if (ItemsToDeleteHasConnector(itemsToRemove, connector.SourceConnectorInfo))
                {
                    connectionsToAlsoRemove.Add(connector);
                }

                if (ItemsToDeleteHasConnector(itemsToRemove, (FullyCreatedConnectorInfo)connector.SinkConnectorInfo))
                {
                    connectionsToAlsoRemove.Add(connector);
                }

            }
            itemsToRemove.AddRange(connectionsToAlsoRemove);
            foreach (var selectedItem in itemsToRemove)
            {
                DiagramViewModel.RemoveItemCommand.Execute(selectedItem);
            }
        }

        private void ExecuteUndoDrawingCommand(object parameter)
        {
            Mediator.Instance.NotifyColleagues<bool>("UndoDrawing", true);
        }
        private void ExecuteCreateNewDiagramCommand(object parameter)
        {
            //ensure that itemsToRemove is cleared ready for any new changes within a session
            itemsToRemove = new List<SelectableDesignerItemViewModelBase>();
            DiagramViewModel.CreateNewDiagramCommand.Execute(null);

            itemId = 0;
            FilePath = "";
            NotifyChanged("FilePath");
        }

        private FullyCreatedConnectorInfo GetFullConnectorInfo(int connectorId, DesignerItemViewModelBase dataItem, ConnectorOrientation connectorOrientation)
        {
            switch (connectorOrientation)
            {
                case ConnectorOrientation.Top:
                    return dataItem.TopConnector;
                case ConnectorOrientation.Left:
                    return dataItem.LeftConnector;
                case ConnectorOrientation.Right:
                    return dataItem.RightConnector;
                case ConnectorOrientation.Bottom:
                    return dataItem.BottomConnector;
                default:
                    throw new InvalidOperationException(
                        string.Format("Found invalid persisted Connector Orientation for Connector Id: {0}", connectorId));
            }
        }

        private static Rect GetBoundingRectangle(IEnumerable<SelectableDesignerItemViewModelBase> items, double margin)
        {
            double x1 = Double.MaxValue;
            double y1 = Double.MaxValue;
            double x2 = Double.MinValue;
            double y2 = Double.MinValue;
            foreach (DesignerItemViewModelBase item in items.OfType<DesignerItemViewModelBase>())
            {
                x1 = Math.Min(item.Left - margin, x1);
                y1 = Math.Min(item.Top - margin, y1);
                x2 = Math.Max(item.Left + item.ItemWidth + margin, x2);
                y2 = Math.Max(item.Top + item.ItemHeight + margin, y2);
            }
            return new Rect(new Point(x1, y1), new Point(x2, y2));
        }

        private DesignerItemViewModelBase GetConnectorDataItem(DiagramViewModel diagramViewModel, int conectorDataItemId, Type connectorDataItemType)
        {
            DesignerItemViewModelBase dataItem = null;
            //if (connectorDataItemType == typeof(ComputationUnitItem))
            //{
            //    dataItem = diagramViewModel.Items.OfType<ComputationUnitViewModel>().Single(x => x.Id == conectorDataItemId);
            //}
            //if (connectorDataItemType == typeof(ServicesItem))
            //{
            //    dataItem = diagramViewModel.Items.OfType<ServicesViewModel>().Single(x => x.Id == conectorDataItemId);
            //}
            return dataItem;
        }

        private bool ItemsToDeleteHasConnector(List<SelectableDesignerItemViewModelBase> itemsToRemove, FullyCreatedConnectorInfo connector)
        {
            return itemsToRemove.Contains(connector.DataItem);
        }
       

    }
}
