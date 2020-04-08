using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='FoodShop']")]
    public sealed class DialogActionFoodShop : DialogAction
    {
        [XPath("@Value")] public String Value;
    }
}