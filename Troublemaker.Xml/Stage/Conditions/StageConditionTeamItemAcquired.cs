using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='TeamItemAcquired']")]
    public sealed class StageConditionTeamItemAcquired : StageCondition
    {
        [XPath("@PsionicStone_FlameCore")] public String PsionicStone_FlameCore;
        [XPath("@Team")] public String Team;
    }
}