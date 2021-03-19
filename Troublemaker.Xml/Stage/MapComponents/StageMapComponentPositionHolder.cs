using System;

namespace Troublemaker.Xml
{
    [XPath("self::PositionHolder")]
    public sealed class StageMapComponentPositionHolder : StageMapComponent
    {
        [XPath("Direction")] public StagePosition Direction;
        [XPath("Position")] public StagePosition Position;
    }
}