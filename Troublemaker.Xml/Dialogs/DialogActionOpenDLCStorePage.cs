using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='OpenDLCStorePage']")]
    public sealed class DialogActionOpenDLCStorePage : DialogAction
    {
        [XPath("@DLCName")] public String DLCName;
    }
}