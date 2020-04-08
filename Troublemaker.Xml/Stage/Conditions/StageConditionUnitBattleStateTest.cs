using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='UnitBattleStateTest']")]
    public sealed class StageConditionUnitBattleStateTest : StageCondition
    {
        [XPath("@BattleState")] public Boolean BattleState;
        [XPath("@Operation")] public String Operation;
        [XPath("@Range")] public Int64 Range;
        [XPath("@Relation")] public String Relation;
        [XPath("@UnitFilterExpr")] public String UnitFilterExpr;
        [XPath("@Value")] public Int64 Value;

        [XPath("Unit")] public StagePointObject Unit;
    }
}