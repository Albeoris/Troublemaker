using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='AddAchievementStat']")]
    public sealed class DialogActionAddAchievementStat : DialogAction
    {
        [XPath("@C_Stat")] public String Stat;
        [XPath("@C_Value")] public String Value;
    }
}