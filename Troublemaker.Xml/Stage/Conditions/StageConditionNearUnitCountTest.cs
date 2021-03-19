using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='NearUnitCountTest']")]
    public sealed class StageConditionNearUnitCountTest : StageCondition
    {
        [XPath("@Operation")] public String Operation;
        [XPath("@Range")] public Int64 Range;
        [XPath("@Relation")] public String Relation;
        [XPath("@UnitFilterExpr")] public String UnitFilterExpr;
        [XPath("@Value")] public Int64 Value;

        [XPath("Unit")] public StagePointObject Unit;
    }
}