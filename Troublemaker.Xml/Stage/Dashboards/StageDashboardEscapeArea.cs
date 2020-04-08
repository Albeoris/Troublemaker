using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Dashboard[@Type='EscapeArea']")]
    public sealed class StageDashboardEscapeArea : StageDashboard
    {
        [XPath("Area")] public StageArea Area;
        [XPath("ExitPos")] public StagePosition ExitPos;
    }
}