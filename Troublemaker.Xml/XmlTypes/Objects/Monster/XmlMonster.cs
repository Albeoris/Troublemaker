using System;

namespace Troublemaker.Xml
{
    [XPath("self::class")]
    public sealed class XmlMonster
    {
        [XPath("@name")] public String Name;
        [XPath("@Lv")] public Int64 Lv;
        [XPath("@Grade")] public String Grade;
        [XPath("@Info")] public String InfoName;
        [XPath("@Object")] public String Object;
        [XPath("@RecoveryRatio")] public Double RecoveryRatio;
        [XPath("@PatrolAwakeMoveAI")] public String PatrolAwakeMoveAI;
        [XPath("@MinRallyProgress")] public String MinRallyProgress;
        [XPath("@AIConfigFunc")] public String AIConfigFunc;
        [XPath("@AutoPlayable")] public String AutoPlayable;
    }
}