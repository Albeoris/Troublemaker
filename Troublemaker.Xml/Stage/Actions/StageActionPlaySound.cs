using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='PlaySound']")]
    public sealed class StageActionPlaySound : StageAction
    {
        [XPath("@SoundGroup")] public String SoundGroup;
        [XPath("@SoundName")] public String SoundName;
        [XPath("@Volume")] public Double Volume;
    }
}