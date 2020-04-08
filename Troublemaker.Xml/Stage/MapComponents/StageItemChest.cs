namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='Item_Chest']")]
    public sealed class StageItemChest : StageItemContainer
    {
        [XPath("ItemCollection/*")] public StageSlotItem[] Item;
    }
}