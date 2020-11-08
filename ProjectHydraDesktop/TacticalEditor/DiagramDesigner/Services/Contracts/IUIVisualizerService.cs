using ProjectHydraDesktop.TacticalEditor.DiagramDesigner.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectHydraDesktop.TacticalEditor
{
    public interface IUIVisualizerService
    {
        bool? ShowUnitEditDialog(ref Dictionary<int, string> colorTypes, ref Integer selected);
        bool? ShowFriendlyUnitEditDialog(ref FriendlyUnitModel unitModel);
    }
}
