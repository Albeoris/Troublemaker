using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='NoEnemyToTeam']")]
    public sealed class StageConditionNoEnemyToTeam : StageCondition
    {
        [XPath("Team")] public String Team;
    }
}