using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='UpdateEquipment']")]
    public sealed class StageActionUpdateEquipment : StageAction
    {
        [XPath("@TemporaryTrue")] public Boolean TemporaryTrue;

        [XPath("Item")] public StageRewardItem Item;
        [XPath("Unit")] public StagePointObject Unit;
    }
}