using System;

namespace Troublemaker.Xml.Quests
{
    [XPath("self::property[@Type='DirectMission']")]
    public sealed class DialogQuestActionDirectMission : DialogQuestAction
    {
        [XPath("@Mission")] public String Mission;
        [XPath("@Lineup")] public String Lineup;
        [XPath("@Grade")] public String Grade;
        [XPath("@Site")] public String Site;
        [XPath("@RewardRatio")] public String RewardRatio;
        [XPath("@Lv")] public String Lv;
    }
}