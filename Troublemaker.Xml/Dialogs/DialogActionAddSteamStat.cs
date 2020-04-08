using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='AddSteamStat']")]
    public sealed class DialogActionAddSteamStat : DialogAction
    {
        [XPath("@C_Stat")] public String Stat;
        [XPath("@C_Value")] public String Value;
    }
}