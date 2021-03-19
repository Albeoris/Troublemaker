using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='ReleasePose']")]
    public sealed class StageActionReleasePose : StageAction
    {
        [XPath("@Animation")] public String Animation;
        [XPath("@FadeIn")] public Boolean FadeIn;
        [XPath("@FadeOut")] public Boolean FadeOut;
        [XPath("@LoopAnimation")] public String LoopAnimation;
        [XPath("@ReleaseAnimation")] public String ReleaseAnimation;

        [XPath("Unit")] public StagePointObject Unit;
    }
}