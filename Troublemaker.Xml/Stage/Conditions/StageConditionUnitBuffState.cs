using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='UnitBuffState']")]
    public sealed class StageConditionUnitBuffState : StageCondition
    {
        [XPath("@BuffName")] public String BuffName;

        [XPath("Unit")] public StagePointObject Unit;
    }
}