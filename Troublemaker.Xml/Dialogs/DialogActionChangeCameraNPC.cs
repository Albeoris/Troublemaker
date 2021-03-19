using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='ChangeCameraNPC']")]
    public sealed class DialogActionChangeCameraNPC : DialogAction
    {
        [XPath("@TargetType")] public String TargetType;
    }
}