using ProjectHydraDesktop.TacticalEditor.DiagramDesigner;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace ProjectHydraDesktop.TacticalEditor.DiagramDesigner
{
    class TestData : INPCBase
    {
        private Brush _color = new SolidColorBrush(Colors.Red);
        public TestData(Brush BgColor)
        {
            _color = BgColor;
        }

        public Brush Color
        {
            get
            {
                return _color;
            }
            set
            {
                if (_color != value)
                {
                    _color = value;
                    NotifyChanged("BackgroundColor");
                }
            }

        }
    }
}
