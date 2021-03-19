using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='Move']")]
    public sealed class StageActionMove : StageAction
    {
        [XPath("@Blink")] public Boolean Blink;
        [XPath("@NoCover")] public String NoCover;
        [XPath("@NoZOC")] public String NoZOC;
        [XPath("@Run")] public Boolean Run;
        [XPath("@Rush")] public Boolean Rush;
        [XPath("@Walk")] public Boolean Walk;

        [XPath("Position")] public StagePosition Position;
        [XPath("Unit")] public StagePointObject Unit;
    }
}