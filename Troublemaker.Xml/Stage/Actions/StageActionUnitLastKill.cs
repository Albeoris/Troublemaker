using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Condition[@Type='UnitLastKill']")]
    public sealed class StageConditionUnitLastKill : StageCondition
    {
        [XPath("Unit")] public StagePointObject Unit;
    }
}