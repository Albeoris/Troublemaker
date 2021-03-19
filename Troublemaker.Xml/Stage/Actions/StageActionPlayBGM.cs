using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='PlayBGM']")]
    public sealed class StageActionPlayBGM : StageAction
    {
        [XPath("@BGMName")] public String BGMName;
        [XPath("@FadeTime")] public Double FadeTime;
        [XPath("@Volume")] public Double Volume;
    }
}