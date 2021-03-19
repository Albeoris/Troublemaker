using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='TurnBack']")]
    public sealed class StageActionTurnBack : StageAction
    {
        [XPath("@FadeIn")] public Boolean FadeIn;
        [XPath("@FadeOut")] public Boolean FadeOut;
        [XPath("@Range")] public Double Range;
        [XPath("@ReleaseAnimation")] public String ReleaseAnimation;
    }
}