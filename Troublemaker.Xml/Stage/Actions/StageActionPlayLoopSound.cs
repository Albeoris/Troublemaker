using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='PlayLoopSound']")]
    public sealed class StageActionPlayLoopSound : StageAction
    {
        [XPath("@Name")] public String Name;
        [XPath("@SoundGroup")] public String SoundGroup;
        [XPath("@SoundName")] public String SoundName;
        [XPath("@Volume")] public Double Volume;
    }
}