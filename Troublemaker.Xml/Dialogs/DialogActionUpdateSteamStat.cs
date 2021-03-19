using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='UpdateSteamStat']")]
    public sealed class DialogActionUpdateSteamStat : DialogAction
    {
        [XPath("@C_Stat")] public String Stat;
        [XPath("@C_Value")] public String Value;
    }
}