using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='StopBGM']")]
    public sealed class DialogActionStopBGM : DialogAction
    {
        [XPath("@Direct")] public Boolean Direct;
        [XPath("@FadeTime")] public Double FadeTime;
    }
}