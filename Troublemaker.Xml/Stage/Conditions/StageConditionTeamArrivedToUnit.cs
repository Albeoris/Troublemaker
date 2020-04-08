using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='TeamArrivedToUnit']")]
    public sealed class StageConditionTeamArrivedToUnit : StageCondition
    {
        [XPath("@CheckEachOther")] public Boolean CheckEachOther;
        [XPath("@FindUnitFilter")] public Boolean FindUnitFilter;
        [XPath("@SearchUnitFilter")] public Boolean SearchUnitFilter;
        [XPath("@Team")] public String Team;
        [XPath("@Range")] public Int64 Range;

        [XPath("SearchUnit")] public StagePointObject SearchUnit;
        [XPath("TargetUnit")] public StagePointObject TargetUnit;
        [XPath("Unit")] public StagePointObject Unit;
    }
}