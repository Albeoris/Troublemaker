using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='TeamBuffState']")]
    public sealed class StageConditionTeamBuffState : StageCondition
    {
        [XPath("@Team")] public String Team;
        [XPath("@BuffName")] public String BuffName;
        [XPath("@Operation")] public String Operation;
        [XPath("@Value")] public Int64 Value;
    }
}