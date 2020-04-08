using System;

namespace Troublemaker.Xml
{
    [XPath("self::StartPosition")]
    public sealed class StageMapComponentStartPosition : StageMapComponent
    {
        [XPath("@Team")] public String Team;

        [XPath("Direction")] public StagePosition Direction;
        [XPath("Position")] public StagePosition Position;
    }
}