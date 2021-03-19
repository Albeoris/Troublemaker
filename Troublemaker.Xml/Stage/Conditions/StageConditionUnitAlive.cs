using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='UnitAlive']")]
    public sealed class StageConditionUnitAlive : StageCondition
    {
        [XPath("Unit")] public StagePointObject Unit;
    }
}