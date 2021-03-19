using System;

namespace Troublemaker.Xml
{
    [XPath("self::class")]
    public sealed class XmlMasteryFatigue
    {
        [XPath("@name")] public String Name;        // "Resilient"
        [XPath("@Priority")] public Int64 Priority; // "10"
    }
}