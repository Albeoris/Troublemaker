using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='PlayBGM']")]
    public sealed class DialogActionPlayBGM : DialogAction
    {
        [XPath("@BGMName")] public String BGMName;
        [XPath("@FadeTime")] public Double FadeTime;
        [XPath("@Volume")] public Double Volume;
    }
}