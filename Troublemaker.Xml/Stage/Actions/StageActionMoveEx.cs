using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='MoveEx']")]
    public sealed class StageActionMoveEx : StageAction
    {
        [XPath("@Blink")] public Boolean Blink;
        [XPath("@NoCover")] public String NoCover;
        [XPath("@NoZOC")] public String NoZOC;
        [XPath("@Range")] public Int64 Range;
        [XPath("@Run")] public Boolean Run;
        [XPath("@Rush")] public Boolean Rush;
        [XPath("@Walk")] public Boolean Walk;

        [XPath("PositionIndicator")] public StagePoint PositionIndicator;
        [XPath("Unit")] public StagePointObject Unit;
    }
}