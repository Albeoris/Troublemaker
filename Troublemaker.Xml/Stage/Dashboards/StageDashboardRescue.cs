using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Dashboard[@Type='Rescue']")]
    public sealed class StageDashboardRescue : StageDashboard
    {
        [XPath("ExitPos")] public StagePosition ExitPos;
    }
}