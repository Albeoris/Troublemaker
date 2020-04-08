using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='TeamInsightToTeam']")]
    public sealed class StageConditionTeamInsightToTeam : StageCondition
    {
        [XPath("@Team")] public String Team;
        [XPath("@Team2")] public String Team2;

        [XPath("Unit")] public StagePointObject Unit;
    }
}