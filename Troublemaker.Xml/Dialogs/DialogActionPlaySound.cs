using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='PlaySound']")]
    public sealed class DialogActionPlaySound : DialogAction
    {
        [XPath("@SoundGroup")] public String SoundGroup;
        [XPath("@NoWait")] public String NoWait;
        [XPath("@SoundName")] public String SoundName;
    }
}