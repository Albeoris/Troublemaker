using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Trigger")]
    public sealed class StageTrigger : IExpandable
    {
        [XPath("@Group")] public String Group;
        [XPath("@Name")] public String Name;

        [XPath("Condition")] public StageCondition Condition;
        [XPath("Action")] public StageAction[] Actions;
        
        public String NodeName => String.IsNullOrEmpty(Name) ? "<NoName>" : Name;

        public IEnumerable<(String name, IExpandable expandable)> EnumerateChildren()
        {
            yield return Condition.Named(nameof(Condition));
            yield return Actions.Named(nameof(Actions));
        }
    }
}