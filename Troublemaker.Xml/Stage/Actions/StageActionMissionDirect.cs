using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='MissionDirect']")]
    public sealed class StageActionMissionDirect : StageAction
    {
        [XPath("@BeginHide")] public Boolean BeginHide;
        [XPath("@DirectType")] public String DirectType;
        [XPath("@EndShow")] public Boolean EndShow;
    }
}