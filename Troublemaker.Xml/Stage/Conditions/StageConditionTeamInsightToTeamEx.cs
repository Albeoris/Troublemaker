using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='TeamInsightToTeamEx']")]
    public sealed class StageConditionTeamInsightToTeamEx : StageCondition
    {
        [XPath("@CheckEachOther")] public Boolean CheckEachOther;
        [XPath("@FindUnitFilter")] public Boolean FindUnitFilter;
        [XPath("@FindUnitFilter2")] public String FindUnitFilter2;
        [XPath("@Team")] public String Team;
        [XPath("@Team2")] public String Team2;

        [XPath("Unit")] public StagePointObject Unit;
    }
}