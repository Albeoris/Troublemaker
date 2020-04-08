using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='VictoryCondition']")]
    public sealed class StageActionVictoryCondition : StageAction
    {
        [XPath("DefeatCondition/*")] public StageGoalCondition[] DefeatCondition;
        [XPath("VictoryCondition/*")] public StageGoalCondition[] VictoryCondition;
        
        public override IEnumerable<(String name, IExpandable expandable)> EnumerateChildren()
        {
            yield return DefeatCondition.Named(nameof(DefeatCondition));
            yield return VictoryCondition.Named(nameof(VictoryCondition));
        }
    }
}