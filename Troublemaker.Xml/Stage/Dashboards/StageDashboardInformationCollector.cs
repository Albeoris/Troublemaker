using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Dashboard[@Type='InformationCollector']")]
    public sealed class StageDashboardInformationCollector : StageDashboard
    {
        [XPath("@Count")] public Int64 Count;
    }
}