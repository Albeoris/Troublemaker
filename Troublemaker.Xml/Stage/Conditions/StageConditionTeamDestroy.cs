using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='TeamDestroy']")]
    public sealed class StageConditionTeamDestroy : StageCondition
    {
        [XPath("@Team")] public String Team;
    }
}