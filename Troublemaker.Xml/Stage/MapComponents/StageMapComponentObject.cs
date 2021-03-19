using System;

namespace Troublemaker.Xml
{
    [XPath("self::Object")]
    public sealed class StageMapComponentObject : StageMapComponent, IStageMapUnit
    {
        [XPath("@DirectingObject")] public Boolean DirectingObject;
        [XPath("@Object")] public String MonsterName { get; set; }
        [XPath("@Team")] public String Team;

        [XPath("Direction")] public StagePosition Direction;
        [XPath("Position")] public StagePosition Position;
        [XPath("RosterInfo")] public StageRosterInfo RosterInfo;
        [XPath("AI")] public StageAI AI;
    }
}