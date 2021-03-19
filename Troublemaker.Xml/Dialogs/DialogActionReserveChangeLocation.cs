using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='ReserveChangeLocation']")]
    public sealed class DialogActionReserveChangeLocation : DialogAction
    {
        [XPath("@Lobby")] public String Lobby;
    }
}