using System;

namespace Troublemaker.Xml
{
    [XPath("self::Item[@Type='Simple']")]
    public sealed class StageRewardItemSimple : StageRewardItem
    {
        [XPath("@Count")] public Int64 Count;
        [XPath("@ItemType")] public String ItemType;
    }
}