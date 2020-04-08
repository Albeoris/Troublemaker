using System;

namespace Troublemaker.Xml
{
    [XPath("self::Variable[@Type='TeamUnitCounter']")]
    public sealed class StageVariableTeamUnitCounter : StageVariable
    {
        [XPath("@Linked")] public Boolean Linked;
        [XPath("@Team")] public String Team;
        [XPath("@Value")] public String Value;
    }
}