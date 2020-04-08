using System;

namespace Troublemaker.Xml
{
    [XPath("self::Ally")]
    public sealed class StageMapComponentAlly : StageMapComponent, IStageMapUnit
    {
        [XPath("@AngerBuff")] public String AngerBuff;
        [XPath("@AutoPlayable")] public String AutoPlayable;
        [XPath("@DirectingObject")] public Boolean DirectingObject;
        [XPath("@Object")] public String MonsterName { get; set; }
        [XPath("@RetreatOrder")] public Int64 RetreatOrder;
        [XPath("@Team")] public String Team;
        
        [XPath("AI")] public StageAI AI;
    }
}