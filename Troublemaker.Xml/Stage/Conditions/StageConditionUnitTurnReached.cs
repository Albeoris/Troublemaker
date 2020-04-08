using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='UnitTurnReached']")]
    public sealed class StageConditionUnitTurnReached : StageCondition
    {
        [XPath("@TurnCount")] public Int64 TurnCount;
        [XPath("@TurnState")] public String TurnState;

        [XPath("Unit")] public StagePointObject Unit;
    }
}