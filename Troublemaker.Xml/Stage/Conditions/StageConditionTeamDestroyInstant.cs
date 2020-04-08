using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='TeamDestroyInstant']")]
    public sealed class StageConditionTeamDestroyInstant : StageCondition
    {
        [XPath("@OnFieldOnly")] public Boolean OnFieldOnly;
        [XPath("@Team")] public String Team;
    }
}