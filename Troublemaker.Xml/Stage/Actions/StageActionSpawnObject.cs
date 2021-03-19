using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='SpawnObject']")]
    public sealed class StageActionSpawnObject : StageAction
    {
        [XPath("Direction")] public StagePosition Direction;
        [XPath("Position")] public StagePosition Position;
        [XPath("Unit")] public StagePointObject Unit;
    }
}