using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='UpdateAchievementStat']")]
    public sealed class DialogActionUpdateAchievementStat : DialogAction
    {
        [XPath("@C_Stat")] public String Stat;
        [XPath("@C_Value")] public String Value;
    }
}