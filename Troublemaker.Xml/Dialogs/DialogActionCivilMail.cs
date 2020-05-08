using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='CivilMail']")]
    public sealed class DialogActionCivilMail : DialogAction
    {
        [XPath("@MailKey")] public String MailKey;
        [XPath("@NoCommit")] public Boolean NoCommit;
    }
}