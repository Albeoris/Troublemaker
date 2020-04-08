using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='MoveToUnit']")]
    public sealed class StageActionMoveToUnit : StageAction
    {
        [XPath("@Blink")] public Boolean Blink;
        [XPath("@NoCover")] public String NoCover;
        [XPath("@NoZOC")] public String NoZOC;
        [XPath("@Run")] public Boolean Run;
        [XPath("@Rush")] public Boolean Rush;
        [XPath("@Walk")] public Boolean Walk;
        [XPath("@Range")] public Int64 Range;

        [XPath("Unit")] public StagePointObject Unit;
        [XPath("Unit2")] public StagePointObject Unit2;
    }
}