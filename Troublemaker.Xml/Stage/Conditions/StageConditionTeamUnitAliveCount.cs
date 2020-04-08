using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='TeamUnitAliveCount']")]
    public sealed class StageConditionTeamUnitAliveCount : StageCondition
    {
        [XPath("@OnFieldOnly")] public Boolean OnFieldOnly;
        [XPath("@Team")] public String Team;
        [XPath("@Operation")] public String Operation;
        [XPath("@Value")] public Int64 Value;
    }
}