using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Dashboard[@Type='Arrest']")]
    public sealed class StageDashboardArrest : StageDashboard
    {
        [XPath("@AngerBuff")] public String AngerBuff;
        [XPath("@StartingBuff")] public String StartingBuff;
        [XPath("@ArrestSet")] public String ArrestSet;
        [XPath("@PosHolderGroup")] public String PosHolderGroup;
        [XPath("@MinCount")] public Int64 MinCount;
        [XPath("@MaxCount")] public Int64 MaxCount;
        [XPath("@Team")] public String Team;

        [XPath("AI")] public StageAI AI;
    }
}