using System;

namespace Troublemaker.Xml
{
    [XPath("self::InvestigationType[@Type='Chest']")]
    public sealed class StageMapComponentInvestigationTargetTypeChest : StageMapComponentInvestigationTargetType
    {
        [XPath("@ChestType")] public String ChestType;
        [XPath("OpenReward_Chest")] public StageItemContainer Reward;
    }
}