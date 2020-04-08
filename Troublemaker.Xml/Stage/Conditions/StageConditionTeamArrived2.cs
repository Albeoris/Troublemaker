using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='TeamArrived2']")]
    public sealed class StageConditionTeamArrived2 : StageCondition
    {
        [XPath("@Team")] public String Team;
        [XPath("@OnFieldOnly")] public String OnFieldOnly;
        [XPath("AreaIndicator")] public StageAreaIndicator AreaIndicator;
    }
}