using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='Shop']")]
    public sealed class DialogActionShop : DialogAction
    {
        [XPath("@Value")] public String Value;
    }
}