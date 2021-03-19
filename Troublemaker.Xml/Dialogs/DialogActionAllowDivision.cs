using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='AllowDivision']")]
    public sealed class DialogActionAllowDivision : DialogAction
    {
        [XPath("@Value")] public String Value;
    }
}