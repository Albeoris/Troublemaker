using System;

namespace Troublemaker.Xml
{
    [XPath("self::Dashboard[@Type='CitizenGen']")]
    public sealed class StageDashboardCitizenGen : StageDashboard
    {
        [XPath("@PosHolderGroup")] public String PosHolderGroup;
        [XPath("@MinCount")] public Int64 MinCount;
        [XPath("@MaxCount")] public Int64 MaxCount;
        [XPath("@CitizenType")] public String CitizenType;
        [XPath("@CitizenGenSet")] public String CitizenGenSet;
    }
}