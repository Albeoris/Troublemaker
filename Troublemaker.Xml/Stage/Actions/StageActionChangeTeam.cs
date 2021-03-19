using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='ChangeTeam']")]
    public sealed class StageActionChangeTeam : StageAction
    {
        [XPath("@Team")] public String Team;

        [XPath("Unit")] public StagePointObject Unit;
    }
}