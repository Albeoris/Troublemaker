using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='ToggleTeamAITimeLimit']")]
    public sealed class StageActionToggleTeamAITimeLimit : StageAction
    {
        [XPath("@Team")] public String Team;
    }
}