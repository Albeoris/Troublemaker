using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='TeamArrived']")]
    public sealed class StageConditionTeamArrived : StageCondition
    {
        [XPath("@Team")] public String Team;
        [XPath("Area")] public StageArea Area;
        [XPath("Unit")] public StagePointObject Unit;
    }
}