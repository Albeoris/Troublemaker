using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='Sleep']")]
    public sealed class DialogActionSleep : DialogAction
    {
        [XPath("@Time")] public Double Time;
    }
}