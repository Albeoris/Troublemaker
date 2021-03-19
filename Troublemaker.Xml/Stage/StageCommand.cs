using System;

namespace Troublemaker.Xml
{
    [XPath("self::Command")]
    public sealed class StageCommand
    {
        [XPath("@Value")] public String Value;
    }
}