using System;

namespace Troublemaker.Xml
{
    [XPath("self::AbilityGuideFlag")]
    public sealed class StageAbilityGuideFlag
    {
        [XPath("@BasicGuide")] public String BasicGuide;
    }
}