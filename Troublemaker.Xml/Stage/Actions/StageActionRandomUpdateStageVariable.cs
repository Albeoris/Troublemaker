using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='RandomUpdateStageVariable']")]
    public sealed class StageActionRandomUpdateStageVariable : StageAction
    {
        [XPath("@Value")] public Int64 Value;
        [XPath("@Value2")] public Int64 Value2;
        [XPath("@Variable")] public String Variable;
    }
}