using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='ChangeTeamAI']")]
    public sealed class StageActionChangeTeamAI : StageAction
    {
        [XPath("@Team")] public String Team;

        [XPath("AIForm")] public StageAI AIForm;
    }
}