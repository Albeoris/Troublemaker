using System;

namespace Troublemaker.Xml
{
    [XPath("self::Obstacle")]
    public sealed class StageMapComponentObstacle : StageMapComponent
    {
        [XPath("@Obstacle")] public String Obstacle;

        [XPath("Direction")] public StagePosition Direction;
        [XPath("Position")] public StagePosition Position;
    }
}