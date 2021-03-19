using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='ScoutRoster']")]
    public sealed class DialogActionScoutRoster : DialogAction
    {
        [XPath("@Title")] public String Title;
        [XPath("@C_RosterName")] public String RosterName;
        [XPath("@MasteryTarget")] public String MasteryTarget;
        [XPath("@SalaryTarget")] public String SalaryTarget;
    }
}