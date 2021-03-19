using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='ReplaceMonster']")]
    public sealed class StageActionReplaceMonster : StageAction
    {
        [XPath("@Object")] public String Object;
        [XPath("Unit")] public StagePoint Unit;
    }
}