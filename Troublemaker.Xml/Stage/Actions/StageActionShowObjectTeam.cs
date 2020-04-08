using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='ShowObjectTeam']")]
    public sealed class StageActionShowObjectTeam : StageAction
    {
        [XPath("@Team")] public String Team;
    }
}