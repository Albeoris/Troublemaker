using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='PlaySound3D']")]
    public sealed class StageActionPlaySound3D : StageAction
    {
        [XPath("@BonePos")] public String BonePos;
        [XPath("@MinDistance")] public Int64 MinDistance;
        [XPath("@SoundGroup")] public String SoundGroup;
        [XPath("@SoundName")] public String SoundName;
        [XPath("@Volume")] public Double Volume;

        [XPath("Unit")] public StagePointObject Unit;
    }
}