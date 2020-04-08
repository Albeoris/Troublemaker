using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='UnitInsightToTeam']")]
    public sealed class StageConditionUnitInsightToTeam : StageCondition
    {
        [XPath("@CheckEachOther")] public Boolean CheckEachOther;
        [XPath("@FindUnitFilter")] public Boolean FindUnitFilter;
        [XPath("@SearchUnitFilter")] public String SearchUnitFilter;
        [XPath("@Team")] public String Team;

        [XPath("Unit")] public StagePointObject Unit;
    }
}