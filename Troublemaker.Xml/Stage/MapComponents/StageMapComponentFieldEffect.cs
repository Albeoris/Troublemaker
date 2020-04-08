using System;

namespace Troublemaker.Xml
{
    [XPath("self::FieldEffect")]
    public sealed class StageMapComponentFieldEffect : StageMapComponent
    {
        [XPath("@FieldEffectType")] public String FieldEffectType;

        [XPath("Direction")] public StagePosition Direction;
        [XPath("Position")] public StagePosition Position;
    }
}