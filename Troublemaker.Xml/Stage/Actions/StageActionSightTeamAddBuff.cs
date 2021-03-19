using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='SightTeamAddBuff']")]
    public sealed class StageActionSightTeamAddBuff : StageAction
    {
        [XPath("@Name")] public String Name;
        [XPath("@Team")] public String Team;
        [XPath("@Value")] public Int64 Value;
        
        [XPath("Unit")] public StagePointObject Unit;
    }
}