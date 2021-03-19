using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='GroupArrived']")]
    public sealed class StageConditionGroupArrived : StageCondition
    {
        [XPath("@Group")] public String Group;
        [XPath("AreaIndicator")] public StageAreaIndicator AreaIndicator;
    }
}