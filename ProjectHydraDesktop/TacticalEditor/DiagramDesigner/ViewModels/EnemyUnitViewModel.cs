using ProjectHydraDesktop.TacticalEditor.DiagramDesigner.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ProjectHydraDesktop.TacticalEditor.DiagramDesigner
{
    class EnemyUnitViewModel : DesignerItemViewModelBase
    {
        private IUIVisualizerService visualiserService;
        public FriendlyUnitModel unitModel;

        public bool Probably { get; set; }
        public bool ConfirmedPosition { get; set; }
        public UnitType Type { get; set; }
        public UnitSize Size { get; set; }
        public EnemyUnitViewModel(int id, DiagramViewModel parent, double left, double top)
           : base(id, parent, left, top)
        {
            init();
        }
        public EnemyUnitViewModel(int id, DiagramViewModel parent, double left, double top, double itemWidth, double itemHeight)
    : base(id, parent, left, top, itemWidth, itemHeight)
        {
            init();
        }
        public EnemyUnitViewModel()
         : base()
        {
            init();
        }

        public ICommand ShowDataChangeWindowCommand { get; private set; }

        public void ExecuteShowDataChangeWindowCommand(object parameter)
        {
            if (visualiserService.ShowFriendlyUnitEditDialog(ref unitModel) == true)
            {
                Probably = unitModel.Probably;
                NotifyChanged("Probably");
                ConfirmedPosition = unitModel.ConfirmedPosition;
                NotifyChanged("ConfirmedPosition");
                Type = unitModel.Type;
                NotifyChanged("Type");
                Size = unitModel.Size;
                NotifyChanged("Size");
            }
        }

        private void init()
        {
            unitModel = new FriendlyUnitModel();
            visualiserService = ApplicationServicesProvider.Instance.Provider.VisualizerService;
            ShowDataChangeWindowCommand = new SimpleCommand(ExecuteShowDataChangeWindowCommand);
            this.ShowConnectors = false;
        }
    }
}

