using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ProjectHydraDesktop.TacticalEditor
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum UnitSize
    {
        [Description("Brak")]
        None,
        [Description("Obsługa/Sekcja")]
        Section,
        [Description("Drużyna")]
        Team,
        [Description("Pluton")]
        Platoon,
        [Description("Kompania")]
        Company ,
        [Description("Batalion")]
        Battalion,
        [Description("Pułk")]
        Regiment,
        [Description("Brygada")]
        Brigade,
        [Description("Dywizja")]
        Division,
        [Description("Korpus")]
        Corps,
    }
}
