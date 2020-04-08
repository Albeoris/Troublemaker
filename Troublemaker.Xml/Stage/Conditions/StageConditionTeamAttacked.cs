using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='TeamAttacked']")]
    public sealed class StageConditionTeamAttacked : StageCondition
    {
        [XPath("@Team")] public String Team;
        [XPath("Unit")] public StagePointObject Unit;
    }
}