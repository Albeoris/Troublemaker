using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='TeamInsightToUnit']")]
    public sealed class StageConditionTeamInsightToUnit : StageCondition
    {
        [XPath("@CheckEachOther")] public Boolean CheckEachOther;
        [XPath("@FindUnitFilter")] public Boolean FindUnitFilter;
        [XPath("@Team")] public String Team;

        [XPath("Unit")] public StagePointObject Unit;
    }
}