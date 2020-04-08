using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='TeamChangeTeam']")]
    public sealed class StageActionTeamChangeTeam : StageAction
    {
        [XPath("@Team")] public String Team;
        [XPath("@Team2")] public String Team2;
    }
}