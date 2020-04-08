using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='TeamDistribute']")]
    public sealed class StageActionTeamDistribute : StageAction
    {
        [XPath("@Team")] public String Team;
        [XPath("@Shuffle")] public Boolean Shuffle;
        [XPath("AreaIndicator")] public StageAreaIndicator AreaIndicator;
    }
}