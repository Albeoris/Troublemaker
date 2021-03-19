using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='StopBGM']")]
    public sealed class StageActionStopBGM : StageAction
    {
        [XPath("@BGMName")] public String BGMName;
        [XPath("@Direct")] public Boolean Direct;
        [XPath("@FadeTime")] public Double FadeTime;
        [XPath("@Volume")] public Double Volume;
    }
}