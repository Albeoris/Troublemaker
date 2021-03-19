using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='MissionDirectInstance']")]
    public sealed class StageActionMissionDirectInstance : StageAction
    {
        [XPath("@BeginHide")] public Boolean BeginHide;
        [XPath("@EndShow")] public Boolean EndShow;

        [XPath("MissionDirectScript/Action")] public StageAction[] MissionDirectScript;
        
        public override IEnumerable<(String name, IExpandable expandable)> EnumerateChildren()
        {
            yield return MissionDirectScript.Named(nameof(MissionDirectScript));
        }
    }
}