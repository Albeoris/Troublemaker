using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='QuestJump']")]
    public sealed class DialogQuestActionQuestJump : DialogAction
    {
        [XPath("@JumpTo")] public String JumpTo;
        [XPath("@C_Quest")] public String CQuest;
        [XPath("@C_JumpTo")] public String CJumpTo;
    }
}