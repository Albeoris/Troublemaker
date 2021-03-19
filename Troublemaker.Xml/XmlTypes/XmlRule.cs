using System;

namespace Troublemaker.Xml
{
    [XPath("self::rule")]
    public sealed class XmlRule
    {
        [XPath("@type")] public String Type; // "table"
        [XPath("@subtype")] public String SubType; // "link"
        [XPath("@target")] public String Target; // "StatusType"
        [XPath("@default")] public String Default; // "CalculatedProperty_MasteryTitle"
    }
}