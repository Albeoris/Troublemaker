using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Dashboard[@Type='CollectItem']")]
    public sealed class StageDashboardCollectItem : StageDashboard
    {
        [XPath("@CollectItemSet")] public String CollectItemSet;
        [XPath("@MinCount")] public Int64 MinCount;
        [XPath("@MaxCount")] public Int64 MaxCount;
        [XPath("@PosHolderGroup")] public String PosHolderGroup;
    }
}