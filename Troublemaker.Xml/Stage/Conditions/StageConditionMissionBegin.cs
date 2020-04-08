using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='MissionBegin']")]
    public sealed class StageConditionMissionBegin : StageCondition
    {
    }
}