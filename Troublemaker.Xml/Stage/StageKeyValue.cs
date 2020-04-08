using System;

namespace Troublemaker.Xml
{
    [XPath("self::PropKV")]
    public sealed class StageKeyValue
    {
        [XPath("@Key")] public String Key;
        [XPath("@Value")] public String Value;
    }
}