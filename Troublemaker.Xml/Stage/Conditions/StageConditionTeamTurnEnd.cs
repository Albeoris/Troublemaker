using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='TeamTurnEnd']")]
    public sealed class StageConditionTeamTurnEnd : StageCondition
    {
        [XPath("@Team")] public String Team;
    }
}