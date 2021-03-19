using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='Switch']")]
    public sealed class StageActionSwitch : StageAction
    {
        [XPath("@TestExpression")] public String TestExpression;

        [XPath("CaseDefinition/*")] public StageCase[] CaseDefinition;
        
        public Boolean CanFlatten => false;
        
        public override IEnumerable<(String name, IExpandable expandable)> EnumerateChildren()
        {
            yield return CaseDefinition.Named(nameof(CaseDefinition));
        }
    }
}