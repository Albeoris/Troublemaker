using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='TeamRemoveBuff']")]
    public sealed class StageActionTeamRemoveBuff : StageAction
    {
        [XPath("@Name")] public String Name;
        [XPath("@Team")] public String Team;

        [XPath("Unit")] public StagePointObject Unit;
    }
}