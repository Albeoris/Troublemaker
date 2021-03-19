using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='MissionEnd']")]
    public sealed class StageConditionMissionEnd : StageCondition
    {
        [XPath("@Team")] public String Team;
    }
}