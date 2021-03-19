using System;

namespace Troublemaker.Xml
{
    [XPath("self::Variable[@Type='TeamInteractionUseCount']")]
    public sealed class StageVariableTeamInteractionUseCount : StageVariable
    {
        [XPath("@Linked")] public Boolean Linked;
        [XPath("@Team")] public String Team;
        [XPath("@Interaction")] public String Value;
    }
}