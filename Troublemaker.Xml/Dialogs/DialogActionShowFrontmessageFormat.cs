using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='ShowFrontmessageFormat']")]
    public sealed class DialogActionShowFrontmessageFormat : DialogAction
    {
        [XPath("@MessageColor")] public String MessageColor;
        [XPath("GameMessageForm")] public DialogGameMessageForm GameMessageForm;
    }
}