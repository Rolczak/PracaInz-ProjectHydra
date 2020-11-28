using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Ink;
using System.Windows.Media;

namespace ProjectHydraDesktop.TacticalEditor.DiagramDesigner
{
    class DiagramViewModel : INPCBase, IDiagramViewModel
    {
        private ObservableCollection<SelectableDesignerItemViewModelBase> items = new ObservableCollection<SelectableDesignerItemViewModelBase>();
        public DiagramViewModel()
        {
            AddItemCommand = new SimpleCommand(ExecuteAddItemCommand);
            RemoveItemCommand = new SimpleCommand(ExecuteRemoveItemCommand);
            ClearSelectedItemsCommand = new SimpleCommand(ExecuteClearSelectedItemsCommand);
            CreateNewDiagramCommand = new SimpleCommand(ExecuteCreateNewDiagramCommand);

            Mediator.Instance.Register(this);
        }
        [MediatorMessageSink("DoneDrawingMessage")]
        public void OnDoneDrawingMessage(bool dummy)
        {
            foreach (var item in Items.OfType<DesignerItemViewModelBase>())
            {
                item.ShowConnectors = false;
            }
        }

        private DrawingAttributes _inkBrush = new DrawingAttributes()
        {
            FitToCurve = true,
            Color = Colors.Black,
            Height = 1,
            Width = 1,
            StylusTip = StylusTip.Ellipse
        };

        public DrawingAttributes InkBrush
        {
            get { return _inkBrush; }
            set {
                _inkBrush = value;
                NotifyChanged("InkBrush");
            }
        }


        private bool _isDrawingChecked = true;
        public bool IsDrawingChecked
        {
            get { return _isDrawingChecked; }
            set
            {
                _isDrawingChecked = value;
                NotifyChanged("IsDrawingChecked");
            }
        }

        public SimpleCommand AddItemCommand { get; private set; }
        public SimpleCommand RemoveItemCommand { get; private set; }
        public SimpleCommand ClearSelectedItemsCommand { get; private set; }
        public SimpleCommand CreateNewDiagramCommand { get; private set; }

        public ObservableCollection<SelectableDesignerItemViewModelBase> Items
        {
            get { return items; }
        }

        public List<SelectableDesignerItemViewModelBase> SelectedItems
        {
            get { return Items.Where(x => x.IsSelected).ToList(); }
        }

        private void ExecuteAddItemCommand(object parameter)
        {
            if (parameter is SelectableDesignerItemViewModelBase)
            {
                SelectableDesignerItemViewModelBase item = (SelectableDesignerItemViewModelBase)parameter;
                item.Parent = this;
                items.Add(item);
            }
        }

        private void ExecuteRemoveItemCommand(object parameter)
        {
            if (parameter is SelectableDesignerItemViewModelBase)
            {
                SelectableDesignerItemViewModelBase item = (SelectableDesignerItemViewModelBase)parameter;
                items.Remove(item);
            }
        }

        private void ExecuteClearSelectedItemsCommand(object parameter)
        {
            foreach (SelectableDesignerItemViewModelBase item in Items)
            {
                item.IsSelected = false;
            }
        }

        private void ExecuteCreateNewDiagramCommand(object parameter)
        {
            Items.Clear();
            Mediator.Instance.NotifyColleaguesAsync<bool>("ClearCanvas",true);
        }
    }
}
