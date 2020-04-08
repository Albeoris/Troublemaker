using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='ChangeLocation']")]
    public sealed class DialogActionChangeLocation : DialogAction
    {
        [XPath("@Lobby")] public String Lobby;
    }
}