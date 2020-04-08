using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='Or']")]
    public sealed class StageConditionOr : StageCondition
    {
        [XPath("*")] public StageCondition[] Items;
        
        public override IEnumerable<(String name, IExpandable expandable)> EnumerateChildren()
        {
            yield return Items.Named(nameof(Items));
        }
    }
}