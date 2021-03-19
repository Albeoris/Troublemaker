using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='Jump']")]
    public sealed class DialogActionJump : DialogAction
    {
        [XPath("@JumpTo")] public String JumpTo;
    }
}