using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='VariableToVariableTest']")]
    public sealed class StageConditionVariableToVariableTest : StageCondition
    {
        [XPath("@Team")] public String Team;
        [XPath("@Ability")] public String Ability;
        [XPath("@Operation")] public String Operation;
        [XPath("@Value")] public String Value;
        [XPath("@Variable")] public String Variable;
        [XPath("@Variable2")] public String Variable2;
    }
}