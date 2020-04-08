using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='UnitBuffStateEvent']")]
    public sealed class StageConditionUnitBuffStateEvent : StageCondition
    {
        [XPath("@BuffName")] public String BuffName;
        [XPath("@CheckKiller")] public Boolean CheckKiller;

        [XPath("Unit")] public StagePointObject Unit;
    }
}