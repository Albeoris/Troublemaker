using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='ChangeLobbyMap']")]
    public sealed class DialogActionChangeLobbyMap : DialogAction
    {
        [XPath("@C_LobbyDef")] public String CLobbyDef;
    }
}