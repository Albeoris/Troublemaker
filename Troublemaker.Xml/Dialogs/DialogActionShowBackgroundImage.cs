using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='ShowBackgroundImage']")]
    public sealed class DialogActionShowBackgroundImage : DialogAction
    {
        [XPath("@DialogType")] public String DialogType;
        [XPath("@DialogEffect")] public String DialogEffect;
        [XPath("@BackgroundImage")] public String BackgroundImage;
        [XPath("@Slow")] public Boolean Slow;
    }
}