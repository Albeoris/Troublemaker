using System;

namespace Troublemaker.Xml
{
    [XPath("self::DrakyEgg")]
    public sealed class StageMapComponentDrakyEgg : StageMapComponent
    {
        [XPath("@Team")] public String Team;

        [XPath("Direction")] public StagePosition Direction;
        [XPath("Position")] public StagePosition Position;
    }
}