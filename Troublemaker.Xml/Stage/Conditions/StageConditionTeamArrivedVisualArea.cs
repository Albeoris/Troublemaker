using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='TeamArrivedVisualArea']")]
    public sealed class StageConditionTeamArrivedVisualArea: StageCondition
    {
        [XPath("@Team")] public String Team;
        [XPath("@DashboardNPC1")] public String DashboardNPC1;
    }
}