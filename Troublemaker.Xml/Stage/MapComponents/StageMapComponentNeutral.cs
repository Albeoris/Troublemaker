using System;

namespace Troublemaker.Xml
{
    [XPath("self::Neutral")]
    public sealed class StageMapComponentNeutral : StageMapComponent, IStageMapUnit
    {
        [XPath("@AngerBuff")] public String AngerBuff;
        [XPath("@Object")] public String MonsterName { get; set; }
        [XPath("@StartingBuff")] public String StartingBuff;
        [XPath("@Team")] public String Team;

        [XPath("Direction")] public StagePosition Direction;
        [XPath("Position")] public StagePosition Position;
        [XPath("RetreatPosition")] public StagePosition RetreatPosition;
        [XPath("AI")] public StageAI AI;
    }
}