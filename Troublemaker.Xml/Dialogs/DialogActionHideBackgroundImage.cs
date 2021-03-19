using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='HideBackgroundImage']")]
    public sealed class DialogActionHideBackgroundImage : DialogAction
    {
        [XPath("@DialogType")] public String DialogType;
        [XPath("@Slow")] public Boolean Slow;
    }
}