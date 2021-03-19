using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='TeamAttackedToUnit']")]
    public sealed class StageConditionTeamAttackedToUnit : StageCondition
    {
        [XPath("@Team")] public String Team;
        [XPath("Unit")] public StagePointObject Unit;
    }
}