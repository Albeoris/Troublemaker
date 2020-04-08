using System;

namespace Troublemaker.Xml
{
    [XPath("self::InvestigationTarget")]
    public sealed class StageMapComponentInvestigationTarget : StageMapComponent
    {
        [XPath("InvestigationType")] public StageMapComponentInvestigationTargetType InvestigationType;
        [XPath("Direction")] public StagePosition Direction;
        [XPath("Position")] public StagePosition Position;
    }
}