using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='HideBackgroundImage']")]
    public sealed class StageActionHideBackgroundImage : StageAction
    {
        [XPath("@DialogEffect")] public String DialogEffect;
        [XPath("@DialogType")] public String DialogType;
        [XPath("@Slow")] public Boolean Slow;
    }
}