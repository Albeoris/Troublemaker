using System;

namespace Troublemaker.Xml
{
    [XPath("self::Slot")]
    public sealed class StageSlotItem
    {
        [XPath("@Priority")] public Int64 Priority;
        [XPath("Item")] public StageRewardItem Item;
    }
}