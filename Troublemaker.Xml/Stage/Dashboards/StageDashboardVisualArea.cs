using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Dashboard[@Type='VisualArea']")]
    public sealed class StageDashboardVisualArea : StageDashboard
    {
        [XPath("@Particle")] public String Particle;
        [XPath("Area")] public StageArea Area;
    }
}