using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='TeamArrivedUnitCountTest']")]
    public sealed class StageConditionTeamArrivedUnitCountTest : StageCondition
    {
        [XPath("@Team")] public String Team;
        [XPath("@Operation")] public String Operation;
        [XPath("@Value")] public Int64 Value;

        [XPath("AreaIndicator")] public StageAreaIndicator AreaIndicator;
    }
}