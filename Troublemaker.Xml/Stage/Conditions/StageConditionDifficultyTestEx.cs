using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='DifficultyTestEx']")]
    public sealed class StageConditionDifficultyTestEx : StageCondition
    {
        [XPath("@DifficultyType")] public String DifficultyType;
        [XPath("@Operation")] public String Operation;
    }
}