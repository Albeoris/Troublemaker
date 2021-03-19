using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='CloseDialog']")]
    public sealed class DialogActionCloseDialog : DialogAction
    {
        [XPath("@DialogType")] public String DialogType;
    }
}