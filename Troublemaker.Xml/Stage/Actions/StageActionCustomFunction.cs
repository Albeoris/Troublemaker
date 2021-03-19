using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='CustomFunction']")]
    public sealed class StageActionCustomFunction : StageAction
    {
        [XPath("@Value")] public String Value;
        [XPath("@Value2")] public String Value2;
    }
}