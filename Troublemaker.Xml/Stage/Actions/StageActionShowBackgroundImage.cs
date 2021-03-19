using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='ShowBackgroundImage']")]
    public sealed class StageActionShowBackgroundImage : StageAction
    {
        [XPath("@BackgroundImage")] public String BackgroundImage;
        [XPath("@DialogEffect")] public String DialogEffect;
        [XPath("@DialogType")] public String DialogType;
        [XPath("@Slow")] public Boolean Slow;
    }
}