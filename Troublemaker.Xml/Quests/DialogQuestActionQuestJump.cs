using System;
using Troublemaker.Xml;

namespace Troublemaker.Xml.Quests
{
    [XPath("self::property[@Type='QuestJump']")]
    public sealed class DialogQuestActionQuestJump : DialogQuestAction
    {
        [XPath("@JumpTo")] public String JumpTo;
    }
}