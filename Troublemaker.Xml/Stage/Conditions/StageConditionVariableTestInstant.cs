using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='VariableTestInstant']")]
    public sealed class StageConditionVariableTestInstant : StageCondition
    {
        [XPath("@Operation")] public String Operation;
        [XPath("@Value")] public String Value;
        [XPath("@Variable")] public String Variable;
    }
}