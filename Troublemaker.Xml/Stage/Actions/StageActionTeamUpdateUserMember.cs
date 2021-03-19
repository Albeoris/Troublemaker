using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='TeamUpdateUserMember']")]
    public sealed class StageActionTeamUpdateUserMember : StageAction
    {
        [XPath("@Team")] public String Team;
        [XPath("@Team2")] public String Team2;
        [XPath("@IncludeDead")] public Boolean IncludeDead;

        [XPath("Unit")] public StagePointObject Unit;
        [XPath("AIForm")] public StageAI AIForm;
    }
}