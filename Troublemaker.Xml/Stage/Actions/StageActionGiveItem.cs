namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='GiveItem']")]
    public sealed class StageActionGiveItem : StageAction
    {
        [XPath("OpenReward_GiveItem")] public StageItemContainer Reward;

        [XPath("Unit")] public StagePointObject Unit;
    }
}