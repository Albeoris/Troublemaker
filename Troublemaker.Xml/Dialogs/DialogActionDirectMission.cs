using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='DirectMission']")]
    public sealed class DialogActionDirectMission : DialogAction
    {
        [XPath("@Mission")] public String Mission;
        [XPath("@Lineup")] public String Lineup;
    }
}