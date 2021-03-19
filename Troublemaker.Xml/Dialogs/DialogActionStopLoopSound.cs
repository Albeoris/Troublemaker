using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='StopLoopSound']")]
    public sealed class DialogActionStopLoopSound : DialogAction
    {
        [XPath("@Name")] public String Name;
    }
}