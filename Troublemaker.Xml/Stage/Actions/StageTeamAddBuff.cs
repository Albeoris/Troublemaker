using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='TeamAddBuff']")]
    public sealed class StageActionTeamAddBuff : StageAction
    {
        [XPath("@Name")] public String Name;
        [XPath("@Team")] public String Team;
        [XPath("@Value")] public Int64 Value;
    }
}