using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='VariableTest']")]
    public sealed class StageConditionVariableTest : StageCondition
    {
        [XPath("@Operation")] public String Operation;
        [XPath("@Value")] public String Value;
        [XPath("@Variable")] public String Variable;
    }
}