using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='ChangeEquipmentType']")]
    public sealed class StageActionChangeEquipmentType : StageAction
    {
        [XPath("@EquipmentType")] public String EquipmentType;

        [XPath("Unit")] public StagePointObject Unit;
    }
}