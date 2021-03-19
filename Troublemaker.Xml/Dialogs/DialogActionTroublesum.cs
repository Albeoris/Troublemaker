using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='Troublesum']")]
    public sealed class DialogActionTroublesum : DialogAction
    {
        [XPath("@Value")] public String Value;
    }
}