using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ProjectHydraDesktop.TacticalEditor
{
    public interface ISupportDataChanges
    {
        ICommand ShowDataChangeWindowCommand { get; }
    }
}
