using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='Script']")]
    public sealed class DialogActionScript : DialogAction
    {
        [XPath("@Script")] public String Script;
    }
}