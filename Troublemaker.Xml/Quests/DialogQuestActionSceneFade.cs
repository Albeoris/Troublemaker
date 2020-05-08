using System;

namespace Troublemaker.Xml.Quests
{
    [XPath("self::property[@Type='SceneFade']")]
    public sealed class DialogQuestActionSceneFade : DialogQuestAction
    {
        [XPath("@Direct")] public Boolean Direct;
        [XPath("@FadeType")] public String FadeType;
    }
}