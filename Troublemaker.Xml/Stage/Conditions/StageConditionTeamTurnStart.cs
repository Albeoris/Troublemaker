using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='TeamTurnStart']")]
    public sealed class StageConditionTeamTurnStart : StageCondition
    {
        [XPath("Team")] public String Team;
        
        [XPath("Unit")] public StagePointObject Unit;
    }
}