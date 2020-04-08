using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='Action']")]
    public sealed class DialogActionAction : DialogAction
    {
        [XPath("@Command")] public String Command;
        [XPath("@Commit")] public Boolean Commit;
        [XPath("@PropertyType")] public String PropertyType;
        [XPath("@PropertyValue")] public String PropertyValue;
    }
}