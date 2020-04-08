using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='PlayLoopSound']")]
    public sealed class DialogActionPlayLoopSound : DialogAction
    {
        [XPath("@Name")] public String Name;
        [XPath("@SoundGroup")] public String SoundGroup;
        [XPath("@NoWait")] public String NoWait;
        [XPath("@SoundName")] public String SoundName;
        [XPath("@FadeTime")] public Double FadeTime;
        [XPath("@Volume")] public Double Volume;
    }
}