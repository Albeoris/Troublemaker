using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='PlayPose']")]
    public sealed class StageActionPlayPose : StageAction
    {
        [XPath("@Animation")] public String Animation;
        [XPath("@FadeIn")] public Boolean FadeIn;
        [XPath("@LoopAnimation")] public String LoopAnimation;

        [XPath("Unit")] public StagePointObject Unit;
    }
}