using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Dashboard[@Type='PsionicStone']")]
    public sealed class StageDashboardPsionicStone : StageDashboard
    {
        [XPath("@MinCount")] public Int64 MinCount;
        [XPath("@MaxCount")] public Int64 MaxCount;

        [XPath("PsionicStoneGen")] public StagePsionicStone PsionicStoneGen;
    }
}