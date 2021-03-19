using System;

namespace Troublemaker.Xml
{
    [XPath("self::class")]
    public sealed class XmlMasterySet
    {
        [XPath("@name")] public String Name; // "Argonaut"
        [XPath("@Type")] public String Type; // "All"
        [XPath("@Priority")] public String Priority; // "1"
        [XPath("@Mastery1")] public String Mastery1; // "Luck"
        [XPath("@Mastery2")] public String Mastery2; // "LimitBreak"
        [XPath("@Mastery3")] public String Mastery3; // "Overcharge"
        [XPath("@Mastery4")] public String Mastery4; // "IndomitableWill"
    }
}