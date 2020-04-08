using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Dashboard[@Type='ObjectMarker']")]
    public sealed class StageDashboardObjectMarker : StageDashboard
    {
        [XPath("Unit")] public StagePointObject Unit;
    }
}