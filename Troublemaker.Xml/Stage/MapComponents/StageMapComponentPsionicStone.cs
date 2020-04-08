using System;

namespace Troublemaker.Xml
{
    [XPath("self::PsionicStone")]
    public sealed class StageMapComponentPsionicStone : StageMapComponent
    {
        [XPath("@DashboardKey")] public String DashboardKey;

        [XPath("Direction")] public StagePosition Direction;
        [XPath("Position")] public StagePosition Position;
    }
}