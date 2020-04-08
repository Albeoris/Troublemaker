using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='TeamRemoveBuffAll']")]
    public sealed class StageActionTeamRemoveBuffAll : StageAction
    {
        [XPath("@Team")] public String Team;
    }
}