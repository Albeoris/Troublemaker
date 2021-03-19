using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='Env']")]
    public sealed class DialogActionEnv : DialogAction
    {
        [XPath("@Key")] public String Key;
        [XPath("@Value")] public String Value;
    }
}