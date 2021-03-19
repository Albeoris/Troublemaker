using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='ToggleKillReward']")]
    public sealed class StageActionToggleKillReward : StageAction
    {
        [XPath("@RewardMode")] public String RewardMode;
    }
}