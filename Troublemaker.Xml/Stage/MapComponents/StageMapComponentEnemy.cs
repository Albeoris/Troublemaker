using System;

namespace Troublemaker.Xml
{
    [XPath("self::Enemy")]
    public sealed class StageMapComponentEnemy : StageMapComponent, IStageMapUnit
    {
        [XPath("@AngerBuff")] public String AngerBuff;
        [XPath("@Object")] public String MonsterName { get; set; }
        [XPath("@PatrolMethod")] public String PatrolMethod;
        [XPath("@PatrolRepeat")] public Int64 PatrolRepeat;
        [XPath("@StartingBuff")] public String StartingBuff;
        [XPath("@Team")] public String Team;

        [XPath("Direction")] public StagePosition Direction;
        [XPath("Position")] public StagePosition Position;
        [XPath("RetreatPosition")] public StagePosition RetreatPosition;
        [XPath("NamedEventOverride")] public StageEvent StageEventNone;
        [XPath("RosterInfo")] public StageRosterInfo RosterInfo;
        [XPath("PatrolRoute/*")] public StageRoute[] PatrolRoute;
        [XPath("AI")] public StageAI AI;
    }
}