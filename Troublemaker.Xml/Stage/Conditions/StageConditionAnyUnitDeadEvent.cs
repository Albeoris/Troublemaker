using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='AnyUnitDeadEvent']")]
    public sealed class StageConditionAnyUnitDeadEvent : StageCondition
    {
        [XPath("@CheckKiller")] public Boolean CheckKiller;
        [XPath("AnyUnit")] public StagePointType AnyUnit;
    }
}