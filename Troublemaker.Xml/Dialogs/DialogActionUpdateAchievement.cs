using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='UpdateAchievement']")]
    public sealed class DialogActionUpdateAchievement : DialogAction
    {
        [XPath("@C_Achievement")] public String Achievement;
        [XPath("@C_Achieved")] public String Achieved;
    }
}