using System;
using System.Collections.Generic;
using Troublemaker.Framework;

namespace Troublemaker.Xml
{
    [XPath(".")]
    public abstract class StageCondition : IExpandable
    {
        [XPath("@ConditionFilter")] public String ConditionFilter;
        [XPath("@_Reverse")] public Boolean Reverse;
        [XPath("ConditionOutput")] public StageConditionOutput ConditionOutput;
        
        public virtual String NodeName => $"{GetType().Name.TrimPrefix("StageCondition")}";
        public virtual IEnumerable<(String name, IExpandable expandable)> EnumerateChildren() => Array.Empty<(String name, IExpandable expandable)>();
    }
}