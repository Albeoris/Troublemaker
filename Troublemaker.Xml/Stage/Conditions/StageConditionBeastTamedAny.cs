using System;

namespace Troublemaker.Xml
{
    [XPath("self::Condition[@Type='BeastTamedAny']")]
    public sealed class StageConditionBeastTamedAny : StageCondition
    {
        [XPath("@BeastKey")] public String BeastKey;
    }
}