using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='SystemChat']")]
    public sealed class DialogActionSystemChat : DialogAction
    {
        [XPath("@Category")] public String Category;
        [XPath("@Message")] public String Message;
        [XPath("@C_Vill")] public String Vill;
    }
}