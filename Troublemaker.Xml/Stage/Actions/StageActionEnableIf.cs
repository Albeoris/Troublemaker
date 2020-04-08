using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='EnableIf']")]
    public sealed class StageActionEnableIf : StageAction
    {
        [XPath("@ArgsExpression")] public String ArgsExpression;
        [XPath("@ScriptName")] public String ScriptName;
    }
}