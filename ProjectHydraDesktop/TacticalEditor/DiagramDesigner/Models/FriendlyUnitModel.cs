using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ProjectHydraDesktop.TacticalEditor.DiagramDesigner.Models
{
    public class FriendlyUnitModel
    {
        public FriendlyUnitModel(bool probably = false, bool confirmedPosition = false, UnitType type= UnitType.None,UnitSize size = UnitSize.None)
        {
            Probably = probably;
            ConfirmedPosition = confirmedPosition;
            Type = type;
            Size = size;
        }
        public bool Probably { get; set; }

        public bool ConfirmedPosition { get; set; }

        public UnitType Type { get; set; }

        public UnitSize Size{ get; set; }
    }
}
