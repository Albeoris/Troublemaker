using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='UpdateStageVariable']")]
    public sealed class StageActionUpdateStageVariable : StageAction
    {
        [XPath("@Value")] public String Value;
        [XPath("@Variable")] public String Variable;
    }
}