using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='TeamDeadEvent']")]
    public sealed class StageConditionTeamDeadEvent : StageCondition
    {
        [XPath("@Team")] public String Team;
        [XPath("@BattleState")] public Boolean BattleState;
        [XPath("@BuffName")] public String BuffName;
        [XPath("@Operation")] public String Operation;
        [XPath("@Value")] public String Value;
        
        [XPath("Unit")] public StagePointObject Unit;
    }
}