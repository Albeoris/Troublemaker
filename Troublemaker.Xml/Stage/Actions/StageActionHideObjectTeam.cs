using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='HideObjectTeam']")]
    public sealed class StageActionHideObjectTeam : StageAction
    {
        [XPath("@Team")] public String Team;
    }
}