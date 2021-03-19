using System;

namespace Troublemaker.Xml
{
    [XPath("self::YashaEgg")]
    public sealed class StageMapComponentYashaEgg : StageMapComponent
    {
        [XPath("@Team")] public String Team;
        [XPath("@Team2")] public String Team2;

        [XPath("Direction")] public StagePosition Direction;
        [XPath("Position")] public StagePosition Position;
    }
}