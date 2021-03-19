using System;

namespace Troublemaker.Xml
{
    [XPath(".")]
    public sealed class StageAI
    {
        [XPath("@AllowGrenadeOneTarget")] public Boolean AllowGrenadeOneTarget;
        [XPath("@AlwaysApplyRallyPoint")] public Boolean AlwaysApplyRallyPoint;
        [XPath("@MinRallyProgress")] public Int64 MinRallyProgress;
        [XPath("@NoPreemptiveAttack")] public Boolean NoPreemptiveAttack;

        [XPath("@RallyPower")] public Int64 RallyPower;
        [XPath("@RallyPower2")] public Int64 RallyPower2;
        [XPath("@RallyRange")] public Int64 RallyRange;
        [XPath("@RallyRange2")] public Int64 RallyRange2;
        
        [XPath("ActivityArea")] public StageArea ActivityArea;
        [XPath("RallyPoint")] public StagePoint RallyPoint;
        [XPath("RallyPoint2")] public StagePoint RallyPoint2;

        [XPath("Direction")] public StagePosition Direction;
        [XPath("Position")] public StagePosition Position;
        [XPath("RetreatPosition")] public StagePosition RetreatPosition;
    }
}