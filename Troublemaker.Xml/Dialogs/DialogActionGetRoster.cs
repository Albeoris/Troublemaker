using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='GetRoster']")]
    public sealed class DialogActionGetRoster : DialogAction
    {
        [XPath("@C_RosterName")] public String RosterName;
        [XPath("@RosterTarget")] public String RosterTarget;
    }
}