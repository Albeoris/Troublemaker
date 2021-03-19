using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='GroupDeadEvent']")]
    public sealed class StageConditionGroupDeadEvent : StageCondition
    {
        [XPath("@Group")] public String Group;
    }
}