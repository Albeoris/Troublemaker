using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='EnablePostEffect']")]
    public sealed class StageActionEnablePostEffect : StageAction
    {
        [XPath("@PostEffectType")] public String PostEffectType;
    }
}