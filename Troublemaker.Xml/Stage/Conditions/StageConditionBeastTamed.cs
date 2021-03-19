using System;

namespace Troublemaker.Xml
{
    [XPath("self::Condition[@Type='BeastTamed']")]
    public sealed class StageConditionBeastTamed : StageCondition
    {
        [XPath("@BeastKey")] public String BeastKey;
        [XPath("@BuffName")] public String BuffName;
        [XPath("Tamer")] public StagePointType Tamer;
        [XPath("Unit")] public StagePointType Unit;
    }
}