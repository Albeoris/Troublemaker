using System;

namespace Troublemaker.Xml
{
    [XPath("self::ResultModifier")]
    public sealed class StageResultModifier
    {
        [XPath("@ApplyBuff")] public String ApplyBuff;
        [XPath("@AttackerState")] public String AttackerState;
        [XPath("@DamageAdjust")] public String DamageAdjust;
        [XPath("@DefenderState")] public String DefenderState;
    }
}