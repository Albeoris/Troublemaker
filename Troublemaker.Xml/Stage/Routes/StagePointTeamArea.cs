using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='TeamArea']")]
    public sealed class StagePointTeamArea : StagePoint
    {
        [XPath("@Team")] public String Team;
        [XPath("AreaIndicator")] public StageAreaIndicator AreaIndicator;
    }
}