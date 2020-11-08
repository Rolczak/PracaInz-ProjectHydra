using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ProjectHydraDesktop.TacticalEditor
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum UnitType
    {
        [Description("Brak")]
        None,
        [Description("Piechota")]
        Infantry,
        [Description("Piechota zmechanizowana")]
        MechanizedInfantry,
        [Description("Piechota zmotoryzowana")]
        MotorizedInfantry,
        [Description("Pancerna")]
        Armour
    }
}
