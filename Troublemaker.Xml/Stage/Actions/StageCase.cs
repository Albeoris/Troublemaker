using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Case")]
    public sealed class StageCase : IExpandable
    {
        [XPath("@CaseValue")] public String CaseValue;
        
        [XPath("ActionList/*")] public StageAction[] ActionList;

        public String NodeName => $"Case ({CaseValue})";
        public Boolean CanFlatten => false;
        
        public IEnumerable<(String name, IExpandable expandable)> EnumerateChildren()
        {
            yield return ActionList.Named(nameof(ActionList));
        }
    }
}