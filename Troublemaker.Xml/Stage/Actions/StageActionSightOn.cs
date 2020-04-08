using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='SightOn']")]
    public sealed class StageActionSightOn : StageAction
    {
        [XPath("@Name")] public String Name;
        [XPath("@Range")] public Int64 Range;

        [XPath("PositionIndicator")] public StagePoint PositionIndicator;
    }
}