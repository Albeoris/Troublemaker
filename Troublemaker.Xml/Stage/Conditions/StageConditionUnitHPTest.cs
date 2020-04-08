using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='UnitHPTest']")]
    public sealed class StageConditionUnitHPTest : StageCondition
    {
        [XPath("@Operation")] public String Operation;
        [XPath("@Value")] public Int64 Value;

        [XPath("SearchUnit")] public StagePointObject SearchUnit;
        [XPath("TargetUnit")] public StagePointObject TargetUnit;
        [XPath("Unit")] public StagePointObject Unit;
    }
}