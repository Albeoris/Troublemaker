using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='AddStageVariable']")]
    public sealed class StageActionAddStageVariable : StageAction
    {
        [XPath("@Value")] public String Value;
        [XPath("@Variable")] public String Variable;
    }
}