using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='DifficultyTest']")]
    public sealed class StageConditionDifficultyTest : StageCondition
    {
        [XPath("@DifficultyType")] public String DifficultyType;
        [XPath("@Operation")] public String Operation;
        [XPath("@Value")] public String Value;
    }
}