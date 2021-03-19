using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='MoveAllToArea']")]
    public sealed class StageActionMoveAllToArea : StageAction
    {
        [XPath("@Blink")] public Boolean Blink;
        [XPath("@NoCover")] public String NoCover;
        [XPath("@NoZOC")] public String NoZOC;
        [XPath("@Range")] public Int64 Range;
        [XPath("@Run")] public Boolean Run;
        [XPath("@Rush")] public Boolean Rush;
        [XPath("@Walk")] public Boolean Walk;

        [XPath("AllUnit")] public StagePointTeamArea AllUnit;
        [XPath("AreaIndicator")] public StageAreaIndicator AreaIndicator;
    }
}