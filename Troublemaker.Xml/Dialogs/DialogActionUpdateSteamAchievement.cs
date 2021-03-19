using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='UpdateSteamAchievement']")]
    public sealed class DialogActionUpdateSteamAchievement : DialogAction
    {
        [XPath("@C_Achievement")] public String Achievement;
        [XPath("@C_Achieved")] public String Achieved;
    }
}