using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='UnitArrived2']")]
    public sealed class StageConditionUnitArrived2 : StageCondition
    {
        [XPath("@Team")] public String Team;
        [XPath("@OnFieldOnly")] public String OnFieldOnly;
        [XPath("AreaIndicator")] public StageAreaIndicator AreaIndicator;
        [XPath("Unit")] public StagePointObject Unit;
    }
}