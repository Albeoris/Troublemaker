using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='SystemMail']")]
    public sealed class DialogActionSystemMail : DialogAction
    {
        [XPath("@MailKey")] public String MailKey;
    }
}