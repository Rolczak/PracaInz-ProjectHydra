using ProjectHydraDesktop.TacticalEditor.DiagramDesigner.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace ProjectHydraDesktop.TacticalEditor.DiagramDesigner
{
    public class PartCreatedConnectionInfo : ConnectorInfoBase
    {
        public Point CurrentLocation { get; private set; }

        public PartCreatedConnectionInfo(Point currentLocation) : base(ConnectorOrientation.None)
        {
            this.CurrentLocation = currentLocation;
        }
    }
}
