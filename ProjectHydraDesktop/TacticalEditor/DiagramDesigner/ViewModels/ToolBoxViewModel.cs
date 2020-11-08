using ProjectHydraDesktop.TacticalEditor.DiagramDesigner;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectHydraDesktop.TacticalEditor
{
    class ToolBoxViewModel
    {
        private List<ToolBoxData> toolBoxItems = new List<ToolBoxData>();

        public ToolBoxViewModel()
        {
            toolBoxItems.Add(new ToolBoxData("../Images/FriendlyUnit.png", typeof(FriendlyUnitViewModel)));
            toolBoxItems.Add(new ToolBoxData("../Images/EnemyUnit.png", typeof(EnemyUnitViewModel)));
        }

        public List<ToolBoxData> ToolBoxItems
        {
            get { return toolBoxItems; }
        }
    }
}
